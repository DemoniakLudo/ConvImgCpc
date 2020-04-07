using System.Drawing;
using System.Windows.Forms;

namespace ConvImgCpc {
	public partial class ImageCpc : Form {
		public byte[] bmpCpc = new byte[0x10000];
		private LockBitmap bmpLock, tmpLock;
		private Label[] colors = new Label[16];
		private CheckBox[] lockColors = new CheckBox[16];
		public int[] lockState = new int[16];
		private int offsetX = 0, offsetY = 0;
		private int zoom;
		private int numCol = 0;
		private int penWidth = 1;
		private Image imgOrigine;
		private Rendu fenetreRendu;

		public delegate void ConvertDelegate(bool doConvertbook);

		private ConvertDelegate Convert;

		public int[] Palette = { 1, 24, 20, 6, 26, 0, 2, 7, 10, 12, 14, 16, 18, 22, 1, 14, 1 };
		private int[] tabOctetMode = { 0x00, 0x80, 0x08, 0x88, 0x20, 0xA0, 0x28, 0xA8, 0x02, 0x82, 0x0A, 0x8A, 0x22, 0xA2, 0x2A, 0xAA };
		private int[] maskMode = new int[3] { 16, 4, 2 };
		public const int Lum0 = 0x00;
		public const int Lum1 = 0x70;
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
		public int BitmapSize { get { return GetAdrCpc(TailleY - 2); } }
		public int ModeCPC = 1;
		public bool cpcPlus = false;

		private int GetAdrCpc(int y) {
			int adrCPC = (y >> 4) * nbCol + (y & 14) * 0x400;
			if (y > 255 && (nbCol * nbLig > 0x3FFF))
				adrCPC += 0x3800;

			return adrCPC;
		}

		public ImageCpc(ConvertDelegate fctConvert) {
			InitializeComponent();
			int tx = pictureBox.Width;
			int ty = pictureBox.Height;
			Bitmap bmp = new Bitmap(tx, ty);
			pictureBox.Image = imgOrigine = bmp;
			bmpLock = new LockBitmap(bmp);
			ModeCPC = 1;
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
			comboZoom.SelectedItem = "1";
			tailleCrayon.SelectedItem = "1";
			Convert = fctConvert;
		}

		public void Reset() {
			int col = System.Drawing.SystemColors.Control.ToArgb();
			bmpLock.LockBits();
			for (int x = 0; x < bmpLock.Width; x++)
				for (int y = 0; y < bmpLock.Height; y++)
					bmpLock.SetPixel(x, y, x < TailleX && y < TailleY ? GetPalCPC(Palette[0]) : col);

			bmpLock.UnlockBits();
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
				return (int)(r + (v << 8) + (b << 16));
			}
			return RgbCPC[c < 27 ? c : 0].GetColor;
		}

		public void SetPixelCpc(int xPos, int yPos, int col, int mode) {
			int nb = 4 >> mode;
			int realColor = GetPalCPC(Palette[col]);
			for (int i = 0; i < nb; i++) {
				bmpLock.SetPixel(xPos + i, yPos, realColor);
				bmpLock.SetPixel(xPos + i, yPos + 1, realColor);
			}
		}

		// Click sur un "lock"
		private void ClickLock(object sender, System.EventArgs e) {
			CheckBox colorLock = sender as CheckBox;
			int numLock = colorLock.Tag != null ? (int)colorLock.Tag : 0;
			lockState[numLock] = colorLock.Checked ? 1 : 0;
		}

