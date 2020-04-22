﻿using System;
using System.Collections.Generic;
using System.Drawing;

namespace ConvImgCpc {
	public static class Conversion {
		const int K_R = 2449;	//9798;
		const int K_V = 4809;	//19235;
		const int K_B = 934;	//3735;

		const int SEUIL_LUM_1 = 85;		// 0x40;
		const int SEUIL_LUM_2 = 170;	// 0x80;

		static private int[,] CoulTrouvee = new int[4096, 272];
		static private LockBitmap bitmap;
		static private int xPix, yPix;
		static private byte[] tblContrast = new byte[256];

		static double[,] floyd =	{	{7, 3},
										{5, 1}};
		static double[,] bayer1 =	{	{1, 3},
										{4 , 2  } };
		static double[,] bayer2 =	{	{0, 12 , 3 , 15 },
										{8 , 4 , 11 , 7 },
										{2 , 14 , 1 , 13 },
										{10 , 6 , 9 , 5 }};
		static double[,] bayer3 =	{	{1 , 9 , 3 , 11 },
										{13 , 5 , 15 , 7 },
										{4 , 12 , 2 , 10 },
										{16 , 8 , 14 , 6 }};
		static double[,] ord1 =		{	{1 , 3 },
										{2 , 4 }};
		static double[,] ord2 =		{	{8 , 3 , 4 },
										{6 , 1 , 2 },
										{7 , 5 , 9 }};
		static double[,] ord3 =		{	{1 , 7 , 4 },
										{5 , 8 , 3 },
										{6 , 2 , 9 }};
		static double[,] ord4 =		{	{0 , 8 , 2 , 10 },
										{12 , 4 , 14 , 6 },
										{3 , 11 , 1 , 9 },
										{15 , 7 , 13 , 5 }};
		static double[,] zigzag1 = {	{0, 4 , 0},
										{3 , 0, 1 },
										{0, 2 , 0}};
		static double[,] zigzag2 = {	{0, 4 , 2 , 0},
										{6 , 0, 5 , 3 },
										{0, 7 , 1 , 0}};
		static double[,] zigzag3 = {	{0, 0, 0, 7 , 0},
										{0, 2 , 6 , 9 , 8 },
										{3 , 0, 1 , 5 , 0},
										{0, 4 , 0, 0, 0}};
		static double[,] test3 =	{	{0, 3 },
										{0, 5 },
										{7 , 1 }};
		static double[,] test2 =	{	{8 , 4 , 5 },
										{3 , 0, 1 },
										{7 , 2 , 6 }};
		static double[,] test = { { 1 }, { 4 }, { 2 } };
		static double[,] test4 =	{	{0, 0, 7 },
										{3 , 5 , 1 }};
		static double[,] test1 = { { 1, 7 } };

		static double[,] matDither;

