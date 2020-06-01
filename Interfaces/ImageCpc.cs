using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace ConvImgCpc {
	public partial class ImageCpc : Form {
		private DirectBitmap bmpLock;
		public DirectBitmap BmpLock { get { return bmpLock; } }
		private DirectBitmap tmpLock;
		private Label[] colors = new Label[16];
		private CheckBox[] lockColors = new CheckBox[16];
		public int[] lockState = new int[16];

		private Image imgOrigine;
		public delegate void ConvertDelegate(bool doConvert, bool noInfo = false);
		public BitmapCpc bitmapCpc = new BitmapCpc();
		public Main main;
		public ConvertDelegate Convert;

		public int[] Palette {
			get { return bitmapCpc.Palette; }
			set { bitmapCpc.Palette = value; }
		}
		private int[] tabOctetMode = { 0x00, 0x80, 0x08, 0x88, 0x20, 0xA0, 0x28, 0xA8, 0x02, 0x82, 0x0A, 0x8A, 0x22, 0xA2, 0x2A, 0xAA };
		public int[,] colMode5 = new int[272, 4];


		public int NbCol { get { return bitmapCpc.NbCol; } }
		public int TailleX {
			get { return bitmapCpc.TailleX; }
			set { bitmapCpc.TailleX = value; }
		}
		public int NbLig { get { return bitmapCpc.NbLig; } }
		public int TailleY {
			get { return bitmapCpc.TailleY; }
			set { bitmapCpc.TailleY = value; }
		}
		public int BitmapSize { get { return bitmapCpc.BitmapSize; } }
		public int modeVirtuel {
			get { return bitmapCpc.modeVirtuel; }
			set { bitmapCpc.modeVirtuel = value; }
		}
		public bool cpcPlus {
			get { return bitmapCpc.cpcPlus; }
			set { bitmapCpc.cpcPlus = value; }
		}

		public int GetAdrCpc(int y) {
			return bitmapCpc.GetAdrCpc(y);
		}

		public ImageCpc(Main m, ConvertDelegate fctConvert) {
			InitializeComponent();
			main = m;
			int tx = pictureBox.Width;
			int ty = pictureBox.Height;
			bmpLock = new DirectBitmap(tx, ty);
			pictureBox.Image = imgOrigine = bmpLock.Bitmap;
			for (int i = 0; i < 16; i++) {
				// Générer les contrôles de "couleurs"
				colors[i] = new Label();
				colors[i].BorderStyle = BorderStyle.FixedSingle;
				colors[i].Location = new Point(4 + i * 48, 568);
				colors[i].Size = new Size(40, 32);
				colors[i].Tag = i;
				colors[i].MouseDown += ClickColor;
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
			tailleCrayon.SelectedItem = "1";
			Convert = fctConvert;
		}

		public void Reset() {
			int col = System.Drawing.SystemColors.Control.ToArgb();
			for (int y = 0; y < bmpLock.Height; y += 2) {
				int startX = y < TailleY ? TailleX : 0;
				bmpLock.SetHorLineDouble(0, y, startX, GetPalCPC(Palette[0]));
				bmpLock.SetHorLineDouble(startX, y, bmpLock.Width - startX, col);
			}
			int tx = 4 >> (modeVirtuel > 7 ? modeVirtuel - 8 : modeVirtuel >= 5 ? 1 : modeVirtuel > 2 ? modeVirtuel - 3 : modeVirtuel);
			int maxCol = modeVirtuel == 6 ? 16 : 1 << tx;
			for (int i = 0; i < 16; i++)
				colors[i].Visible = lockColors[i].Visible = i < maxCol;

			ToolModeDraw(null);
		}

		public void SetPixelCpc(int xPos, int yPos, int col, int tx) {
			bmpLock.SetHorLineDouble(xPos, yPos, tx, GetPalCPC(modeVirtuel == 5 ? colMode5[yPos >> 1, col] : Palette[col]));
		}

		public void Render(bool forceDrawZoom = false) {
			UpdatePalette();
			if (chkDoRedo.Checked && modeEdition.Checked) {
				Enabled = false;
				List<MemoPoint> lst = undo.lstUndoRedo;
				foreach (MemoPoint p in lst) {
					int Tx = 4 >> (modeVirtuel > 7 ? modeVirtuel - 8 : modeVirtuel >= 5 ? 1 : modeVirtuel >= 3 ? (p.posy & 2) == 0 ? modeVirtuel - 2 : modeVirtuel - 3 : modeVirtuel);
					bmpLock.SetHorLineDouble(p.posx, p.posy, Tx, p.newColor);
				}
				forceDrawZoom = true;
				bpUndo.Enabled = undo.CanUndo;
				bpRedo.Enabled = undo.CanRedo;
				Enabled = true;
			}
			if (zoom != 1) {
				if (tmpLock == null)
					tmpLock = new DirectBitmap(imgOrigine.Width, imgOrigine.Height);

				if (forceDrawZoom) {
					for (int y = 0; y < imgOrigine.Height; y += 2) {
						int ySrc = Math.Min(offsetY + (y / zoom), TailleY - 1);
						for (int x = 0; x < imgOrigine.Width; x += zoom)
							tmpLock.SetHorLineDouble(x, y, Math.Min(zoom, imgOrigine.Width - x - 1), bmpLock.GetPixel(offsetX + (x / zoom), ySrc));
					}
				}
				pictureBox.Image = tmpLock.Bitmap;
			}
			else {
				pictureBox.Image = imgCopy != null ? imgCopy.Bitmap : bmpLock.Bitmap;
				tmpLock = null;
			}
			pictureBox.Refresh();
			if (fenetreRendu != null) {
				fenetreRendu.Picture.Image = imgOrigine;
				fenetreRendu.Picture.Refresh();
			}
		}

		#region Lecture/Sauvegarde
		public void SauvePng(string fileName) {
			if (modeVirtuel == 6) {
				DirectBitmap bmpRaster = new DirectBitmap(bmpLock.Bitmap.Width >> 1, bmpLock.Bitmap.Height >> 1);
				DirectBitmap bmp4Cols = new DirectBitmap(bmpLock.Bitmap.Width >> 1, bmpLock.Bitmap.Height >> 1);
				RvbColor c2 = new RvbColor(0);
				int posx = 0;
				for (int y = 0; y < bmpRaster.Height; y++) {
					for (int x = 0; x < bmpRaster.Width; x++) {
						RvbColor c = bmpLock.GetPixelColor(x << 1, y << 1);
						for (int i = 0; i < 16; i++) {
							RvbColor p = BitmapCpc.RgbCPC[Palette[i]];
							if (p.r == c.r && p.v == c.v && p.b == c.b) {
								if (i > 2) {
									c = BitmapCpc.RgbCPC[Palette[3]];
									c2 = p;

									for (int r = x & 0xFF8; r < x; r++)
										bmpRaster.SetPixel(r, y, c2);

									posx = 0;
								}
								break;
							}
						}
						bmp4Cols.SetPixel(x, y, c);
						bmpRaster.SetPixel(x, y, c2);
						posx++;
					}
				}
				bmp4Cols.Bitmap.Save(fileName + ".1", System.Drawing.Imaging.ImageFormat.Png);
				bmpRaster.Bitmap.Save(fileName + ".2", System.Drawing.Imaging.ImageFormat.Png);
			}
			bmpLock.Bitmap.Save(fileName, System.Drawing.Imaging.ImageFormat.Png);
			main.SetInfo("Sauvegarde image PNG ok.");
		}

		public void SauveScr(string fileName, Param param) {
			bitmapCpc.CreeBmpCpc(bmpLock);
			SauveImage.SauveScr(fileName, bitmapCpc, param, false);
			main.SetInfo("Sauvegarde image CPC ok.");
		}

		public void SauveCmp(string fileName, Param param, string version = null) {
			bitmapCpc.CreeBmpCpc(bmpLock);
			SauveImage.SauveScr(fileName, bitmapCpc, param, true, version);
			main.SetInfo("Sauvegarde image compactée ok.");
		}

		private byte[] MakeSprite() {
			byte[] ret = new byte[(TailleX * TailleY) >> 4];
			Array.Clear(ret, 0, ret.Length);
			int posRet = 0;
			for (int y = 0; y < TailleY; y += 2) {
				int modeCPC = (modeVirtuel > 7 ? modeVirtuel - 8 : modeVirtuel >= 5 ? 1 : modeVirtuel >= 3 ? (y & 2) == 0 ? modeVirtuel - 2 : modeVirtuel - 3 : modeVirtuel);
				int tx = 4 >> modeCPC;
				for (int x = 0; x < TailleX; x += 8) {
					byte pen = 0, octet = 0;
					for (int p = 0; p < 8; p++)
						if ((p % tx) == 0) {
							RvbColor col = bmpLock.GetPixelColor(x + p, y);
							if (cpcPlus) {
								for (pen = 0; pen < 16; pen++) {
									if ((col.v >> 4) == (Palette[pen] >> 8) && (col.r >> 4) == ((Palette[pen] >> 4) & 0x0F) && (col.b >> 4) == (Palette[pen] & 0x0F))
										break;
								}
							}
							else {
								for (pen = 0; pen < 16; pen++) {
									RvbColor fixedCol = BitmapCpc.RgbCPC[Palette[pen]];
									if (fixedCol.r == col.r && fixedCol.b == col.b && fixedCol.v == col.v)
										break;
								}
							}
							octet |= (byte)(tabOctetMode[pen] >> (p / tx));
						}
					ret[posRet++] = octet;
				}
			}
			return ret;
		}

		public void SauveSprite(string fileName, string version, Param param) {
			byte[] ret = MakeSprite();
			StreamWriter sw = Save.OpenAsm(fileName, version, param);
			Save.SauveAssembleur(sw, ret, ret.Length, param);
			Save.CloseAsm(sw);
			main.SetInfo("Sauvegarde sprite assembleur ok.");
		}

		public void SauveSpriteCmp(string fileName, string version, Param param) {
			byte[] ret = MakeSprite();
			byte[] sprCmp = new byte[ret.Length];
			int l = PackDepack.Pack(ret, ret.Length, sprCmp, 0);
			StreamWriter sw = Save.OpenAsm(fileName, version, param);
			Save.SauveAssembleur(sw, sprCmp, l, param);
			Save.CloseAsm(sw);
			main.SetInfo("Sauvegarde sprite assembleur compacté ok.");
		}

		public byte[] GetCpcScr(Param param, bool spriteMode = false) {
			int maxSize = (TailleX >> 3) + ((TailleY - 2) >> 4) * (TailleX >> 3) + ((TailleY - 2) & 14) * 0x400;
			if (spriteMode)
				maxSize = (TailleX * TailleY) >> 4;
			else
				if (maxSize >= 0x4000)
					maxSize += 0x3800;

			byte[] ret = new byte[maxSize];
			Array.Clear(ret, 0, ret.Length);
			int posRet = 0;
			for (int y = 0; y < TailleY; y += 2) {
				int modeCPC = (modeVirtuel > 7 ? modeVirtuel - 8 : modeVirtuel >= 5 ? 1 : modeVirtuel >= 3 ? (y & 2) == 0 ? modeVirtuel - 2 : modeVirtuel - 3 : modeVirtuel);
				int adrCPC = GetAdrCpc(y);
				int tx = 4 >> modeCPC;
				for (int x = 0; x < TailleX; x += 8) {
					byte pen = 0, octet = 0;
					for (int p = 0; p < 8; p++)
						if ((p % tx) == 0) {
							RvbColor col = bmpLock.GetPixelColor(x + p, y);
							if (cpcPlus) {
								for (pen = 0; pen < 16; pen++) {
									if ((col.v >> 4) == (Palette[pen] >> 8) && (col.r >> 4) == ((Palette[pen] >> 4) & 0x0F) && (col.b >> 4) == (Palette[pen] & 0x0F))
										break;
								}
							}
							else {
								for (pen = 0; pen < 16; pen++) {
									RvbColor fixedCol = BitmapCpc.RgbCPC[Palette[pen]];
									if (fixedCol.r == col.r && fixedCol.b == col.b && fixedCol.v == col.v)
										break;
								}
							}
							octet |= (byte)(tabOctetMode[pen] >> (p / tx));
						}
					if (!spriteMode)
						posRet = bitmapCpc.GetAdrCpc(y) + (x >> 3);

					ret[posRet++] = octet;
				}
			}
			return ret;
		}

		public void SauveDeltaPack(string fileName, string version, Param param, bool reboucle) {
			if (NbCol * NbLig > 0x4000)
				MessageBox.Show("Les animations avec des écrans de plus de 16ko ne sont pas supportés...");
			else
				new SaveAnim(fileName, version, this, param).ShowDialog();
		}

		public void LirePalette(string fileName, Param param) {
			if (SauveImage.LirePalette(fileName, this, param)) {
				UpdatePalette();
				main.SetInfo("Lecture palette ok.");
			}
			else
				main.SetInfo("Erreur lecture palette...");
		}

		public void SauvePalette(string fileName, Param param) {
			SauveImage.SauvePalette(fileName, this, param);
			main.SetInfo("Sauvegarde palette ok.");
		}
		#endregion

		#region Gestion palette
		private int GetPalCPC(int c) {
			return cpcPlus ? (((c & 0xF0) >> 4) * 17) + ((((c & 0xF00) >> 8) * 17) << 8) + (((c & 0x0F) * 17) << 16) : BitmapCpc.RgbCPC[c < 27 ? c : 0].GetColor;
		}

		// Click sur un "lock"
		private void ClickLock(object sender, EventArgs e) {
			CheckBox colorLock = sender as CheckBox;
			int numLock = colorLock.Tag != null ? (int)colorLock.Tag : 0;
			lockState[numLock] = colorLock.Checked ? 1 : 0;
			Convert(false);
		}

		// Changement de la palette
		private void ClickColor(object sender, MouseEventArgs e) {
			Label colorClick = sender as Label;
			int pen = colorClick.Tag != null ? (int)colorClick.Tag : 0;
			if (!modeEdition.Checked) {
				EditColor ed = new EditColor(pen, Palette[pen], bitmapCpc.GetColorPal(pen).GetColorArgb, cpcPlus);
				ed.ShowDialog(this);
				if (ed.isValide) {
					Palette[pen] = ed.ValColor;
					UpdatePalette();
					Convert(false);
				}
			}
			else {
				RvbColor col = bitmapCpc.GetColorPal(pen);
				if (e.Button == System.Windows.Forms.MouseButtons.Left) {
					drawCol = pen;
					drawColor.BackColor = Color.FromArgb(col.r, col.v, col.b);
				}
				else {
					undrawCol = pen;
					undrawColor.BackColor = Color.FromArgb(col.r, col.v, col.b);
				}
			}
		}

		private void UpdatePalette() {
			for (int i = 0; i < 16; i++) {
				colors[i].BackColor = Color.FromArgb(bitmapCpc.GetColorPal(i).GetColorArgb);
				colors[i].Refresh();
			}
		}

		private void lockAllPal_CheckedChanged(object sender, System.EventArgs e) {
			for (int i = 0; i < 16; i++) {
				lockColors[i].Checked = lockAllPal.Checked;
				lockState[i] = lockAllPal.Checked ? 1 : 0;
			}
			Convert(false);
		}
		#endregion
	}
}
