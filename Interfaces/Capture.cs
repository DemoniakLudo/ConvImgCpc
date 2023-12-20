using System;
using System.Windows.Forms;

namespace ConvImgCpc {
	public partial class Capture : Form {
		private int captSizeX = 1, captSizeY = 1;
		public int CaptSizeX { get { return captSizeX; } }
		public int CaptSizeY { get { return captSizeY; } }
		private DirectBitmap bmp = new DirectBitmap(2048, 2048);
		private Main main;
		private ImageCpc imgCpc;

		public Capture(Main m, ImageCpc i) {
			InitializeComponent();
			pictCapture.Image = bmp.Bitmap;
			RazCapture();
			comboBanque.SelectedIndex = 0;
			m.ChangeLanguage(Controls, "Capture");
			main = m;
			imgCpc = i;
		}

		private void RazCapture() {
			for (int y = 0; y < 512; y += 2)
				bmp.SetHorLineDouble(0, y, 512, 0);

			pictCapture.Refresh();
		}

		public void SetCapture(DirectBitmap src, int posx, int posy) {
			main.imgCpc.CaptureSprite(captSizeX, captSizeY, posx, posy, bmp);
			pictCapture.Refresh();
		}

		private void GetCaptSizeX() {
			int v = 0;
			int.TryParse(txbNbX.Text, out v);
			int numBank = comboBanque.SelectedIndex;
			if (v > 0 && v <= 16 && v * captSizeY <= 16 * (7 - numBank)) {
				captSizeX = v;
				numSprite.Maximum = (16 * (7 - numBank) - CaptSizeX * captSizeY) % 16;
				RazCapture();
			}
		}

		private void GetCaptSizeY() {
			int v = 0;
			int.TryParse(txbNbY.Text, out v);
			int numBank = comboBanque.SelectedIndex;
			if (v > 0 && v <= 16 && v * captSizeX <= 16 * (7 - numBank)) {
				captSizeY = v;
				numSprite.Maximum = (16 * (7 - numBank) - CaptSizeX * captSizeY) % 16;
				RazCapture();
			}
		}

		private void rbCapt1_CheckedChanged(object sender, EventArgs e) {
			lblNbX.Visible = lblNbY.Visible = txbNbX.Visible = txbNbY.Visible = false;
			captSizeX = captSizeY = 1;
			numSprite.Maximum = 15;
			RazCapture();
		}

		private void rbCapt2_CheckedChanged(object sender, EventArgs e) {
			lblNbX.Visible = lblNbY.Visible = txbNbX.Visible = txbNbY.Visible = false;
			captSizeX = captSizeY = 2;
			numSprite.Maximum = 12;
			RazCapture();
		}

		private void rbCapt4_CheckedChanged(object sender, EventArgs e) {
			lblNbX.Visible = lblNbY.Visible = txbNbX.Visible = txbNbY.Visible = false;
			captSizeX = captSizeY = 4;
			numSprite.Maximum = 0;
			RazCapture();
		}

		private void rbCaptUser_CheckedChanged(object sender, EventArgs e) {
			lblNbX.Visible = lblNbY.Visible = txbNbX.Visible = txbNbY.Visible = true;
			GetCaptSizeX();
			GetCaptSizeY();
		}

		private void txbNbX_TextChanged(object sender, EventArgs e) {
			GetCaptSizeX();
		}

		private void txbNbY_TextChanged(object sender, EventArgs e) {
			GetCaptSizeY();
		}

		private void bpCapture_Click(object sender, EventArgs e) {
			int numSpr = (int)numSprite.Value;
			int numBank = comboBanque.SelectedIndex;
			for (int spry = 0; spry < captSizeY; spry++)
				for (int sprx = 0; sprx < captSizeX; sprx++) {
					for (int y = 0; y < 16; y++)
						for (int x = 0; x < 16; x++)
							Cpc.spritesHard[numBank, numSpr, x, y] = (byte)Cpc.GetPenColor(bmp, (x << 3) + (sprx << 7), (y << 3) + (spry << 7));

					numSpr++;
					if (numSpr > 15) {
						numSpr = 0;
						numBank++;
					}
				}
			// Copie de la palette image dans la palette des sprites
			for (int c = 0; c < 16; c++)
				Cpc.paletteSprite[c] = Cpc.Palette[c];

			numSprite.Value = numSpr;
			comboBanque.SelectedIndex = numBank;
			MessageBox.Show("Capture Ok.");
		}

		private void Capture_FormClosed(object sender, FormClosedEventArgs e) {
			imgCpc.modeCaptureSprites.Checked = false;
		}
	}
}
