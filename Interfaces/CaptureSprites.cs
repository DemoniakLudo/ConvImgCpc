﻿using System;
using System.Drawing;
using System.Windows.Forms;

namespace ConvImgCpc {
	public partial class ImageCpc : Form {
		private Capture fenetreCapture;
		private int captx = -1, capty = -1;

		private void CaptureSprites(MouseEventArgs e) {
			int sprSizeX = fenetreCapture.CaptSizeX << 5;
			int sprSizeY = fenetreCapture.CaptSizeY << 5;
			Graphics g = Graphics.FromImage(pictureBox.Image);
			if (captx > -1 && capty > -1) {
				XorDrawing.DrawXorRectangle(g, (Bitmap)pictureBox.Image, captx, capty, captx + sprSizeX, capty + sprSizeY);
				if (e.Button == MouseButtons.Left)
					fenetreCapture.SetCapture(BmpLock, captx, capty);
			}
			captx = e.X / (chkX2.Checked ? 2 : 1) & 0xFFE;
			capty = e.Y / (chkX2.Checked ? 2 : 1) & 0xFFE;
			XorDrawing.DrawXorRectangle(g, (Bitmap)pictureBox.Image, captx, capty, captx + sprSizeX, capty + sprSizeY);
			pictureBox.Refresh();
		}

		private void pictureBox_MouseLeave(object sender, EventArgs e) {
			if (fenetreCapture != null) {
				int sprSizeX = fenetreCapture.CaptSizeX << 5;
				int sprSizeY = fenetreCapture.CaptSizeY << 5;
				Graphics g = Graphics.FromImage(pictureBox.Image);
				if (captx > -1 && capty > -1)
					XorDrawing.DrawXorRectangle(g, (Bitmap)pictureBox.Image, captx, capty, captx + sprSizeX, capty + sprSizeY);

				pictureBox.Refresh();
				captx = -1;
				capty = -1;
			}
		}

		private void CloseCapture() {
			if (fenetreCapture != null) {
				fenetreCapture.Hide();
				fenetreCapture.Close();
				fenetreCapture.Dispose();
				fenetreCapture = null;
			}
		}

		private void modeCaptureSprites_CheckedChanged(object sender, EventArgs e) {
			if (modeCaptureSprites.Checked) {
				main.Enabled = false;
				fenetreCapture = new Capture(main);
				fenetreCapture.Show();
			}
			else {
				CloseCapture();
				main.Enabled = true;
			}
		}

		private void chkGrilleSprite_CheckedChanged(object sender, EventArgs e) {
			main.Enabled = !chkGrilleSprite.Checked;
			Render(true);
			if ( !chkGrilleSprite.Checked)
				Render(true, true);
			
		}
	}
}