using System;
using System.Collections.Generic;
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
		private ImageCpc imgCpc;
		private ImageSource imgSrc;
		private Param param;
		private Main main;

		public EditTrameAscii(Main m, ImageSource s, ImageCpc i, Param p) {
			main = m;
			imgSrc = s;
			imgCpc = i;
			bmpCpc = i.bitmapCpc;
			param = p;
			InitializeComponent();
			bmpTrame = new DirectBitmap(pictEditMatrice.Width, pictEditMatrice.Height);
			pictEditMatrice.Image = bmpTrame.Bitmap;
			DrawMatrice();
			DrawTrame();
			lblPen0.BackColor = Color.FromArgb(bmpCpc.GetColorPal(0).GetColorArgb);
			lblPen1.BackColor = Color.FromArgb(bmpCpc.GetColorPal(1).GetColorArgb);
			lblPen2.BackColor = Color.FromArgb(bmpCpc.GetColorPal(2).GetColorArgb);
			lblPen3.BackColor = Color.FromArgb(bmpCpc.GetColorPal(3).GetColorArgb);
			DrawPens();
		}

		private void DrawMatrice() {
			DirectBitmap bmp = new DirectBitmap(pictAllMatrice.Width, pictAllMatrice.Height);
			pictAllMatrice.Image = bmp.Bitmap;
			for (int i = 0; i < 16; i++) {
				for (int y = 0; y < 4; y++) {
					for (int x = 0; x < 4; x++) {
						for (int zx = 0; zx < (x == 3 ? 7 : 8); zx++)
							for (int zy = 0; zy < 8; zy++) {
								RvbColor c = bmpCpc.GetColorPal(BitmapCpc.trameM1[i, x, y]);
								bmp.SetPixel(zx + ((x + (i << 2)) << 3), zy + (y << 3), c);
							}
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
						for (int zy = 0; zy < 78; zy++) {
							RvbColor c = bmpCpc.GetColorPal(BitmapCpc.trameM1[numTrame, x, y]);
							bmpTrame.SetPixel(zx + (x * 80), zy + (y * 80), c);
						}
				}
			}
			pictEditMatrice.Refresh();
		}

		private void DrawPens() {
			lblPenLeft.BackColor = Color.FromArgb(bmpCpc.GetColorPal(penLeft).GetColorArgb);
			lblPenRight.BackColor = Color.FromArgb(bmpCpc.GetColorPal(penRight).GetColorArgb);
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

		private void bpAutoGene_Click(object sender, EventArgs e) {
			Enabled = false;
			int nbImages = main.GetMaxImages();
			List<TrameM1> lstTrame = new List<TrameM1>();
			for (int i = 0; i < nbImages; i++) {
				main.SelectImage(i);
				DirectBitmap tmp = main.GetResizeBitmap();
				Conversion.CnvTrame(tmp, imgCpc, lstTrame, param);
				tmp.Dispose();
				lstTrame.Sort((x, y) => y.nbFound - x.nbFound);
			}
			int maxTrame = Math.Min(16, lstTrame.Count);
			for (int i = 0; i < maxTrame; i++)
				for (int y = 0; y < 4; y++)
					for (int x = 0; x < 4; x++)
						BitmapCpc.trameM1[i, x, y] = lstTrame[i].GetPix(x, y);

			DrawMatrice();
			DrawTrame();
			Enabled = true;
		}
	}

	public class TrameM1 {
		int[,] trame = new int[4, 4];
		public int nbFound = 0;

		public void SetPix(int x, int y, int p) {
			trame[x, y] = p;
		}

		public int GetPix(int x, int y) {
			return trame[x, y];
		}

		public int GetNbFound() {
			return nbFound;
		}

		public bool IsSame(TrameM1 t) {
			bool same = true;
			for (int y = 0; y < 4; y++)
				for (int x = 0; x < 4; x++)
					if (trame[x, y] != t.GetPix(x, y))
						return false;

			if (same)
				nbFound++;

			return same;
		}
	}
}
