using System;
using System.Drawing;

namespace ConvImgCpc {
	public static class Conversion {
		const int K_R = 9798;
		const int K_V = 19235;
		const int K_B = 3735;

		const int SEUIL_LUM_1 = 0x55;
		const int SEUIL_LUM_2 = 0xAA;

		public delegate void DlgCalcDiff(int diff, int decalMasque, int modeCpc, int tx);
		static private DlgCalcDiff fctCalcDiff = null;
		static private int[] Coul = new int[4096];
		static private LockBitmap bitmap;
		static private int xPix, yPix;
		static private int coeffMat;
		static private byte[] tblContrast = new byte[256];

		static int[,] bayer2 = new int[2, 2] {	{1, 3 },
												{4, 2 } };
		static int[,] bayer3 = new int[4, 4] {	{0, 12, 3, 15},
												{8, 4, 11, 7},
												{2, 14, 1, 13},
												{10, 6, 9, 5}};
		static int[,] bayer4 = new int[4, 4] {	{1, 9, 3, 11},
												{13, 5, 15, 7},
												{4, 12, 2, 10},
												{16, 8, 14, 6}};
		static int[,] ord1 = new int[3, 3] {	{8, 3, 4},
												{6, 1, 2},
												{7, 5, 9}};
		static int[,] ord2 = new int[3, 3] {	{1, 7, 4},
												{5, 8, 3},
												{6, 2, 9}};
		static int[,] ord4 = new int[4, 4] {	{0, 8, 2, 10},
												{12, 4, 14, 6},
												{3, 11, 1, 9},
												{15, 7, 13, 5}};
		static int[,] zigzag1 = new int[3, 3] {	{0, 4, 0},
												{3, 0, 1},
												{0, 2, 0}};
		static int[,] zigzag2 = new int[3, 4] {	{0, 4, 2, 0},
												{6, 0, 5, 3},
												{0, 7, 1, 0}};
		static int[,] floyd = new int[2, 2] {	{7, 3},
												{5, 1}};
		static int[,] test3 = new int[3, 2] {	{0, 3,},
												{0, 5},
												{7, 1}};
		static int[,] test2 = new int[3, 3] {	{8, 4, 5},
												{3, 0, 1},
												{7, 2, 6}};
		static int[,] test1 = new int[3, 1] { { 1 }, { 4 }, { 2 } };

		static int[,] test = new int[2, 3] {	{0, 0, 7},
												{3, 5, 1}};

		static int[,] mat = bayer2;

		static byte MinMax(int value) {
			return value >= 0 ? value <= 255 ? (byte)value : (byte)255 : (byte)0;
		}

		static void CalcDiffMethodeMat(int diff, int decalMasque, int mode, int Tx) {
			for (int y = 0; y < mat.GetLength(1); y++) {
				int adr = ((((yPix + 2 * y) * bitmap.Width) + xPix) << 2) + decalMasque;
				for (int x = 0; x < mat.GetLength(0); x++) {
					if (adr < bitmap.Pixels.Length)
						bitmap.Pixels[adr] = (byte)MinMax(bitmap.Pixels[adr] + (diff * mat[x, y]) / coeffMat);

					adr += Tx << 2;
				}
			}
		}

		// Modification luminosité / saturation
		static private void SetLumiSat(float lumi, float satur, ref float r, ref float v, ref float b) {
			float min = Math.Min(r, Math.Min(v, b));
			float max = Math.Max(r, Math.Max(v, b));
			float dif = max - min;
			float hue = 0;
			if (max > min) {
				if (v == max) {
					hue = (b - r) / dif * 60f + 120f;
				}
				else
					if (b == max) {
						hue = (r - v) / dif * 60f + 240f;
					}
					else
						if (b > v) {
							hue = (v - b) / dif * 60f + 360f;
						}
						else {
							hue = (v - b) / dif * 60f;
						}
				if (hue < 0)
					hue = hue + 360f;
			}
			hue *= 255f / 360f;
			float sat = satur * (dif / max) * 255f;
			float bri = lumi * max;
			r = v = b = bri;
			if (sat != 0) {
				max = bri;
				dif = bri * sat / 255f;
				min = bri - dif;
				float h = hue * 360f / 255f;
				if (h < 60f) {
					r = max;
					v = h * dif / 60f + min;
					b = min;
				}
				else
					if (h < 120f) {
						r = -(h - 120f) * dif / 60f + min;
						v = max;
						b = min;
					}
					else
						if (h < 180f) {
							r = min;
							v = max;
							b = (h - 120f) * dif / 60f + min;
						}
						else
							if (h < 240f) {
								r = min;
								v = -(h - 240f) * dif / 60f + min;
								b = max;
							}
							else
								if (h < 300f) {
									r = (h - 240f) * dif / 60f + min;
									v = min;
									b = max;
								}
								else
									if (h <= 360f) {
										r = max;
										v = min;
										b = -(h - 360f) * dif / 60 + min;
									}
									else
										r = v = b = 0;
			}
		}

