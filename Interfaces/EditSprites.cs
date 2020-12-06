using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace ConvImgCpc {
	public partial class EditSprites : Form {
		private int numSprite = 0, numBank = 0;
		private byte penLeft = 1;
		private DirectBitmap bmpSprite;
		private Main main;
		private Label[] colors = new Label[16];

		public EditSprites(Main m) {
			InitializeComponent();
			main = m;
			for (int c = 0; c < 16; c++) {
				// Générer les contrôles de "couleurs"
				colors[c] = new Label();
				colors[c].BorderStyle = BorderStyle.FixedSingle;
				colors[c].Location = new Point(730, 72 + c * 40);
				colors[c].Size = new Size(40, 32);
				colors[c].Tag = c;
				colors[c].MouseDown += ClickColor;
				if (BitmapCpc.paletteSprite[c] == 0)
					BitmapCpc.paletteSprite[c] = BitmapCpc.Palette[c];

				int col = BitmapCpc.paletteSprite[c];
				colors[c].BackColor = Color.FromArgb((byte)((col & 0x0F) * 17), (byte)(((col & 0xF00) >> 8) * 17), (byte)(((col & 0xF0) >> 4) * 17));
				Controls.Add(colors[c]);
			}
			bmpSprite = new DirectBitmap(pictEditMatrice.Width, pictEditMatrice.Height);
			pictEditMatrice.Image = bmpSprite.Bitmap;
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
				EditColor ed = new EditColor(pen, col, colRvb.GetColor, true);
				ed.ShowDialog(this);
				if (ed.isValide) {
					BitmapCpc.paletteSprite[pen] = ed.ValColor;
					col = ed.ValColor;
					colors[pen].BackColor = Color.FromArgb((byte)((col & 0x0F) * 17), (byte)(((col & 0xF00) >> 8) * 17), (byte)(((col & 0xF0) >> 4) * 17));
					colors[pen].Refresh();
					DrawMatrice();
					DrawSprite();
				}
			}
			else {
				penLeft = (byte)pen;
				DrawPens();
			}
		}

		private void DrawMatrice() {
			DirectBitmap bmp = new DirectBitmap(pictAllSprites.Width, pictAllSprites.Height);
			pictAllSprites.Image = bmp.Bitmap;
			for (int spr = 0; spr < 16; spr++) {
				for (int y = 0; y < 16; y++) {
					for (int x = 0; x < 16; x++) {
						for (int zx = 0; zx < (x == 15 ? 3 : 4); zx++)
							for (int zy = 0; zy < 4; zy++) {
								int col = BitmapCpc.paletteSprite[BitmapCpc.spritesHard[numBank, spr, x, y]];
								RvbColor c = new RvbColor((byte)((col & 0x0F) * 17), (byte)(((col & 0xF00) >> 8) * 17), (byte)(((col & 0xF0) >> 4) * 17));
								bmp.SetPixel(zx + ((x + (spr << 4)) << 2), zy + (y << 2), c);
							}
					}
				}
			}
			pictAllSprites.Refresh();
		}

		private void DrawSprite() {
			lblSelSprite.Text = "Sprite n° : " + numSprite.ToString();
			bpPrev.Visible = numSprite > 0;
			bpSuiv.Visible = numSprite < 15;
			for (int y = 0; y < 16; y++) {
				for (int x = 0; x < 16; x++) {
					for (int zx = 0; zx < 38; zx++)
						for (int zy = 0; zy < 38; zy++) {
							int col = BitmapCpc.paletteSprite[BitmapCpc.spritesHard[numBank, numSprite, x, y]];
							RvbColor c = new RvbColor((byte)((col & 0x0F) * 17), (byte)(((col & 0xF00) >> 8) * 17), (byte)(((col & 0xF0) >> 4) * 17));
							bmpSprite.SetPixel(zx + (x * 40), zy + (y * 40), c);
						}
				}
			}
			pictEditMatrice.Refresh();
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
				numSprite = y;
				DrawSprite();
			}
		}

		private void pictEditMatrice_MouseMove(object sender, MouseEventArgs e) {
			int x = e.X / 40;
			int y = e.Y / 40;
			if (x >= 0 && y >= 0 && x < 16 && y < 16) {
				if (e.Button == MouseButtons.Left) {
					BitmapCpc.spritesHard[numBank, numSprite, x, y] = penLeft;
					DrawMatrice();
					DrawSprite();
				}
				else
					if (e.Button == MouseButtons.Right) {
						BitmapCpc.spritesHard[numBank, numSprite, x, y] = 0;
						DrawMatrice();
						DrawSprite();
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
				//try {
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
				string filePalette = System.IO.Path.ChangeExtension(dlg.FileName, "kit");
				if (File.Exists(filePalette)) {
					fileScr = new FileStream(filePalette, FileMode.Open, FileAccess.Read);
					tabBytes = new byte[fileScr.Length];
					fileScr.Read(tabBytes, 0, tabBytes.Length);
					fileScr.Close();
					if (CpcSystem.CheckAmsdos(tabBytes)) {
						BitmapCpc.paletteSprite[0] = 0;
						colors[0].BackColor = Color.Black;
						colors[0].Refresh();
						for (int i = 0; i < 15; i++) {
							int kit = tabBytes[128 + (i << 1)] + (tabBytes[129 + (i << 1)] << 8);
							int col = (kit & 0xF00) + ((kit & 0x0F) << 4) + ((kit & 0xF0) >> 4);
							BitmapCpc.paletteSprite[i + 1] = col;
							colors[i + 1].BackColor = Color.FromArgb((byte)((col & 0x0F) * 17), (byte)(((col & 0xF00) >> 8) * 17), (byte)(((col & 0xF0) >> 4) * 17));
							colors[i + 1].Refresh();
						}
					}
					//}
					//catch {
					//	main.DisplayErreur("Impossible de lire les sprites.");
					//}}}
				}
				DrawMatrice();
				DrawSprite();
			}
		}

		private void SauveSprites(bool allBank = false) {
			SaveFileDialog dlg = new SaveFileDialog();
			dlg.Filter = "Modèle Sprites (.spr)|*.spr";
			if (dlg.ShowDialog() == DialogResult.OK) {
				try {
					short size = (short)(allBank ? 0x4000 : 0x1000);
					CpcAmsdos entete = CpcSystem.CreeEntete(Path.GetFileName(dlg.FileName), 0x4000, size, 0);
					BinaryWriter fp = new BinaryWriter(new FileStream(dlg.FileName, FileMode.Create));
					fp.Write(CpcSystem.AmsdosToByte(entete));
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
					fp.Write(buffer);
					fp.Close();
					// Sauvegarde palette au format .KIT
					string filePalette = System.IO.Path.ChangeExtension(dlg.FileName, "kit");
					entete = CpcSystem.CreeEntete(Path.GetFileName(filePalette), -32768, 30, 0);
					fp = new BinaryWriter(new FileStream(filePalette, FileMode.Create));
					fp.Write(CpcSystem.AmsdosToByte(entete));
					for (int i = 0; i < 15; i++) {
						int kit = BitmapCpc.paletteSprite[i + 1];
						byte c1 = (byte)(((kit & 0x0F) << 4) + ((kit & 0xF0) >> 4));
						byte c2 = (byte)(kit >> 8);
						fp.Write(c1);
						fp.Write(c2);
					}
					fp.Close();
				}
				catch {
					main.DisplayErreur("Impossible de sauver les sprites.");
				}
			}
		}

		private void bpSave_Click(object sender, EventArgs e) {
			SauveSprites();
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
			DirectBitmap bmp = new DirectBitmap(pictTest.Width, pictTest.Height);
			pictTest.Image = bmp.Bitmap;
			int nb = rb1Sprite.Checked ? 1 : rb2Sprite.Checked ? 2 : 4;
			int mask = rb1Sprite.Checked ? 0xF : rb2Sprite.Checked ? 0x0c : 0x00;
			int taillex = 1 << (int)zoomX.Value;
			int tailley = 2 << (int)zoomY.Value;
			int start = numSprite & mask;
			for (int y = 0; y < nb; y++)
				for (int x = 0; x < nb; x++)
					DrawSpriteTest(bmp, start++, x * taillex * 16, y * tailley * 16);
		}

		private void comboBanque_SelectedIndexChanged(object sender, EventArgs e) {
			numBank = comboBanque.SelectedIndex;
			DrawMatrice();
			DrawSprite();
		}
	}
}