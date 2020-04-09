using System;
using System.Collections.Generic;
using System.Drawing;

namespace ConvImgCpc {
	public static class Conversion {
		const int K_R = 9798;
		const int K_V = 19235;
		const int K_B = 3735;

		const int SEUIL_LUM_1 = 0x40;
		const int SEUIL_LUM_2 = 0x80;

		static private int[,] CoulTrouvee = new int[4096, 272];
		static private LockBitmap bitmap;
		static private int xPix, yPix;
		static private byte[] tblContrast = new byte[256];

		static double[,] floyd = {	{7 / 1600.0, 3 / 1600.0},
									{5 / 1600.0, 1 / 1600.0}};
		static double[,] bayer2 = {	{1 / 1600.0, 3 / 1600.0 },
									{4 / 1600.0, 2 / 1600.0 } };
		static double[,] bayer3 = {	{0, 12 / 32000.0, 3 / 32000.0, 15 / 32000.0},
									{8 / 32000.0, 4 / 32000.0, 11 / 32000.0, 7 / 32000.0},
									{2 / 32000.0, 14 / 32000.0, 1 / 32000.0, 13 / 32000.0},
									{10 / 32000.0, 6 / 32000.0, 9 / 32000.0, 5 / 32000.0}};
		static double[,] bayer4 = {	{1 / 32000.0, 9 / 32000.0, 3 / 32000.0, 11 / 32000.0},
									{13 / 32000.0, 5 / 32000.0, 15 / 32000.0, 7 / 32000.0},
									{4 / 32000.0, 12 / 32000.0, 2 / 32000.0, 10 / 32000.0},
									{16 / 32000.0, 8 / 32000.0, 14 / 32000.0, 6 / 32000.0}};
		static double[,] ord1 = {	{1 / 1600.0, 3 / 1600.0},
									{2 / 1600.0, 4 / 1600.0}};
		static double[,] ord2 = {	{8 / 4000.0, 3 / 4000.0, 4 / 4000.0},
									{6 / 4000.0, 1 / 4000.0, 2 / 4000.0},
									{7 / 4000.0, 5 / 4000.0, 9 / 4000.0}};
		static double[,] ord3 = {	{1 / 4000.0, 7 / 4000.0, 4 / 4000.0},
									{5 / 4000.0, 8 / 4000.0, 3 / 4000.0},
									{6 / 4000.0, 2 / 4000.0, 9 / 4000.0}};
		static double[,] ord4 = {	{0 / 32000.0, 8 / 32000.0, 2 / 32000.0, 10 / 32000.0},
									{12 / 32000.0, 4 / 32000.0, 14 / 32000.0, 6 / 32000.0},
									{3 / 32000.0, 11 / 32000.0, 1 / 32000.0, 9 / 32000.0},
									{15 / 32000.0, 7 / 32000.0, 13 / 32000.0, 5 / 32000.0}};
		static double[,] zigzag1 = {	{0, 4 / 1600.0, 0},
										{3 / 1600.0, 0, 1 / 1600.0},
										{0, 2 / 1600.0, 0}};
		static double[,] zigzag2 = {	{0, 4 / 4000.0, 2 / 4000.0, 0},
										{6 / 4000.0, 0, 5 / 4000.0, 3 / 4000.0},
										{0, 7 / 4000.0, 1 / 4000.0, 0}};
		static double[,] zigzag3 = {	{0, 0, 0, 7 / 4000.0, 0},
										{0, 2 / 4000.0, 6 / 4000.0, 9 / 4000.0, 8 / 4000.0},
										{3 / 4000.0, 0, 1 / 4000.0, 5 / 4000.0, 0},
										{0, 4 / 4000.0, 0, 0, 0}};
		static double[,] test3 = {	{0, 3 / 1600.0},
									{0, 5 / 1600.0},
									{7 / 1600.0, 1 / 1600.0}};
		static double[,] test2 = {	{8 / 4000.0, 4 / 4000.0, 5 / 4000.0},
									{3 / 4000.0, 0, 1 / 4000.0},
									{7 / 4000.0, 2 / 4000.0, 6 / 4000.0}};
		static double[,] test1 = { { 1 }, { 4 }, { 2 } };
		static double[,] test4 = {	{0, 0, 7 / 1600.0},
									{3 / 1600.0, 5 / 1600.0, 1 / 1600.0}};
		static double[,] test = { { 1 / 1600.0, 7 / 1600.0 } };

