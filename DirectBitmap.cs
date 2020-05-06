using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace ConvImgCpc {
	public class DirectBitmap : IDisposable {
		public Bitmap Bitmap { get; private set; }
		public Int32[] Bits { get; private set; }
		public bool Disposed { get; private set; }
		public int Height { get; private set; }
		public int Width { get; private set; }

		protected GCHandle BitsHandle { get; private set; }

		public DirectBitmap(int width, int height) {
			Width = width;
			Height = height;
			Bits = new Int32[width * height];
			BitsHandle = GCHandle.Alloc(Bits, GCHandleType.Pinned);
			Bitmap = new Bitmap(width, height, width * 4, PixelFormat.Format32bppPArgb, BitsHandle.AddrOfPinnedObject());
		}

		public void SetPixel(int x, int y, int c) {
			int index = x + (y * Width);
			Bits[index] = c;
		}

		public int GetPixel(int x, int y) {
			int index = x + (y * Width);
			return Bits[index];
		}

		public RvbColor GetPixelColor(int x, int y) {
			return new RvbColor(GetPixel(x, y));
		}

		public void SetHorLine(int pixelX, int pixelY, int lineLength, int color) {
			for (; lineLength-- > 0; ) 
				SetPixel(pixelX, pixelY, color);
		}

		public void SetHorLine2Y(int pixelX, int pixelY, int lineLength, int color) {
			for (; lineLength-- > 0; ) {
				SetPixel(pixelX, pixelY, color);
				SetPixel(pixelX, pixelY+1, color);
			}
		}

		public void SetPixel(int x, int y, RvbColor color) {
			SetPixel(x, y, color.GetColorArgb);
		}

		public void Dispose() {
			if (Disposed) return;
			Disposed = true;
			Bitmap.Dispose();
			BitsHandle.Free();
		}
	}
}
