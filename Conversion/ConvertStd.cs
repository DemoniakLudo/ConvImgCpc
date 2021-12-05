namespace ConvImgCpc {
	public static partial class Conversion {
		// Conversion "standard"
		static private void ConvertStd(DirectBitmap source, Param prm, ImageCpc dest, int maxPen, RvbColor[,] tabCol) {
			int offsetY = prm.modeImpDraw && Cpc.TailleY == 544 ? 2 : 0;
			for (int y = 0; y < Cpc.TailleY; y += 2) {
				int Tx = Cpc.CalcTx(y);
				maxPen = Cpc.MaxPen(y);
				for (int x = 0; x < Cpc.TailleX; x += Tx) {
					int oldDist = 0x7FFFFFFF;
					RvbColor pix = source.GetPixelColor(x, y);
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
					dest.SetPixelCpc(x, y + offsetY, choix, Tx);
				}
			}
		}
	}
}
