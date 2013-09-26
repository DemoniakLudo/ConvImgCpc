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
			if (result == DialogResult.OK)
				imgSrc.SetBitmap(new Bitmap(dlg.FileName), checkImageSource.Checked);
		}

		private void bpConvert_Click(object sender, System.EventArgs e) {
			if (imgSrc.GetImage != null) {
				bpConvert.Enabled = false;
				imgCpc.Reset();
				Conversion.Convert(imgSrc.GetImage,
									imgCpc.bitmapCpc,
									Conversion.SizeMode.KeepLarger,
									methode.SelectedIndex,
									matrice.SelectedIndex + 2,
									(int)pctTrame.Value,
									imgCpc.lockState,
									(int)lumi.Value,
									(int)sat.Value,
									100,
									modePlus.Checked,
									false,
									false,
									false,
									false,
									false,
									renderMode.SelectedIndex);
				if (checkImageCPC.Checked)
					imgCpc.Render();
				bpConvert.Enabled = true;
			}
		}

		private void nbCols_ValueChanged(object sender, System.EventArgs e) {
			imgCpc.bitmapCpc.TailleX = (int)nbCols.Value << 3;
			imgCpc.Reset();
			UpdateImgCPC();
		}

		private void nbLignes_ValueChanged(object sender, System.EventArgs e) {
			imgCpc.bitmapCpc.TailleY = (int)nbLignes.Value << 1;
			imgCpc.Reset();
			UpdateImgCPC();
		}

		private void mode_SelectedIndexChanged(object sender, System.EventArgs e) {
			imgCpc.bitmapCpc.ModeCPC = int.Parse(mode.SelectedItem.ToString());
			imgCpc.Reset();
			UpdateImgCPC();
		}

		private void modePlus_CheckedChanged(object sender, System.EventArgs e) {
			imgCpc.bitmapCpc.cpcPlus = modePlus.Checked;
		}

		private void bpRazLumi_Click(object sender, System.EventArgs e) {
			lumi.Value = 100;
		}

		private void bpRazSat_Click(object sender, System.EventArgs e) {
			sat.Value = 100;
		}
	}
}
