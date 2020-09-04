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

		protected GCHandle BitsHandle { get; private set; }

		public DirectBitmap(int width, int height) {
			CreateBitmap(width, height);
		}

		public DirectBitmap(DirectBitmap source) {
			CreateBitmap(source.Width, source.Height);
			CopyBits(source);
		}

		public void CopyBits(DirectBitmap source) {
			Array.Copy(source.tabBits, tabBits, tabBits.Length);
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
			return (int)(tabBits[y < Height ? (x + (y * Width)) : 0] & 0xFFFFFF);
		}

		public RvbColor GetPixelColor(int x, int y) {
			return new RvbColor((int)tabBits[y < Height ? (x + (y * Width)) : 0]);
		}

		public void SetHorLineDouble(int pixelX, int pixelY, int lineLength, int c) {
			uint color = (uint)c | 0xFF000000;
			int index = pixelX + (pixelY * Width);
			for (; lineLength-- > 0; ) {
				tabBits[index] = color;
				if (index + Width < Length)
					tabBits[index + Width] = color;

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
