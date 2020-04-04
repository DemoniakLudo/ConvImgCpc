using System.Drawing;
using System.Windows.Forms;

namespace ConvImgCpc {
	public partial class ImageCpc : Form {
		private LockBitmap bmpLock;
		private Label[] colors = new Label[16];
		private CheckBox[] lockColors = new CheckBox[16];
		public int[] lockState = new int[16];
		private int offsetX = 0, offsetY = 0;
		private int zoom;
		private int numCol = 0;
		private int penWidth = 1;
		private EditImage editImage;

		public delegate void ConvertDelegate(bool doConvertbook);

		private ConvertDelegate Convert;

		public int[] Palette = new int[17];
		static private int[] paletteStandardCPC = { 1, 24, 20, 6, 26, 0, 2, 7, 10, 12, 14, 16, 18, 22, 1, 14 };
		static private int[] tabMasqueMode0 = { 0xAA, 0x55 };
		static private int[] tabMasqueMode1 = { 0x88, 0x44, 0x22, 0x11 };
		static private int[] tabMasqueMode2 = { 0x80, 0x40, 0x20, 0x10, 0x08, 0x04, 0x02, 0x01 };
		static private int[] tabOctetMode01 = { 0x00, 0x80, 0x08, 0x88, 0x20, 0xA0, 0x28, 0xA8, 0x02, 0x82, 0x0A, 0x8A, 0x22, 0xA2, 0x2A, 0xAA };
		const int maxColsCpc = 96;
		const int maxLignesCpc = 272;

		public const int Lum0 = 0x00;
		public const int Lum1 = 0x66;
		public const int Lum2 = 0xFF;

		static public RvbColor[] RgbCPC = new RvbColor[27] {
							new RvbColor( Lum0, Lum0, Lum0),
							new RvbColor( Lum1, Lum0, Lum0),
							new RvbColor( Lum2, Lum0, Lum0),
							new RvbColor( Lum0, Lum0, Lum1),
							new RvbColor( Lum1, Lum0, Lum1),
							new RvbColor( Lum2, Lum0, Lum1),
							new RvbColor( Lum0, Lum0, Lum2),
							new RvbColor( Lum1, Lum0, Lum2),
							new RvbColor( Lum2, Lum0, Lum2),
							new RvbColor( Lum0, Lum1, Lum0),
							new RvbColor( Lum1, Lum1, Lum0),
							new RvbColor( Lum2, Lum1, Lum0),
							new RvbColor( Lum0, Lum1, Lum1),
							new RvbColor( Lum1, Lum1, Lum1),
							new RvbColor( Lum2, Lum1, Lum1),
							new RvbColor( Lum0, Lum1, Lum2),
							new RvbColor( Lum1, Lum1, Lum2),
							new RvbColor( Lum2, Lum1, Lum2),
							new RvbColor( Lum0, Lum2, Lum0),
							new RvbColor( Lum1, Lum2, Lum0),
							new RvbColor( Lum2, Lum2, Lum0),
							new RvbColor( Lum0, Lum2, Lum1),
							new RvbColor( Lum1, Lum2, Lum1),
							new RvbColor( Lum2, Lum2, Lum1),
							new RvbColor( Lum0, Lum2, Lum2),
							new RvbColor( Lum1, Lum2, Lum2),
							new RvbColor( Lum2, Lum2, Lum2)
							};

		private int nbCol = 80;
		public int NbCol { get { return nbCol; } }
		public int TailleX {
			get { return nbCol << 3; }
			set { nbCol = value >> 3; }
		}
		private int nbLig = 200;
		public int NbLig { get { return nbLig; } }
		public int TailleY {
			get { return nbLig << 1; }
			set { nbLig = value >> 1; }
		}
		public int ModeCPC = 1;
		public bool cpcPlus = false;

		public ImageCpc(ConvertDelegate fctConvert) {
			InitializeComponent();
			int tx = pictureBox.Width;
			int ty = pictureBox.Height;
			Bitmap bmp = new Bitmap(tx, ty);
			pictureBox.Image = bmp;
			bmpLock = new LockBitmap(bmp);

			TailleX = 768;
			TailleY = 544;
			ModeCPC = 1;
			for (int i = 0; i < 16; i++)
				Palette[i] = paletteStandardCPC[i];

			for (int i = 0; i < 16; i++) {
				// Générer les contrôles de "couleurs"
				colors[i] = new Label();
				colors[i].BorderStyle = BorderStyle.FixedSingle;
				colors[i].Location = new Point(4 + i * 48, 568);
				colors[i].Size = new Size(40, 32);
				colors[i].Tag = i;
				colors[i].Click += ClickColor;
				Controls.Add(colors[i]);
				// Générer les contrôles de "bloquage couleur"
				lockColors[i] = new CheckBox();
				lockColors[i].Location = new Point(16 + i * 48, 600);
				lockColors[i].Size = new Size(20, 20);
				lockColors[i].Tag = i;
				lockColors[i].Click += ClickLock;
				Controls.Add(lockColors[i]);
				lockColors[i].Update();
			}
			Reset();
			ChangeZoom(1);
			Convert = fctConvert;
		}

