using System;
using System.Drawing;

namespace ConvImgCpc {
	public static class Conversion {
		//const int K_R = 256;
		//const int K_V = 256;
		//const int K_B = 256;
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
		static private RvbColor[] tabCol = new RvbColor[16];
		static private int coeffMat;
		static private byte[] tblContrast = new byte[256];

		static byte MinMax(int value) {
			return value >= 0 ? value <= 255 ? (byte)value : (byte)255 : (byte)0;
		}

		static int CalcDist(int r1, int v1, int b1, int r2, int v2, int b2) {
			return (r1 > r2 ? (r1 - r2) * K_R : (r2 - r1) * K_R) + (v1 > v2 ? (v1 - v2) * K_V : (v2 - v1) * K_V) + (b1 > b2 ? (b1 - b2) * K_B : (b2 - b1) * K_B);
		}

		static int CalcDist(RvbColor c, int r, int v, int b) {
			return (c.red > r ? (c.red - r) * K_R : (r - c.red) * K_R) + (c.green > v ? (c.green - v) * K_V : (v - c.green) * K_V) + (c.blue > b ? (c.blue - b) * K_B : (b - c.blue) * K_B);
		}

		static private void AddPixel(int x, int y, int corr, int decalMasque) {
			int adr = (((y * bitmap.Width) + x) << 2) + decalMasque;
			if (adr < bitmap.Pixels.Length)
				bitmap.Pixels[adr] = (byte)MinMax(bitmap.Pixels[adr] + corr);
		}

		static void CalcDiffMethode1Mat2(int diff, int decalMasque, int mode, int Tx) {
			int incY = mode < 3 ? 2 : 4;
			AddPixel(xPix + Tx, yPix, (diff * 7) / coeffMat, decalMasque);
			AddPixel(xPix - Tx, yPix + incY, (diff * 3) / coeffMat, decalMasque);
			AddPixel(xPix, yPix + incY, (diff * 5) / coeffMat, decalMasque);
			AddPixel(xPix + Tx, yPix + incY, diff / coeffMat, decalMasque);
		}

		static void CalcDiffMethode2Mat2(int diff, int decalMasque, int mode, int Tx) {
			int incY = mode < 3 ? 2 : 4;
			AddPixel(xPix + Tx, yPix, (diff << 3) / coeffMat, decalMasque);
			AddPixel(xPix - Tx, yPix + incY, (diff << 2) / coeffMat, decalMasque);
			AddPixel(xPix, yPix + incY, (diff << 1) / coeffMat, decalMasque);
			AddPixel(xPix + Tx, yPix + incY, diff / coeffMat, decalMasque);
		}

		static void CalcDiffMethode3Mat2(int diff, int decalMasque, int mode, int Tx) {
			int incY = mode < 3 ? 2 : 4;
			AddPixel(xPix + Tx, yPix, (diff * 3) / coeffMat, decalMasque);
			AddPixel(xPix - Tx, yPix + incY, (diff << 1) / coeffMat, decalMasque);
			AddPixel(xPix, yPix + incY, (diff << 1) / coeffMat, decalMasque);
		}

		static void CalcDiffMethode1Mat3(int diff, int decalMasque, int mode, int Tx) {
			int incY = mode < 3 ? 2 : 4;
			AddPixel(xPix + Tx, yPix, (diff * 7) / coeffMat, decalMasque);
			AddPixel(xPix + 2 * Tx, yPix, (diff * 5) / coeffMat, decalMasque);

			AddPixel(xPix - 2 * Tx, yPix + 2, (diff * 3) / coeffMat, decalMasque);
			AddPixel(xPix - Tx, yPix + incY, (diff * 5) / coeffMat, decalMasque);
			AddPixel(xPix, yPix + 2, (diff * 7) / coeffMat, decalMasque);
			AddPixel(xPix + Tx, yPix + incY, (diff * 5) / coeffMat, decalMasque);
			AddPixel(xPix + 2 * Tx, yPix + incY, (diff * 3) / coeffMat, decalMasque);

			AddPixel(xPix - 2 * Tx, yPix + incY + incY, diff / coeffMat, decalMasque);
			AddPixel(xPix - Tx, yPix + incY + incY, (diff * 3) / coeffMat, decalMasque);
			AddPixel(xPix, yPix + incY + incY, (diff * 5) / coeffMat, decalMasque);
			AddPixel(xPix + Tx, yPix + incY + incY, (diff * 3) / coeffMat, decalMasque);
			AddPixel(xPix + 2 * Tx, yPix + incY + incY, diff / coeffMat, decalMasque);
		}

