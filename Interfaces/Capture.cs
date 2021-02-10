using System;
using System.Windows.Forms;

namespace ConvImgCpc {
	public partial class Capture : Form {
		private int captSize = 1;
		public int CaptSize { get { return captSize; } }
		private DirectBitmap bmp = new DirectBitmap(512, 512);

		public Capture(Main m) {
			InitializeComponent();
			pictCapture.Image = bmp.Bitmap;
			RazCapture();
			comboBanque.SelectedIndex = 0;
			m.ChangeLanguage(Controls, "Capture");
		}

		private void RazCapture() {
			for (int y = 0; y < 512; y += 2)
				bmp.SetHorLineDouble(0, y, 512, 0);

			pictCapture.Refresh();
		}

		public void SetCapture(DirectBitmap src, int posx, int posy) {
			int sprSize = CaptSize << 5;
			for (int x = 0; x < sprSize; x += 2)
				for (int y = 0; y < sprSize; y += 2) {
					int c = src.GetPixel(posx + x, posy + y);
					for (int zx = 0; zx < 8; zx++)
						for (int zy = 0; zy < 8; zy++)
							bmp.SetPixel(zx + x * 4, zy + y * 4, c);
				}
			pictCapture.Refresh();
		}

		private void rbCapt1_CheckedChanged(object sender, EventArgs e) {
			captSize = 1;
			numSprite.Maximum = 15;
			RazCapture();
		}

		private void rbCapt2_CheckedChanged(object sender, EventArgs e) {
			captSize = 2;
			numSprite.Maximum = 12;
			RazCapture();
		}

		private void rbCapt4_CheckedChanged(object sender, EventArgs e) {
			captSize = 4;
			numSprite.Maximum = 0;
			RazCapture();
		}

		private void bpCapture_Click(object sender, EventArgs e) {
			int numSpr = (int)numSprite.Value;
			for (int spry = 0; spry < captSize; spry++)
				for (int sprx = 0; sprx < captSize; sprx++) {
					for (int y = 0; y < 16; y++)
						for (int x = 0; x < 16; x++) {
							int pen = 0;
							RvbColor col = bmp.GetPixelColor((x << 3) + (sprx << 7), (y << 3) + (spry << 7));
							for (pen = 0; pen < 16; pen++) {
								if ((col.v >> 4) == (BitmapCpc.Palette[pen] >> 8) && (col.b >> 4) == ((BitmapCpc.Palette[pen] >> 4) & 0x0F) && (col.r >> 4) == (BitmapCpc.Palette[pen] & 0x0F))
									break;
							}
							BitmapCpc.spritesHard[comboBanque.SelectedIndex, numSpr, x, y] = (byte)(pen & 0x0F);
						}
					numSpr++;
				}
			// Copie de la palette image dans la palette des sprites
			for (int c = 0; c < 16; c++)
				BitmapCpc.paletteSprite[c] = BitmapCpc.Palette[c];

			MessageBox.Show("Capture Ok.");
		}
	}
}
