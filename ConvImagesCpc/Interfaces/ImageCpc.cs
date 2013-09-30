using ConvImgCpc;
using System.Drawing;
using System.Windows.Forms;

namespace CpcConvImg {
	public partial class ImageCpc: Form {
		private Bitmap bmp;
		private LockBitmap bmpLock;
		public BitmapCpc bitmapCpc;
		private Label[] colors = new Label[16];
		private CheckBox[] lockColors = new CheckBox[16];
		public int[] lockState = new int[16];
		private int offsetX = 0, offsetY = 0;
		private int zoom;
		private int numCol = 0;
		private int penWidth = 1;
		private EditImage editImage;

		public ImageCpc() {
			InitializeComponent();
			int tx = pictureBox.Width;
			int ty = pictureBox.Height;
			bmp = new Bitmap(tx, ty);
			pictureBox.Image = bmp;
			bmpLock = new LockBitmap(bmp);
			bitmapCpc = new BitmapCpc(640, 400, 1); // ###
			for (int i = 0; i < 16; i++) {
				// Générer les contrôles de "couleurs"
				colors[i] = new Label();
				colors[i].BorderStyle = BorderStyle.FixedSingle;
				colors[i].Location = new Point(4 + i * 48, 564);
				colors[i].Size = new Size(40, 32);
				colors[i].Tag = i;
				colors[i].Click += ClickColor;
				Controls.Add(colors[i]);
				// Générer les contrôles de "bloquage couleur"
				lockColors[i] = new CheckBox();
				lockColors[i].Location = new Point(16 + i * 48, 598);
				lockColors[i].Size = new Size(20, 20);
				lockColors[i].Tag = i;
				lockColors[i].Click += ClickLock;
				Controls.Add(lockColors[i]);
				lockColors[i].Update();
			}
			Reset();
			ChangeZoom(1);
		}

		// Click sur un "lock"
		void ClickLock(object sender, System.EventArgs e) {
			CheckBox colorLock = sender as CheckBox;
			int numLock = colorLock.Tag != null ? (int)colorLock.Tag : 0;
			lockState[numLock] = colorLock.Checked ? 1 : 0;
		}

		// Changement de la palette
		void ClickColor(object sender, System.EventArgs e) {
			Label colorClick = sender as Label;
			numCol = colorClick.Tag != null ? (int)colorClick.Tag : 0;
			if (!modeEdition.Checked) {
				EditColor ed = new EditColor(numCol, bitmapCpc.Palette[numCol], bitmapCpc.GetPaletteColor(numCol).GetColorArgb, bitmapCpc.cpcPlus);
				ed.ShowDialog(this);
				if (ed.isValide) {
					bitmapCpc.SetPalette(numCol, ed.ValColor);
					UpdatePalette();
				}
			}
			else {
				if (editImage != null)
					editImage.SetPenColor(bitmapCpc.GetPaletteColor(numCol));
			}
		}

		public void Reset() {
			int col = System.Drawing.SystemColors.Control.ToArgb();
			bmpLock.LockBits();
			for (int x = 0; x < bmpLock.Width; x++)
				for (int y = 0; y < bmpLock.Height; y++)
					bmpLock.SetPixel(x, y, col);
			bmpLock.UnlockBits();
			vScrollBar.Height = bitmapCpc.TailleY;
			vScrollBar.Left = bitmapCpc.TailleX + 3;
			hScrollBar.Width = bitmapCpc.TailleX;
			hScrollBar.Top = bitmapCpc.TailleY + 3;
		}

		public void SetPixelCpc(int pixelX, int pixelY, int pix) {
			bitmapCpc.SetPixelCpc(pixelX, pixelY, pix);
		}

		public void UpdatePalette() {
			for (int i = 0; i < 16; i++) {
				colors[i].BackColor = System.Drawing.Color.FromArgb(bitmapCpc.GetPaletteColor(i).GetColorArgb);
				colors[i].Refresh();
			}
		}

		public void SetPalette(int entree, int valeur) {
			bitmapCpc.SetPalette(entree, valeur);
			UpdatePalette();
		}

		public void Render() {
			bitmapCpc.Render(bmpLock, bitmapCpc.ModeCPC, zoom, offsetX, offsetY, false);
			pictureBox.Refresh();
			UpdatePalette();
		}

		private void lockAllPal_CheckedChanged(object sender, System.EventArgs e) {
			for (int i = 0; i < 16; i++) {
				lockColors[i].Checked = lockAllPal.Checked;
				lockState[i] = lockAllPal.Checked ? 1 : 0;
			}
		}

		/* Mode édition */
		private void modeEdition_CheckedChanged(object sender, System.EventArgs e) {
			if (modeEdition.Checked) {
				if (editImage == null)
					editImage = new EditImage();

				editImage.Show();
				editImage.evtChangeZoom += ChangeZoom;
				editImage.evtChangePen += ChangePen;
				editImage.evtClose += CloseEditImage;
				editImage.SetPenColor(bitmapCpc.GetPaletteColor(numCol));
			}
			else {
				if (editImage != null) {
					editImage.Hide();
					editImage.evtChangeZoom -= ChangeZoom;
					editImage.evtChangePen -= ChangePen;
					editImage.evtClose -= CloseEditImage;
					editImage = null;
					ChangeZoom(1);
				}
			}
		}

		private void ChangeZoom(int p) {
			zoom = p;
			vScrollBar.Enabled = hScrollBar.Enabled = zoom > 1;
			hScrollBar.Maximum = hScrollBar.LargeChange + bitmapCpc.TailleX - (bitmapCpc.TailleX / zoom);
			vScrollBar.Maximum = vScrollBar.LargeChange + bitmapCpc.TailleY - (bitmapCpc.TailleY / zoom);
			hScrollBar.Value = offsetX = (bitmapCpc.TailleX - bitmapCpc.TailleX / zoom) / 2;
			vScrollBar.Value = offsetY = (bitmapCpc.TailleY - bitmapCpc.TailleY / zoom) / 2;
			Render();
		}

		private void ChangePen(int p) {
			penWidth = p;
		}

		private void CloseEditImage() {
			modeEdition.Checked = false;
		}

		private void DrawPen(MouseEventArgs e) {
			if (modeEdition.Checked) {
				int Tx = (4 >> (bitmapCpc.ModeCPC == 3 ? 1 : bitmapCpc.ModeCPC));
				for (int y = 0; y < penWidth * 2; y += 2)
					for (int x = 0; x < penWidth * Tx; x += Tx) {
						int xReel = x + offsetX + (e.X / zoom) - ((penWidth * Tx) >> 1) + 1;
						int yReel = y + offsetY + (e.Y / zoom) - penWidth + 1;
						if (xReel >= 0 && yReel > 0 && xReel < bitmapCpc.TailleX && yReel < bitmapCpc.TailleY)
							bitmapCpc.SetPixelCpc(xReel, yReel, numCol);
					}
				Render();
			}
		}

		private void pictureBox_MouseDown(object sender, MouseEventArgs e) {
			if (modeEdition.Checked)
				DrawPen(e);
		}

		private void pictureBox_MouseMove(object sender, MouseEventArgs e) {
			if (e.Button == MouseButtons.Left && modeEdition.Checked)
				DrawPen(e);
		}

		private void vScrollBar_Scroll(object sender, ScrollEventArgs e) {
			offsetY = (vScrollBar.Value >> 1) << 1;
			Render();
		}

		private void hScrollBar_Scroll(object sender, ScrollEventArgs e) {
			offsetX = (hScrollBar.Value >> 3) << 3;
			Render();
		}
	}
}
