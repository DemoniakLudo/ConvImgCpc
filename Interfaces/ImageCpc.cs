using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace ConvImgCpc {
	public partial class ImageCpc : Form {
		private DirectBitmap bmpLock, tmpLock;
		public DirectBitmap BmpLock { get { return bmpLock; } }
		private Label[] colors = new Label[16];
		private CheckBox[] lockColors = new CheckBox[16];
		public int[] lockState = new int[16];
		private int offsetX = 0, offsetY = 0;
		private int zoom = 1;
		private bool setZoomRect = false;
		private int zoomRectx, zoomRecty, zoomRectw, zoomRecth;
		private int numCol = 0;
		private int penWidth = 1;
		private Image imgOrigine;
		private Rendu fenetreRendu;
		private UndoRedo undo = new UndoRedo();
		private bool doDraw = false;
		public delegate void ConvertDelegate(bool doConvertbook);
		public BitmapCpc bitmapCpc = new BitmapCpc();

		private ConvertDelegate Convert;

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

		private Main main;

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
			int tx = 4 >> (modeVirtuel >= 5 ? 1 : modeVirtuel > 2 ? modeVirtuel - 3 : modeVirtuel);
			int maxCol = modeVirtuel == 6 ? 16 : 1 << tx;
			for (int i = 0; i < 16; i++)
				colors[i].Visible = lockColors[i].Visible = i < maxCol;
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
					int Tx = 4 >> (modeVirtuel >= 5 ? 1 : modeVirtuel >= 3 ? (p.posy & 2) == 0 ? modeVirtuel - 2 : modeVirtuel - 3 : modeVirtuel);
					bmpLock.SetHorLineDouble(p.posx, p.posy, Tx, p.newColor);
				}
				forceDrawZoom = true;
				bpUndo.Enabled = undo.CanUndo;
				bpRedo.Enabled = undo.CanRedo;
				Enabled = true;
			}
			if (zoom != 1) {
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
				pictureBox.Image = bmpLock.Bitmap;
				tmpLock = null;
			}
			pictureBox.Refresh();
			if (fenetreRendu != null) {
				fenetreRendu.Picture.Image = imgOrigine;
				fenetreRendu.Picture.Refresh();
			}
		}

		public void SetNbColors(int nbCol) {
			lblNbColors.Text = "Nbre de couleurs : " + nbCol;
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
			pictureBox.Image.Save(fileName, System.Drawing.Imaging.ImageFormat.Png);
		}

		public void SauveScr(string fileName, Param param) {
			bitmapCpc.CreeBmpCpc(bmpLock);
			SauveImage.SauveScr(fileName, bitmapCpc, param, false);
		}

		public void SauveCmp(string fileName, Param param) {
			bitmapCpc.CreeBmpCpc(bmpLock);
			SauveImage.SauveScr(fileName, bitmapCpc, param, true);
		}

		private StreamWriter OpenAsm(string fileName, string version, Param param = null) {
			StreamWriter sw = File.CreateText(fileName);
			sw.WriteLine("; Généré par ConvImgCpc" + version.Replace('\n', ' '));
			if (param != null) {
				sw.WriteLine("; mode écran " + param.modeVirtuel);
				sw.WriteLine("; Taille (nbColsxNbLignes) " + NbCol.ToString() + "x" + NbLig.ToString());
			}
			return sw;
		}

		private void CloseAsm(StreamWriter sw) {
			sw.Close();
			sw.Dispose();
		}

		public void SauveAssembleur(StreamWriter sw, byte[] tabByte, int length) {
			string line = "\tDB\t";
			int nbOctets = 0;
			for (int i = 0; i < length; i++) {
				line += "#" + tabByte[i].ToString("X2") + ",";
				if (++nbOctets >= Math.Min(16, NbCol)) {
					sw.WriteLine(line.Substring(0, line.Length - 1));
					line = "\tDB\t";
					nbOctets = 0;
				}
			}
			if (nbOctets > 0)
				sw.WriteLine(line.Substring(0, line.Length - 1));
		}

		public void SauveSprite(string fileName, string version, Param param) {
			byte[] ret = new byte[(TailleX * TailleY) >> 4];
			Array.Clear(ret, 0, ret.Length);
			int posRet = 0;
			for (int y = 0; y < TailleY; y += 2) {
				int modeCPC = (modeVirtuel >= 5 ? 1 : modeVirtuel >= 3 ? (y & 2) == 0 ? modeVirtuel - 2 : modeVirtuel - 3 : modeVirtuel);
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
			StreamWriter sw = OpenAsm(fileName, version, param);
			SauveAssembleur(sw, ret, ret.Length);
			CloseAsm(sw);
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
				int modeCPC = (modeVirtuel >= 5 ? 1 : modeVirtuel >= 3 ? (y & 2) == 0 ? modeVirtuel - 2 : modeVirtuel - 3 : modeVirtuel);
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
			byte[] bufOut = new byte[0x8000];
			StreamWriter sw = OpenAsm(fileName, version, param);
			int nbImages = main.GetMaxImages();
			if (reboucle) {
				main.SelectImage(nbImages - 1);
				Convert(true);
				DeltaPack.Pack(this, param, bufOut, true);
			}
			int ltot = 0;
			DeltaPack.Pack(this, param, bufOut, true);
			for (int i = 0; i < nbImages; i++) {
				main.SelectImage(i);
				Convert(true);
				Application.DoEvents();
				int l = DeltaPack.Pack(this, param, bufOut, i == 0 && !reboucle);
				sw.WriteLine("Delta" + i.ToString() + ":\t\t; Taille #" + l.ToString("X4"));
				SauveAssembleur(sw, bufOut, l);
				ltot += l;
			}
			sw.WriteLine("AnimDelta:\t\t; Taille totale #" + ltot.ToString("X4"));
			string line = "\tDW\t";
			int nbFramesWrite = 0;
			for (int i = 0; i < nbImages; i++) {
				line += "Delta" + i.ToString() + ",";
				if (++nbFramesWrite >= Math.Min(16, NbCol)) {
					sw.WriteLine(line.Substring(0, line.Length - 1));
					line = "\tDW\t";
					nbFramesWrite = 0;
				}
			}
			sw.WriteLine(line + "0");
			CloseAsm(sw);
		}

		public void LirePalette(string fileName, Param param) {
			if (SauveImage.LirePalette(fileName, this, param))
				UpdatePalette();
		}

		public void SauvePalette(string fileName, Param param) {
			SauveImage.SauvePalette(fileName, this, param);
		}
		#endregion

		#region Gestion palette
		private RvbColor GetPaletteColor(int col) {
			return cpcPlus ? new RvbColor((byte)(((Palette[col] & 0xF0) >> 4) * 17), (byte)(((Palette[col] & 0xF00) >> 8) * 17), (byte)((Palette[col] & 0x0F) * 17)) : BitmapCpc.RgbCPC[Palette[col] < 27 ? Palette[col] : 0];
		}

		private int GetPalCPC(int c) {
			return cpcPlus ? (((c & 0xF0) >> 4) * 17) + ((((c & 0xF00) >> 8) * 17) << 8) + (((c & 0x0F) * 17) << 16) : BitmapCpc.RgbCPC[c < 27 ? c : 0].GetColor;
		}

		// Click sur un "lock"
		private void ClickLock(object sender, System.EventArgs e) {
			CheckBox colorLock = sender as CheckBox;
			int numLock = colorLock.Tag != null ? (int)colorLock.Tag : 0;
			lockState[numLock] = colorLock.Checked ? 1 : 0;
			Convert(false);
		}

		// Changement de la palette
		private void ClickColor(object sender, System.EventArgs e) {
			Label colorClick = sender as Label;
			numCol = colorClick.Tag != null ? (int)colorClick.Tag : 0;
			if (!modeEdition.Checked) {
				EditColor ed = new EditColor(numCol, Palette[numCol], GetPaletteColor(numCol).GetColorArgb, cpcPlus);
				ed.ShowDialog(this);
				if (ed.isValide) {
					Palette[numCol] = ed.ValColor;
					UpdatePalette();
					Convert(false);
				}
			}
			else {
				RvbColor col = GetPaletteColor(numCol);
				crayonColor.BackColor = Color.FromArgb(col.b, col.v, col.r);
			}
		}

		private void UpdatePalette() {
			for (int i = 0; i < 16; i++) {
				colors[i].BackColor = Color.FromArgb(GetPaletteColor(i).GetColorArgb);
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

		#region Gestion déplacement souris
		private void modeEdition_CheckedChanged(object sender, System.EventArgs e) {
			zoom = 1;
			chkRendu.Checked = false;
			offsetX = offsetY = 0;
			vScrollBar.Visible = hScrollBar.Visible = setZoomRect = false;
			if (modeEdition.Checked) {
				undo.Reset();
				grpEdition.Visible = tailleCrayon.Enabled = true;
				bpUndo.Enabled = bpRedo.Enabled = false;
				tailleCrayon_SelectedIndexChanged(null, null);
			}
			else {
				CloseRendu();
				grpEdition.Visible = tailleCrayon.Enabled = false;
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

		private void GestMouseEdit(MouseEventArgs e, int yReel) {
			if (e.Button == MouseButtons.Left) {
				for (int y = 0; y < penWidth * 2; y += 2) {
					int mode = (modeVirtuel >= 5 ? 1 : modeVirtuel >= 3 ? (yReel & 2) == 0 ? modeVirtuel - 2 : modeVirtuel - 3 : modeVirtuel);
					int Tx = (4 >> mode);
					int nbCol = modeVirtuel == 6 ? 16 : 1 << Tx;
					int realColor = GetPalCPC(Palette[numCol % nbCol]);
					int yStart = zoom * (yReel - offsetY);
					for (int x = 0; x < penWidth * Tx; x += Tx) {
						int xReel = (x + offsetX + (e.X / zoom)) & -Tx;
						if (xReel >= 0 && yReel >= 0 && xReel < TailleX && yReel < TailleY) {
							undo.MemoUndoRedo(xReel, yReel, bmpLock.GetPixel(xReel, yReel), realColor, doDraw);
							doDraw = true;
							bmpLock.SetHorLineDouble(xReel, yReel, Tx, realColor);
							if (zoom != 1)
								for (int yz = yStart; yz < Math.Min(tmpLock.Height, yStart + (zoom << 1)); yz += 2)
									tmpLock.SetHorLineDouble(zoom * (xReel - offsetX), yz, zoom * Tx, realColor);
						}
					}
					yReel += 2;
				}
				Render();
			}
			else
				if (e.Button == MouseButtons.Right) {
					if (zoom == 1) {
						if (!setZoomRect) {
							setZoomRect = true;
							zoomRectx = e.X;
							zoomRecty = e.Y;
							zoomRecth = 0;
							zoomRectw = 0;
						}
						else {
							Graphics g = Graphics.FromImage(pictureBox.Image);
							if (zoomRecth != 0 || zoomRectw != 0) {
								XorDrawing.DrawXorRectangle(g, (Bitmap)pictureBox.Image, zoomRectx, zoomRecty, zoomRectx + zoomRectw, zoomRecty + zoomRecth);
							}
							zoomRectw = e.X - zoomRectx;
							zoomRecth = e.Y - zoomRecty;
							XorDrawing.DrawXorRectangle(g, (Bitmap)pictureBox.Image, zoomRectx, zoomRecty, zoomRectx + zoomRectw, zoomRecty + zoomRecth);
							pictureBox.Refresh();
						}
					}
					else {
						zoom = 1;
						offsetX = offsetY = 0;
						vScrollBar.Visible = hScrollBar.Visible = false;
						Render(true);
					}
				}
				else {
					bpUndo.Enabled = undo.CanUndo;
					bpRedo.Enabled = undo.CanRedo;
					if (doDraw) {
						doDraw = false;
						undo.EndUndoRedo();
					}
					if (setZoomRect) {
						setZoomRect = false;
						if (zoomRectw != 0 && zoomRecth != 0) {
							Graphics g = Graphics.FromImage(pictureBox.Image);
							XorDrawing.DrawXorRectangle(g, (Bitmap)pictureBox.Image, zoomRectx, zoomRecty, zoomRectx + zoomRectw, zoomRecty + zoomRecth);
							zoom = Math.Max(1, Math.Min(Math.Abs(768 / zoomRectw), Math.Abs(544 / zoomRecth)) & 0x7E);
							vScrollBar.Visible = hScrollBar.Visible = zoom > 1;
							hScrollBar.Maximum = hScrollBar.LargeChange + TailleX - (TailleX / zoom);
							vScrollBar.Maximum = vScrollBar.LargeChange + TailleY - (TailleY / zoom);
							hScrollBar.Value = Math.Max(0, Math.Min(Math.Min(zoomRectx, zoomRectx + zoomRectw), TailleX - ((imgOrigine.Width + zoom) / zoom)));
							vScrollBar.Value = Math.Max(0, Math.Min(Math.Min(zoomRecty, zoomRecty + zoomRecth), TailleY - ((imgOrigine.Height + zoom) / zoom)));
							offsetX = (hScrollBar.Value >> 3) << 3;
							offsetY = (vScrollBar.Value >> 1) << 1;
							if (zoom != 1) {
								tmpLock = new DirectBitmap(imgOrigine.Width, imgOrigine.Height);
							}
							Render(true);
						}
					}
				}
		}

		private int posx = 0, posy = 0, sizex = 0, sizey = 0;
		private int memoMouseX = 0, memoMouseY = 0;
		private bool movePos = false, moveSize = false;

		private void GestMouse(MouseEventArgs e) {
			if (e.Button == MouseButtons.Left) {
				if (!movePos) {
					main.GetSizePos(ref posx, ref posy, ref sizex, ref sizey);
					movePos = true;
					memoMouseX = e.X;
					memoMouseY = e.Y;
				}
				else {
					main.SetSizePos(posx + memoMouseX - e.X, posy + memoMouseY - e.Y, sizex, sizey, true);
				}
			}
			else {
				if (e.Button == MouseButtons.Right) {
					if (!moveSize) {
						main.GetSizePos(ref posx, ref posy, ref sizex, ref sizey);
						moveSize = true;
						memoMouseX = e.X;
						memoMouseY = e.Y;
					}
					else {
						main.SetSizePos(posx, posy, sizex - memoMouseX + e.X, sizey - memoMouseY + e.Y, true);
					}
				}
				else {
					movePos = moveSize = false;
				}
			}
		}

		private void TrtMouseMove(object sender, MouseEventArgs e) {
			if (modeEdition.Checked) {
				int yReel = (offsetY + (e.Y / zoom)) & 0xFFE;
				int mode = (modeVirtuel >= 5 ? 1 : modeVirtuel >= 3 ? (yReel & 2) == 0 ? modeVirtuel - 2 : modeVirtuel - 3 : modeVirtuel);
				int Tx = (4 >> mode);
				RvbColor col = GetPaletteColor(numCol % (modeVirtuel == 6 ? 16 : 1 << Tx));
				crayonColor.BackColor = Color.FromArgb(col.b, col.v, col.r);
				crayonColor.Width = 35 * Tx;
				crayonColor.Refresh();
				try {
					GestMouseEdit(e, yReel);
				}
				catch (Exception ex) {
					string msg = ex.StackTrace + Environment.NewLine + Environment.NewLine
								+ "modeVirtuel=" + modeVirtuel + Environment.NewLine
								+ "yReel=" + yReel + Environment.NewLine
								+ "Zoom=" + zoom + Environment.NewLine
								+ "zoomRectx=" + zoomRectx + Environment.NewLine
								+ "zoomRecty=" + zoomRecty + Environment.NewLine
								+ "zoomRecth=" + zoomRecth + Environment.NewLine
								+ "zoomRectw=" + zoomRectw;
					MessageBox.Show(msg, ex.Message);
				}
			}
			else {
				GestMouse(e);
			}
		}
		#endregion

		private void vScrollBar_Scroll(object sender, ScrollEventArgs e) {
			offsetY = (vScrollBar.Value >> 1) << 1;
			Render(true);
		}

		private void hScrollBar_Scroll(object sender, ScrollEventArgs e) {
			offsetX = (hScrollBar.Value >> 3) << 3;
			Render(true);
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

		private void bpUndo_Click(object sender, System.EventArgs e) {
			Enabled = false;
			List<MemoPoint> lst = undo.Undo();
			foreach (MemoPoint p in lst) {
				int Tx = 4 >> (modeVirtuel >= 5 ? 1 : modeVirtuel >= 3 ? (p.posy & 2) == 0 ? modeVirtuel - 2 : modeVirtuel - 3 : modeVirtuel);
				bmpLock.SetHorLineDouble(p.posx, p.posy, Tx, p.oldColor);
			}
			Render(true);
			bpUndo.Enabled = undo.CanUndo;
			bpRedo.Enabled = undo.CanRedo;
			Enabled = true;
		}

		private void bpRedo_Click(object sender, System.EventArgs e) {
			Enabled = false;
			List<MemoPoint> lst = undo.Redo();
			foreach (MemoPoint p in lst) {
				int Tx = 4 >> (modeVirtuel >= 5 ? 1 : modeVirtuel >= 3 ? (p.posy & 2) == 0 ? modeVirtuel - 2 : modeVirtuel - 3 : modeVirtuel);
				bmpLock.SetHorLineDouble(p.posx, p.posy, Tx, p.newColor);
			}
			Render(true);
			bpUndo.Enabled = undo.CanUndo;
			bpRedo.Enabled = undo.CanRedo;
			Enabled = true;
		}
	}
}
