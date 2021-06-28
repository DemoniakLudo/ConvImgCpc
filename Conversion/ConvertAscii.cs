namespace ConvImgCpc {
	public static partial class Conversion {
		// Conversion ASC0 à ASC2
		static private void ConvertAscii(DirectBitmap source, Param prm, ImageCpc dest, int maxPen, RvbColor[,] tabCol) {
			int incY = 8;
			RvbColor pix;
			for (int y = 0; y < BitmapBase.TailleY; y += incY) {
				int Tx = BitmapBase.CalcTx(y);
				maxPen = BitmapBase.MaxPen(y);
				for (int x = 0; x < BitmapBase.TailleX; x += Tx) {
					int oldDist = 0x7FFFFFFF;
					int r = 0, v = 0, b = 0;
					for (int j = 0; j < incY; j += 2) {
						pix = source.GetPixelColor(x, y + j);
						r += pix.r;
						v += pix.v;
						b += pix.b;
					}
					pix = new RvbColor((byte)(r >> 2), (byte)(v >> 2), (byte)(b >> 2));
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
					int offsetY = prm.modeImpDraw && BitmapBase.TailleY == 544 ? 2 : 0;
					for (int j = 0; j < incY; j += 2)
						dest.SetPixelCpc(x, y + offsetY + j, choix, Tx);
				}
			}
		}
	}
}