		static void CalcDiffMethode2Mat3(int diff, int decalMasque, int mode, int Tx) {
			int incY = mode < 3 ? 2 : 4;
			AddPixel(xPix + Tx, yPix, (diff << 2) / coeffMat, decalMasque);
			AddPixel(xPix + 2 * Tx, yPix, (diff << 1) / coeffMat, decalMasque);

			AddPixel(xPix - 2 * Tx, yPix + incY, diff / coeffMat, decalMasque);
			AddPixel(xPix - Tx, yPix + incY, (diff << 1) / coeffMat, decalMasque);
			AddPixel(xPix, yPix + incY, diff / coeffMat, decalMasque);
			AddPixel(xPix + Tx, yPix + incY, (diff << 1) / coeffMat, decalMasque);
			AddPixel(xPix + 2 * Tx, yPix + incY, diff / coeffMat, decalMasque);
		}

		static void CalcDiffMethode3Mat3(int diff, int decalMasque, int mode, int Tx) {
			int incY = mode < 3 ? 2 : 4;
			AddPixel(xPix + Tx, yPix, (diff << 2) / coeffMat, decalMasque);
			AddPixel(xPix + 2 * Tx, yPix, (diff * 3) / coeffMat, decalMasque);

			AddPixel(xPix - 2 * Tx, yPix + incY, diff / coeffMat, decalMasque);
			AddPixel(xPix - Tx, yPix + incY, (diff << 1) / coeffMat, decalMasque);
			AddPixel(xPix, yPix + incY, (diff * 3) / coeffMat, decalMasque);
			AddPixel(xPix + Tx, yPix + incY, diff / coeffMat, decalMasque);
			AddPixel(xPix + 2 * Tx, yPix + incY, (diff << 1) / coeffMat, decalMasque);
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
		// p.methode, p.matrice, p.pct, p.cpcPlus, p.newMethode, dest.ModeCPC, p.cpcPlus, p.reductPal1, p.reductPal2, p.newReduct, p.pctLumi, p.pctSat, p.pctContrast);
		//
		static private int ConvertPasse1(int xdest, int ydest, Param prm, int Mode) {
			//if (prm.cpcPlus)
			//	prm.pct <<= 2;

			for (int i = 0; i < Coul.Length; i++)
				Coul[i] = 0;

			float lumi = prm.pctLumi / 100.0F;
			float satur = prm.pctSat / 100.0F;
			double c = prm.pctContrast / 100.0;
			for (int i = 0; i < 256; i++) {
				double contrast = ((((i / 255.0) - 0.5) * c) + 0.5) * 255;
				tblContrast[i] = (byte)MinMax((int)contrast);
			}
			fctCalcDiff = null;
			if (prm.pct > 0)
				switch (prm.matrice) {
					case 2:
						switch (prm.methode) {
							case 1:
								fctCalcDiff = CalcDiffMethode1Mat2;
								coeffMat = 1600 / prm.pct;
								break;

							case 2:
								fctCalcDiff = CalcDiffMethode2Mat2;
								coeffMat = 1500 / prm.pct;
								break;

							case 3:
								fctCalcDiff = CalcDiffMethode3Mat2;
								coeffMat = 800 / prm.pct;
								break;
						}
						break;
					case 3:
						switch (prm.methode) {
							case 1:
								fctCalcDiff = CalcDiffMethode1Mat3;
								coeffMat = 4800 / prm.pct;
								break;

							case 2:
								fctCalcDiff = CalcDiffMethode2Mat3;
								coeffMat = 1600 / prm.pct;
								break;

							case 3:
								fctCalcDiff = CalcDiffMethode3Mat3;
								coeffMat = 1600 / prm.pct;
								break;
						}
						break;
				}

			RvbColor choix, p = new RvbColor(0);
			for (yPix = 0; yPix < ydest; yPix += 2) {
				int Tx = 4 >> (Mode >= 3 ? (yPix & 2) == Mode - 3 ? Mode - 2 : 0 : Mode);
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
							int OldDist1 = 0x7FFFFFFF;
							for (int i = 0; i < 27; i++) {
								RvbColor s = BitmapCpc.RgbCPC[i];
								int Dist1 = CalcDist(s.red, s.green, s.blue, p.red, p.green, p.blue);
								if (Dist1 < OldDist1) {
									OldDist1 = Dist1;
									indexChoix = i;
									if (Dist1 == 0)
										break;
								}
							}
						}
						choix = BitmapCpc.RgbCPC[indexChoix];
					}
					Coul[indexChoix]++;

					if (fctCalcDiff != null) {
						// Modifie composante Rouge
						fctCalcDiff(p.red - choix.red, 0, Mode, Tx);

						// Modifie composante Verte
						fctCalcDiff(p.green - choix.green, 1, Mode, Tx);

						// Modifie composante Bleue
						fctCalcDiff(p.blue - choix.blue, 2, Mode, Tx);
					}
					bitmap.SetPixel(xPix, yPix, choix.GetColor);
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
		static void Passe2(BitmapCpc dest, int[] CChoix, int maxCol, bool cpcPlus, int pixMode) {
			for (int i = 0; i < maxCol; i++)
				tabCol[i] = cpcPlus ? new RvbColor((byte)(((CChoix[i] & 0xF0) >> 4) * 17), (byte)(((CChoix[i] & 0xF00) >> 8) * 17), (byte)((CChoix[i] & 0x0F) * 17)) : BitmapCpc.RgbCPC[CChoix[i] < 27 ? CChoix[i] : 0];

			for (int y = 0; y < dest.TailleY; y += 2) {
				int Tx = 4 >> (dest.ModeCPC >= 3 ? (y & 2) == 0 ? dest.ModeCPC - 2 : dest.ModeCPC - 3 : dest.ModeCPC);
				for (int x = 0; x < dest.TailleX; x += Tx) {
					int oldDist = 0x7FFFFFFF;
					int p = bitmap.GetPixel(x, y);
					int choix = 0;
					for (int i = 0; i < maxCol; i++) {
						int c = tabCol[i].GetColor;
						int Dist = ((p & 0xFF) > (c & 0xFF) ? ((p & 0xFF) - (c & 0xFF)) * K_R : ((c & 0xFF) - (p & 0xFF)) * K_R) + (((p >> 8) & 0xFF) > ((c >> 8) & 0xFF) ? (((p >> 8) & 0xFF) - ((c >> 8) & 0xFF)) * K_V : (((c >> 8) & 0xFF) - ((p >> 8) & 0xFF)) * K_V) + ((p >> 16) > (c >> 16) ? ((p - c) >> 16) * K_B : ((c - p) >> 16) * K_B);
						if (Dist < oldDist) {
							choix = i;
							oldDist = Dist;
							if (Dist == 0)
								break;
						}
					}
					dest.SetPixelCpc(x, y, choix, (dest.ModeCPC >= 3 ? (y & 2) == 0 ? dest.ModeCPC - 2 : dest.ModeCPC - 3 : dest.ModeCPC));
				}
			}
		}

		static public int Convert(Bitmap source, BitmapCpc dest, Param p) {
			int i, ret = 0;
			int[] memoLockState = new int[16];
			for (i = 0; i < 16; i++)
				memoLockState[i] = p.lockState[i];


			if (dest.ModeCPC <= 2)
				ret = DoConvert(source, dest, p);
			else {
				BitmapCpc bmpTmp = new BitmapCpc(dest.TailleX, dest.TailleY, 1);
				switch (dest.ModeCPC) {
					case 3:
						dest.ModeCPC = 1;
						ret = DoConvert(source, dest, p);
						for (i = 0; i < 4; i++)
							p.lockState[i] = 1;

						for (int y = 0; y < dest.TailleY; y += 4) {
							for (int x = 0; x < dest.TailleX; x += 8)
								bmpTmp.SetByte(x, y, dest.GetByte(x, y));
						}
						dest.ModeCPC = 0;
						ret = DoConvert(source, dest, p);

						for (int y = 0; y < dest.TailleY; y += 4) {
							for (int x = 0; x < dest.TailleX; x += 8)
								dest.SetByte(x, y, bmpTmp.GetByte(x, y));
						}
						dest.ModeCPC = 3;
						break;

					case 4:
						dest.ModeCPC = 2;
						ret = DoConvert(source, dest, p);
						for (i = 0; i < 2; i++)
							p.lockState[i] = 1;

						for (int y = 0; y < dest.TailleY; y += 4) {
							for (int x = 0; x < dest.TailleX; x += 8)
								bmpTmp.SetByte(x, y, dest.GetByte(x, y));
						}
						dest.ModeCPC = 1;
						ret = DoConvert(source, dest, p);

						for (int y = 0; y < dest.TailleY; y += 4) {
							for (int x = 0; x < dest.TailleX; x += 8)
								dest.SetByte(x, y, bmpTmp.GetByte(x, y));
						}
						dest.ModeCPC = 4;
						break;
				}
			}
			for (i = 0; i < 16; i++)
				p.lockState[i] = memoLockState[i];

			return ret;
		}

		static private int DoConvert(Bitmap source, BitmapCpc dest, Param p) {
			int[] CChoix = new int[16];
			int maxCol = 1 << (4 >> (dest.ModeCPC == 3 ? 0 : dest.ModeCPC));
			for (int i = 0; i < 16; i++)
				CChoix[i] = dest.Palette[i];

			int tailleX = dest.TailleX;
			int tailleY = dest.TailleY;
			double ratio = source.Width * tailleY / (double)(source.Height * tailleX);
			Bitmap tmp = new Bitmap(tailleX, tailleY);
			Graphics g = Graphics.FromImage(tmp);
			switch (p.sizeMode) {
				case Param.SizeMode.KeepSmaller:
					if (ratio < 1) {
						int newW = (int)(tailleX * ratio);
						g.DrawImage(source, (tailleX - newW) >> 1, 0, newW, tailleY);
					}
					else {
						int newH = (int)(tailleY / ratio);
						g.DrawImage(source, 0, (tailleY - newH) >> 1, tailleX, newH);
					}
					bitmap = new LockBitmap(tmp);
					break;

				case Param.SizeMode.KeepLarger:
					if (ratio < 1) {
						int newY = (int)(tailleY / ratio);
						g.DrawImage(source, 0, (tailleY - newY) >> 1, tailleX, newY);
					}
					else {
						int newX = (int)(tailleX * ratio);
						g.DrawImage(source, (tailleX - newX) >> 1, 0, newX, tailleY);
					}
					bitmap = new LockBitmap(tmp);
					break;

				case Param.SizeMode.Fit:
					bitmap = new LockBitmap(new Bitmap(source, tailleX, tailleY));
					break;
			}

			bitmap.LockBits();

			int nbCol = ConvertPasse1(dest.TailleX, dest.TailleY, p, dest.ModeCPC);
			
			
			RechercheCMax(CChoix, maxCol, p.lockState, p.cpcPlus, p.sortPal);
			
			
			Passe2(dest, CChoix, maxCol, p.cpcPlus, p.pixMode);
			
			
			for (int i = 0; i < 16; i++)
				dest.SetPalette(i, CChoix[i]);

			bitmap.UnlockBits();
			return nbCol;
		}
	}
}