		static double[,] matDither;

		static private Dictionary<string, double[,]> dicMat = new Dictionary<string, double[,]>() {
			{"Floyd-Steinberg (2x2)",	floyd},
			{ "Bayer 1 (2X2)",			bayer2},
			{ "Bayer 2 (4x4)",			bayer3},
			{ "Bayer 3 (4X4)",			bayer4},
			{ "Ordered 1 (2x2)",		ord1},
			{ "Ordered 2 (3x3)",		ord2},
			{ "Ordered 3 (3x3)",		ord3},
			{ "Ordered 4 (4x4)",		ord4},
			{ "ZigZag1 (3x3)",			zigzag1},
			{ "ZigZag2 (4x3)",			zigzag2},
			{ "ZigZag3 (5x4)",			zigzag3},
			{ "Test",					test},
		};

		static byte MinMax(int value) {
			return value >= 0 ? value <= 255 ? (byte)value : (byte)255 : (byte)0;
		}

		// Applique une matrice de tramage
		static void CalcDiffMethodeMat(int diff, int decalMasque, int Tx) {
			for (int y = 0; y < matDither.GetLength(1); y++) {
				int adr = ((((yPix + 2 * y) * bitmap.Width) + xPix) << 2) + decalMasque;
				for (int x = 0; x < matDither.GetLength(0); x++) {
					if (adr < bitmap.Pixels.Length)
						bitmap.Pixels[adr] = (byte)MinMax((int)(bitmap.Pixels[adr] + (diff * matDither[x, y])));

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
						hue = (v - b) / dif * 60f + (b > v ? 360f : 0);
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
		static private int ConvertPasse1(int xdest, int ydest, Param prm, int modeVirtuel) {
			int pct = prm.cpcPlus ? prm.pct << 2 : prm.pct;

			for (int i = 0; i < CoulTrouvee.GetLength(0); i++)
				for (int l = 0; l < (modeVirtuel == 5 ? 272 : 1); l++)
					CoulTrouvee[i, l] = 0;

			float lumi = prm.pctLumi / 100.0F;
			float satur = prm.pctSat / 100.0F;
			double c = prm.pctContrast / 100.0;
			for (int i = 0; i < 256; i++)
				tblContrast[i] = (byte)MinMax((int)(((((i / 255.0) - 0.5) * c) + 0.5) * 255));

			if (pct > 0 && dicMat.ContainsKey(prm.methode))
				matDither = dicMat[prm.methode];
			else
				pct = 0;

			RvbColor choix, p = new RvbColor(0);
			for (yPix = 0; yPix < ydest; yPix += 2) {
				int Tx = 4 >> (modeVirtuel == 5 ? 1 : modeVirtuel >= 3 ? (yPix & 2) == 0 ? modeVirtuel - 2 : modeVirtuel - 3 : modeVirtuel);
				for (xPix = 0; xPix < xdest; xPix += Tx) {
					float r = 0, v = 0, b = 0;
					for (int i = 0; i < Tx; i++) {
						p = bitmap.GetPixelColor(xPix + i, yPix);
						r += p.red;
						b += p.blue;
						v += p.green;
					}
					p.red = (byte)(r / Tx);
					p.green = (byte)(v / Tx);
					p.blue = (byte)(b / Tx);
					if (p.red != 0 || p.green != 0 || p.blue != 0) {
						r = tblContrast[p.red];
						v = tblContrast[p.green];
						b = tblContrast[p.blue];
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
								int dist = (p.red - s.red) * (p.red - s.red) * K_R + (p.green - s.green) * (p.green - s.green) * K_V + (p.blue - s.blue) * (p.blue - s.blue) * K_B;
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

					CoulTrouvee[indexChoix, (modeVirtuel == 5 ? yPix >> 1 : 0)]++;
					if (pct > 0) {
						CalcDiffMethodeMat(pct * (p.red - choix.red), 0, Tx);		// Modif. rouge
						CalcDiffMethodeMat(pct * (p.green - choix.green), 1, Tx);	// Modif. Vert
						CalcDiffMethodeMat(pct * (p.blue - choix.blue), 2, Tx);		// Modif. Bleu
					}
					bitmap.SetPixel(xPix, yPix, choix);
				}
			}
			if (prm.cpcPlus) {
				//
				// Réduction du nombre de couleurs pour éviter les couleurs
				// trop proches
				//
				if (satur > 0) {
					// Masquer 1 bit par composante
					for (int i = 0; i < CoulTrouvee.GetLength(0); i++) {
						if (((i & 1) == 1 && prm.reductPal1) || ((i & 1) == 0 && prm.reductPal2)) {
							int c1 = (i & 0xC00) * 0xFFF / 0xC00;
							int c2 = (i & 0xC0) * 0xFF / 0xC0;
							int c3 = (i & 0x0C) * 0x0F / 0x0C;
							int t = CoulTrouvee[i, 0];
							CoulTrouvee[i, 0] = 0;
							if (prm.newReduct)
								CoulTrouvee[(c1 & 0xF00) + (c2 & 0xF0) + (c3 & 0x0F), 0] += t;
							else
								CoulTrouvee[(c1 & 0xC00) + (c2 & 0xC0) + (c3 & 0x0C), 0] += t;
						}
					}
				}
			}
			int NbCol = 0;
			for (int i = 0; i < CoulTrouvee.GetLength(0); i++)
				if (CoulTrouvee[i, 0] > 0)
					NbCol++;

			return NbCol;
		}

		//
		// Recherche les x couleurs les plus utilisées parmis les n possibles (remplit le tableau CChoix)
		//
		static void RechercheCMax(int[] palette, int maxCol, int[] lockState, bool cpcPlus, bool sortPal) {
			int x, FindMax = cpcPlus ? 4096 : 27;

			for (x = 0; x < maxCol; x++)
				if (lockState[x] > 0)
					CoulTrouvee[palette[x], 0] = 0;

			for (x = 0; x < maxCol; x++) {
				int valMax = 0;
				if (lockState[x] == 0) {
					for (int i = 0; i < FindMax; i++) {
						if (valMax < CoulTrouvee[i, 0]) {
							valMax = CoulTrouvee[i, 0];
							palette[x] = i;
						}
					}
					CoulTrouvee[palette[x], 0] = 0;
				}
			}

			if (sortPal)
				for (x = 0; x < maxCol - 1; x++)
					for (int y = x + 1; y < maxCol; y++)
						if (lockState[x] == 0 && lockState[y] == 0 && palette[x] > palette[y]) {
							int tmp = palette[x];
							palette[x] = palette[y];
							palette[y] = tmp;
						}
		}

		static void RechercheCMax(int[] palette, int[,] colMode5, int[] lockState, int yMax, bool cpcPlus, bool sortPal) {
			int x, FindMax = cpcPlus ? 4096 : 27;

			for (x = 0; x < 4; x++)
				if (lockState[x] > 0)
					CoulTrouvee[palette[x], 0] = 0;

			// Recherche les couleurs fixes (2 et 3)
			for (x = 2; x < 4; x++) {
				int valMax = 0;
				if (lockState[x] == 0) {
					for (int i = 0; i < FindMax; i++) {
						int suym = 0;
						for (int y = 0; y < yMax >> 1; y++)
							suym += CoulTrouvee[i, y];

						if (valMax < suym) {
							valMax = suym;
							palette[x] = i;
						}
					}
					for (int y = 0; y < yMax >> 1; y++)
						CoulTrouvee[palette[x], y] = 0;
				}
			}

			// Recherche les couleurs par ligne
			for (int y = 0; y < yMax >> 1; y++) {
				for (x = 0; x < 2; x++) {
					int valMax = 0;
					if (lockState[x] == 0) {
						for (int i = 0; i < FindMax; i++) {
							int coeff = 0;
							for (int j = 0; j < 8; j++) {
								int c = CoulTrouvee[i, y] + (y - j >= 0 ? CoulTrouvee[i, y - j] >> coeff : 0) + (y < (yMax >> 1) - j ? CoulTrouvee[i, y + j] >> coeff : 0);
								if (valMax < c) {
									valMax = c;
									colMode5[x, y] = i;
								}
								coeff++;
							}
						}
						CoulTrouvee[colMode5[x, y], y] = 0;
					}
				}
			}

			if (sortPal)
				if (lockState[2] == 0 && lockState[3] == 0 && palette[2] > palette[3]) {
					int tmp = palette[2];
					palette[2] = palette[3];
					palette[3] = tmp;
				}
		}

		//
		// Passe 2 : réduit l'image à MaxCol couleurs.
		//
		static private void Passe2(ImageCpc dest, Param p) {
			RvbColor[,] tabCol = new RvbColor[16, 272];
			int[] MemoLockState = new int[16];
			int i, modeCpc = (dest.ModeVirtuel == 5 ? 1 : dest.ModeVirtuel >= 3 ? dest.ModeVirtuel - 3 : dest.ModeVirtuel);
			int Tx = 4 >> modeCpc;
			int maxCol = 1 << Tx;

			for (i = 0; i < 16; i++)
				MemoLockState[i] = p.lockState[i];

			if (dest.ModeVirtuel == 3 || dest.ModeVirtuel == 4) {
				int newMax = 4 >> (dest.ModeVirtuel - 3);
				RechercheCMax(dest.Palette, newMax, MemoLockState, p.cpcPlus, p.sortPal);
				for (i = 0; i < newMax; i++)
					MemoLockState[i] = 1;
			}
			if (dest.ModeVirtuel == 5)
				RechercheCMax(dest.Palette, dest.colMode5, MemoLockState, dest.TailleY, p.cpcPlus, p.sortPal);
			else
				RechercheCMax(dest.Palette, maxCol, MemoLockState, p.cpcPlus, p.sortPal);

			// réduit l'image à MaxCol couleurs.
			for (int y = 0; y < dest.TailleY; y += 2)
				for (i = 0; i < maxCol; i++)
					tabCol[i, y >> 1] = p.cpcPlus ? new RvbColor((byte)(((dest.Palette[i] & 0xF0) >> 4) * 17), (byte)(((dest.Palette[i] & 0xF00) >> 8) * 17), (byte)((dest.Palette[i] & 0x0F) * 17))
						: ImageCpc.RgbCPC[dest.ModeVirtuel == 5 && i < 2 ? dest.colMode5[i, y >> 1] < 27 ? dest.colMode5[i, y >> 1] : 0 : dest.Palette[i] < 27 ? dest.Palette[i] : 0];

			for (int y = 0; y < dest.TailleY; y += 2) {
				modeCpc = (dest.ModeVirtuel == 5 ? 1 : dest.ModeVirtuel >= 3 ? (y & 2) == 0 ? dest.ModeVirtuel - 2 : dest.ModeVirtuel - 3 : dest.ModeVirtuel);
				Tx = 4 >> modeCpc;
				maxCol = 1 << Tx;
				for (int x = 0; x < dest.TailleX; x += Tx) {
					int oldDist = 0x7FFFFFFF;
					RvbColor pix = bitmap.GetPixelColor(x, y);
					int choix = 0;
					for (i = 0; i < maxCol; i++) {
						RvbColor c = tabCol[i, y >> 1];
						int dist = Math.Abs(pix.red - c.red) * K_R + Math.Abs(pix.green - c.green) * K_V + Math.Abs(pix.blue - c.blue) * K_B;
						if (dist < oldDist) {
							choix = i;
							oldDist = dist;
							if (dist == 0)
								break;
						}
					}
					if (dest.ModeVirtuel == 5)
						dest.SetPixelMode5(x, y, choix);
					else
						dest.SetPixelCpc(x, y, choix, modeCpc);
				}
			}
		}

		static public int Convert(Bitmap source, ImageCpc dest, Param p) {
			dest.LockBits();
			bitmap = new LockBitmap(source);
			bitmap.LockBits();
			int nbCol = ConvertPasse1(dest.TailleX, dest.TailleY, p, dest.ModeVirtuel);
			Passe2(dest, p);
			bitmap.UnlockBits();
			dest.UnlockBits();
			return nbCol;
		}
	}
}