		public void SetPalette(int entree, int valeur) {
			Palette[entree] = valeur;
		}

		public RvbColor GetPaletteColor(int col) {
			if (cpcPlus)
				return new RvbColor((byte)(((Palette[col] & 0xF0) >> 4) * 17), (byte)(((Palette[col] & 0xF00) >> 8) * 17), (byte)((Palette[col] & 0x0F) * 17));

			return RgbCPC[Palette[col] < 27 ? Palette[col] : 0];
		}

		private int GetPalCPC(int c) {
			if (cpcPlus) {
				byte b = (byte)((c & 0x0F) * 17);
				byte r = (byte)(((c & 0xF0) >> 4) * 17);
				byte v = (byte)(((c & 0xF00) >> 8) * 17);
				return (int)(r + (v << 8) + (b << 16) + 0xFF000000);
			}
			return RgbCPC[c < 27 ? c : 0].GetColor;
		}

		public void SetPixelCpc(int xPos, int yPos, int col, int mode) {
			int nb = 4 >> mode;
			for (int i = 0; i < nb; i++) {
				bmpLock.SetPixel(xPos + i, yPos, GetPalCPC(Palette[col]));
				bmpLock.SetPixel(xPos + i, yPos + 1, GetPalCPC(Palette[col]));
			}
		}

		public int GetPixel(int pixelX, int pixelY) {
			return bmpLock.GetPixel(pixelX, pixelY);
		}

		public void CopyPixel(int xPos, int yPos, int color) {
			bmpLock.SetPixel(xPos, yPos, color);
			bmpLock.SetPixel(xPos, yPos + 1, color);
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
				EditColor ed = new EditColor(numCol, Palette[numCol], GetPaletteColor(numCol).GetColorArgb, cpcPlus);
				ed.ShowDialog(this);
				if (ed.isValide) {
					SetPalette(numCol, ed.ValColor);
					UpdatePalette();
					Convert(false);
				}
			}
			else {
				if (editImage != null)
					editImage.SetPenColor(GetPaletteColor(numCol));
			}
		}

		public void Reset() {
			int col = 0;
			bmpLock.LockBits();
			for (int x = 0; x < bmpLock.Width; x++)
				for (int y = 0; y < bmpLock.Height; y++)
					bmpLock.SetPixel(x, y, col);

			bmpLock.UnlockBits();
			vScrollBar.Height = TailleY;
			vScrollBar.Left = TailleX + 3;
			hScrollBar.Width = TailleX;
			hScrollBar.Top = TailleY + 3;
		}

		public void UpdatePalette() {
			for (int i = 0; i < 16; i++) {
				colors[i].BackColor = Color.FromArgb(GetPaletteColor(i).GetColorArgb);
				colors[i].Refresh();
			}
		}

		public void Render() {
			UpdatePalette();
			pictureBox.Refresh();
		}

		public void LockBits() {
			bmpLock.LockBits();
		}

		public void UnlockBits() {
			bmpLock.UnlockBits();
		}

		private void lockAllPal_CheckedChanged(object sender, System.EventArgs e) {
			for (int i = 0; i < 16; i++) {
				lockColors[i].Checked = lockAllPal.Checked;
				lockState[i] = lockAllPal.Checked ? 1 : 0;
			}
			Convert(false);
		}

		#region Mode édition
		private void modeEdition_CheckedChanged(object sender, System.EventArgs e) {
			if (modeEdition.Checked) {
				if (editImage == null)
					editImage = new EditImage();

				editImage.Show();
				editImage.evtChangeZoom += ChangeZoom;
				editImage.evtChangePen += ChangePen;
				editImage.evtClose += CloseEditImage;
				editImage.SetPenColor(GetPaletteColor(numCol));
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
			hScrollBar.Maximum = hScrollBar.LargeChange + TailleX - (TailleX / zoom);
			vScrollBar.Maximum = vScrollBar.LargeChange + TailleY - (TailleY / zoom);
			hScrollBar.Value = offsetX = (TailleX - TailleX / zoom) / 2;
			vScrollBar.Value = offsetY = (TailleY - TailleY / zoom) / 2;
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
				for (int y = 0; y < penWidth * 2; y += 2) {
					int mode = (ModeCPC >= 3 ? (y & 2) == 0 ? ModeCPC - 2 : ModeCPC - 3 : ModeCPC);
					int Tx = (4 >> mode);
					for (int x = 0; x < penWidth * Tx; x += Tx) {
						int xReel = x + offsetX + (e.X / zoom) - ((penWidth * Tx) >> 1) + 1;
						int yReel = y + offsetY + (e.Y / zoom) - penWidth + 1;
						if (xReel >= 0 && yReel > 0 && xReel < TailleX && yReel < TailleY)
							SetPixelCpc(xReel, yReel, numCol, mode);
					}
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
		#endregion
	}
}