		static private Dictionary<string, double[,]> dicMat = new Dictionary<string, double[,]>() {
			{"Floyd-Steinberg (2x2)",	floyd},
			{ "Bayer 1 (2X2)",			bayer1},
			{ "Bayer 2 (4x4)",			bayer2},
			{ "Bayer 3 (4X4)",			bayer3},
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
				hue = v == max ? (b - r) / dif * 60f + 120f : b == max ? (r - v) / dif * 60f + 240f : (v - b) / dif * 60f + (b > v ? 360f : 0);
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
			//System.Array.Clear(CoulTrouvee, 0, CoulTrouvee.GetLength(0)*CoulTrouvee.GetLength(1));
			for (int i = 0; i < CoulTrouvee.GetLength(0); i++)
				for (int l = 0; l < (modeVirtuel == 5 ? 272 : 1); l++)
					CoulTrouvee[i, l] = 0;

			double c = prm.pctContrast / 100.0;
			for (int i = 0; i < 256; i++)
				tblContrast[i] = (byte)MinMax((int)(((((i / 255.0) - 0.5) * c) + 0.5) * 255));

			int pct = prm.cpcPlus ? prm.pct << 2 : prm.pct;
			if (pct > 0 && dicMat.ContainsKey(prm.methode)) {
				matDither = dicMat[prm.methode];
				double sum = 0;
				for (int y = 0; y < matDither.GetLength(1); y++)
					for (int x = 0; x < matDither.GetLength(0); x++)
						sum += matDither[x, y];

				sum *= 100.0;
				for (int y = 0; y < matDither.GetLength(1); y++)
					for (int x = 0; x < matDither.GetLength(0); x++)
						matDither[x, y] /= sum;
			}
			else
				pct = 0;

			RvbColor choix, p = new RvbColor(0);
			int indexChoix = 0;
			for (yPix = 0; yPix < ydest; yPix += 2) {
				int Tx = 4 >> (modeVirtuel == 5 ? 1 : modeVirtuel >= 3 ? (yPix & 2) == 0 ? modeVirtuel - 2 : modeVirtuel - 3 : modeVirtuel);
				for (xPix = 0; xPix < xdest; xPix += Tx) {
					if (prm.lissage) {
						float r = 0, v = 0, b = 0;
						for (int i = 0; i < Tx; i++) {
							p = bitmap.GetPixelColor(xPix + i, yPix);
							r += p.r;
							b += p.b;
							v += p.v;
						}
						p.r = (byte)(r / Tx);
						p.v = (byte)(v / Tx);
						p.b = (byte)(b / Tx);
					}
					else 
						p = bitmap.GetPixelColor(xPix, yPix);
					
					if (p.r != 0 || p.v != 0 || p.b != 0) {
						float r = tblContrast[p.r];
						float v = tblContrast[p.v];
						float b = tblContrast[p.b];
						if (prm.pctLumi != 100 || prm.pctSat != 100)
							SetLumiSat(prm.pctLumi / 100.0F, prm.pctSat / 100.0F, ref r, ref v, ref b);

						p.r = (byte)MinMax((int)r);
						p.v = (byte)MinMax((int)v);
						p.b = (byte)MinMax((int)b);
					}

					// Recherche le point dans la couleur cpc la plus proche
					if (prm.cpcPlus) {
						int nr = p.r >> 4;
						int nv = p.v >> 4;
						int nb = p.b >> 4;
						if (prm.reductPal1) {
							nr &= 0x0E;
							nv &= 0x0E;
							nb &= 0x0E;
						}
						if (prm.reductPal2) {
							nr &= 0x0D;
							nv &= 0x0D;
							nb &= 0x0D;
						}
						choix = new RvbColor((byte)(nr * 17), (byte)(nv * 17), (byte)(nb * 17));
						indexChoix = ((choix.v << 4) & 0xF00) + ((choix.r) & 0xF0) + ((choix.b) >> 4);
					}
					else {
						if (!prm.newMethode)
							indexChoix = (p.r > SEUIL_LUM_2 ? 2 : p.r > SEUIL_LUM_1 ? 1 : 0) + (p.b > SEUIL_LUM_2 ? 6 : p.b > SEUIL_LUM_1 ? 3 : 0) + (p.v > SEUIL_LUM_2 ? 18 : p.v > SEUIL_LUM_1 ? 9 : 0);
						else {
							int oldDist = 0x7FFFFFFF;
							for (int i = 0; i < 27; i++) {
								RvbColor s = ImageCpc.RgbCPC[i];
								int dist = (p.r - s.r) * (p.r - s.r) * K_R + (p.v - s.v) * (p.v - s.v) * K_V + (p.b - s.b) * (p.b - s.b) * K_B;
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
						CalcDiffMethodeMat(pct * (p.r - choix.r), 0, Tx);		// Modif. rouge
						CalcDiffMethodeMat(pct * (p.v - choix.v), 1, Tx);	// Modif. Vert
						CalcDiffMethodeMat(pct * (p.b - choix.b), 2, Tx);		// Modif. Bleu
					}
					bitmap.SetPixel(xPix, yPix, prm.setPalCpc ? choix : p);
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

		//
		// Recherche les couleurs pour le mode "X"
		//
		static void RechercheCMaxModeX(int[] palette, int[] colMode5, int[] lockState, int yMax, bool cpcPlus, bool sortPal) {
			int x, FindMax = cpcPlus ? 4096 : 27;

			for (x = 0; x < 4; x++)
				if (lockState[x] > 0)
					CoulTrouvee[palette[x], 0] = 0;

			// Recherche les couleurs fixes (0 à 2)
			for (x = 0; x < 3; x++) {
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
			int j = 1;
			for (int y = 0; y < yMax >> 1; y++) {
				int valMax = 0;
				if (lockState[3] == 0) {
					for (int i = 0; i < FindMax; i++) {
						int c = CoulTrouvee[i, y] + (y - j >= 0 ? (int)(CoulTrouvee[i, y - j] * 0.5) : 0) + (y < (yMax >> 1) - j ? (int)(CoulTrouvee[i, y + j] * .5) : 0);
						if (valMax < c) {
							valMax = c;
							colMode5[y] = i;
						}
					}
					CoulTrouvee[colMode5[y], y] = 0;
				}
			}

			if (sortPal)
				for (x = 0; x < 2; x++)
					for (int y = x + 1; y < 3; y++)
						if (lockState[x] == 0 && lockState[y] == 0 && palette[x] > palette[y]) {
							int tmp = palette[x];
							palette[x] = palette[y];
							palette[y] = tmp;
						}
		}

		//
		// Passe 2 : réduit l'image à MaxCol couleurs.
		//
		static private void Passe2(ImageCpc dest, Param p) {
			RvbColor[,] tabCol = new RvbColor[16, 272];
			int[] MemoLockState = new int[16];
			int i, modeCpc = (dest.modeVirtuel == 5 ? 1 : dest.modeVirtuel >= 3 ? dest.modeVirtuel - 3 : dest.modeVirtuel);
			int Tx = 4 >> modeCpc;
			int maxCol = 1 << Tx;

			for (i = 0; i < 16; i++)
				MemoLockState[i] = p.lockState[i];

			if (dest.modeVirtuel == 3 || dest.modeVirtuel == 4) {
				int newMax = 4 >> (dest.modeVirtuel - 3);
				RechercheCMax(dest.Palette, newMax, MemoLockState, p.cpcPlus, p.sortPal);
				for (i = 0; i < newMax; i++)
					MemoLockState[i] = 1;
			}
			if (dest.modeVirtuel == 5)
				RechercheCMaxModeX(dest.Palette, dest.colMode5, MemoLockState, dest.TailleY, p.cpcPlus, p.sortPal);
			else
				RechercheCMax(dest.Palette, maxCol, MemoLockState, p.cpcPlus, p.sortPal);

			// réduit l'image à MaxCol couleurs.
			for (int y = 0; y < dest.TailleY; y += 2)
				for (i = 0; i < maxCol; i++)
					tabCol[i, y >> 1] = p.cpcPlus ? new RvbColor((byte)(((dest.Palette[i] & 0xF0) >> 4) * 17), (byte)(((dest.Palette[i] & 0xF00) >> 8) * 17), (byte)((dest.Palette[i] & 0x0F) * 17))
						: ImageCpc.RgbCPC[dest.modeVirtuel == 5 && i == 3 ? dest.colMode5[y >> 1] < 27 ? dest.colMode5[y >> 1] : 0 : dest.Palette[i] < 27 ? dest.Palette[i] : 0];

			if (p.motif) {
				for (int y = 0; y < dest.TailleY; y += 2) {
					modeCpc = (dest.modeVirtuel == 5 ? 1 : dest.modeVirtuel >= 3 ? (y & 2) == 0 ? dest.modeVirtuel - 2 : dest.modeVirtuel - 3 : dest.modeVirtuel);
					Tx = 4 >> modeCpc;
					maxCol = 1 << Tx;
					for (int x = 0; x < dest.TailleX; x += (Tx << 1)) {
						int dist, oldDist = 0x7FFFFFFF;
						RvbColor pix1 = bitmap.GetPixelColor(x, y);
						RvbColor pix2 = bitmap.GetPixelColor(x + Tx, y);
						int choix1 = 0, choix2 = 0;
						for (int i1 = 0; i1 < maxCol; i1++) {
							int i3 = (y & 2) == 0 ? i1 : maxCol - 1 - i1;
							RvbColor c1 = tabCol[i3, y >> 1];
							for (int i2 = 0; i2 < maxCol; i2++) {
								RvbColor c2 = tabCol[i2, y >> 1];
								dist = Math.Abs(pix1.r - c1.r) * K_R
									+ Math.Abs(pix1.v - c1.v) * K_V
									+ Math.Abs(pix1.b - c1.b) * K_B
									+ Math.Abs(pix2.r - c2.r) * K_R
									+ Math.Abs(pix2.v - c2.v) * K_V
									+ Math.Abs(pix2.b - c2.b) * K_B;
								if (i2 != i3 || i3 == 0 || pix1.r != pix2.r || pix1.b != pix2.b || pix1.v != pix2.v || dist == 0) {
									if (dist < oldDist) {
										choix1 = i3;
										choix2 = i2;
										oldDist = dist;
										if (dist == 0) {
											i1 = i2 = 333;
											break;
										}
									}
								}
							}
						}
						dest.SetPixelCpc(x, y, choix1, Tx);
						dest.SetPixelCpc(x + Tx, y, choix2, Tx);
					}
				}

			}
			else {
				for (int y = 0; y < dest.TailleY; y += 2) {
					modeCpc = (dest.modeVirtuel == 5 ? 1 : dest.modeVirtuel >= 3 ? (y & 2) == 0 ? dest.modeVirtuel - 2 : dest.modeVirtuel - 3 : dest.modeVirtuel);
					Tx = 4 >> modeCpc;
					maxCol = 1 << Tx;
					for (int x = 0; x < dest.TailleX; x += Tx) {
						int oldDist = 0x7FFFFFFF;
						RvbColor pix = bitmap.GetPixelColor(x, y);
						int choix = 0;
						for (i = 0; i < maxCol; i++) {
							RvbColor c = tabCol[i, y >> 1];
							int dist = Math.Abs(pix.r - c.r) * K_R + Math.Abs(pix.v - c.v) * K_V + Math.Abs(pix.b - c.b) * K_B;
							if (dist < oldDist) {
								choix = i;
								oldDist = dist;
								if (dist == 0)
									break;
							}
						}
						if (dest.modeVirtuel == 5)
							dest.SetPixelMode5(x, y, choix);
						else
							dest.SetPixelCpc(x, y, choix, Tx);
					}
				}
			}
		}

		static public int Convert(Bitmap source, ImageCpc dest, Param p) {
			dest.LockBits();
			bitmap = new LockBitmap(source);
			bitmap.LockBits();
			int nbCol = ConvertPasse1(dest.TailleX, dest.TailleY, p, dest.modeVirtuel);
			Passe2(dest, p);
			bitmap.UnlockBits();
			dest.UnlockBits();
			return nbCol;
		}
	}
}