		//
		// Passe 1 : Réduit la palette aux x couleurs de la palette du CPC.
		// Effectue également un traitement de l'erreur (tramage) si demandé.
		// Calcule le nombre de couleurs utilisées dans l'image, et
		// remplit un tableau avec ces couleurs
		//
		static private int ConvertPasse1(int xdest, int ydest, Param prm, int Mode) {
			//if (prm.cpcPlus)
			//	prm.pct <<= 2;

			for (int i = 0; i < Coul.Length; i++)
				Coul[i] = 0;

			float lumi = prm.pctLumi / 100.0F;
			float satur = prm.pctSat / 100.0F;
			double c = prm.pctContrast / 100.0;
			for (int i = 0; i < 256; i++)
				tblContrast[i] = (byte)MinMax((int)(((((i / 255.0) - 0.5) * c) + 0.5) * 255));

			fctCalcDiff = null;
			if (prm.pct > 0) {
				fctCalcDiff = CalcDiffMethodeMat;
				switch (prm.methode) {
					case "Bayer 1 (2X2)":
						mat = bayer2;
						break;

					case "Ordered 1 (3x3)":
						mat = ord1;
						break;

					case "Ordered 2 (3x3)":
						mat = ord2;
						break;

					case "Bayer 2 (4x4)":
						mat = bayer3;
						break;

					case "Bayer 3 (4X4)":
						mat = bayer4;
						break;

					case "Ordered 3 (4x4)":
						mat = ord4;
						break;

					case "ZigZag1 (3x3)":
						mat = zigzag1;
						break;

					case "ZigZag2 (4x3)":
						mat = zigzag2;
						break;

					case "Floyd-Steinberg (2x2)":
						mat = floyd;
						break;

					case "Test":
						mat = test;
						break;

					default:
						fctCalcDiff = null;
						break;
				}
				if (fctCalcDiff != null) {
					int rowsOrHeight = mat.GetLength(0);
					int colsOrWidth = mat.GetLength(1);
					int sum = 0;
					for (int j = 0; j < colsOrWidth; j++)
						for (int i = 0; i < rowsOrHeight; i++)
							sum += mat[i, j];

					coeffMat = sum * 100 / prm.pct;
				}
			}

			RvbColor choix, p = new RvbColor(0);
			for (yPix = 0; yPix < ydest; yPix += 2) {
				int Tx = 4 >> (Mode >= 3 ? (yPix & 2) == 0 ? Mode - 2 : Mode - 3 : Mode);
				for (xPix = 0; xPix < xdest; xPix += Tx) {
					p = bitmap.GetPixelColor(xPix, yPix);
					if (p.red != 0 && p.green != 0 && p.blue != 0) {
						float r = tblContrast[p.red];
						float v = tblContrast[p.green];
						float b = tblContrast[p.blue];
						if (prm.pctLumi != 100 || prm.pctSat != 100)
							SetLumiSat(lumi, satur, ref r, ref v, ref b);

						p.red = (byte)MinMax((int)r);
						p.green = (byte)MinMax((int)v);
						p.blue = (byte)MinMax((int)b);
					}

					// Recherche le point dans la couleur cpc la plus proche
					int indexChoix = 0;
					if (prm.cpcPlus) {
						choix = new RvbColor((byte)((p.red >> 4) * 17), (byte)((p.green >> 4) * 17), (byte)((p.blue >> 4) * 17));
						indexChoix = ((choix.green << 4) & 0xF00) + ((choix.red) & 0xF0) + ((choix.blue) >> 4);
					}
					else {
						if (!prm.newMethode)
							indexChoix = (p.red > SEUIL_LUM_2 ? 2 : p.red > SEUIL_LUM_1 ? 1 : 0) + (p.blue > SEUIL_LUM_2 ? 6 : p.blue > SEUIL_LUM_1 ? 3 : 0) + (p.green > SEUIL_LUM_2 ? 18 : p.green > SEUIL_LUM_1 ? 9 : 0);
						else {
							int oldDist = 0x7FFFFFFF;
							for (int i = 0; i < 27; i++) {
								RvbColor s = ImageCpc.RgbCPC[i];
								int dist = Math.Abs(p.red - s.red) * K_R + Math.Abs(p.green - s.green) * K_V + Math.Abs(p.blue - s.blue) * K_B;
								if (dist < oldDist) {
									oldDist = dist;
									indexChoix = i;
									if (dist == 0)
										break;
								}
							}
						}
						choix = ImageCpc.RgbCPC[indexChoix];
					}
					Coul[indexChoix]++;

					if (fctCalcDiff != null) {
						fctCalcDiff(p.red - choix.red, 0, Mode, Tx);		// Modif. rouge
						fctCalcDiff(p.green - choix.green, 1, Mode, Tx);	// Modif. Vert
						fctCalcDiff(p.blue - choix.blue, 2, Mode, Tx);		// Modif. Bleu
					}
					bitmap.SetPixel(xPix, yPix, choix);
				}
			}
			if (prm.cpcPlus) {
				//
				// Réduction du nombre de couleurs pour éviter les couleurs
				// trop proches
				//
				if (Mode < 3) {
					// Masquer 1 bit par composante
					for (int i = 0; i < Coul.Length; i++) {
						if (((i & 1) == 1 && prm.reductPal1) || ((i & 1) == 0 && prm.reductPal2)) {
							int c1 = (i & 0xC00) * 0xFFF / 0xC00;
							int c2 = (i & 0xC0) * 0xFF / 0xC0;
							int c3 = (i & 0x0C) * 0x0F / 0x0C;
							int t = Coul[i];
							Coul[i] = 0;
							if (prm.newReduct)
								Coul[(c1 & 0xF00) + (c2 & 0xF0) + (c3 & 0x0F)] += t;
							else
								Coul[(c1 & 0xC00) + (c2 & 0xC0) + (c3 & 0x0C)] += t;
						}
					}
				}
			}
			int NbCol = 0;
			for (int i = 0; i < Coul.Length; i++)
				if (Coul[i] > 0)
					NbCol++;

			return NbCol;
		}

