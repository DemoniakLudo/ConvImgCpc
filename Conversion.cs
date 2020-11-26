using System;
using System.Collections.Generic;

namespace ConvImgCpc {
	public static class Conversion {
		static private int[,] coulTrouvee = new int[4096, 272];
		static private byte[] tblContrast = new byte[256];

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

		static private int GetNumColorPixelCpc(Param prm, RvbColor p) {
			int indexChoix = 0;

			if (!prm.newMethode)
				indexChoix = (p.r > prm.cstR2 ? 6 : p.r > prm.cstR1 ? 3 : 0) + (p.b > prm.cstB2 ? 2 : p.b > prm.cstB1 ? 1 : 0) + (p.v > prm.cstV2 ? 18 : p.v > prm.cstV1 ? 9 : 0);
			else {
				int oldDist = 0x7FFFFFFF;
				for (int i = 0; i < 27; i++) {
					RvbColor s = BitmapCpc.RgbCPC[i];
					int dist = (s.r - p.r) * (s.r - p.r) * prm.coefR + (s.v - p.v) * (s.v - p.v) * prm.coefV + (s.b - p.b) * (s.b - p.b) * prm.coefB;
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

		static private RvbColor GetPixel(DirectBitmap source, int xPix, int yPix, int Tx, Param prm, int pct) {
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

			switch (prm.bitsRVB) {
				//12 bits (4 bits par composantes)
				case 12:
					p.r = (byte)((p.r >> 4) * 17);
					p.v = (byte)((p.v >> 4) * 17);
					p.b = (byte)((p.b >> 4) * 17);
					break;

				// 9 bits (3 bits par composantes)
				case 9:
					p.r = (byte)((p.r >> 5) * 36);
					p.v = (byte)((p.v >> 5) * 36);
					p.b = (byte)((p.b >> 5) * 36);
					break;

				// 6 bits (2 bits par composantes)
				case 6:
					p.r = (byte)((p.r >> 6) * 85);
					p.v = (byte)((p.v >> 6) * 85);
					p.b = (byte)((p.b >> 6) * 85);
					break;
			}

			int m1 = (prm.reductPal1 ? 0x10 : 0x00) | (prm.reductPal3 ? 0x20 : 0x00);
			int m2 = (prm.reductPal2 ? 0xE0 : 0xFF) & (prm.reductPal4 ? 0xD0 : 0xFF);
			p.r = MinMaxByte(((p.r | m1) & m2) * prm.pctRed / 100.0);
			p.v = MinMaxByte(((p.v | m1) & m2) * prm.pctGreen / 100.0);
			p.b = MinMaxByte(((p.b | m1) & m2) * prm.pctBlue / 100.0);
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
			if (pct > 0) {
				RvbColor choix = prm.cpcPlus ? new RvbColor((byte)((p.r >> 4) * 17), (byte)((p.v >> 4) * 17), (byte)((p.b >> 4) * 17)) : BitmapCpc.RgbCPC[GetNumColorPixelCpc(prm, p)];
				Dither.DoDitherFull(source, xPix, yPix, Tx, p, choix, prm.diffErr);
			}
			return p;
		}

		static private int ConvertTrameTc(DirectBitmap source, ImageCpc dest, Param prm) {
			int pct = Dither.SetMatDither(prm);
			RvbColor p = new RvbColor(0);
			int indexChoix = 0;
			for (int yPix = 0; yPix < BitmapCpc.TailleY; yPix += 4) {
				int Tx = BitmapCpc.CalcTx(yPix);
				for (int xPix = 0; xPix < BitmapCpc.TailleX; xPix += Tx) {
					for (int yy = 0; yy < 4; yy += 2) {
						p = GetPixel(source, xPix, yy + yPix, Tx, prm, pct);
						indexChoix = GetNumColorPixelCpc(prm, p);
						RvbColor choix = BitmapCpc.RgbCPC[indexChoix];
						coulTrouvee[indexChoix, (BitmapCpc.modeVirtuel == 5 || BitmapCpc.modeVirtuel == 6 ? yPix >> 1 : 0)]++;
						source.SetPixel(xPix, yy + yPix, prm.setPalCpc ? choix : p);
					}
					// Moyenne des 2 pixels
					RvbColor p0 = source.GetPixelColor(xPix, yPix);
					RvbColor p1 = source.GetPixelColor(xPix, yPix + 2);
					int totr = (p0.r + p1.r);
					int totv = (p0.v + p1.v);
					int totb = (p0.b + p1.b);
					RvbColor n1 = BitmapCpc.RgbCPC[(totr > prm.cstR3 ? 6 : totr > prm.cstR1 ? 3 : 0)
												+ (totv > prm.cstV3 ? 18 : totv > prm.cstV1 ? 9 : 0)
												+ (totb > prm.cstB3 ? 2 : totb > prm.cstB1 ? 1 : 0)];
					RvbColor n2 = BitmapCpc.RgbCPC[(totr > prm.cstR4 ? 6 : totr > prm.cstR2 ? 3 : 0)
												+ (totv > prm.cstV4 ? 18 : totv > prm.cstV2 ? 9 : 0)
												+ (totb > prm.cstB4 ? 2 : totb > prm.cstB2 ? 1 : 0)];
					source.SetPixel(xPix, yPix, (xPix & Tx) == 0 ? n1 : n2);
					source.SetPixel(xPix, yPix + 2, (xPix & Tx) == 0 ? n2 : n1);
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

		static private int ConvertTrameTcPlus(DirectBitmap source, ImageCpc dest, Param prm) {
			int pct = Dither.SetMatDither(prm);
			RvbColor choix, p = new RvbColor(0);
			int indexChoix = 0;
			for (int yPix = 0; yPix < BitmapCpc.TailleY; yPix += 4) {
				int Tx = BitmapCpc.CalcTx(yPix);
				for (int xPix = 0; xPix < BitmapCpc.TailleX; xPix += Tx) {
					for (int yy = 0; yy < 4; yy += 2) {
						p = GetPixel(source, xPix, yy + yPix, Tx, prm, pct);
						choix = new RvbColor((byte)((p.r >> 4) * 17), (byte)((p.v >> 4) * 17), (byte)((p.b >> 4) * 17));
						indexChoix = ((choix.v << 4) & 0xF00) + ((choix.b) & 0xF0) + ((choix.r) >> 4);
						coulTrouvee[indexChoix, (BitmapCpc.modeVirtuel == 5 || BitmapCpc.modeVirtuel == 6 ? yPix >> 1 : 0)]++;
						source.SetPixel(xPix, yy + yPix, prm.setPalCpc ? choix : p);
					}
					// Moyenne des 2 pixels
					RvbColor p0 = source.GetPixelColor(xPix, yPix);
					RvbColor p1 = source.GetPixelColor(xPix, yPix + 2);
					int r0 = (p0.r & 0xA0) | (p1.r & 0x50);
					int v0 = (p0.v & 0xA0) | (p1.v & 0x50);
					int b0 = (p0.b & 0xA0) | (p1.b & 0x50);
					int r1 = (p1.r & 0xA0) | (p0.r & 0x50);
					int v1 = (p1.v & 0xA0) | (p0.v & 0x50);
					int b1 = (p1.b & 0xA0) | (p0.b & 0x50);
					RvbColor n1 = new RvbColor((byte)((r0 >> 4) * 17), (byte)((v0 >> 4) * 17), (byte)((b0 >> 4) * 17));
					RvbColor n2 = new RvbColor((byte)((r1 >> 4) * 17), (byte)((v1 >> 4) * 17), (byte)((b1 >> 4) * 17));
					source.SetPixel(xPix, yPix, (xPix & Tx) == 0 ? n1 : n2);
					source.SetPixel(xPix, yPix + 2, (xPix & Tx) == 0 ? n2 : n1);
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
			int pct = Dither.SetMatDither(prm);
			RvbColor choix, p = new RvbColor(0);
			int indexChoix = 0;
			for (int yPix = 0; yPix < BitmapCpc.TailleY; yPix += 2) {
				int Tx = BitmapCpc.CalcTx(yPix);
				for (int xPix = 0; xPix < BitmapCpc.TailleX; xPix += Tx) {
					p = GetPixel(source, xPix, yPix, Tx, prm, pct);
					// Recherche le point dans la couleur cpc la plus proche
					if (prm.cpcPlus) {
						choix = new RvbColor((byte)((p.r >> 4) * 17), (byte)((p.v >> 4) * 17), (byte)((p.b >> 4) * 17));
						indexChoix = ((choix.v << 4) & 0xF00) + ((choix.b) & 0xF0) + ((choix.r) >> 4);
					}
					else {
						indexChoix = GetNumColorPixelCpc(prm, p);
						choix = BitmapCpc.RgbCPC[indexChoix];
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
		static void RechercheCMax1(int maxPen, int[] lockState, Param p) {
			int x, FindMax = BitmapCpc.cpcPlus ? 4096 : 27;
			for (x = 0; x < maxPen; x++)
				if (lockState[x] > 0)
					coulTrouvee[BitmapCpc.Palette[x], 0] = 0;

			for (x = 0; x < maxPen; x++) {
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
				for (x = 0; x < maxPen - 1; x++)
					for (int c = x + 1; c < maxPen; c++)
						if (lockState[x] == 0 && lockState[c] == 0 && BitmapCpc.Palette[x] > BitmapCpc.Palette[c]) {
							int tmp = BitmapCpc.Palette[x];
							BitmapCpc.Palette[x] = BitmapCpc.Palette[c];
							BitmapCpc.Palette[c] = tmp;
						}
		}

		//
		// Recherche les x meilleurs couleurs parmis les n possibles (remplit le tableau BitmapCpc.Palette)
		//
		static void RechercheCMax2(int maxPen, int[] lockState, Param p) {
			int firstCol, x, FindMax = BitmapCpc.cpcPlus ? 4096 : 27;
			for (x = 0; x < maxPen; x++)
				if (lockState[x] > 0)
					coulTrouvee[BitmapCpc.Palette[x], 0] = 0;

			// Recherche la couleur la plus utilisée
			for (firstCol = 0; firstCol < maxPen; firstCol++) {
				int valMax = 0;
				if (lockState[firstCol] == 0) {
					for (int i = 0; i < FindMax; i++) {
						if (valMax < coulTrouvee[i, 0]) {
							valMax = coulTrouvee[i, 0];
							BitmapCpc.Palette[firstCol] = i;
						}
					}
					for (int y = 0; y < 272; y++)
						coulTrouvee[BitmapCpc.Palette[firstCol], y] = 0;

					break; // Première couleur trouvée => sortie
				}
			}

			// Recherche les autres couleurs ayant la plus grande différence avec la première couleur trouvée
			RvbColor colFirst = BitmapCpc.cpcPlus ? new RvbColor((byte)((BitmapCpc.Palette[firstCol] & 0x0F) * 17), (byte)(((BitmapCpc.Palette[firstCol] & 0xF00) >> 8) * 17), (byte)(((BitmapCpc.Palette[firstCol] & 0xF0) >> 4) * 17)) : BitmapCpc.RgbCPC[BitmapCpc.Palette[firstCol]];
			bool takeDist = false;
			for (x = 0; x < maxPen; x++) {
				if (takeDist) {
					int valMax = 0;
					if (lockState[x] == 0) {
						for (int i = 0; i < FindMax; i++) {
							if (valMax < coulTrouvee[i, 0]) {
								valMax = coulTrouvee[i, 0];
								BitmapCpc.Palette[x] = i;
							}
						}
					}
				}
				else {
					if (lockState[x] == 0 && x != firstCol) {
						int dist, oldDist = 0;
						for (int i = 0; i < FindMax; i++) {
							RvbColor c = BitmapCpc.cpcPlus ? new RvbColor((byte)((i & 0x0F) * 17), (byte)(((i & 0xF00) >> 8) * 17), (byte)(((i & 0xF0) >> 4) * 17)) : BitmapCpc.RgbCPC[i];
							dist = (c.r - colFirst.r) * (c.r - colFirst.r) * p.coefR + (c.v - colFirst.v) * (c.v - colFirst.v) * p.coefV + (c.b - colFirst.b) * (c.b - colFirst.b) * p.coefB;
							if (dist > oldDist && coulTrouvee[i, 0] > 0) {
								oldDist = dist;
								BitmapCpc.Palette[x] = i;
							}
						}
						firstCol = x;
					}
				}
				for (int y = 0; y < 272; y++)
					coulTrouvee[BitmapCpc.Palette[x], y] = 0;

				takeDist = !takeDist;
			}

			if (p.sortPal)
				for (x = 0; x < maxPen - 1; x++)
					for (int c = x + 1; c < maxPen; c++)
						if (lockState[x] == 0 && lockState[c] == 0 && BitmapCpc.Palette[x] > BitmapCpc.Palette[c]) {
							int tmp = BitmapCpc.Palette[x];
							BitmapCpc.Palette[x] = BitmapCpc.Palette[c];
							BitmapCpc.Palette[c] = tmp;
						}
		}

		static void RechercheCMax(int maxPen, int[] lockState, Param p) {
			if (p.newReduc)
				RechercheCMax2(maxPen, lockState, p);
			else
				RechercheCMax1(maxPen, lockState, p);
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

		static private void SetPixTrameM1(DirectBitmap bitmap, Param prm, ImageCpc dest, int maxPen, RvbColor[,] tabCol) {
			for (int y = 0; y <= BitmapCpc.TailleY - 8; y += 8) {
				maxPen = BitmapCpc.MaxPen(y);
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

		static private void SetPixCol(DirectBitmap bitmap, Param prm, ImageCpc dest, int maxPen, RvbColor[,] tabCol) {
			int incY = BitmapCpc.modeVirtuel >= 8 ? 8 : 2;
			RvbColor pix;
			for (int y = 0; y < BitmapCpc.TailleY; y += incY) {
				int Tx = BitmapCpc.CalcTx(y);
				maxPen = BitmapCpc.MaxPen(y);
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
					for (int i = 0; i < maxPen; i++) {
						RvbColor c = tabCol[i, y >> 1];
						if (c != null) {
							int dist = (c.r - pix.r) * (c.r - pix.r) * prm.coefR + (c.v - pix.v) * (c.v - pix.v) * prm.coefV + (c.b - pix.b) * (c.b - pix.b) * prm.coefB;
							if (dist < oldDist) {
								choix = i;
								oldDist = dist;
								if (dist == 0)
									i = maxPen;
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
						int dist = (c.r - pix.r) * (c.r - pix.r) * prm.coefR + (c.v - pix.v) * (c.v - pix.v) * prm.coefV + (c.b - pix.b) * (c.b - pix.b) * prm.coefB;
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
		// Passe 2 : réduit l'image à MaxPen couleurs.
		//
		static private void Passe2(DirectBitmap source, ImageCpc dest, Param p, ref int colSplit) {
			RvbColor[,] tabCol = new RvbColor[16, 272];
			int[] MemoLockState = new int[16];
			int i;
			int Tx = BitmapCpc.CalcTx();
			int maxPen = BitmapCpc.MaxPen(2);
			for (i = 0; i < 16; i++)
				MemoLockState[i] = p.lockState[i];

			if (BitmapCpc.modeVirtuel == 3 || BitmapCpc.modeVirtuel == 4) {
				int newMax = BitmapCpc.MaxPen(0);
				RechercheCMax(newMax, MemoLockState, p);
				for (i = 0; i < newMax; i++)
					MemoLockState[i] = 1;
			}
			if (BitmapCpc.modeVirtuel == 5 || BitmapCpc.modeVirtuel == 6) {
				if (BitmapCpc.modeVirtuel == 5)
					colSplit = RechercheCMaxModeX(dest.colMode5, MemoLockState, BitmapCpc.TailleY, p);
				else {
					colSplit = RechercheCMaxModeSplit(dest.colMode5, MemoLockState, BitmapCpc.TailleY, p);
					maxPen = 9;
				}
				// réduit l'image à MaxPen couleurs.
				for (int y = 0; y < BitmapCpc.TailleY >> 1; y++)
					for (i = 0; i < maxPen; i++)
						tabCol[i, y] = p.cpcPlus ? new RvbColor((byte)((dest.colMode5[y, i] & 0x0F) * 17), (byte)(((dest.colMode5[y, i] & 0xF00) >> 8) * 17), (byte)(((dest.colMode5[y, i] & 0xF0) >> 4) * 17))
							: BitmapCpc.RgbCPC[dest.colMode5[y, i] < 27 ? dest.colMode5[y, i] : 0];
			}
			else {
				RechercheCMax(maxPen, MemoLockState, p);
				// réduit l'image à MaxPen couleurs.
				for (int y = 0; y < BitmapCpc.TailleY; y += 2) {
					maxPen = BitmapCpc.MaxPen(y);
					for (i = 0; i < maxPen; i++)
						tabCol[i, y >> 1] = dest.bitmapCpc.GetColorPal(i);
				}
			}
			if (BitmapCpc.modeVirtuel == 7)
				SetPixTrameM1(source, p, dest, maxPen, tabCol);
			else
				if (BitmapCpc.modeVirtuel == 6)
					SetPixColSplit(source, p, dest, tabCol);
				else
					SetPixCol(source, p, dest, maxPen, tabCol);
		}

		static public int Convert(DirectBitmap source, ImageCpc dest, Param p, bool noInfo = false) {
			System.Array.Clear(coulTrouvee, 0, coulTrouvee.GetLength(0) * coulTrouvee.GetLength(1));
			double c = p.pctContrast / 100.0;
			for (int i = 0; i < 256; i++)
				tblContrast[i] = MinMaxByte(((((i / 255.0) - 0.5) * c) + 0.5) * 255.0);

			int nbCol = p.trameTc ? p.cpcPlus ? ConvertTrameTcPlus(source, dest, p) : ConvertTrameTc(source, dest, p) : ConvertPasse1(source, dest, p);
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
								int dist = (c.r - pix.r) * (c.r - pix.r) * prm.coefR + (c.v - pix.v) * (c.v - pix.v) * prm.coefV + (c.b - pix.b) * (c.b - pix.b) * prm.coefB;
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
						if (t.IsSame(locTrame, 4)) {
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
