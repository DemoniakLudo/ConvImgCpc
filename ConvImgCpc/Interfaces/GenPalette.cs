using System;
using System.Drawing;
using System.Windows.Forms;

namespace ConvImgCpc {
	public partial class GenPalette : Form {
		private int[] palette;
		private int minStart;
		Action FctToDo;

		public GenPalette(int[] p, int ms, Action a) {
			InitializeComponent();
			palette = p;
			minStart = ms;
			txbFrom.Text = minStart.ToString();
			int cs = palette[minStart];
			txbStartR.Text = (cs & 0x0F).ToString();
			txbStartV.Text = (cs >> 8).ToString();
			txbStartB.Text = ((cs & 0xF0) >> 4).ToString();
			int ce = palette[15];
			txbEndR.Text = (ce & 0x0F).ToString();
			txbEndV.Text = (ce >> 8).ToString();
			txbEndB.Text = ((ce & 0xF0) >> 4).ToString();
			FctToDo = a;
		}

		private void trkStartR_Scroll(object sender, EventArgs e) {
			txbStartR.Text = trkStartR.Value.ToString();
			CalcPalette();
		}

		private void trkStartV_Scroll(object sender, EventArgs e) {
			txbStartV.Text = trkStartV.Value.ToString();
			CalcPalette();
		}

		private void trkStartB_Scroll(object sender, EventArgs e) {
			txbStartB.Text = trkStartB.Value.ToString();
			CalcPalette();
		}

		private void txbStartR_TextChanged(object sender, EventArgs e) {
			int i = -1;
			if (int.TryParse(txbStartR.Text, out i) && i >= 0 && i <= 15) {
				trkStartR.Value = i;
				lblStartColor.BackColor = Color.FromArgb(trkStartR.Value * 17, trkStartV.Value * 17, trkStartB.Value * 17);
			}
		}

		private void txbStartV_TextChanged(object sender, EventArgs e) {
			int i = -1;
			if (int.TryParse(txbStartV.Text, out i) && i >= 0 && i <= 15) {
				trkStartV.Value = i;
				lblStartColor.BackColor = Color.FromArgb(trkStartR.Value * 17, trkStartV.Value * 17, trkStartB.Value * 17);
			}
		}

		private void txbStartB_TextChanged(object sender, EventArgs e) {
			int i = -1;
			if (int.TryParse(txbStartB.Text, out i) && i >= 0 && i <= 15) {
				trkStartB.Value = i;
				lblStartColor.BackColor = Color.FromArgb(trkStartR.Value * 17, trkStartV.Value * 17, trkStartB.Value * 17);
			}
		}

		private void trkEndR_Scroll(object sender, EventArgs e) {
			txbEndR.Text = trkEndR.Value.ToString();
			CalcPalette();
		}

		private void trkEndV_Scroll(object sender, EventArgs e) {
			txbEndV.Text = trkEndV.Value.ToString();
			CalcPalette();
		}

		private void trkEndB_Scroll(object sender, EventArgs e) {
			txbEndB.Text = trkEndB.Value.ToString();
			CalcPalette();
		}

		private void txbEndR_TextChanged(object sender, EventArgs e) {
			int i = -1;
			if (int.TryParse(txbEndR.Text, out i) && i >= 0 && i <= 15) {
				trkEndR.Value = i;
				lblEndColor.BackColor = Color.FromArgb(trkEndR.Value * 17, trkEndV.Value * 17, trkEndB.Value * 17);
			}
		}

		private void txbEndV_TextChanged(object sender, EventArgs e) {
			int i = -1;
			if (int.TryParse(txbEndV.Text, out i) && i >= 0 && i <= 15) {
				trkEndV.Value = i;
				lblEndColor.BackColor = Color.FromArgb(trkEndR.Value * 17, trkEndV.Value * 17, trkEndB.Value * 17);
			}
		}

		private void txbEndB_TextChanged(object sender, EventArgs e) {
			int i = -1;
			if (int.TryParse(txbEndB.Text, out i) && i >= 0 && i <= 15) {
				trkEndB.Value = i;
				lblEndColor.BackColor = Color.FromArgb(trkEndR.Value * 17, trkEndV.Value * 17, trkEndB.Value * 17);
			}
		}

		private void bpGenerate_Click(object sender, EventArgs e) {
			CalcPalette();
			if (FctToDo == null)
				Close();
		}

		private void CalcPalette() {
			int start = 0, end = 0;
			double rs = 0, vs = 0, bs = 0, re = 0, ve = 0, be = 0;
			if (int.TryParse(txbFrom.Text, out start) && int.TryParse(txbTo.Text, out end) && start >= minStart && end <= 15 && start < end) {
				if (double.TryParse(txbStartR.Text, out rs) && double.TryParse(txbStartV.Text, out vs) && double.TryParse(txbStartB.Text, out bs)
					&& double.TryParse(txbEndR.Text, out re) && double.TryParse(txbEndV.Text, out ve) && double.TryParse(txbEndB.Text, out be)) {
					if (rs >= 0 && vs >= 0 && bs >= 0 && re >= 0 && ve >= 0 && be >= 0 && rs < 16 && vs < 16 && bs < 16 && re < 16 && ve < 16 && be < 16) {
						rs *= 17;
						vs *= 17;
						bs *= 17;
						re *= 17;
						ve *= 17;
						be *= 17;
						double kr = (re - rs) / (end - start);
						double kv = (ve - vs) / (end - start);
						double kb = (be - bs) / (end - start);
						for (int i = start; i < end; i++) {
							palette[i] = (int)((rs / 17) + ((int)(bs / 17) << 4) + ((int)(vs / 17) << 8));
							rs += kr;
							bs += kb;
							vs += kv;
						}
						palette[end] = (int)((re / 17) + ((int)(be / 17) << 4) + ((int)(ve / 17) << 8));
					}
				}
			}
			if (FctToDo != null)
				FctToDo();
		}

		private void bpGetCol_Click(object sender, EventArgs e) {
			int start = 0;
			if (int.TryParse(txbFrom.Text, out start)) {
				int cs = palette[start];
				txbStartR.Text = (cs & 0x0F).ToString();
				txbStartV.Text = (cs >> 8).ToString();
				txbStartB.Text = ((cs & 0xF0) >> 4).ToString();
			}
		}
	}
}
