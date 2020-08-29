using System;
using System.Windows.Forms;

namespace ConvImgCpc {
	public partial class EffetsPalette : Form {
		private Main main;

		public EffetsPalette(Main m) {
			InitializeComponent();
			main = m;
		}

		public void InitValues() {
			lumi.Value = main.param.pctLumi;
			sat.Value = main.param.pctSat;
			contrast.Value = main.param.pctContrast;
			red.Value = main.param.pctRed;
			green.Value = main.param.pctGreen;
			blue.Value = main.param.pctBlue;
			modePlus.Checked = main.param.cpcPlus;
			newMethode.Visible = !modePlus.Checked;
			newMethode.Checked = main.param.newMethode;
			reducPal1.Visible = reducPal2.Visible = reducPal3.Visible = reducPal4.Visible = modePlus.Checked;
			reducPal1.Checked = main.param.reductPal1;
			reducPal2.Checked = main.param.reductPal2;
			reducPal3.Checked = main.param.reductPal3;
			reducPal4.Checked = main.param.reductPal4;
			sortPal.Checked = main.param.sortPal;
		}

		private void red_ValueChanged(object sender, EventArgs e) {
			main.param.pctRed = red.Value;
			main.Convert(false);
		}

		private void green_ValueChanged(object sender, EventArgs e) {
			main.param.pctGreen = green.Value;
			main.Convert(false);
		}

		private void blue_ValueChanged(object sender, EventArgs e) {
			main.param.pctBlue = blue.Value;
			main.Convert(false);
		}

		private void bpRmoins_Click(object sender, EventArgs e) {
			if (red.Value > 0)
				red.Value--;
		}

		private void bpVmoins_Click(object sender, EventArgs e) {
			if (green.Value > 0)
				green.Value--;
		}

		private void bpBmoins_Click(object sender, EventArgs e) {
			if (blue.Value > 0)
				blue.Value--;
		}

		private void bpRplus_Click(object sender, EventArgs e) {
			if (red.Value < 200)
				red.Value++;
		}

		private void bpVplus_Click(object sender, EventArgs e) {
			if (green.Value < 200)
				green.Value++;
		}

		private void bpBplus_Click(object sender, EventArgs e) {
			if (blue.Value < 200)
				blue.Value++;
		}

		private void RazR_Click(object sender, EventArgs e) {
			main.param.pctRed = red.Value = 100;
			main.Convert(false);
		}

		private void RazV_Click(object sender, EventArgs e) {
			main.param.pctGreen = green.Value = 100;
			main.Convert(false);
		}

		private void RazB_Click(object sender, EventArgs e) {
			main.param.pctBlue = blue.Value = 100;
			main.Convert(false);
		}

		private void lumi_ValueChanged(object sender, EventArgs e) {
			main.param.pctLumi = (int)lumi.Value;
			main.Convert(false);
		}

		private void sat_ValueChanged(object sender, EventArgs e) {
			main.param.pctSat = nb.Checked ? 0 : (int)sat.Value;
			main.Convert(false);
		}

		private void contrast_ValueChanged(object sender, EventArgs e) {
			main.param.pctContrast = (int)contrast.Value;
			main.Convert(false);
		}

		private void bpLumMoins_Click(object sender, EventArgs e) {
			if (lumi.Value > lumi.Minimum)
				lumi.Value = lumi.Value - 1;
		}

		private void bpLumPlus_Click(object sender, EventArgs e) {
			if (lumi.Value < lumi.Maximum)
				lumi.Value = lumi.Value + 1;
		}

		private void bpSatMoins_Click(object sender, EventArgs e) {
			if (sat.Value > sat.Minimum)
				sat.Value = sat.Value - 1;
		}

		private void bpSatPlus_Click(object sender, EventArgs e) {
			if (sat.Value < sat.Maximum)
				sat.Value = sat.Value + 1;
		}

		private void bpCtrstMoins_Click(object sender, EventArgs e) {
			if (contrast.Value > contrast.Minimum)
				contrast.Value = contrast.Value - 1;
		}

		private void bpCtrstPlus_Click(object sender, EventArgs e) {
			if (contrast.Value < contrast.Maximum)
				contrast.Value = contrast.Value + 1;
		}

		private void bpRazLumi_Click(object sender, EventArgs e) {
			main.param.pctLumi = lumi.Value = 100;
			main.Convert(false);
		}

		private void bpRazSat_Click(object sender, EventArgs e) {
			main.param.pctSat = sat.Value = 100;
			main.Convert(false);
		}

		private void bpRazContrast_Click(object sender, EventArgs e) {
			main.param.pctContrast = contrast.Value = 100;
			main.Convert(false);
		}

		private void sortPal_CheckedChanged(object sender, EventArgs e) {
			main.param.sortPal = sortPal.Checked;
			main.Convert(false);
		}

		private void newMethode_CheckedChanged(object sender, EventArgs e) {
			main.param.newMethode = newMethode.Checked;
			main.Convert(false);
		}

		private void nb_CheckedChanged(object sender, EventArgs e) {
			bpSatMoins.Enabled = bpSatPlus.Enabled = bpRazSat.Enabled = sat.Enabled = !nb.Checked;
			main.param.pctSat = nb.Checked ? 0 : (int)sat.Value;
			main.Convert(false);
		}

		private void reducPal1_CheckedChanged(object sender, EventArgs e) {
			main.param.reductPal1 = reducPal1.Checked;
			main.Convert(false);
		}

		private void reducPal2_CheckedChanged(object sender, EventArgs e) {
			main.param.reductPal2 = reducPal2.Checked;
			main.Convert(false);
		}

		private void reducPal3_CheckedChanged(object sender, EventArgs e) {
			main.param.reductPal3 = reducPal3.Checked;
			main.Convert(false);
		}

		private void reducPal4_CheckedChanged(object sender, EventArgs e) {
			main.param.reductPal4 = reducPal4.Checked;
			main.Convert(false);
		}

		private void modePlus_CheckedChanged(object sender, EventArgs e) {
			BitmapCpc.cpcPlus = modePlus.Checked;
			newMethode.Visible = !modePlus.Checked;
			reducPal1.Visible = reducPal2.Visible = reducPal3.Visible = reducPal4.Visible = modePlus.Checked;
			main.param.cpcPlus = modePlus.Checked;
			main.Convert(false);
		}
	}
}
