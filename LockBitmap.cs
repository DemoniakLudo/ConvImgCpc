using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace ConvImgCpc {
	public class LockBitmap {
		private Bitmap source = null;
		private IntPtr Iptr = IntPtr.Zero;
		BitmapData bitmapData = null;

		public Bitmap Source { get { return source; } }
		public byte[] Pixels { get; set; }
		public int Depth { get; private set; }
		public int Width { get; private set; }
		public int Height { get; private set; }

		public LockBitmap(Bitmap source) {
			this.source = source;
			Width = source.Width;
			Height = source.Height;
			Depth = Bitmap.GetPixelFormatSize(source.PixelFormat);
			Pixels = new byte[Width * Height * (Depth >> 3)];
		}

		public void LockBits() {
			bitmapData = source.LockBits(new Rectangle(0, 0, Width, Height), ImageLockMode.ReadWrite, PixelFormat.Format32bppRgb);
			Iptr = bitmapData.Scan0;
			Marshal.Copy(Iptr, Pixels, 0, Pixels.Length);
		}

		public void UnlockBits() {
			Marshal.Copy(Pixels, 0, Iptr, Pixels.Length);
			source.UnlockBits(bitmapData);
		}

		public int GetPixel(int pixelX, int pixelY) {
			int adr = ((pixelY * Width) + pixelX) << 2;
			return (Pixels[adr] + (Pixels[adr + 1] << 8) + (Pixels[adr + 2] << 16) + (int)(Pixels[adr + 3] << 24));
		}

		public RvbColor GetPixelColor(int pixelX, int pixelY) {
			int adr = ((pixelY * Width) + pixelX) << 2;
			return new RvbColor(Pixels[adr] + (Pixels[adr + 1] << 8) + (Pixels[adr + 2] << 16));
		}

		public void SetPixel(int pixelX, int pixelY, int color) {
			int adr = ((pixelY * Width) + pixelX) << 2;
			Pixels[adr++] = (byte)(color);
			Pixels[adr++] = (byte)(color >> 8);
			Pixels[adr++] = (byte)(color >> 16);
			Pixels[adr] = 0xFF;
		}

		public void SetHorLine(int pixelX, int pixelY, int lineLength, int color) {
			int adr = ((pixelY * Width) + pixelX) << 2;
			byte r = (byte)color;
			byte v = (byte)(color >> 8);
			byte b = (byte)(color >> 16);
			for (; lineLength-- > 0; ) {
				Pixels[adr++] = r;
				Pixels[adr++] = v;
				Pixels[adr++] = b;
				Pixels[adr++] = 0xFF;
			}
		}

		public void SetHorLine2Y(int pixelX, int pixelY, int lineLength, int color) {
			int adr1 = ((pixelY * Width) + pixelX) << 2;
			int adr2 = adr1 + (Width << 2);
			byte r = (byte)color;
			byte v = (byte)(color >> 8);
			byte b = (byte)(color >> 16);
			for (; lineLength-- > 0; ) {
				Pixels[adr1++] = Pixels[adr2++] = r;
				Pixels[adr1++] = Pixels[adr2++] = v;
				Pixels[adr1++] = Pixels[adr2++] = b;
				Pixels[adr1++] = Pixels[adr2++] = 0xFF;
			}
		}

		public void SetPixel(int pixelX, int pixelY, RvbColor color) {
			int adr = ((pixelY * Width) + pixelX) << 2;
			Pixels[adr++] = color.r;
			Pixels[adr++] = color.v;
			Pixels[adr++] = color.b;
			Pixels[adr] = 0xFF;
		}
	}
}