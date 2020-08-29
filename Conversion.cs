using System;
using System.Collections.Generic;

namespace ConvImgCpc {
	public static class Conversion {
		static private int[,] coulTrouvee = new int[4096, 272];
		static private byte[] tblContrast = new byte[256];

		static double[,] floyd =	{	{7, 3},
										{5, 1}};
		static double[,] bayer1 =	{	{1, 3},
										{4, 2 }};
		static double[,] bayer2 =	{	{0, 12, 3, 15},
										{8, 4, 11, 7},
										{2, 14, 1, 13},
										{10, 6, 9, 5}};
		static double[,] bayer3 =	{	{1, 9, 3, 11 },
										{13, 5, 15, 7},
										{4, 12, 2, 10},
										{16, 8, 14, 6}};
		static double[,] ord1 =		{	{1, 3},
										{2, 4}};
		static double[,] ord2 =		{	{8, 3, 4},
										{6, 1, 2},
										{7, 5, 9}};
		static double[,] ord3 =		{	{0, 8, 2, 10},
										{12, 4, 14, 6},
										{3, 11, 1, 9},
										{15, 7, 13, 5}};
		static double[,] ord4 =		{	{0,48,12,60, 3,51,15,63},
										{32,16,44,28,35,19,47,31},
										{8,56, 4,52,11,59, 7,55},
										{40,24,36,20,43,27,39,23},
										{2,50,14,62, 1,49,13,61},
										{34,18,46,30,33,17,45,29},
										{10,58, 6,54, 9,57, 5,53},
										{42,26,38,22,41,25,37,21 }};
		static double[,] zigzag1 = {	{0, 4, 0},
										{3, 0, 1 },
										{0, 2, 0}};
		static double[,] zigzag2 = {	{0, 4, 2, 0},
										{6, 0, 5, 3 },
										{0, 7, 1, 0}};
		static double[,] zigzag3 = {	{0, 0, 0, 7, 0},
										{0, 2, 6, 9, 8 },
										{3, 0, 1, 5, 0},
										{0, 4, 0, 0, 0}};

		static double[,] test0 =	{	{1, 16 },
										{16, 1}};

		static double[,] test1 =	{	{1, 4 },
										{4, 1}};

		static double[,] test2 =	{	{4, 3 },
										{2, 1}};

		static double[,] test3 =	{	{8, 4, 5},
										{3, 0, 1},
										{7, 2, 6}};

		static double[,] test4 =	{	{0, 3 },
										{0, 5 },
										{7, 1 }};

		static double[,] test5 =	{	{0, 0, 7 },
										{3, 5, 1 }};

		static double[,] test6 =	{	{6, 8, 4 },
										{1, 0, 3 },
										{5, 2, 7}};

		static double[,] test7 =	{	{12, 11, 0 },
										{13, 10, 19 },
										{11, 13, 0}};

		static double[,] test8 =	{	{3, 7, 6, 2},
										{5, 4, 1, 0}};

		static double[,] test9 =	{	{1, 5, 10, 14},
										{3, 7, 8, 12},
										{13, 9, 6, 2},
										{15, 11, 4, 0}};

		static double[,] matDither;

		static private Dictionary<string, double[,]> dicMat = new Dictionary<string, double[,]>() {
			{"Floyd-Steinberg (2x2)",	floyd},
			{ "Bayer 1 (2X2)",			bayer1},
			{ "Bayer 2 (4x4)",			bayer2},
			{ "Bayer 3 (4X4)",			bayer3},
			{ "Ordered 1 (2x2)",		ord1},
			{ "Ordered 2 (4x4)",		ord3},
			{ "Ordered 3 (8x8)",		ord4},
			{ "ZigZag1 (3x3)",			zigzag1},
			{ "ZigZag2 (4x3)",			zigzag2},
			{ "ZigZag3 (5x4)",			zigzag3},
			{ "Test0",					test0},
			{ "Test1",					test1},
			{ "Test2",					test2},
			{ "Test3",					test3},
			{ "Test4",					test4},
			{ "Test5",					test5},
			{ "Test6",					test6},
			{ "Test7",					test7},
			{ "Test8",					test8},
			{ "Test9",					test9},
		};

