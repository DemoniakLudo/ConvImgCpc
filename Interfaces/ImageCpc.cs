using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Windows.Forms;

namespace ConvImgCpc {
	public partial class ImageCpc : Form {
		public DirectBitmap[] tabBmpLock;
		public DirectBitmap BmpLock { get { return tabBmpLock[selImage]; } }
		private DirectBitmap tmpLock;
		private Label[] colors = new Label[16];
		private CheckBox[] lockColors = new CheckBox[16];
		public int[] lockState = new int[16];
		private Image imgOrigine;
		public delegate void ConvertDelegate(bool doConvert, bool noInfo = false);
		private BitmapCpc[] TabBitmapCpc;
		public BitmapCpc bitmapCpc { get { return TabBitmapCpc[selImage]; } }
		public int selImage = 0, maxImage = 0;
		public Main main;
		public ConvertDelegate Convert;
		private int[] tabOctetMode = { 0x00, 0x80, 0x08, 0x88, 0x20, 0xA0, 0x28, 0xA8, 0x02, 0x82, 0x0A, 0x8A, 0x22, 0xA2, 0x2A, 0xAA };
		public int[,] colMode5 = new int[272, 16];

		public ImageCpc(Main m, ConvertDelegate fctConvert) {
			InitializeComponent();
			main = m;
			for (int i = 0; i < 16; i++) {
				// Générer les contrôles de "couleurs"
				colors[i] = new Label();
				colors[i].BorderStyle = BorderStyle.FixedSingle;
				colors[i].Location = new Point(4 + i * 48, 568 - 564);
				colors[i].Size = new Size(40, 32);
				colors[i].Tag = i;
				colors[i].MouseDown += ClickColor;
				Controls.Add(colors[i]);
				// Générer les contrôles de "bloquage couleur"
				lockColors[i] = new CheckBox();
				lockColors[i].Location = new Point(16 + i * 48, 600 - 564);
				lockColors[i].Size = new Size(20, 20);
				lockColors[i].Tag = i;
				lockColors[i].Click += ClickLock;
				Controls.Add(lockColors[i]);
				lockColors[i].Update();
			}
			InitBitmapCpc(1, 100);
			Reset();
			tailleCrayon.SelectedItem = "1";
			Convert = fctConvert;
			pictureBox.Image = imgOrigine = BmpLock.Bitmap;
		}

		public void InitBitmapCpc(int nbImage, int tps) {
			if (nbImage == 0)
				nbImage++;

			selImage = 0;
			maxImage = nbImage;
			TabBitmapCpc = new BitmapCpc[nbImage];
			tabBmpLock = new DirectBitmap[nbImage];
			for (int i = 0; i < nbImage; i++) {
				TabBitmapCpc[i] = new BitmapCpc();
				tabBmpLock[i] = new DirectBitmap(pictureBox.Width, pictureBox.Height);
				tabBmpLock[i].Tps = tps;
			}
		}

		public void Reset(bool force = false) {
			if (!bitmapCpc.isCalc || force) {
				int startImg = force ? 0 : selImage;
				int endImg = force ? maxImage : selImage;
				for (int i = startImg; i < endImg; i++) {
					selImage = i;
					int col = System.Drawing.SystemColors.Control.ToArgb();
					for (int y = 0; y < tabBmpLock[i].Height; y += 2) {
						int startX = y < BitmapCpc.TailleY ? BitmapCpc.TailleX : 0;
						tabBmpLock[i].SetHorLineDouble(0, y, startX, GetPalCPC(BitmapCpc.Palette[0]));
						tabBmpLock[i].SetHorLineDouble(startX, y, tabBmpLock[i].Width - startX, col);
					}
				}
			}
			int tx = BitmapCpc.CalcTx();
			int maxPen = BitmapCpc.MaxPen(2);
			for (int i = 0; i < 16; i++)
				colors[i].Visible = lockColors[i].Visible = i < maxPen;

			ToolModeDraw(null);
		}

		public void SetImgCopy() {
			if (imgMotif != null && imgCopy != null)
				imgCopy.CopyBits(BmpLock);
		}

		public void SetPixelCpc(int xPos, int yPos, int col, int tx) {
			BmpLock.SetHorLineDouble(xPos, yPos, tx, GetPalCPC(BitmapCpc.modeVirtuel == 5 || BitmapCpc.modeVirtuel == 6 ? colMode5[yPos >> 1, col] : BitmapCpc.Palette[col]));
		}

		public void SetImpDrawMode(bool impDrawMode) {
			modeImpDraw = pictImpDraw.Visible = BitmapCpc.TailleY == 544 && BitmapCpc.TailleX == 768 && impDrawMode;
		}

