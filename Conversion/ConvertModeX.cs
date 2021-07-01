namespace ConvImgCpc {
	public static partial class Conversion {
		//
		// Recherche les couleurs pour le mode "X"
		//
		static int RechercheCMaxModeX(int[,] colMode5, int[] lockState, int yMax, Param prm) {
			int c, FindMax = Cpc.cpcPlus ? 4096 : 27;

			// Les deux premières couleurs sont "fixes"
			for (c = 0; c < 2; c++) {
				if (lockState[c] > 0) {
					for (int y = 0; y < 272; y++) {
						coulTrouvee[Cpc.Palette[c], y] = 0;
						colMode5[y, c] = Cpc.Palette[c];
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
							Cpc.Palette[c] = i;
						}
					}
					for (int y = 0; y < 272; y++) {
						coulTrouvee[Cpc.Palette[c], y] = 0;
						colMode5[y, c] = Cpc.Palette[c];
					}
				}
			}

			// Recherche les couleurs par ligne
			for (c = 2; c < 4; c++) {
				for (int y = 0; y < yMax >> 1; y++) {
					int valMax = 0;
					for (int i = 0; i < FindMax; i++) {
						for (int deltay = -(prm.trackModeX << 1); deltay <= (prm.trackModeX << 1); deltay++) {
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

		// Conversion "Mode X"
		static private void ConvertModeX(DirectBitmap source, Param prm, ImageCpc dest, RvbColor[,] tabCol) {
			RvbColor pix;
			for (int y = 0; y < Cpc.TailleY; y += 2) {
				for (int x = 0; x < Cpc.TailleX; x += 2) {
					int oldDist = 0x7FFFFFFF;
					RvbColor p = source.GetPixelColor(x, y);
					pix = new RvbColor(p.r, p.v, p.b);
					int choix = 0;
					for (int i = 0; i < 4; i++) {
						RvbColor c = tabCol[i, y >> 1];
						if (c != null) {
							int dist = (c.r - pix.r) * (c.r - pix.r) * prm.coefR + (c.v - pix.v) * (c.v - pix.v) * prm.coefV + (c.b - pix.b) * (c.b - pix.b) * prm.coefB;
							if (dist < oldDist) {
								choix = i;
								oldDist = dist;
								if (dist == 0)
									i = 4;
							}
						}
					}
					int offsetY = prm.modeImpDraw && Cpc.TailleY == 544 ? 2 : 0;
					dest.SetPixelCpc(x, y + offsetY, choix, 2);
				}
			}
		}
	}
}
