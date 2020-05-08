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
		public int Length { get { return Width * Height; } }

		protected GCHandle BitsHandle { get; private set; }

		public DirectBitmap(int width, int height) {
			Width = width;
			Height = height;
			Bits = new Int32[width * height];
			BitsHandle = GCHandle.Alloc(Bits, GCHandleType.Pinned);
			Bitmap = new Bitmap(width, height, width * 4, PixelFormat.Format32bppRgb, BitsHandle.AddrOfPinnedObject());
		}

		public void SetPixel(int x, int y, int c) {
			Bits[x + (y * Width)] = c;
		}

		public void SetPixel(int x, int y, RvbColor color) {
			Bits[x + (y * Width)] = color.GetColorArgb;
		}

		public int GetPixel(int x, int y) {
			return Bits[x + (y * Width)];
		}

		public RvbColor GetPixelColor(int x, int y) {
			return new RvbColor(Bits[x + (y * Width)]);
		}

		public void SetHorLineDouble(int pixelX, int pixelY, int lineLength, int color) {
			int index = pixelX + (pixelY * Width);
			for (; lineLength-- > 0; ) {
				Bits[index] = color;
				if (index + Width < Length)
					Bits[index + Width] = color;

				index++;
			}
		}

		public void Dispose() {
			if (!Disposed) {
				Disposed = true;
				Bitmap.Dispose();
				BitsHandle.Free();
			}
		}
	}
}