		// Changement de la palette
		private void ClickColor(object sender, System.EventArgs e) {
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
				RvbColor col = GetPaletteColor(numCol);
				crayonColor.BackColor = Color.FromArgb(col.blue, col.green, col.red);
			}
		}

		public void UpdatePalette() {
			for (int i = 0; i < 16; i++) {
				colors[i].BackColor = Color.FromArgb(GetPaletteColor(i).GetColorArgb);
				colors[i].Refresh();
			}
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

		public void SauveScr(string fileName, Param param) {
			for (int y = 0; y < TailleY; y += 2) {
				int modeCPC = (ModeCPC >= 3 ? (y & 2) == 0 ? ModeCPC - 2 : ModeCPC - 3 : ModeCPC);
				int adrCPC = GetAdrCpc(y);
				int tx = 4 >> modeCPC;
				for (int x = 0; x < TailleX; x += 8) {
					byte pen = 0, octet = 0;
					for (int p = 0; p < 8; p++)
						if ((p % tx) == 0) {
							RvbColor col = bmpLock.GetPixelColor(x + p, y);
							if (param.cpcPlus) {
								for (pen = 0; pen < 16; pen++) {
									if ((col.green >> 4) == (Palette[pen] >> 8) && (col.red >> 4) == ((Palette[pen] >> 4) & 0x0F) && (col.blue >> 4) == (Palette[pen] & 0x0F))
										break;
								}
							}
							else {
								for (pen = 0; pen < 16; pen++) {
									RvbColor fixedCol = RgbCPC[Palette[pen]];
									if (fixedCol.red == col.red && fixedCol.blue == col.blue && fixedCol.green == col.green)
										break;
								}
							}
							octet |= (byte)(tabOctetMode[pen] >> (p / tx));
						}
					bmpCpc[adrCPC + (x >> 3)] = octet;
				}
			}
			SauveImage.SauveEcran(fileName, this, param.cpcPlus);
		}

		public void SauveBmp(string fileName) {
			Bitmap bmp = new Bitmap(TailleX, TailleY);
			LockBitmap loc = new LockBitmap(bmp);
			loc.LockBits();
			bmpLock.LockBits();
			for (int y = 0; y < TailleY; y++)
				for (int x = 0; x < TailleX; x++)
					loc.SetPixel(x, y, bmpLock.GetPixel(x, y));

			bmpLock.UnlockBits();
			loc.UnlockBits();
			bmp.Save(fileName, System.Drawing.Imaging.ImageFormat.Bmp);
			bmp.Dispose();
		}

		private void modeEdition_CheckedChanged(object sender, System.EventArgs e) {
			if (modeEdition.Checked) {
				grpEdition.Visible = comboZoom.Enabled = tailleCrayon.Enabled = true;
				comboZoom_SelectedIndexChanged(null, null);
				tailleCrayon_SelectedIndexChanged(null, null);
			}
			else {
				CloseRendu();
				grpEdition.Visible = comboZoom.Enabled = tailleCrayon.Enabled = vScrollBar.Visible = hScrollBar.Visible = false;
				zoom = 1;
				Render();
			}
		}

		private void CloseRendu() {
			if (fenetreRendu != null) {
				fenetreRendu.Hide();
				fenetreRendu.Close();
				fenetreRendu.Dispose();
				fenetreRendu = null;
			}
		}

		public void Render() {
			UpdatePalette();
			if (zoom != 1) {
				Bitmap MyBitMap = new Bitmap(imgOrigine.Width, imgOrigine.Height);
				tmpLock = new LockBitmap(MyBitMap);
				tmpLock.LockBits();
				bmpLock.LockBits();
				for (int y = 0; y < imgOrigine.Height; y++) {
					int ySrc = offsetY + (y / zoom);
					for (int x = 0; x < imgOrigine.Width; x++) {
						int xSrc = offsetX + (x / zoom);
						tmpLock.SetPixel(x, y, bmpLock.GetPixel(xSrc, ySrc));
					}
				}
				bmpLock.UnlockBits();
				tmpLock.UnlockBits();
				pictureBox.Image = MyBitMap;
			}
			else {
				pictureBox.Image = imgOrigine;
				tmpLock = null;
			}
			pictureBox.Refresh();
			if (fenetreRendu != null) {
				fenetreRendu.Picture.Image = imgOrigine;
				fenetreRendu.Picture.Refresh();
			}
		}

		private void DrawPen(MouseEventArgs e) {
			LockBits();
			if (tmpLock != null)
				tmpLock.LockBits();

			int penSizeY = penWidth * 2;
			for (int y = 0; y < penSizeY; y += 2) {
				int yReel = (y + offsetY + (e.Y / zoom)) & 0xFFE;
				int mode = (ModeCPC >= 3 ? (yReel & 2) == 0 ? ModeCPC - 2 : ModeCPC - 3 : ModeCPC);
				int Tx = (4 >> mode);
				int pensizeX = penWidth * Tx;
				int col = numCol % maskMode[mode];
				for (int x = 0; x < pensizeX; x += Tx) {
					int xReel = (x + offsetX + (e.X / zoom)) & -Tx;
					if (xReel >= 0 && yReel >= 0 && xReel < TailleX && yReel < TailleY)
						SetPixelCpc(xReel, yReel, col, mode);
				}
			}
			if (tmpLock != null)
				tmpLock.UnlockBits();

			UnlockBits();
			Render();
		}

		private void TrtMouseMove(MouseEventArgs e) {
			if (modeEdition.Checked) {
				int yReel = (offsetY + (e.Y / zoom)) & 0xFFE;
				int mode = (ModeCPC >= 3 ? (yReel & 2) == 0 ? ModeCPC - 2 : ModeCPC - 3 : ModeCPC);
				int Tx = (4 >> mode);
				int xReel = (offsetX + (e.X / zoom)) & -Tx;
				if (xReel >= 0 && yReel >= 0 && xReel < TailleX && yReel < TailleY) {
					RvbColor col = GetPaletteColor(numCol % maskMode[mode]);
					crayonColor.BackColor = Color.FromArgb(col.blue, col.green, col.red);
					crayonColor.Refresh();
					if (e.Button == MouseButtons.Left)
						DrawPen(e);
				}
			}
		}

		private void pictureBox_MouseDown(object sender, MouseEventArgs e) {
			TrtMouseMove(e);
		}

		private void pictureBox_MouseMove(object sender, MouseEventArgs e) {
			TrtMouseMove(e);
		}

		private void vScrollBar_Scroll(object sender, ScrollEventArgs e) {
			offsetY = (vScrollBar.Value >> 1) << 1;
			Render();
		}

		private void hScrollBar_Scroll(object sender, ScrollEventArgs e) {
			offsetX = (hScrollBar.Value >> 3) << 3;
			Render();
		}

		private void comboZoom_SelectedIndexChanged(object sender, System.EventArgs e) {
			zoom = int.Parse(comboZoom.SelectedItem.ToString());
			vScrollBar.Visible = hScrollBar.Visible = zoom > 1;
			hScrollBar.Maximum = hScrollBar.LargeChange + TailleX - (TailleX / zoom);
			vScrollBar.Maximum = vScrollBar.LargeChange + TailleY - (TailleY / zoom);
			hScrollBar.Value = offsetX = (TailleX - TailleX / zoom) / 2;
			vScrollBar.Value = offsetY = (TailleY - TailleY / zoom) / 2;
			Render();
		}

		private void tailleCrayon_SelectedIndexChanged(object sender, System.EventArgs e) {
			penWidth = int.Parse(tailleCrayon.SelectedItem.ToString());
		}

		private void chkRendu_CheckedChanged(object sender, System.EventArgs e) {
			if (chkRendu.Checked) {
				fenetreRendu = new Rendu();
				fenetreRendu.Show();
				Render();
			}
			else
				CloseRendu();
		}
	}
}
