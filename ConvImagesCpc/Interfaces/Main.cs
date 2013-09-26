using ConvImgCpc;
using System.Drawing;
using System.Windows.Forms;

namespace CpcConvImg {
	public partial class Main: Form {
		private ImageSource imgSrc;
		private ImageCpc imgCpc;

		public Main() {
			InitializeComponent();
			imgSrc = new ImageSource();
			imgCpc = new ImageCpc();
			nbCols.Value = imgCpc.bitmapCpc.TailleX >> 3;
			nbLignes.Value = imgCpc.bitmapCpc.TailleY >> 1;
			mode.SelectedIndex = imgCpc.bitmapCpc.ModeCPC;
			methode.SelectedIndex = 0;
			matrice.SelectedIndex = 0;
			renderMode.SelectedIndex = 0;
		}

		private void checkImageSource_CheckedChanged(object sender, System.EventArgs e) {
			imgSrc.Visible = checkImageSource.Checked;
		}

		private void UpdateImgCPC() {
			if (checkImageCPC.Checked)
				imgCpc.Render();
		}

		private void checkImageCPC_CheckedChanged(object sender, System.EventArgs e) {
			UpdateImgCPC();
			imgCpc.Visible = checkImageCPC.Checked;
		}

		private void bpReadSrc_Click(object sender, System.EventArgs e) {
			OpenFileDialog dlg = new OpenFileDialog();
			dlg.Filter = "Images (*.bmp, *.gif, *.png, *.jpg)|*.bmp;*.gif;*.png;*.jpg";
			DialogResult result = dlg.ShowDialog();
			if (result == DialogResult.OK) {
				imgSrc.SetBitmap(new Bitmap(dlg.FileName), checkImageSource.Checked);
				bpConvert_Click(autoRecalc.Checked ? sender : null, e);
			}
		}

		private void bpConvert_Click(object sender, System.EventArgs e) {
			if (imgSrc.GetImage != null && sender != null) {
				bpConvert.Enabled = false;
				imgCpc.Reset();
				Conversion.Convert(imgSrc.GetImage,
									imgCpc.bitmapCpc,
									radioKeepLarger.Checked ? Conversion.SizeMode.KeepLarger : radioKeepSmaller.Checked ? Conversion.SizeMode.KeepSmaller : Conversion.SizeMode.Fit,
									methode.SelectedIndex,
									matrice.SelectedIndex + 2,
									(int)pctTrame.Value,
									imgCpc.lockState,
									(int)lumi.Value,
									nb.Checked ? 0 : (int)sat.Value,
									(int)contrast.Value,
									modePlus.Checked,
									newMethode.Checked,
									reducPal1.Checked,
									reducPal2.Checked,
									newReduc.Checked,
									sortPal.Checked,
									renderMode.SelectedIndex);
				bpConvert.Enabled = true;
			}
			UpdateImgCPC();
		}

		private void nbCols_ValueChanged(object sender, System.EventArgs e) {
			imgCpc.bitmapCpc.TailleX = (int)nbCols.Value << 3;
			imgCpc.Reset();
			bpConvert_Click(autoRecalc.Checked ? sender : null, e);
		}

		private void nbLignes_ValueChanged(object sender, System.EventArgs e) {
			imgCpc.bitmapCpc.TailleY = (int)nbLignes.Value << 1;
			imgCpc.Reset();
			bpConvert_Click(autoRecalc.Checked ? sender : null, e);
		}

		private void mode_SelectedIndexChanged(object sender, System.EventArgs e) {
			imgCpc.bitmapCpc.ModeCPC = int.Parse(mode.SelectedItem.ToString());
			imgCpc.Reset();
			bpConvert_Click(autoRecalc.Checked ? sender : null, e);
		}

		private void modePlus_CheckedChanged(object sender, System.EventArgs e) {
			imgCpc.bitmapCpc.cpcPlus = modePlus.Checked;
			newMethode.Enabled = !modePlus.Checked;
			reducPal1.Enabled = reducPal2.Enabled = modePlus.Checked;
			bpConvert_Click(autoRecalc.Checked ? sender : null, e);
		}

		private void radioFit_CheckedChanged(object sender, System.EventArgs e) {
			bpConvert_Click(autoRecalc.Checked ? sender : null, e);
		}

		private void radioKeepSmaller_CheckedChanged(object sender, System.EventArgs e) {
			bpConvert_Click(autoRecalc.Checked ? sender : null, e);
		}

		private void radioKeepLarger_CheckedChanged(object sender, System.EventArgs e) {
			bpConvert_Click(autoRecalc.Checked ? sender : null, e);
		}

		private void pctTrame_ValueChanged(object sender, System.EventArgs e) {
			bpConvert_Click(autoRecalc.Checked ? sender : null, e);
		}

		private void methode_SelectedIndexChanged(object sender, System.EventArgs e) {
			bpConvert_Click(autoRecalc.Checked ? sender : null, e);
		}

		private void matrice_SelectedIndexChanged(object sender, System.EventArgs e) {
			bpConvert_Click(autoRecalc.Checked ? sender : null, e);
		}

		private void renderMode_SelectedIndexChanged(object sender, System.EventArgs e) {
			bpConvert_Click(autoRecalc.Checked ? sender : null, e);
		}

		private void lumi_ValueChanged(object sender, System.EventArgs e) {
			bpConvert_Click(autoRecalc.Checked ? sender : null, e);
		}

		private void sat_ValueChanged(object sender, System.EventArgs e) {
			bpConvert_Click(autoRecalc.Checked ? sender : null, e);
		}

		private void contrast_ValueChanged(object sender, System.EventArgs e) {
			bpConvert_Click(autoRecalc.Checked ? sender : null, e);
		}

		private void sortPal_CheckedChanged(object sender, System.EventArgs e) {
			bpConvert_Click(autoRecalc.Checked ? sender : null, e);
		}

		private void newMethode_CheckedChanged(object sender, System.EventArgs e) {
			bpConvert_Click(autoRecalc.Checked ? sender : null, e);
		}

		private void cpcPlus_CheckedChanged(object sender, System.EventArgs e) {
			reducPal1.Enabled = reducPal2.Enabled = modePlus.Checked;
			bpConvert_Click(autoRecalc.Checked ? sender : null, e);
		}

		private void nb_CheckedChanged(object sender, System.EventArgs e) {
			bpRazSat.Enabled = sat.Enabled = !nb.Checked;
			bpConvert_Click(autoRecalc.Checked ? sender : null, e);
		}

		private void reducPal1_CheckedChanged(object sender, System.EventArgs e) {
			newReduc.Enabled = reducPal1.Checked || reducPal2.Checked;
			bpConvert_Click(autoRecalc.Checked ? sender : null, e);
		}

		private void reducPal2_CheckedChanged(object sender, System.EventArgs e) {
			newReduc.Enabled = reducPal1.Checked || reducPal2.Checked;
			bpConvert_Click(autoRecalc.Checked ? sender : null, e);
		}

		private void newReduc_CheckedChanged(object sender, System.EventArgs e) {
			bpConvert_Click(autoRecalc.Checked ? sender : null, e);
		}

		private void bpRazLumi_Click(object sender, System.EventArgs e) {
			lumi.Value = 100;
		}

		private void bpRazSat_Click(object sender, System.EventArgs e) {
			sat.Value = 100;
		}

		private void bpRazContrast_Click(object sender, System.EventArgs e) {
			contrast.Value = 100;
		}

		private void autoRecalc_CheckedChanged(object sender, System.EventArgs e) {
			bpConvert_Click(autoRecalc.Checked ? sender : null, e);
		}
	}
}