		//
		// Recherche les x couleurs les plus utilisées parmis les n possibles (remplit le tableau CChoix)
		//
		static void RechercheCMax(int[] CChoix, int maxCol, int[] lockState, bool cpcPlus, bool sortPal) {
			int x, FindMax = cpcPlus ? 4096 : 27;

			for (x = 0; x < maxCol; x++)
				if (lockState[x] > 0)
					Coul[CChoix[x]] = 0;

			for (x = 0; x < maxCol; x++) {
				int valMax = 0;
				if (lockState[x] == 0) {
					for (int i = 0; i < FindMax; i++) {
						if (valMax < Coul[i]) {
							valMax = Coul[i];
							CChoix[x] = i;
						}
					}
					Coul[CChoix[x]] = 0;
				}
			}

			if (sortPal)
				for (x = 0; x < maxCol - 1; x++)
					for (int y = x + 1; y < maxCol; y++)
						if (lockState[x] == 0 && lockState[y] == 0 && CChoix[x] > CChoix[y]) {
							int tmp = CChoix[x];
							CChoix[x] = CChoix[y];
							CChoix[y] = tmp;
						}
		}

		//
		// Passe 2 : réduit l'image à MaxCol couleurs.
		//
		static private void Passe2(ImageCpc dest, Param p) {
			RvbColor[] tabCol = new RvbColor[16];
			int[] MemoLockState = new int[16];
			int i, modeCpc = (dest.ModeCPC >= 3 ? dest.ModeCPC - 3 : dest.ModeCPC);
			int Tx = 4 >> modeCpc;
			int maxCol = 1 << Tx;

			for (i = 0; i < 16; i++)
				MemoLockState[i] = p.lockState[i];

			if (dest.ModeCPC >= 3) {
				int newMax = 4 >> (dest.ModeCPC - 3);
				RechercheCMax(dest.Palette, newMax, MemoLockState, p.cpcPlus, p.sortPal);
				for (i = 0; i < newMax; i++)
					MemoLockState[i] = 1;
			}
			RechercheCMax(dest.Palette, maxCol, MemoLockState, p.cpcPlus, p.sortPal);

			// réduit l'image à MaxCol couleurs.
			for (i = 0; i < maxCol; i++)
				tabCol[i] = p.cpcPlus ? new RvbColor((byte)(((dest.Palette[i] & 0xF0) >> 4) * 17), (byte)(((dest.Palette[i] & 0xF00) >> 8) * 17), (byte)((dest.Palette[i] & 0x0F) * 17)) : ImageCpc.RgbCPC[dest.Palette[i] < 27 ? dest.Palette[i] : 0];

			for (int y = 0; y < dest.TailleY; y += 2) {
				modeCpc = (dest.ModeCPC >= 3 ? (y & 2) == 0 ? dest.ModeCPC - 2 : dest.ModeCPC - 3 : dest.ModeCPC);
				Tx = 4 >> modeCpc;
				maxCol = 1 << Tx;
				for (int x = 0; x < dest.TailleX; x += Tx) {
					int oldDist = 0x7FFFFFFF;
					RvbColor pix = bitmap.GetPixelColor(x, y);
					int choix = 0;
					for (i = 0; i < maxCol; i++) {
						RvbColor c = tabCol[i];
						int dist = Math.Abs(pix.red - c.red) * K_R + Math.Abs(pix.green - c.green) * K_V + Math.Abs(pix.blue - c.blue) * K_B;
						if (dist < oldDist) {
							choix = i;
							oldDist = dist;
							if (dist == 0)
								break;
						}
					}
					dest.SetPixelCpc(x, y, choix, modeCpc);
				}
			}
		}

		static public int Convert(Bitmap source, ImageCpc dest, Param p) {
			bitmap = new LockBitmap(source);
			bitmap.LockBits();
			int nbCol = ConvertPasse1(dest.TailleX, dest.TailleY, p, dest.ModeCPC);
			dest.LockBits();
			Passe2(dest, p);
			dest.UnlockBits();
			bitmap.UnlockBits();
			return nbCol;
		}
	}
}