		public void Render(bool forceDrawZoom = false) {
			UpdatePalette();
			modeCaptureSprites.Visible = BitmapCpc.modeVirtuel == 11;
			modeEdition.Visible = BitmapCpc.modeVirtuel != 11;
			if (chkDoRedo.Checked && modeEdition.Checked) {
				Enabled = false;
				List<MemoPoint> lst = undo.lstUndoRedo;
				foreach (MemoPoint p in lst) {
					int Tx = BitmapCpc.CalcTx(p.posy);
					BmpLock.SetHorLineDouble(p.posx, p.posy, Tx, p.newColor);
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
						int ySrc = offsetY + (y / zoom);
						for (int x = 0; x < imgOrigine.Width; x += zoom)
							tmpLock.SetHorLineDouble(x, y, Math.Min(zoom, imgOrigine.Width - x - 1), ySrc > BitmapCpc.TailleY - 1 ? SystemColors.Control.ToArgb() : BmpLock.GetPixel(offsetX + (x / zoom), ySrc));
					}
				}
				pictureBox.Image = tmpLock.Bitmap;
			}
			else {
				pictureBox.Image = imgCopy != null ? imgCopy.Bitmap : BmpLock.Bitmap;
				tmpLock = null;
			}
			pictureBox.Refresh();
			if (fenetreRendu != null) {
				fenetreRendu.SetImage(BmpLock.Bitmap);
				fenetreRendu.Picture.Refresh();
			}
		}

