using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace ConvImgCpc {
	public class DirectBitmap : IDisposable {
		public Bitmap Bitmap { get; private set; }
		public uint[] tabBits { get; private set; }
		public bool Disposed { get; private set; }
		public int Height { get; private set; }
		public int Width { get; private set; }
		public int Length { get { return Width * Height; } }
		public int Tps { get; set; }

		protected GCHandle BitsHandle { get; private set; }

		public DirectBitmap(int width, int height) {
			CreateBitmap(width, height);
		}

		public DirectBitmap(string fileName) {
			CreateBitmap(fileName);
		}

		public void CopyBits(DirectBitmap source) {
			Array.Copy(source.tabBits, tabBits, tabBits.Length);
		}

		private void CreateBitmap(string fileName) {
			Bitmap tmp = new Bitmap(fileName);
			Width = tmp.Width;
			Height = tmp.Height;
			tabBits = new uint[tmp.Width * tmp.Height];
			BitsHandle = GCHandle.Alloc(tabBits, GCHandleType.Pinned);
			for (int x = 0; x < tmp.Width; x++)
				for (int y = 0; y < tmp.Height; y++) {
					Color c = tmp.GetPixel(x, y);
					SetPixel(x, y, c.ToArgb());
				}
			Bitmap = new Bitmap(tmp.Width, tmp.Height);
			tmp.Dispose();
		}

		private void CreateBitmap(int width, int height) {
			Width = width;
			Height = height;
			tabBits = new uint[width * height];
			BitsHandle = GCHandle.Alloc(tabBits, GCHandleType.Pinned);
			Bitmap = new Bitmap(width, height, width * 4, PixelFormat.Format32bppRgb, BitsHandle.AddrOfPinnedObject());
		}

		public void SetPixel(int x, int y, int c) {
			if (y < Height)
				tabBits[x + (y * Width)] = (uint)c | 0xFF000000;
		}

		public void SetPixel(int x, int y, RvbColor color) {
			if (y < Height)
				tabBits[x + (y * Width)] = (uint)color.GetColorArgb | 0xFF000000;
		}

		public int GetPixel(int x, int y) {
			return x < Width && y < Height ? (int)(tabBits[y < Height ? (x + (y * Width)) : 0] & 0xFFFFFF) : 0;
		}

		public RvbColor GetPixelColor(int x, int y) {
			return new RvbColor((int)tabBits[y < Height ? (x + (y * Width)) : 0]);
		}

		public void SetHorLineDouble(int pixelX, int pixelY, int lineLength, int c) {
			uint color = (uint)c | 0xFF000000;
			int index = pixelX + (pixelY * Width);
			for (; lineLength-- > 0;) {
				if (index < Length) {
					tabBits[index] = color;
					if (index + Width < Length)
						tabBits[index + Width] = color;
				}
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
