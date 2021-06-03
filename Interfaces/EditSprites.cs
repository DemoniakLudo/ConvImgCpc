using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace ConvImgCpc {
	public partial class EditSprites : Form {
		private int numSprite = 0, numBank = 0, selSprite = -1;
		private byte penLeft = 1;
		private DirectBitmap bmpSprite, bmpAllSprites, bmpTest;
		private Main main;
		private Label[] colors = new Label[16];
		private Main.PackMethode pkMethod;

		public EditSprites(Main m, Main.PackMethode pk) {
			InitializeComponent();
			main = m;
			pkMethod = pk;
			main.ChangeLanguage(Controls, "EditSprites");
			bool paletteSpriteOk = false;
			for (int c = 0; c < 16; c++) {
				// Générer les contrôles de "couleurs"
				colors[c] = new Label();
				colors[c].BorderStyle = BorderStyle.FixedSingle;
				colors[c].Location = new Point(730, 72 + c * 40);
				colors[c].Size = new Size(40, 32);
				colors[c].Tag = c;
				colors[c].MouseDown += ClickColor;
				if (BitmapCpc.paletteSprite[c] != 0)
					paletteSpriteOk = true;

				int col = BitmapCpc.paletteSprite[c];
				colors[c].BackColor = Color.FromArgb((byte)((col & 0x0F) * 17), (byte)(((col & 0xF00) >> 8) * 17), (byte)(((col & 0xF0) >> 4) * 17));
				Controls.Add(colors[c]);
			}

			if (!paletteSpriteOk)
				for ( int c = 0; c < 16; c++)
					BitmapCpc.paletteSprite[c] = BitmapCpc.Palette[c];

			bmpSprite = new DirectBitmap(pictEditSprite.Width, pictEditSprite.Height);
			pictEditSprite.Image = bmpSprite.Bitmap;
			bmpAllSprites = new DirectBitmap(pictAllSprites.Width, pictAllSprites.Height);
			pictAllSprites.Image = bmpAllSprites.Bitmap;
			bmpTest = new DirectBitmap(pictTest.Width, pictTest.Height);
			pictTest.Image = bmpTest.Bitmap;
			comboBanque.SelectedIndex = 0;
			DrawPens();
		}

		// Changement de la palette
		private void ClickColor(object sender, MouseEventArgs e) {
			Label colorClick = sender as Label;
			int pen = colorClick.Tag != null ? (int)colorClick.Tag : 0;
			if (e.Button == MouseButtons.Right) {
				int col = BitmapCpc.paletteSprite[pen];
				RvbColor colRvb = new RvbColor((byte)((col & 0x0F) * 17), (byte)(((col & 0xF00) >> 8) * 17), (byte)(((col & 0xF0) >> 4) * 17));
				EditColor ed = new EditColor(main, pen, col, colRvb.GetColor, true);
				ed.ShowDialog(this);
				if (ed.isValide) {
					BitmapCpc.paletteSprite[pen] = ed.ValColor;
					col = ed.ValColor;
					colors[pen].BackColor = Color.FromArgb((byte)((col & 0x0F) * 17), (byte)(((col & 0xF00) >> 8) * 17), (byte)(((col & 0xF0) >> 4) * 17));
					colors[pen].Refresh();
					DrawMatrice();
				}
			}
			else {
				penLeft = (byte)pen;
				DrawPens();
			}
		}

		private void DrawMatrice() {
			for (int spr = 0; spr < 16; spr++) {
				for (int y = 0; y < 16; y++) {
					for (int x = 0; x < 16; x++) {
						int col = BitmapCpc.paletteSprite[BitmapCpc.spritesHard[numBank, spr, x, y]];
						RvbColor c = new RvbColor((byte)((col & 0x0F) * 17), (byte)(((col & 0xF00) >> 8) * 17), (byte)(((col & 0xF0) >> 4) * 17));
						for (int zx = 0; zx < (x == 15 ? 3 : 4); zx++)
							for (int zy = 0; zy < 4; zy++)
								bmpAllSprites.SetPixel(zx + ((x + (spr << 4)) << 2), zy + (y << 2), c);

						if (spr == numSprite)
							for (int zx = 0; zx < 38; zx++)
								for (int zy = 0; zy < 38; zy++)
									bmpSprite.SetPixel(zx + (x * 40), zy + (y * 40), c);
					}
				}
			}
			pictAllSprites.Refresh();
			pictEditSprite.Refresh();
		}

		private void DrawSprite() {
			lblSelSprite.Text = "Sprite n° : " + numSprite.ToString();
			bpPrev.Visible = numSprite > 0;
			bpSuiv.Visible = numSprite < 15;
			for (int y = 0; y < 16; y++) {
				for (int x = 0; x < 16; x++) {
					int col = BitmapCpc.paletteSprite[BitmapCpc.spritesHard[numBank, numSprite, x, y]];
					RvbColor c = new RvbColor((byte)((col & 0x0F) * 17), (byte)(((col & 0xF00) >> 8) * 17), (byte)(((col & 0xF0) >> 4) * 17));
					for (int zx = 0; zx < 38; zx++)
						for (int zy = 0; zy < 38; zy++)
							bmpSprite.SetPixel(zx + (x * 40), zy + (y * 40), c);
				}
			}
			pictEditSprite.Refresh();
		}

		private void SetPixelSprite(int x, int y) {
			int col = BitmapCpc.paletteSprite[BitmapCpc.spritesHard[numBank, numSprite, x, y]];
			RvbColor c = new RvbColor((byte)((col & 0x0F) * 17), (byte)(((col & 0xF00) >> 8) * 17), (byte)(((col & 0xF0) >> 4) * 17));
			for (int zx = 0; zx < 38; zx++)
				for (int zy = 0; zy < 38; zy++)
					bmpSprite.SetPixel(zx + (x * 40), zy + (y * 40), c);

			for (int zx = 0; zx < (x == 15 ? 3 : 4); zx++)
				for (int zy = 0; zy < 4; zy++)
					bmpAllSprites.SetPixel(zx + ((x + (numSprite << 4)) << 2), zy + (y << 2), c);

			pictEditSprite.Refresh();
			pictAllSprites.Refresh();
		}

		private void DrawPens() {
			int cl = BitmapCpc.paletteSprite[penLeft];
			int cr = BitmapCpc.paletteSprite[0];
			lblPenLeft.BackColor = Color.FromArgb((byte)((cl & 0x0F) * 17), (byte)(((cl & 0xF00) >> 8) * 17), (byte)(((cl & 0xF0) >> 4) * 17));
			lblPenRight.BackColor = Color.FromArgb((byte)((cr & 0x0F) * 17), (byte)(((cr & 0xF00) >> 8) * 17), (byte)(((cr & 0xF0) >> 4) * 17));
		}

		private void pictAllMatrice_MouseDown(object sender, MouseEventArgs e) {
			int y = e.X / 64;
			if (y >= 0 && y < 16) {
				if (e.Button == MouseButtons.Left && y != numSprite) {
					numSprite = y;
					DrawSprite();
				}
				if (e.Button == MouseButtons.Right) {
					if (selSprite == -1) {
						selSprite = y;
						for (int i = 0; i < 64; i++) {
							bmpAllSprites.SetPixel(0 + (selSprite << 6), i, 0xFFFFFF);
							bmpAllSprites.SetPixel(63 + (selSprite << 6), i, 0xFFFFFF);
							bmpAllSprites.SetPixel(i + (selSprite << 6), 0, 0xFFFFFF);
							bmpAllSprites.SetPixel(i + (selSprite << 6), 63, 0xFFFFFF);
						}
					}
					else {
						for (int i = 0; i < 64; i++) {
							bmpAllSprites.SetPixel(0 + (selSprite << 6), i, 0);
							bmpAllSprites.SetPixel(63 + (selSprite << 6), i, 0);
							bmpAllSprites.SetPixel(i + (selSprite << 6), 0, 0);
							bmpAllSprites.SetPixel(i + (selSprite << 6), 63, 0);
						}
						int swapSprite = y;
						if (swapSprite != selSprite) {
							for (y = 0; y < 16; y++)
								for (int x = 0; x < 16; x++) {
									byte tmp = BitmapCpc.spritesHard[numBank, selSprite, x, y];
									BitmapCpc.spritesHard[numBank, selSprite, x, y] = BitmapCpc.spritesHard[numBank, swapSprite, x, y];
									BitmapCpc.spritesHard[numBank, swapSprite, x, y] = tmp;
								}
						}
						selSprite = -1;
						DrawMatrice();
					}
					pictAllSprites.Refresh();
				}
			}
		}

		private void pictEditMatrice_MouseMove(object sender, MouseEventArgs e) {
			int x = e.X / 40;
			int y = e.Y / 40;
			if (x >= 0 && y >= 0 && x < 16 && y < 16) {
				if (e.Button == MouseButtons.Left) {
					BitmapCpc.spritesHard[numBank, numSprite, x, y] = penLeft;
					SetPixelSprite(x, y);
				}
				else
					if (e.Button == MouseButtons.Right) {
					BitmapCpc.spritesHard[numBank, numSprite, x, y] = 0;
					SetPixelSprite(x, y);
				}
			}
		}

		private void bpPrev_Click(object sender, EventArgs e) {
			numSprite--;
			DrawSprite();
		}

		private void bpSuiv_Click(object sender, EventArgs e) {
			numSprite++;
			DrawSprite();
		}

		private void bpRead_Click(object sender, EventArgs e) {
			OpenFileDialog dlg = new OpenFileDialog();
			dlg.Filter = "Modèle Sprites (.spr)|*.spr";
			if (dlg.ShowDialog() == DialogResult.OK) {
				try {
					FileStream fileScr = new FileStream(dlg.FileName, FileMode.Open, FileAccess.Read);
					byte[] tabBytes = new byte[fileScr.Length];
					fileScr.Read(tabBytes, 0, tabBytes.Length);
					fileScr.Close();
					if (CpcSystem.CheckAmsdos(tabBytes)) {
						CpcAmsdos enteteAms = CpcSystem.GetAmsdos(tabBytes);
						// Décodage sprite
						int pos = 0;
						int startBank = enteteAms.RealLength == 0x1000 ? numBank : 0;
						for (int bank = startBank; bank < 4; bank++)
							for (int i = 0; i < 16; i++) {
								for (int y = 0; y < 16; y++) {
									for (int x = 0; x < 16; x++) {
										BitmapCpc.spritesHard[bank, i, x, y] = tabBytes[128 + pos++];
										if (pos >= enteteAms.RealLength) {
											x = 16;
											y = 16;
											i = 16;
											bank = 4;
										}
									}
								}
							}
					}
					string filePalette = Path.ChangeExtension(dlg.FileName, "kit");
					if (File.Exists(filePalette))
						main.ReadPaletteKit(filePalette, colors);

				}
				catch {
					main.DisplayErreur("Impossible de lire les sprites.");
				}
				DrawMatrice();
			}
		}

		private void SavePaletteKitAsm(StreamWriter sw) {
			sw.WriteLine("PaletteSprites");
			string s = "	DB	";
			for (int i = 0; i < 15; i++) {
				int kit = BitmapCpc.paletteSprite[i + 1];
				byte c1 = (byte)(((kit & 0x0F) << 4) + ((kit & 0xF0) >> 4));
				byte c2 = (byte)(kit >> 8);
				s += "#" + c1.ToString("X2") + ",#" + c2.ToString("X2");
				if (i < 14)
					s += ",";
			}
			sw.WriteLine(s);
		}

		private void SauveSprites(bool allBank = false) {
			SaveFileDialog dlg = new SaveFileDialog();
			dlg.Filter = "Modèle Sprites (.spr)|*.spr|"
						+ "Fichier assembleur (.asm)|*.asm|"
						+ "Fichier assembleur compacté individuellement (.asm)|*.asm";
			if (dlg.ShowDialog() == DialogResult.OK) {
				Enabled = false;
				try {
					short size = (short)(allBank ? 0x4000 : 0x1000);
					byte[] buffer = new byte[size];
					int pos = 0;
					int startBank = allBank ? 0 : numBank;
					int endBank = allBank ? 4 : numBank + 1;
					for (int bank = startBank; bank < endBank; bank++)
						for (int i = 0; i < 16; i++) {
							for (int y = 0; y < 16; y++) {
								for (int x = 0; x < 16; x++)
									buffer[pos++] = BitmapCpc.spritesHard[bank, i, x, y];
							}
						}
					switch (dlg.FilterIndex) {
						case 1:
							CpcAmsdos entete = CpcSystem.CreeEntete(Path.GetFileName(dlg.FileName), 0x4000, size, 0);
							BinaryWriter fp = new BinaryWriter(new FileStream(dlg.FileName, FileMode.Create));
							fp.Write(CpcSystem.AmsdosToByte(entete));
							fp.Write(buffer);
							fp.Close();
							// Sauvegarde palette au format .KIT
							main.SavePaletteKit(Path.ChangeExtension(dlg.FileName, "kit"), BitmapCpc.paletteSprite);
							break;

						case 2:
							// Sauvegarde assembleur
							StreamWriter sw = SaveAsm.OpenAsm(dlg.FileName, "");
							SaveAsm.GenereDatas(sw, buffer, size, 16, 16, "SpriteHard");
							SavePaletteKitAsm(sw);
							SaveAsm.CloseAsm(sw);
							break;

						case 3:
							// Sauvegarde assembleur compacté 
							StreamWriter sw2 = SaveAsm.OpenAsm(dlg.FileName, "");
							int numSpr = 0;
							byte[] spr = new byte[256];
							byte[] sprPk = new byte[512];
							for (int bank = startBank; bank < endBank; bank++)
								for (int i = 0; i < 16; i++) {
									Array.Copy(buffer, numSpr * 256, spr, 0, 256);
									int l = new PackModule().Pack(spr, 256, sprPk, 0, pkMethod);
									sw2.WriteLine("SpriteHardPk" + numSpr.ToString("00"));
									SaveAsm.GenereDatas(sw2, sprPk, l, 16);
									numSpr++;
								}
							SavePaletteKitAsm(sw2);
							SaveAsm.CloseAsm(sw2);
							break;
					}
				}
				catch {
					main.DisplayErreur("Impossible de sauver les sprites.");
				}
				Enabled = true;
			}
		}

		private void bpSave_Click(object sender, EventArgs e) {
			SauveSprites();
		}

		private void bpReadPal_Click(object sender, EventArgs e) {
			OpenFileDialog dlg = new OpenFileDialog();
			dlg.Filter = "Palette CPC+ (.kit)|*.kit";
			if (dlg.ShowDialog() == DialogResult.OK) {
				try {
					main.ReadPaletteKit(dlg.FileName, colors);
					DrawMatrice();
				}
				catch {
					main.DisplayErreur("Impossible de lire palette.");
				}
			}
		}

		private void bpSavePal_Click(object sender, EventArgs e) {
			SaveFileDialog dlg = new SaveFileDialog();
			dlg.Filter = "Palette CPC+ (.kit)|*.kit";
			if (dlg.ShowDialog() == DialogResult.OK) {
				try {
					main.SavePaletteKit(dlg.FileName, BitmapCpc.paletteSprite);
				}
				catch {
					main.DisplayErreur("Impossible de sauver la palette.");
				}
			}
		}

		private void bpSaveAll_Click(object sender, EventArgs e) {
			SauveSprites(true);
		}

		private void DrawSpriteTest(DirectBitmap bmp, int spr, int ofsx, int ofsy) {
			int taillex = 1 << (int)zoomX.Value;
			int tailley = 2 << (int)zoomY.Value;
			for (int y = 0; y < 16; y++) {
				for (int x = 0; x < 16; x++) {
					for (int zx = 0; zx < taillex; zx++)
						for (int zy = 0; zy < tailley; zy++) {
							int col = BitmapCpc.paletteSprite[BitmapCpc.spritesHard[numBank, spr, x, y]];
							RvbColor c = new RvbColor((byte)((col & 0x0F) * 17), (byte)(((col & 0xF00) >> 8) * 17), (byte)(((col & 0xF0) >> 4) * 17));
							bmp.SetPixel(zx + (x * taillex) + ofsx, zy + (y * tailley) + ofsy, c);
						}
				}
			}
		}

		private void bpTest_Click(object sender, EventArgs e) {
			int nb = rb1Sprite.Checked ? 1 : rb2Sprite.Checked ? 2 : 4;
			int mask = rb1Sprite.Checked ? 0xF : rb2Sprite.Checked ? 0x0c : 0x00;
			int taillex = 1 << (int)zoomX.Value;
			int tailley = 2 << (int)zoomY.Value;
			int start = numSprite & mask;

			for (int y = 0; y < 512; y++)
				for (int x = 0; x < 512; x++)
					bmpTest.SetPixel(x, y, 0);

			for (int y = 0; y < nb; y++)
				for (int x = 0; x < nb; x++)
					DrawSpriteTest(bmpTest, start++, x * taillex * 16, y * tailley * 16);

			pictTest.Refresh();
		}

		private void comboBanque_SelectedIndexChanged(object sender, EventArgs e) {
			numBank = comboBanque.SelectedIndex;
			DrawMatrice();
		}
	}
}