		// Déplacement/Zoom image
		private void MoveOrSize(MouseEventArgs e) {
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
				else
					movePos = moveSize = false;
			}
		}

		#region Lecture/Sauvegarde
		public void SauvePng(string fileName, Param param) {
			if (BitmapCpc.modeVirtuel == 6) {
				string singleName = Path.GetDirectoryName(fileName) + "\\" + Path.GetFileNameWithoutExtension(fileName);
				DirectBitmap bmpRaster = new DirectBitmap(BmpLock.Bitmap.Width >> 1, BmpLock.Bitmap.Height >> 1);
				//DirectBitmap bmp4Cols = new DirectBitmap(BmpLock.Bitmap.Width >> 1, BmpLock.Bitmap.Height >> 1);
				RvbColor c2 = new RvbColor(0);
				int posx = 0;
				for (int y = 0; y < bmpRaster.Height; y++) {
					bool memoC = false;
					for (int x = 0; x < bmpRaster.Width; x++) {
						RvbColor c = BmpLock.GetPixelColor(x << 1, y << 1);
						for (int i = 0; i < 16; i++) {
							RvbColor p = BitmapCpc.RgbCPC[colMode5[y, i]];
							if (p.r == c.r && p.v == c.v && p.b == c.b) {
								if (i > 2) {
									//c = BitmapCpc.RgbCPC[colMode5[y, 3]];
									c2 = p;
									int start = memoC ? x & 0xFF8 : 0;
									memoC = true;
									for (int r = start; r < x; r++)
										bmpRaster.SetPixel(r, y, c2);

									//									posx = 0;
								}
								break;
							}
						}
						//bmp4Cols.SetPixel(x, y, c);
						bmpRaster.SetPixel(x, y, c2);
						posx++;
					}
				}
				bmpRaster.Bitmap.Save(singleName + "_Rasters" + ".png", System.Drawing.Imaging.ImageFormat.Png);
				bitmapCpc.CreeBmpCpcForceMode1(BmpLock);
				SauveImage.SauveScr(singleName + ".scr", bitmapCpc, this, param, Main.PackMethode.None);
			}
			else
				BmpLock.Bitmap.Save(fileName, System.Drawing.Imaging.ImageFormat.Png);

			main.SetInfo("Sauvegarde image PNG ok.");
		}

		public void SauveScr(string fileName, Param param) {
			bitmapCpc.CreeBmpCpc(BmpLock, colMode5);
			SauveImage.SauveScr(fileName, bitmapCpc, this, param, Main.PackMethode.None);
			main.SetInfo("Sauvegarde image CPC ok.");
		}

		public void SauveCmp(string fileName, Param param, Main.PackMethode pkMethode, string version = null) {
			bitmapCpc.CreeBmpCpc(BmpLock, colMode5);
			if (BitmapCpc.modeVirtuel >= 7) {
				SaveAnim sa = new SaveAnim(main, fileName, version, this, param, pkMethode);
				sa.DoSave(true, pkMethode);
				sa.Dispose();
			}
			else
				SauveImage.SauveScr(fileName, bitmapCpc, this, param, pkMethode, version, colMode5);

			main.SetInfo("Sauvegarde image compactée ok.");
		}

		private byte[] MakeSprite() {
			byte[] ret = new byte[(BitmapCpc.TailleX * BitmapCpc.TailleY) >> 4];
			Array.Clear(ret, 0, ret.Length);
			for (int y = 0; y < BitmapCpc.TailleY; y += 2) {
				int tx = BitmapCpc.CalcTx(y);
				for (int x = 0; x < BitmapCpc.TailleX; x += 8) {
					byte pen = 0, octet = 0;
					for (int p = 0; p < 8; p++)
						if ((p % tx) == 0) {
							RvbColor col = BmpLock.GetPixelColor(x + p, y);
							if (BitmapCpc.cpcPlus) {
								for (pen = 0; pen < 16; pen++) {
									if ((col.v >> 4) == (BitmapCpc.Palette[pen] >> 8) && (col.r >> 4) == ((BitmapCpc.Palette[pen] >> 4) & 0x0F) && (col.b >> 4) == (BitmapCpc.Palette[pen] & 0x0F))
										break;
								}
							}
							else {
								for (pen = 0; pen < 16; pen++) {
									RvbColor fixedCol = BitmapCpc.RgbCPC[BitmapCpc.Palette[pen]];
									if (fixedCol.r == col.r && fixedCol.b == col.b && fixedCol.v == col.v)
										break;
								}
							}
							if (pen > 15) {
								pen = 0; // Pb peut survenir si la palette n'est pas la même pour chaque image d'une animation...
							}

							octet |= (byte)(tabOctetMode[pen % 16] >> (p / tx));
						}
					ret[(x >> 3) + (y >> 1) * (BitmapCpc.TailleX >> 3)] = octet;
				}
			}
			return ret;
		}

		public void SauveSprite(string fileName, string version) {
			byte[] ret = MakeSprite();
			StreamWriter sw = SaveAsm.OpenAsm(fileName, version);
			SaveAsm.GenereDatas(sw, ret, ret.Length, BitmapCpc.TailleX >> (BitmapCpc.TailleX <= 320 ? 3 : 4));
			SaveAsm.CloseAsm(sw);
			main.SetInfo("Sauvegarde sprite assembleur ok.");
		}

		public void SauveSpriteCmp(string fileName, string version, Main.PackMethode pkMethode) {
			byte[] ret = MakeSprite();
			byte[] sprCmp = new byte[ret.Length];
			int l = new PackModule().Pack(ret, ret.Length, sprCmp, 0, pkMethode);
			StreamWriter sw = SaveAsm.OpenAsm(fileName, version);
			SaveAsm.GenereDatas(sw, sprCmp, l, 16);
			SaveAsm.CloseAsm(sw);
			main.SetInfo("Sauvegarde sprite assembleur compacté ok.");
		}

		public void SauveImp(string fileName) {
			int nbImages = main.GetMaxImages();
			int l = 0;
			for (int i = 0; i < nbImages; i++) {
				main.SelectImage(i, true);
				Convert(!bitmapCpc.isCalc, true);
				l += MakeSprite().Length;
			}
			l += 3; // 3 octets de fin de fichier
			byte[] endData = { (byte)nbImages, (byte)(BitmapCpc.TailleX >> 3), (byte)(BitmapCpc.TailleY >> 1) };
			CpcAmsdos entete = CpcSystem.CreeEntete(fileName, 0x4000, (short)l, -13622);
			BinaryWriter fp = new BinaryWriter(new FileStream(fileName, FileMode.Create));
			fp.Write(CpcSystem.AmsdosToByte(entete));
			for (int i = 0; i < nbImages; i++) {
				main.SelectImage(i, true);
				byte[] sprite = MakeSprite();
				fp.Write(sprite, 0, sprite.Length);
			}
			fp.Write(endData, 0, endData.Length);
			fp.Close();
		}

		public byte[] GetCpcScr(Param param, bool spriteMode = false) {
			int maxSize = (BitmapCpc.TailleX >> 3) + ((BitmapCpc.TailleY - 2) >> 4) * (BitmapCpc.TailleX >> 3) + ((BitmapCpc.TailleY - 2) & 14) * 0x400;
			if (spriteMode)
				maxSize = (BitmapCpc.TailleX * BitmapCpc.TailleY) >> 4;
			else
				if (maxSize >= 0x4000)
					maxSize += 0x3800;

			byte[] ret = new byte[maxSize];
			Array.Clear(ret, 0, ret.Length);
			int posRet = 0;
			for (int y = 0; y < BitmapCpc.TailleY; y += 2) {
				int adrCPC = BitmapCpc.GetAdrCpc(y);
				int tx = BitmapCpc.CalcTx(y);
				for (int x = 0; x < BitmapCpc.TailleX; x += 8) {
					byte pen = 0, octet = 0;
					for (int p = 0; p < 8; p++)
						if ((p % tx) == 0) {
							RvbColor col = BmpLock.GetPixelColor(x + p, y);
							if (BitmapCpc.cpcPlus) {
								for (pen = 0; pen < 16; pen++) {
									if ((col.v >> 4) == (BitmapCpc.Palette[pen] >> 8) && (col.r >> 4) == ((BitmapCpc.Palette[pen] >> 4) & 0x0F) && (col.b >> 4) == (BitmapCpc.Palette[pen] & 0x0F))
										break;
								}
							}
							else {
								for (pen = 0; pen < 16; pen++) {
									RvbColor fixedCol = BitmapCpc.RgbCPC[BitmapCpc.Palette[pen]];
									if (fixedCol.r == col.r && fixedCol.b == col.b && fixedCol.v == col.v)
										break;
								}
							}
							octet |= (byte)(tabOctetMode[pen] >> (p / tx));
						}
					if (!spriteMode)
						posRet = BitmapCpc.GetAdrCpc(y) + (x >> 3);

					ret[posRet++] = octet;
				}
			}
			return ret;
		}

		public void SauveDeltaPack(string fileName, string version, Param param, bool reboucle, Main.PackMethode pkMethode) {
			if (BitmapCpc.NbCol * BitmapCpc.NbLig > 0x4000)
				MessageBox.Show("Les animations avec des écrans de plus de 16ko ne sont pas supportés...");
			else
				new SaveAnim(main, fileName, version, this, param, pkMethode).ShowDialog();
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

		public void SauveEgx(string fileName, Param param) {
			int mode = BitmapCpc.modeVirtuel;
			int model1 = mode == 3 ? 1 : 2;
			int model2 = mode == 3 ? 0 : 1;
			BitmapCpc.modeVirtuel = model1;
			if (fileName.ToUpper().EndsWith(".SCR"))
				fileName = fileName.Substring(0, fileName.Length - 4);

			bitmapCpc.CreeBmpCpc(BmpLock, colMode5, true, 0);
			SauveImage.SauveScr(fileName + "0.SCR", bitmapCpc, this, param, Main.PackMethode.None);
			BitmapCpc.modeVirtuel = model2;
			bitmapCpc.CreeBmpCpc(BmpLock, colMode5, true, 1);
			SauveImage.SauveScr(fileName + "1.SCR", bitmapCpc, this, param, Main.PackMethode.None);
			main.SetInfo("Sauvegarde des deux images CPC ok.");
			BitmapCpc.modeVirtuel = mode;
		}
		#endregion

		#region Gestion palette
		private int GetPalCPC(int c) {
			return BitmapCpc.cpcPlus ? (((c & 0xF0) >> 4) * 17) + ((((c & 0xF00) >> 8) * 17) << 8) + (((c & 0x0F) * 17) << 16) : BitmapCpc.RgbCPC[c < 27 ? c : 0].GetColor;
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
				EditColor ed = new EditColor(main, pen, BitmapCpc.Palette[pen], bitmapCpc.GetColorPal(pen).GetColorArgb, BitmapCpc.cpcPlus);
				ed.ShowDialog(this);
				if (ed.isValide) {
					BitmapCpc.Palette[pen] = ed.ValColor;
					lockColors[pen].Checked = true;
					lockState[pen] = 1;
					UpdatePalette();
					Convert(false);
				}
			}
			else {
				if (editToolMode != EditTool.Draw)
					rbDraw.Checked = true;

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

		// Copier la palette dans le presse-papier
		private void bpCopyPal_Click(object sender, EventArgs e) {
			string palTxt = "";
			int maxPen = BitmapCpc.MaxPen(2);
			for (int i = 0; i < maxPen; i++) {
				int val = BitmapCpc.Palette[i];
				string valStr = BitmapCpc.cpcPlus ? ("&" + val.ToString("X3")) : val.ToString();
				palTxt += valStr + (i < maxPen - 1 ? "," : "");
			}
			Clipboard.SetText(palTxt);
			MessageBox.Show("Palette copiée dans le presse-papier");
		}

		#endregion

		private void ImageCpc_FormClosing(object sender, FormClosingEventArgs e) {
			e.Cancel = true;
		}
	}
}
