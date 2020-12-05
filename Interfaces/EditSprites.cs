using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace ConvImgCpc {
	public partial class EditSprites : Form {
		private int numSprite = 0;
		private int penLeft = 1, penRight = 0;
		private DirectBitmap bmpSprite;
		private Main main;
		private Label[] colors = new Label[16];
		private int[, ,] spritesHard = new int[16, 16, 16];
		private int[] palette = new int[16];

		public EditSprites(Main m) {
			main = m;
			InitializeComponent();
			for (int c = 0; c < 16; c++) {
				// Générer les contrôles de "couleurs"
				colors[c] = new Label();
				colors[c].BorderStyle = BorderStyle.FixedSingle;
				colors[c].Location = new Point(830, 72 + c * 40);
				colors[c].Size = new Size(40, 32);
				colors[c].Tag = c;
				colors[c].MouseDown += ClickColor;
				int col = BitmapCpc.Palette[c];
				colors[c].BackColor = Color.FromArgb((byte)((col & 0x0F) * 17), (byte)(((col & 0xF00) >> 8) * 17), (byte)(((col & 0xF0) >> 4) * 17));
				Controls.Add(colors[c]);
				palette[c] = col;
			}
			bmpSprite = new DirectBitmap(pictEditMatrice.Width, pictEditMatrice.Height);
			pictEditMatrice.Image = bmpSprite.Bitmap;
			DrawMatrice();
			DrawSprite();
			DrawPens();
		}

		// Changement de la palette
		private void ClickColor(object sender, MouseEventArgs e) {
			Label colorClick = sender as Label;
			int pen = colorClick.Tag != null ? (int)colorClick.Tag : 0;
			if (e.Button == MouseButtons.Right) {
				int col = palette[pen];
				RvbColor colRvb = new RvbColor((byte)((col & 0x0F) * 17), (byte)(((col & 0xF00) >> 8) * 17), (byte)(((col & 0xF0) >> 4) * 17));
				EditColor ed = new EditColor(pen, col, colRvb.GetColor, true);
				ed.ShowDialog(this);
				if (ed.isValide) {
					palette[pen] = ed.ValColor;
					col = ed.ValColor;
					colors[pen].BackColor = Color.FromArgb((byte)((col & 0x0F) * 17), (byte)(((col & 0xF00) >> 8) * 17), (byte)(((col & 0xF0) >> 4) * 17));
					colors[pen].Refresh();
					DrawMatrice();
					DrawSprite();
				}
			}
			else {
				penLeft = pen;
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
								int col = palette[spritesHard[spr, x, y]];
								RvbColor c = new RvbColor((byte)((col & 0x0F) * 17), (byte)(((col & 0xF00) >> 8) * 17), (byte)(((col & 0xF0) >> 4) * 17));
								bmp.SetPixel(zx + ((x + (spr << 4)) << 2), zy + (y << 2), c);
							}
					}
				}
			}
			pictAllSprites.Refresh();
		}

		private void DrawSprite() {
			lblSelSprite.Text = "Sprite en cours : " + numSprite.ToString();
			bpPrev.Visible = numSprite > 0;
			bpSuiv.Visible = numSprite < 15;
			for (int y = 0; y < 16; y++) {
				for (int x = 0; x < 16; x++) {
					for (int zx = 0; zx < 38; zx++)
						for (int zy = 0; zy < 38; zy++) {
							int col = palette[spritesHard[numSprite, x, y]];
							RvbColor c = new RvbColor((byte)((col & 0x0F) * 17), (byte)(((col & 0xF00) >> 8) * 17), (byte)(((col & 0xF0) >> 4) * 17));
							bmpSprite.SetPixel(zx + (x * 40), zy + (y * 40), c);
						}
				}
			}
			pictEditMatrice.Refresh();
		}

		private void DrawPens() {
			int cl = palette[penLeft];
			int cr = palette[penRight];
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
					spritesHard[numSprite, x, y] = penLeft;
					DrawMatrice();
					DrawSprite();
				}
				else
					if (e.Button == MouseButtons.Right) {
						spritesHard[numSprite, x, y] = penRight;
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
			dlg.Filter = "Modèle Sprites (.xml)|*.xml";
			if (dlg.ShowDialog() == DialogResult.OK) {
				FileStream fileParam = File.Open(dlg.FileName, FileMode.Open);
				try {
					int[] sprite;
					sprite = (int[])new XmlSerializer(typeof(int[])).Deserialize(fileParam);
					int pos = 0;
					for (int i = 0; i < 16; i++)
						for (int y = 0; y < 16; y++)
							for (int x = 0; x < 16; x++)
								spritesHard[i, x, y] = sprite[pos++];

					DrawMatrice();
					DrawSprite();
				}
				catch {
					main.DisplayErreur("Impossible de lire la Sprite.");
				}
				fileParam.Close();
			}
		}

		private void bpSave_Click(object sender, EventArgs e) {
			SaveFileDialog dlg = new SaveFileDialog();
			dlg.Filter = "Modèle Sprites (.xml)|*.xml";
			if (dlg.ShowDialog() == DialogResult.OK) {
				int[] sprite = new int[4096];
				int pos = 0;
				for (int i = 0; i < 16; i++)
					for (int y = 0; y < 16; y++)
						for (int x = 0; x < 16; x++)
							sprite[pos++] = spritesHard[i, x, y];

				FileStream file = File.Open(dlg.FileName, FileMode.Create);
				try {
					new XmlSerializer(typeof(int[])).Serialize(file, sprite);
				}
				catch {
					main.DisplayErreur("Impossible de sauver les sprites.");
				}
				file.Close();
			}
		}

		private void DrawSpriteTest(DirectBitmap bmp, int spr, int ofsx, int ofsy) {
			int taillex = 1 << (int)zoomX.Value;
			int tailley = 2 << (int)zoomY.Value;
			for (int y = 0; y < 16; y++) {
				for (int x = 0; x < 16; x++) {
					for (int zx = 0; zx < taillex; zx++)
						for (int zy = 0; zy < tailley; zy++) {
							int col = palette[spritesHard[spr, x, y]];
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
	}
}