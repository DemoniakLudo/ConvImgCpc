﻿using System;
using System.Collections.Generic;
using System.Drawing;
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
		private int startGrille, tailleGrille;
		private bool drawGrille = false;

		public ImageCpc(Main m, ConvertDelegate fctConvert) {
			InitializeComponent();
			main = m;
			for (int i = 0; i < 16; i++) {
				// Générer les contrôles de "couleurs"
				colors[i] = new Label();
				colors[i].BorderStyle = BorderStyle.FixedSingle;
				colors[i].Location = new Point(168 + i * 48, 568 - 564);
				colors[i].Size = new Size(40, 32);
				colors[i].Tag = i;
				colors[i].MouseDown += ClickColor;
				Controls.Add(colors[i]);
				// Générer les contrôles de "bloquage couleur"
				lockColors[i] = new CheckBox();
				lockColors[i].Location = new Point(180 + i * 48, 600 - 564);
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
					int col = SystemColors.Control.ToArgb();
					for (int y = 0; y < tabBmpLock[i].Height; y += 2) {
						int startX = y < Cpc.TailleY ? Cpc.TailleX : 0;
						tabBmpLock[i].SetHorLineDouble(0, y, startX, Cpc.GetPalCPC(Cpc.Palette[0]));
						tabBmpLock[i].SetHorLineDouble(startX, y, tabBmpLock[i].Width - startX, col);
					}
				}
			}
			int tx = Cpc.CalcTx();
			int maxPen = Cpc.MaxPen(Cpc.yEgx ^ 2);
			for (int i = 0; i < 16; i++)
				colors[i].Visible = lockColors[i].Visible = i < maxPen;

			ToolModeDraw(null);
		}

		public void SetImgCopy() {
			if (imgMotif != null && imgCopy != null)
				imgCopy.CopyBits(BmpLock);
		}

		public void SetPixelCpc(int xPos, int yPos, int col, int tx) {
			BmpLock.SetHorLineDouble(xPos, yPos, tx, Cpc.GetPalCPC(Cpc.modeVirtuel == 5 || Cpc.modeVirtuel == 6 ? colMode5[Math.Min(271, yPos >> 1), col] : Cpc.Palette[col]));
		}

		public void SetImpDrawMode(bool impDrawMode) {
			modeImpDraw = pictImpDraw.Visible = Cpc.TailleY == 544 && Cpc.TailleX == 768 && impDrawMode;
		}

		public void Render(bool forceDrawZoom = false, bool withSpriteGrid = false) {
			bpGenPal.Visible = Cpc.cpcPlus;
			UpdatePalette();
			modeCaptureSprites.Visible = chkGrilleSprite.Visible = Cpc.modeVirtuel == 11;
			chkGrille.Visible = modeEdition.Visible = Cpc.modeVirtuel != 11;
			if (chkDoRedo.Checked && modeEdition.Checked) {
				Enabled = false;
				List<MemoPoint> lst = undo.lstUndoRedo;
				foreach (MemoPoint p in lst) {
					int Tx = Cpc.CalcTx(p.posy);
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
							tmpLock.SetHorLineDouble(x, y, Math.Min(zoom, imgOrigine.Width - x - 1), ySrc > Cpc.TailleY - 1 ? SystemColors.Control.ToArgb() : BmpLock.GetPixel(offsetX + (x / zoom), ySrc));
					}
				}
				pictureBox.Image = tmpLock.Bitmap;
			}
			else {
				pictureBox.Image = imgCopy != null ? imgCopy.Bitmap : BmpLock.Bitmap;
				tmpLock = null;
			}
			if (drawGrille) {
				int x = startGrille * 16;
				Graphics g = Graphics.FromImage(pictureBox.Image);
				while (x < Cpc.TailleX) {
					XorDrawing.DrawXorLine(g, (Bitmap)pictureBox.Image, x, 0, x, Cpc.TailleY, false);
					x += tailleGrille * 16;
				}
			}
			if (chkGrilleSprite.Checked || withSpriteGrid) {
				Graphics g = Graphics.FromImage(pictureBox.Image);
				for (int x = 0; x < Cpc.TailleX; x += 32)
					XorDrawing.DrawXorLine(g, (Bitmap)pictureBox.Image, x, 0, x, Cpc.TailleY, false);

				for (int y = 0; y < Cpc.TailleY; y += 32)
					XorDrawing.DrawXorLine(g, (Bitmap)pictureBox.Image, 0, y, Cpc.TailleX, y, false);
			}
			pictureBox.Refresh();
			if (fenetreRendu != null) {
				fenetreRendu.SetImage(BmpLock.Bitmap);
				fenetreRendu.Picture.Refresh();
			}
		}

		public void CaptureSprite(int captSizeX, int captSizeY, int posx, int posy, DirectBitmap bmp) {
			Graphics g = Graphics.FromImage(pictureBox.Image);
			int x, y;
			if (chkGrilleSprite.Checked) {
				for (x = 0; x < Cpc.TailleX; x += 32)
					XorDrawing.DrawXorLine(g, (Bitmap)pictureBox.Image, x, 0, x, Cpc.TailleY, false);

				for (y = 0; y < Cpc.TailleY; y += 32)
					XorDrawing.DrawXorLine(g, (Bitmap)pictureBox.Image, 0, y, Cpc.TailleX, y, false);
			}
			int sprSizeX = captSizeX << 5;
			int sprSizeY = captSizeY << 5;
			for (x = 0; x < sprSizeX; x += 2)
				for (y = 0; y < sprSizeY; y += 2) {
					int c = BmpLock.GetPixel(posx + x, posy + y);
					for (int zx = 0; zx < 8; zx++)
						for (int zy = 0; zy < 8; zy++)
							bmp.SetPixel(zx + x * 4, zy + y * 4, c);
				}
			if (chkGrilleSprite.Checked) {
				for (x = 0; x < Cpc.TailleX; x += 32)
					XorDrawing.DrawXorLine(g, (Bitmap)pictureBox.Image, x, 0, x, Cpc.TailleY, false);

				for (y = 0; y < Cpc.TailleY; y += 32)
					XorDrawing.DrawXorLine(g, (Bitmap)pictureBox.Image, 0, y, Cpc.TailleX, y, false);
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
		public void SauvePng(string fileName) {
			if (Cpc.modeVirtuel == 6) {
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
							RvbColor p = Cpc.RgbCPC[colMode5[y, i]];
							if (p.r == c.r && p.v == c.v && p.b == c.b) {
								if (i > 2) {
									//c = BitmapBase.RgbCPC[colMode5[y, 3]];
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
				SauveImage.SauveScr(singleName + ".scr", bitmapCpc, main, Main.PackMethode.None, Main.OutputFormat.Binary);
			}
			else
				BmpLock.Bitmap.Save(fileName, System.Drawing.Imaging.ImageFormat.Png);

			main.SetInfo("Sauvegarde image PNG ok.");
		}

		public void SauveScr(string fileName, Main.OutputFormat format) {
			bitmapCpc.CreeBmpCpc(BmpLock, colMode5);
			SauveImage.SauveScr(fileName, bitmapCpc, main, Main.PackMethode.None, format);
			main.SetInfo("Sauvegarde image CPC ok.");
		}

		public void SauveCmp(string fileName, Main.PackMethode pkMethode, Main.OutputFormat format, string version = null) {
			bitmapCpc.CreeBmpCpc(BmpLock, colMode5);
			if (Cpc.modeVirtuel >= 7 && version != null) {
				SaveAnim sa = new SaveAnim(main, fileName, version, pkMethode);
				sa.DoSave(true, pkMethode);
				sa.Dispose();
			}
			else
				SauveImage.SauveScr(fileName, bitmapCpc, main, pkMethode, format, version, colMode5);

			main.SetInfo("Sauvegarde image compactée ok.");
		}

		private byte[] MakeSprite() {
			byte[] ret = new byte[(Cpc.TailleX * Cpc.TailleY) >> 4];
			Array.Clear(ret, 0, ret.Length);
			for (int y = 0; y < Cpc.TailleY; y += 2) {
				int tx = Cpc.CalcTx(y);
				for (int x = 0; x < Cpc.TailleX; x += 8) {
					byte pen = 0, octet = 0;
					for (int p = 0; p < 8; p++)
						if ((p % tx) == 0) {
							RvbColor col = BmpLock.GetPixelColor(x + p, y);
							if (Cpc.cpcPlus) {
								for (pen = 0; pen < 16; pen++) {
									if ((col.v >> 4) == (Cpc.Palette[pen] >> 8) && (col.r >> 4) == ((Cpc.Palette[pen] >> 4) & 0x0F) && (col.b >> 4) == (Cpc.Palette[pen] & 0x0F))
										break;
								}
							}
							else {
								for (pen = 0; pen < 16; pen++) {
									RvbColor fixedCol = Cpc.RgbCPC[Cpc.Palette[pen]];
									if (fixedCol.r == col.r && fixedCol.b == col.b && fixedCol.v == col.v)
										break;
								}
							}
							if (pen > 15) {
								pen = 0; // Pb peut survenir si la palette n'est pas la même pour chaque image d'une animation...
							}

							octet |= (byte)(tabOctetMode[pen % 16] >> (p / tx));
						}
					ret[(x >> 3) + (y >> 1) * (Cpc.TailleX >> 3)] = octet;
				}
			}
			return ret;
		}

		public void SauveSprite(string fileName, string version) {
			byte[] ret = MakeSprite();
			StreamWriter sw = SaveAsm.OpenAsm(fileName, version);
			SaveAsm.GenereDatas(sw, ret, ret.Length, Cpc.TailleX >> (Cpc.TailleX <= 320 ? 3 : 4));
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
			byte[] endData = { (byte)nbImages, (byte)(Cpc.TailleX >> 3), (byte)(Cpc.TailleY >> 1) };
			CpcAmsdos entete = Cpc.CreeEntete(fileName, 0x4000, (short)l, -13622);
			BinaryWriter fp = new BinaryWriter(new FileStream(fileName, FileMode.Create));
			fp.Write(Cpc.AmsdosToByte(entete));
			for (int i = 0; i < nbImages; i++) {
				main.SelectImage(i, true);
				byte[] sprite = MakeSprite();
				fp.Write(sprite, 0, sprite.Length);
			}
			fp.Write(endData, 0, endData.Length);
			fp.Close();
		}

		public byte[] GetCpcScr(Param param, bool spriteMode = false) {
			int maxSize = (Cpc.TailleX >> 3) + ((Cpc.TailleY - 2) >> 4) * (Cpc.TailleX >> 3) + ((Cpc.TailleY - 2) & 14) * 0x400;
			if (spriteMode)
				maxSize = (Cpc.TailleX * Cpc.TailleY) >> 4;
			else
				if (maxSize >= 0x4000)
					maxSize += 0x3800;

			byte[] ret = new byte[maxSize];
			Array.Clear(ret, 0, ret.Length);
			int posRet = 0;
			for (int y = 0; y < Cpc.TailleY; y += 2) {
				int adrCPC = Cpc.GetAdrCpc(y);
				int tx = Cpc.CalcTx(y);
				for (int x = 0; x < Cpc.TailleX; x += 8) {
					byte pen = 0, octet = 0;
					for (int p = 0; p < 8; p++)
						if ((p % tx) == 0) {
							RvbColor col = BmpLock.GetPixelColor(x + p, y);
							if (Cpc.cpcPlus) {
								for (pen = 0; pen < 16; pen++) {
									if ((col.v >> 4) == (Cpc.Palette[pen] >> 8) && (col.b >> 4) == ((Cpc.Palette[pen] >> 4) & 0x0F) && (col.r >> 4) == (Cpc.Palette[pen] & 0x0F))
										break;
								}
							}
							else {
								for (pen = 0; pen < 16; pen++) {
									RvbColor fixedCol = Cpc.RgbCPC[Cpc.Palette[pen]];
									if (fixedCol.r == col.r && fixedCol.b == col.b && fixedCol.v == col.v)
										break;
								}
							}
							octet |= (byte)(tabOctetMode[pen] >> (p / tx));
						}
					if (!spriteMode)
						posRet = Cpc.GetAdrCpc(y) + (x >> 3);

					ret[posRet++] = octet;
				}
			}
			return ret;
		}

		public void SauveDeltaPack(string fileName, string version, bool reboucle, Main.PackMethode pkMethode) {
			if (Cpc.NbCol * Cpc.NbLig > 0x4000)
				MessageBox.Show("Les animations avec des écrans de plus de 16ko ne sont pas supportés...");
			else
				new SaveAnim(main, fileName, version, pkMethode).ShowDialog();
		}

		public void SauvBump(string fileName, string version) {
			RvbColor c2 = new RvbColor(0);
			int pos = 0;
			byte[] bump = new byte[Cpc.TailleY * Cpc.TailleX / 64];
			for (int y = 0; y < Cpc.TailleY; y += 16) {
				for (int x = 0; x < Cpc.TailleX; x += 8) {
					for (int yy = 0; yy < 2; yy++) {
						byte pen = 0;
						RvbColor col = BmpLock.GetPixelColor(x, y + (yy << 3));
						if (Cpc.cpcPlus) {
							for (pen = 0; pen < 16; pen++) {
								if ((col.v >> 4) == (Cpc.Palette[pen] >> 8) && (col.b >> 4) == ((Cpc.Palette[pen] >> 4) & 0x0F) && (col.r >> 4) == (Cpc.Palette[pen] & 0x0F))
									break;
							}
						}
						else {
							for (pen = 0; pen < 16; pen++) {
								RvbColor fixedCol = Cpc.RgbCPC[Cpc.Palette[pen]];
								if (fixedCol.r == col.r && fixedCol.b == col.b && fixedCol.v == col.v)
									break;
							}
						}
						bump[pos++] = pen;
					}
				}
			}
			StreamWriter sw = SaveAsm.OpenAsm(fileName, version);
			SaveAsm.GenereDatas(sw, bump, bump.Length, 16);
			SaveAsm.CloseAsm(sw);
			main.SetInfo("Sauvegarde bump ok.");
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

		public void SauveEgx(string fileName) {
			int mode = Cpc.modeVirtuel;
			int model1 = mode == 3 ? 1 : 2;
			int model2 = mode == 3 ? 0 : 1;
			Cpc.modeVirtuel = model1;
			if (fileName.ToUpper().EndsWith(".SCR"))
				fileName = fileName.Substring(0, fileName.Length - 4);

			bitmapCpc.CreeBmpCpc(BmpLock, colMode5, true, 0);
			SauveImage.SauveScr(fileName + "0.SCR", bitmapCpc, main, Main.PackMethode.None, Main.OutputFormat.Binary);
			Cpc.modeVirtuel = model2;
			bitmapCpc.CreeBmpCpc(BmpLock, colMode5, true, 1);
			SauveImage.SauveScr(fileName + "1.SCR", bitmapCpc, main, Main.PackMethode.None, Main.OutputFormat.Binary);
			main.SetInfo("Sauvegarde des deux images CPC ok.");
			Cpc.modeVirtuel = mode;
		}
		#endregion

		#region Gestion palette
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
				EditColor ed = new EditColor(main, pen, Cpc.Palette[pen], bitmapCpc.GetColorPal(pen).GetColorArgb, Cpc.cpcPlus);
				ed.ShowDialog(this);
				if (ed.isValide) {
					Cpc.Palette[pen] = ed.ValColor;
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
				if (e.Button == MouseButtons.Left) {
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

		private void lockAllPal_CheckedChanged(object sender, EventArgs e) {
			for (int i = 0; i < 16; i++) {
				lockColors[i].Checked = lockAllPal.Checked;
				lockState[i] = lockAllPal.Checked ? 1 : 0;
			}
			Convert(false);
		}

		// Copier la palette dans le presse-papier
		private void bpCopyPal_Click(object sender, EventArgs e) {
			string palTxt = "";
			int maxPen = Cpc.MaxPen(Cpc.yEgx ^ 2);
			for (int i = 0; i < maxPen; i++) {
				int val = Cpc.Palette[i];
				string valStr = Cpc.cpcPlus ? ("&" + val.ToString("X3")) : val.ToString();
				palTxt += valStr + (i < maxPen - 1 ? "," : "");
			}
			Clipboard.SetText(palTxt);
			MessageBox.Show("Palette copiée dans le presse-papier");
		}
		#endregion

		private void chkX2_CheckedChanged(object sender, EventArgs e) {
			if (chkX2.Checked) {
				Width = 973 + 768;
				Height = 668 + 544;
				pictureBox.Width = 1536;
				pictureBox.Height = 1088;
				vScrollBar.Left = 939 + 768;
				hScrollBar.Top = 608 + 544;
			}
			else {
				Width = 973;
				Height = 715;
				pictureBox.Width = 768;
				pictureBox.Height = 544;
				vScrollBar.Left = 939;
				hScrollBar.Top = 608;
			}
			pictureBox.Refresh();
		}

		private void ImageCpc_FormClosing(object sender, FormClosingEventArgs e) {
			e.Cancel = true;
		}

		private void chkGrille_CheckedChanged(object sender, EventArgs e) {
			if (chkGrille.Checked) {
				if (int.TryParse(txbStartNop.Text, out startGrille) && int.TryParse(txbTailleNop.Text, out tailleGrille)) {
					if (tailleGrille > 0 && tailleGrille <= 16 && startGrille + tailleGrille < 64) {
						txbStartNop.Enabled = txbTailleNop.Enabled = false;
						drawGrille = true;
						Render(true);
					}
					else
						chkGrille.Checked = false;
				}
				else
					chkGrille.Checked = false;
			}
			else {
				Render(true);
				drawGrille = false;
				txbStartNop.Enabled = txbTailleNop.Enabled = true;
			}
		}

		private void bpGenPal_Click(object sender, EventArgs e) {
			GenPalette g = new GenPalette(Cpc.Palette, 0);
			g.ShowDialog();
			for (int c = 0; c < 16; c++) {
				int col = Cpc.Palette[c];
				int r = ((col & 0x0F) * 17);
				int v = (((col & 0xF00) >> 8) * 17);
				int b = (((col & 0xF0) >> 4) * 17);
				colors[c].BackColor = Color.FromArgb(r, v, b);
				colors[c].Refresh();
				lockColors[c].Checked = true;
				lockState[c] = 1;
			}
			Convert(false);
		}

		private void bpLoadWin_Click(object sender, EventArgs e) {
			Enabled = false;
			OpenFileDialog dlg = new OpenFileDialog();
			dlg.Filter = " (*.win)|*.win";
			DialogResult result = dlg.ShowDialog();
			if (result == DialogResult.OK) {
				FileStream fileScr = new FileStream(dlg.FileName, FileMode.Open, FileAccess.Read);
				int l = (int)fileScr.Length;
				byte[] tabRead = new byte[l];
				fileScr.Read(tabRead, 0, l);
				fileScr.Close();
				if (Cpc.CheckAmsdos(tabRead)) {
					imgMotif = new BitmapCpc(tabRead, 128).DrawBitmap((tabRead[l - 4] + (tabRead[l - 3] << 8)) >> 3, tabRead[l - 2], true);
					if (zoom != 1) {
						zoom = 1;
						DoZoom();
					}
					imgCopy = new DirectBitmap(BmpLock.Width, BmpLock.Height);
					imgCopy.CopyBits(BmpLock);
					pictureBox.Image = imgCopy.Bitmap;
					pictureBox.Refresh();
				}
			}
			Enabled = true;
		}

		private void bpCopyImage_Click(object sender, EventArgs e) {
			DirectBitmap bmpTmp = new DirectBitmap(Cpc.TailleX, Cpc.TailleY);
			for (int x = 0; x < Cpc.TailleX; x++)
				for (int y = 0; y < Cpc.TailleY; y++)
					bmpTmp.SetPixel(x, y, BmpLock.GetPixel(x, y));

			Clipboard.SetImage(bmpTmp.Bitmap);
			bmpTmp.Dispose();
		}
	}
}
