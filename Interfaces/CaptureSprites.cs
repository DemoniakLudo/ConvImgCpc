using System;
using System.Drawing;
using System.Windows.Forms;

namespace ConvImgCpc {
	public partial class ImageCpc : Form {
		private Capture fenetreCapture;
		private int captx = -1, capty = -1;

		private void CaptureSprites(MouseEventArgs e) {
			int sprSize = fenetreCapture.CaptSize << 5;
			Graphics g = Graphics.FromImage(pictureBox.Image);
			if (captx > -1 && capty > -1) {
				XorDrawing.DrawXorRectangle(g, (Bitmap)pictureBox.Image, captx, capty, captx + sprSize, capty + sprSize);
				if (e.Button == MouseButtons.Left)
					fenetreCapture.SetCapture(BmpLock, captx, capty);
			}
			captx = e.X & 0xFFE;
			capty = e.Y & 0xFFE;
			XorDrawing.DrawXorRectangle(g, (Bitmap)pictureBox.Image, captx, capty, captx + sprSize, capty + sprSize);
			pictureBox.Refresh();
		}

		private void pictureBox_MouseLeave(object sender, EventArgs e) {
			if (fenetreCapture != null) {
				int sprSize = fenetreCapture.CaptSize << 5;
				Graphics g = Graphics.FromImage(pictureBox.Image);
				if (captx > -1 && capty > -1)
					XorDrawing.DrawXorRectangle(g, (Bitmap)pictureBox.Image, captx, capty, captx + sprSize, capty + sprSize);

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
				fenetreCapture = new Capture();
				fenetreCapture.Show();
			}
			else
				CloseCapture();
		}
	}
}