		static byte MinMaxByte(double value) {
			return value >= 0 ? value <= 255 ? (byte)value : (byte)255 : (byte)0;
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

		static private int SetMatDither(Param prm) {
			int pct = prm.cpcPlus ? prm.pct << 3 : prm.pct << 1;
			if (pct > 0 && dicMat.ContainsKey(prm.methode)) {
				matDither = dicMat[prm.methode];
				double sum = 0;
				for (int y = 0; y < matDither.GetLength(1); y++)
					for (int x = 0; x < matDither.GetLength(0); x++)
						sum += matDither[x, y];

				for (int y = 0; y < matDither.GetLength(1); y++)
					for (int x = 0; x < matDither.GetLength(0); x++)
						matDither[x, y] = (matDither[x, y] * pct) / sum;
			}
			else
				pct = 0;

			return pct;
		}

		static private int GetNumColorPixelCpc(Param prm, RvbColor p) {
			int indexChoix = 0;

			if (!prm.newMethode)
				indexChoix = (p.r > prm.seuilLumR2 ? 6 : p.r > prm.seuilLumR1 ? 3 : 0) + (p.b > prm.seuilLumB2 ? 2 : p.b > prm.seuilLumB1 ? 1 : 0) + (p.v > prm.seuilLumV2 ? 18 : p.v > prm.seuilLumV1 ? 9 : 0);
			else {
				int oldDist = 0x7FFFFFFF;
				for (int i = 0; i < 27; i++) {
					RvbColor s = BitmapCpc.RgbCPC[i];
					int dist = Math.Abs(s.r - p.r) * prm.coefR + Math.Abs(s.v - p.v) * prm.coefV + Math.Abs(s.b - p.b) * prm.coefB;
					//int dist = (p.r - s.r) * (p.r - s.r) * prm.coefR + (p.v - s.v) * (p.v - s.v) * prm.coefV + (p.b - s.b) * (p.b - s.b) * prm.coefB;
					if (dist < oldDist) {
						oldDist = dist;
						indexChoix = i;
						if (dist == 0)
							break;
					}
				}
			}
			return indexChoix;
		}

		static private RvbColor TraitePixel(DirectBitmap source, int xPix, int yPix, int Tx, Param prm, int pct) {
			RvbColor p = new RvbColor(0);
			if (prm.lissage) {
				float r = 0, v = 0, b = 0;
				for (int i = 0; i < Tx; i++) {
					p = source.GetPixelColor(xPix + i, yPix);
					r += p.r;
					b += p.b;
					v += p.v;
				}
				p.r = (byte)(r / Tx);
				p.v = (byte)(v / Tx);
				p.b = (byte)(b / Tx);
			}
			else
				p = source.GetPixelColor(xPix, yPix);

			p.r = MinMaxByte(p.r * prm.pctRed / 100);
			p.v = MinMaxByte(p.v * prm.pctGreen / 100);
			p.b = MinMaxByte(p.b * prm.pctBlue / 100);
			if (p.r != 0 || p.v != 0 || p.b != 0) {
				float r = tblContrast[p.r];
				float v = tblContrast[p.v];
				float b = tblContrast[p.b];
				if (prm.pctLumi != 100 || prm.pctSat != 100)
					SetLumiSat(prm.pctLumi > 100 ? (100 + (prm.pctLumi - 100) * 2) / 100.0F : prm.pctLumi / 100.0F, prm.pctSat / 100.0F, ref r, ref v, ref b);

				p.r = MinMaxByte(r);
				p.v = MinMaxByte(v);
				p.b = MinMaxByte(b);
			}
			// Appliquer la matrice de tramage
			if (pct > 0 && prm.methode != "Floyd-Steinberg (2x2)") {
				int xm = (xPix / Tx) % matDither.GetLength(0);
				int ym = ((yPix) >> 1) % matDither.GetLength(1);
				p.r = MinMaxByte(p.r + matDither[xm, ym]);
				p.v = MinMaxByte(p.v + matDither[xm, ym]);
				p.b = MinMaxByte(p.b + matDither[xm, ym]);
			}
			return p;
		}

		static private int ConvertTrameTc(DirectBitmap source, ImageCpc dest, Param prm) {
			int pct = SetMatDither(prm);
			RvbColor p = new RvbColor(0);
			int indexChoix = 0;
			for (int yPix = 0; yPix < BitmapCpc.TailleY; yPix += 4) {
				int Tx = BitmapCpc.CalcTx(yPix);
				for (int xPix = 0; xPix < BitmapCpc.TailleX; xPix += Tx) {
					for (int yy = 0; yy < 4; yy += 2) {
						p = TraitePixel(source, xPix, yy + yPix, Tx, prm, pct);
						indexChoix = GetNumColorPixelCpc(prm, p);
						RvbColor choix = BitmapCpc.RgbCPC[indexChoix];
						if (pct > 0 && prm.methode == "Floyd-Steinberg (2x2)") {
							for (int y = 0; y < matDither.GetLength(1); y++)
								for (int x = 0; x < matDither.GetLength(0); x++)
									if (xPix + Tx * x < source.Width && yPix + 2 * y < source.Height) {
										RvbColor pix = source.GetPixelColor(xPix + Tx * x, yPix + (y << 1));
										pix.r = MinMaxByte(pix.r + (p.r - choix.r) * matDither[x, y] / 256);
										pix.v = MinMaxByte(pix.v + (p.v - choix.v) * matDither[x, y] / 256);
										pix.b = MinMaxByte(pix.b + (p.b - choix.b) * matDither[x, y] / 256);
										source.SetPixel(xPix + Tx * x, yPix + (y << 1), pix);
									}
						}

						coulTrouvee[indexChoix, (BitmapCpc.modeVirtuel == 5 || BitmapCpc.modeVirtuel == 6 ? yPix >> 1 : 0)]++;
						source.SetPixel(xPix, yy + yPix, prm.setPalCpc ? choix : p);
					}
					// Moyenne des 2 pixels
					RvbColor p0 = source.GetPixelColor(xPix, yPix);
					RvbColor p1 = source.GetPixelColor(xPix, yPix + 2);
					int totr = (p0.r + p1.r);
					int totv = (p0.v + p1.v);
					int totb = (p0.b + p1.b);
					RvbColor n1 = BitmapCpc.RgbCPC[(totr > prm.seuilLumR1 + prm.seuilLumR2 ? 6 : totr > prm.seuilLumR1 ? 3 : 0)
												+ (totv > prm.seuilLumV1 + prm.seuilLumV2 ? 18 : totv > prm.seuilLumV1 ? 9 : 0)
												+ (totb > prm.seuilLumB1 + prm.seuilLumB2 ? 2 : totb > prm.seuilLumB1 ? 1 : 0)];
					RvbColor n2 = BitmapCpc.RgbCPC[(totr > prm.seuilLumR2 * 2 ? 6 : totr > prm.seuilLumR2 ? 3 : 0)
												+ (totv > prm.seuilLumV2 * 2 ? 18 : totv > prm.seuilLumV2 ? 9 : 0)
												+ (totb > prm.seuilLumB2 * 2 ? 2 : totb > prm.seuilLumB2 ? 1 : 0)];
					int dist1 = Math.Abs(p0.r - n1.r) * prm.coefR + Math.Abs(p0.v - n1.v) * prm.coefV + Math.Abs(p0.b - n1.b) * prm.coefB
								+ Math.Abs(p1.r - n2.r) * prm.coefR + Math.Abs(p1.v - n2.v) * prm.coefV + Math.Abs(p1.b - n2.b) * prm.coefB;
					int dist2 = Math.Abs(p0.r - n2.r) * prm.coefR + Math.Abs(p0.v - n2.v) * prm.coefV + Math.Abs(p0.b - n2.b) * prm.coefB
								+ Math.Abs(p1.r - n1.r) * prm.coefR + Math.Abs(p1.v - n1.v) * prm.coefV + Math.Abs(p1.b - n1.b) * prm.coefB;
					if ((xPix & Tx) == 0) {
						//		if (dist1 < dist2) {
						source.SetPixel(xPix, yPix, n1);
						source.SetPixel(xPix, yPix + 2, n2);
					}
					else {
						source.SetPixel(xPix, yPix, n2);
						source.SetPixel(xPix, yPix + 2, n1);
					}
				}
			}
			int nbCol = 0;
			for (int i = 0; i < coulTrouvee.GetLength(0); i++) {
				bool memoCol = false;
				for (int y = 0; y < 272; y++)
					if (!memoCol && coulTrouvee[i, y] > 0) {
						nbCol++;
						memoCol = true;
					}
			}
			return nbCol;
		}

		//
		// Passe 1 : Réduit la palette aux x couleurs de la palette du CPC.
		// Effectue également un traitement de l'erreur (tramage) si demandé.
		// Calcule le nombre de couleurs utilisées dans l'image, et
		// remplit le tableau coulTrouvee avec ces couleurs
		//
		static private int ConvertPasse1(DirectBitmap source, ImageCpc dest, Param prm) {
			int pct = SetMatDither(prm);
			RvbColor choix, p = new RvbColor(0);
			int indexChoix = 0;
			for (int yPix = 0; yPix < BitmapCpc.TailleY; yPix += 2) {
				int Tx = BitmapCpc.CalcTx(yPix);
				for (int xPix = 0; xPix < BitmapCpc.TailleX; xPix += Tx) {
					p = TraitePixel(source, xPix, yPix, Tx, prm, pct);
					// Recherche le point dans la couleur cpc la plus proche
					if (prm.cpcPlus) {
						int nr = p.r >> 4;
						int nv = p.v >> 4;
						int nb = p.b >> 4;
						if (prm.reductPal1) {
							nr |= 0x01;
							nv |= 0x01;
							nb |= 0x01;
						}
						if (prm.reductPal2) {
							nr &= 0x0E;
							nv &= 0x0E;
							nb &= 0x0E;
						}
						if (prm.reductPal3) {
							nr |= 0x02;
							nv |= 0x02;
							nb |= 0x02;
						}
						if (prm.reductPal4) {
							nr &= 0x0D;
							nv &= 0x0D;
							nb &= 0x0D;
						}
						choix = new RvbColor((byte)(nr * 17), (byte)(nv * 17), (byte)(nb * 17));
						indexChoix = ((choix.v << 4) & 0xF00) + ((choix.b) & 0xF0) + ((choix.r) >> 4);
					}
					else {
						indexChoix = GetNumColorPixelCpc(prm, p);
						choix = BitmapCpc.RgbCPC[indexChoix];
					}
					if (pct > 0 && prm.methode == "Floyd-Steinberg (2x2)") {
						for (int y = 0; y < matDither.GetLength(1); y++)
							for (int x = 0; x < matDither.GetLength(0); x++)
								if (xPix + Tx * x < source.Width && yPix + 2 * y < source.Height) {
									RvbColor pix = source.GetPixelColor(xPix + Tx * x, yPix + (y << 1));
									pix.r = MinMaxByte(pix.r + (p.r - choix.r) * matDither[x, y] / 256);
									pix.v = MinMaxByte(pix.v + (p.v - choix.v) * matDither[x, y] / 256);
									pix.b = MinMaxByte(pix.b + (p.b - choix.b) * matDither[x, y] / 256);
									source.SetPixel(xPix + Tx * x, yPix + (y << 1), pix);
								}
					}
					coulTrouvee[indexChoix, (BitmapCpc.modeVirtuel == 5 || BitmapCpc.modeVirtuel == 6 ? yPix >> 1 : 0)]++;
					source.SetPixel(xPix, yPix, prm.setPalCpc ? choix : p);
				}
			}
			int nbCol = 0;
			for (int i = 0; i < coulTrouvee.GetLength(0); i++) {
				bool memoCol = false;
				for (int y = 0; y < 272; y++)
					if (!memoCol && coulTrouvee[i, y] > 0) {
						nbCol++;
						memoCol = true;
					}
			}
			return nbCol;
		}

		//
		// Recherche les x couleurs les plus utilisées parmis les n possibles (remplit le tableau BitmapCpc.Palette)
		//
		static void RechercheCMax(int maxCol, int[] lockState, Param p) {
			int x, FindMax = BitmapCpc.cpcPlus ? 4096 : 27;
			for (x = 0; x < maxCol; x++)
				if (lockState[x] > 0)
					coulTrouvee[BitmapCpc.Palette[x], 0] = 0;

			for (x = 0; x < maxCol; x++) {
				int valMax = 0;
				if (lockState[x] == 0) {
					for (int i = 0; i < FindMax; i++) {
						if (valMax < coulTrouvee[i, 0]) {
							valMax = coulTrouvee[i, 0];
							BitmapCpc.Palette[x] = i;
						}
					}
					for (int y = 0; y < 272; y++)
						coulTrouvee[BitmapCpc.Palette[x], y] = 0;
				}
			}
			if (p.sortPal)
				for (x = 0; x < maxCol - 1; x++)
					for (int c = x + 1; c < maxCol; c++)
						if (lockState[x] == 0 && lockState[c] == 0 && BitmapCpc.Palette[x] > BitmapCpc.Palette[c]) {
							int tmp = BitmapCpc.Palette[x];
							BitmapCpc.Palette[x] = BitmapCpc.Palette[c];
							BitmapCpc.Palette[c] = tmp;
						}
		}

		//
		//
		// Recherche les couleurs pour le mode "split"
		//
		static int RechercheCMaxModeSplit(int[,] colMode5, int[] lockState, int yMax, Param p) {
			int c, FindMax = BitmapCpc.cpcPlus ? 4096 : 27;

			// Les trois premières couleurs sont "fixes"
			for (c = 0; c < 3; c++) {
				if (lockState[c] > 0) {
					for (int y = 0; y < 272; y++) {
						coulTrouvee[BitmapCpc.Palette[c], y] = 0;
						colMode5[y, c] = BitmapCpc.Palette[c];
					}
				}
				else {
					int valMax = 0;
					for (int i = 0; i < FindMax; i++) {
						int valFound = 0;
						for (int y = 0; y < yMax >> 1; y++)
							valFound += coulTrouvee[i, y];

						if (valMax < valFound) {
							valMax = valFound;
							BitmapCpc.Palette[c] = i;
						}
					}
					for (int y = 0; y < 272; y++) {
						coulTrouvee[BitmapCpc.Palette[c], y] = 0;
						colMode5[y, c] = BitmapCpc.Palette[c];
					}
				}
			}

			// Recherche les couleurs par ligne
			for (c = 3; c < 9; c++) {
				for (int y = 0; y < yMax >> 1; y++) {
					int valMax = 0;
					for (int i = 0; i < FindMax; i++) {
						for (int deltay = -(p.trackModeX << 1); deltay <= (p.trackModeX << 1); deltay++) {
							if (y + deltay >= 0 && y + deltay < (yMax >> 1) && valMax < coulTrouvee[i, y + deltay]) {
								valMax = coulTrouvee[i, y + deltay];
								colMode5[y, c] = i;
							}
						}
					}
					coulTrouvee[colMode5[y, c], y] = 0;
				}
			}
			int[] cFound = new int[4096];

			int nbCol = 0;
			for (int i = 0; i < 16; i++) {
				for (int y = 0; y < 272; y++) {
					if (cFound[colMode5[y, i]] == 0) {
						cFound[colMode5[y, i]] = 1;
						nbCol++;
					}
				}
			}
			return nbCol;
		}

		//
		// Recherche les couleurs pour le mode "X"
		//
		static int RechercheCMaxModeX(int[,] colMode5, int[] lockState, int yMax, Param p) {
			int c, FindMax = BitmapCpc.cpcPlus ? 4096 : 27;

			// Les deux premières couleurs sont "fixes"
			for (c = 0; c < 2; c++) {
				if (lockState[c] > 0) {
					for (int y = 0; y < 272; y++) {
						coulTrouvee[BitmapCpc.Palette[c], y] = 0;
						colMode5[y, c] = BitmapCpc.Palette[c];
					}
				}
				else {
					int valMax = 0;
					for (int i = 0; i < FindMax; i++) {
						int valFound = 0;
						for (int y = 0; y < yMax >> 1; y++)
							valFound += coulTrouvee[i, y];

						if (valMax < valFound) {
							valMax = valFound;
							BitmapCpc.Palette[c] = i;
						}
					}
					for (int y = 0; y < 272; y++) {
						coulTrouvee[BitmapCpc.Palette[c], y] = 0;
						colMode5[y, c] = BitmapCpc.Palette[c];
					}
				}
			}

			// Recherche les couleurs par ligne
			for (c = 2; c < 4; c++) {
				for (int y = 0; y < yMax >> 1; y++) {
					int valMax = 0;
					for (int i = 0; i < FindMax; i++) {
						for (int deltay = -(p.trackModeX << 1); deltay <= (p.trackModeX << 1); deltay++) {
							if (y + deltay >= 0 && y + deltay < (yMax >> 1) && valMax < coulTrouvee[i, y + deltay]) {
								valMax = coulTrouvee[i, y + deltay];
								colMode5[y, c] = i;
							}
						}
					}
					coulTrouvee[colMode5[y, c], y] = 0;
				}
			}
			int[] cFound = new int[4096];

			int nbCol = 0;
			for (int i = 0; i < 16; i++) {
				for (int y = 0; y < 272; y++) {
					if (cFound[colMode5[y, i]] == 0) {
						cFound[colMode5[y, i]] = 1;
						nbCol++;
					}
				}
			}
			return nbCol;
		}

		static private void SetPixTrameM1(DirectBitmap bitmap, Param prm, ImageCpc dest, int maxCol, RvbColor[,] tabCol) {
			for (int y = 0; y <= BitmapCpc.TailleY - 8; y += 8) {
				maxCol = BitmapCpc.MaxCol(y);
				for (int x = 0; x < BitmapCpc.TailleX; x += 8) {
					int choix = 0, oldDist = 0x7FFFFFFF;
					for (int i = 0; i < 16; i++) {
						int dist = 0, r1 = 0, v1 = 0, b1 = 0, r2 = 0, v2 = 0, b2 = 0;
						for (int ym = 0; ym < 4; ym++) {
							for (int xm = 0; xm < 4; xm++) {
								RvbColor pix = bitmap.GetPixelColor(x + (xm << 1), y + (ym << 1));
								RvbColor c = tabCol[BitmapCpc.trameM1[i, xm, ym], ym + (y >> 1)];
								r1 += pix.r;
								v1 += pix.v;
								b1 += pix.b;
								r2 += c.r;
								v2 += c.v;
								b2 += c.b;
							}
						}
						dist = Math.Abs(r1 - r2) * prm.coefR + Math.Abs(v1 - v2) * prm.coefV + Math.Abs(b1 - b2) * prm.coefB;
						if (dist < oldDist) {
							choix = i;
							oldDist = dist;
							if (dist == 0)
								i = 16;
						}
					}
					for (int ym = 0; ym < 4; ym++)
						for (int xm = 0; xm < 4; xm++)
							dest.SetPixelCpc(x + (xm << 1), y + (ym << 1), BitmapCpc.trameM1[choix, xm, ym], 2);
				}
			}
		}

		static private void SetPixCol(DirectBitmap bitmap, Param prm, ImageCpc dest, int maxCol, RvbColor[,] tabCol) {
			int incY = BitmapCpc.modeVirtuel >= 8 ? 8 : 2;
			RvbColor pix;
			for (int y = 0; y < BitmapCpc.TailleY; y += incY) {
				int Tx = BitmapCpc.CalcTx(y);
				maxCol = BitmapCpc.MaxCol(y);
				for (int x = 0; x < BitmapCpc.TailleX; x += Tx) {
					int oldDist = 0x7FFFFFFF;
					int r = 0, v = 0, b = 0;
					for (int j = 0; j < incY; j += 2) {
						pix = bitmap.GetPixelColor(x, y + j);
						r += pix.r;
						v += pix.v;
						b += pix.b;
					}
					r = r / (incY >> 1);
					v = v / (incY >> 1);
					b = b / (incY >> 1);
					pix = new RvbColor((byte)r, (byte)v, (byte)b);
					int choix = 0;
					for (int i = 0; i < maxCol; i++) {
						RvbColor c = tabCol[i, y >> 1];
						if (c != null) {
							int dist = Math.Abs(pix.r - c.r) * prm.coefR + Math.Abs(pix.v - c.v) * prm.coefV + Math.Abs(pix.b - c.b) * prm.coefB;
							if (dist < oldDist) {
								choix = i;
								oldDist = dist;
								if (dist == 0)
									i = maxCol;
							}
						}
					}
					for (int j = 0; j < incY; j += 2)
						dest.SetPixelCpc(x, y + j, choix, Tx);
				}
			}
		}

		static private void SetPixColSplit(DirectBitmap bitmap, Param prm, ImageCpc dest, RvbColor[,] tabCol) {
			for (int y = 0; y < BitmapCpc.TailleY; y += 2) {
				int tailleSplit = 0, colSplit = -1;
				for (int x = 0; x < BitmapCpc.TailleX; x += 2) {
					int oldDist = 0x7FFFFFFF;
					RvbColor pix = bitmap.GetPixelColor(x, y);
					int choix = 0, memoPen = 0;
					for (int i = 0; i < 9; i++) {
						memoPen = i;
						if (colSplit != -1 && tailleSplit < 32 && i > 2)
							memoPen = colSplit;

						RvbColor c = tabCol[memoPen, y >> 1];
						int dist = Math.Abs(pix.r - c.r) * prm.coefR + Math.Abs(pix.v - c.v) * prm.coefV + Math.Abs(pix.b - c.b) * prm.coefB;
						if (dist < oldDist) {
							choix = memoPen;
							oldDist = dist;
							if (dist == 0 || memoPen == colSplit)
								i = 9;
						}
					}
					if (choix > 2) {
						if (colSplit != choix) {
							colSplit = choix;
							tailleSplit = 0;
						}
					}
					//if (choix == colSplit)
					tailleSplit++;

					dest.SetPixelCpc(x, y, choix, 2);
				}
			}
		}

		//
		// Passe 2 : réduit l'image à MaxCol couleurs.
		//
		static private void Passe2(DirectBitmap source, ImageCpc dest, Param p, ref int colSplit) {
			RvbColor[,] tabCol = new RvbColor[16, 272];
			int[] MemoLockState = new int[16];
			int i;
			int Tx = BitmapCpc.CalcTx();
			int maxCol = BitmapCpc.MaxCol(2);
			for (i = 0; i < 16; i++)
				MemoLockState[i] = p.lockState[i];

			if (BitmapCpc.modeVirtuel == 3 || BitmapCpc.modeVirtuel == 4) {
				int newMax = BitmapCpc.MaxCol(0);
				RechercheCMax(newMax, MemoLockState, p);
				for (i = 0; i < newMax; i++)
					MemoLockState[i] = 1;
			}
			if (BitmapCpc.modeVirtuel == 5 || BitmapCpc.modeVirtuel == 6) {
				if (BitmapCpc.modeVirtuel == 5)
					colSplit = RechercheCMaxModeX(dest.colMode5, MemoLockState, BitmapCpc.TailleY, p);
				else {
					colSplit = RechercheCMaxModeSplit(dest.colMode5, MemoLockState, BitmapCpc.TailleY, p);
					maxCol = 9;
				}
				// réduit l'image à MaxCol couleurs.
				for (int y = 0; y < BitmapCpc.TailleY >> 1; y++)
					for (i = 0; i < maxCol; i++)
						tabCol[i, y] = p.cpcPlus ? new RvbColor((byte)((dest.colMode5[y, i] & 0x0F) * 17), (byte)(((dest.colMode5[y, i] & 0xF00) >> 8) * 17), (byte)(((dest.colMode5[y, i] & 0xF0) >> 4) * 17))
							: BitmapCpc.RgbCPC[dest.colMode5[y, i] < 27 ? dest.colMode5[y, i] : 0];
			}
			else {
				RechercheCMax(maxCol, MemoLockState, p);
				// réduit l'image à MaxCol couleurs.
				for (int y = 0; y < BitmapCpc.TailleY; y += 2) {
					maxCol = BitmapCpc.MaxCol(y);
					for (i = 0; i < maxCol; i++)
						tabCol[i, y >> 1] = dest.bitmapCpc.GetColorPal(i);
				}
			}
			if (BitmapCpc.modeVirtuel == 7)
				SetPixTrameM1(source, p, dest, maxCol, tabCol);
			else
				if (BitmapCpc.modeVirtuel == 6)
					SetPixColSplit(source, p, dest, tabCol);
				else
					SetPixCol(source, p, dest, maxCol, tabCol);
		}

		static public int Convert(DirectBitmap source, ImageCpc dest, Param p, bool noInfo = false) {
			System.Array.Clear(coulTrouvee, 0, coulTrouvee.GetLength(0) * coulTrouvee.GetLength(1));
			double c = p.pctContrast / 100.0;
			for (int i = 0; i < 256; i++)
				tblContrast[i] = MinMaxByte(((((i / 255.0) - 0.5) * c) + 0.5) * 255);

			int nbCol = p.trameTc ? ConvertTrameTc(source, dest, p) : ConvertPasse1(source, dest, p);
			int colSplit = 0;
			Passe2(source, dest, p, ref colSplit);
			if (!noInfo) {
				dest.main.SetInfo("Conversion terminée, nombre de couleurs dans l'image:" + nbCol);
				if (colSplit > 0)
					dest.main.SetInfo("Couleurs générées avec split : " + colSplit);
			}

			dest.bitmapCpc.isCalc = true;
			return nbCol;
		}

		// Calcul automatique matrice 4x4 en mode 1
		static public void CnvTrame(DirectBitmap source, Param prm, ImageCpc dest, List<TrameM1> lstTrame, Param p) {
			p.modeVirtuel = 1;
			ConvertPasse1(source, dest, p);
			RechercheCMax(4, p.lockState, p);
			for (int y = 0; y < BitmapCpc.TailleY; y += 8) {
				for (int x = 0; x < BitmapCpc.TailleX; x += 8) {
					TrameM1 locTrame = new TrameM1();
					for (int maty = 0; maty < 4; maty++) {
						for (int matx = 0; matx < 4; matx++) {
							RvbColor pix = source.GetPixelColor(x + (matx << 1), y + (maty << 1));
							int oldDist = 0x7FFFFFFF;
							int choix = 0;
							for (int i = 0; i < 4; i++) {
								RvbColor c = dest.bitmapCpc.GetColorPal(i);
								int dist = Math.Abs(pix.r - c.r) * prm.coefR + Math.Abs(pix.v - c.v) * prm.coefV + Math.Abs(pix.b - c.b) * prm.coefB;
								if (dist < oldDist) {
									choix = i;
									oldDist = dist;
									if (dist == 0)
										i = 4;
								}
							}
							locTrame.SetPix(matx, maty, choix);
						}
					}
					bool found = false;
					foreach (TrameM1 t in lstTrame) {
						if (t.IsSame(locTrame, 3)) {
							found = true;
							break;
						}
					}
					if (!found)
						lstTrame.Add(locTrame);
				}
			}
		}
	}
}
