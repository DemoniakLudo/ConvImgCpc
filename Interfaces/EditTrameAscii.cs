﻿using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace ConvImgCpc {
	public partial class EditTrameAscii : Form {
		private BitmapCpc bmpCpc;
		private int numTrame = 0;
		private int penLeft = 1, penRight = 0;
		private DirectBitmap bmpTrame;

		public EditTrameAscii(BitmapCpc b) {
			bmpCpc = b;
			InitializeComponent();
			bmpTrame = new DirectBitmap(pictEditMatrice.Width, pictEditMatrice.Height);
			pictEditMatrice.Image = bmpTrame.Bitmap;
			DrawMatrice();
			DrawTrame();
			lblPen0.BackColor = Color.FromArgb(BitmapCpc.RgbCPC[bmpCpc.Palette[0]].GetColorArgb);
			lblPen1.BackColor = Color.FromArgb(BitmapCpc.RgbCPC[bmpCpc.Palette[1]].GetColorArgb);
			lblPen2.BackColor = Color.FromArgb(BitmapCpc.RgbCPC[bmpCpc.Palette[2]].GetColorArgb);
			lblPen3.BackColor = Color.FromArgb(BitmapCpc.RgbCPC[bmpCpc.Palette[3]].GetColorArgb);
			DrawPens();
		}

		private void DrawMatrice() {
			DirectBitmap bmp = new DirectBitmap(pictAllMatrice.Width, pictAllMatrice.Height);
			pictAllMatrice.Image = bmp.Bitmap;
			for (int i = 0; i < 16; i++) {
				for (int y = 0; y < 4; y++) {
					for (int x = 0; x < 4; x++) {
						for (int zx = 0; zx < (x == 3 ? 7 : 8); zx++)
							for (int zy = 0; zy < 8; zy++)
								bmp.SetPixel(zx + ((x + (i << 2)) << 3), zy + (y << 3), BitmapCpc.RgbCPC[bmpCpc.Palette[BitmapCpc.trameM1[i, x, y]]]);
					}
				}
			}
			pictAllMatrice.Refresh();
		}

		private void DrawTrame() {
			bpPrev.Visible = numTrame > 0;
			bpSuiv.Visible = numTrame < 15;
			for (int y = 0; y < 4; y++) {
				for (int x = 0; x < 4; x++) {
					for (int zx = 0; zx < 78; zx++)
						for (int zy = 0; zy < 78; zy++)
							bmpTrame.SetPixel(zx + (x * 80), zy + (y * 80), BitmapCpc.RgbCPC[bmpCpc.Palette[BitmapCpc.trameM1[numTrame, x, y]]]);
				}
			}
			pictEditMatrice.Refresh();
		}

		private void DrawPens() {
			lblPenLeft.BackColor = Color.FromArgb(BitmapCpc.RgbCPC[bmpCpc.Palette[penLeft]].GetColorArgb);
			lblPenRight.BackColor = Color.FromArgb(BitmapCpc.RgbCPC[bmpCpc.Palette[penRight]].GetColorArgb);
		}

		private void pictAllMatrice_MouseDown(object sender, MouseEventArgs e) {
			int y = e.X / 32;
			if (y >= 0 && y < 16) {
				numTrame = y;
				DrawTrame();
			}
		}

		private void pictEditMatrice_MouseMove(object sender, MouseEventArgs e) {
			int x = e.X / 80;
			int y = e.Y / 80;
			if (x >= 0 && y >= 0 && x < 4 && y < 4) {
				if (e.Button == MouseButtons.Left) {
					BitmapCpc.trameM1[numTrame, x, y] = penLeft;
					DrawMatrice();
					DrawTrame();
				}
				else
					if (e.Button == MouseButtons.Right) {
						BitmapCpc.trameM1[numTrame, x, y] = penRight;
						DrawMatrice();
						DrawTrame();
					}
			}
		}

		private void bpPrev_Click(object sender, EventArgs e) {
			numTrame--;
			DrawTrame();
		}

		private void bpSuiv_Click(object sender, EventArgs e) {
			numTrame++;
			DrawTrame();
		}

		private void lblPen0_Click(object sender, MouseEventArgs e) {
			if (e.Button == MouseButtons.Left)
				penLeft = 0;
			else
				penRight = 0;

			DrawPens();
		}

		private void lblPen1_Click(object sender, MouseEventArgs e) {
			if (e.Button == MouseButtons.Left)
				penLeft = 1;
			else
				penRight = 1;

			DrawPens();
		}

		private void lblPen2_Click(object sender, MouseEventArgs e) {
			if (e.Button == MouseButtons.Left)
				penLeft = 2;
			else
				penRight = 2;

			DrawPens();
		}

		private void lblPen3_Click(object sender, MouseEventArgs e) {
			if (e.Button == MouseButtons.Left)
				penLeft = 3;
			else
				penRight = 3;

			DrawPens();
		}

		private void bpRead_Click(object sender, EventArgs e) {
			OpenFileDialog dlg = new OpenFileDialog();
			dlg.Filter = "Modèle trames (.xml)|*.xml";
			if (dlg.ShowDialog() == DialogResult.OK) {
				FileStream fileParam = File.Open(dlg.FileName, FileMode.Open);
				try {
					int[] trame;
					trame = (int[])new XmlSerializer(typeof(int[])).Deserialize(fileParam);
					int pos = 0;
					for (int i = 0; i < 16; i++)
						for (int y = 0; y < 4; y++)
							for (int x = 0; x < 4; x++)
								BitmapCpc.trameM1[i, x, y] = trame[pos++];

					DrawMatrice();
					DrawTrame();
				}
				catch (Exception) {
				}
				fileParam.Close();
			}
		}

		private void bpSave_Click(object sender, EventArgs e) {
			SaveFileDialog dlg = new SaveFileDialog();
			dlg.Filter = "Modèle trames (.xml)|*.xml";
			if (dlg.ShowDialog() == DialogResult.OK) {
				int[] trame = new int[256];
				int pos = 0;
				for (int i = 0; i < 16; i++)
					for (int y = 0; y < 4; y++)
						for (int x = 0; x < 4; x++)
							trame[pos++] = BitmapCpc.trameM1[i, x, y];

				FileStream file = File.Open(dlg.FileName, FileMode.Create);
				try {
					new XmlSerializer(typeof(int[])).Serialize(file, trame);
				}
				catch (Exception) {
				}
				file.Close();
			}
		}
	}
}