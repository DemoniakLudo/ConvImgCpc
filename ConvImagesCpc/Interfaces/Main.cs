using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;
using ConvImgCpc;

namespace CpcConvImg {
	public partial class Main: Form {
		private ImageSource imgSrc;
		private ImageCpc imgCpc;
		private Param param = new Param();

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
			param.pctContrast = param.pctLumi = param.pctSat = 100;
			param.matrice = 2;
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
				param.sizeMode = radioKeepLarger.Checked ? Param.SizeMode.KeepLarger : radioKeepSmaller.Checked ? Param.SizeMode.KeepSmaller : Param.SizeMode.Fit;
				param.methode = methode.SelectedIndex;
				param.matrice = matrice.SelectedIndex + 2;
				param.lockState = imgCpc.lockState;
				Conversion.Convert(imgSrc.GetImage, imgCpc.bitmapCpc, param, checkBox1.Checked);
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
			imgCpc.bitmapCpc.ModeCPC = int.Parse(mode.SelectedItem.ToString(), System.Globalization.CultureInfo.CurrentCulture);
			imgCpc.Reset();
			bpConvert_Click(autoRecalc.Checked ? sender : null, e);
		}

		private void modePlus_CheckedChanged(object sender, System.EventArgs e) {
			imgCpc.bitmapCpc.cpcPlus = modePlus.Checked;
			newMethode.Enabled = !modePlus.Checked;
			reducPal1.Enabled = reducPal2.Enabled = modePlus.Checked;
			param.cpcPlus = modePlus.Checked;
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
			param.pct = (int)pctTrame.Value;
			bpConvert_Click(autoRecalc.Checked ? sender : null, e);
		}

		private void methode_SelectedIndexChanged(object sender, System.EventArgs e) {
			bpConvert_Click(autoRecalc.Checked ? sender : null, e);
		}

		private void matrice_SelectedIndexChanged(object sender, System.EventArgs e) {
			bpConvert_Click(autoRecalc.Checked ? sender : null, e);
		}

		private void renderMode_SelectedIndexChanged(object sender, System.EventArgs e) {
			param.pixMode = renderMode.SelectedIndex;
			bpConvert_Click(autoRecalc.Checked ? sender : null, e);
		}

		private void lumi_ValueChanged(object sender, System.EventArgs e) {
			param.pctLumi = (int)lumi.Value;
			bpConvert_Click(autoRecalc.Checked ? sender : null, e);
		}

		private void sat_ValueChanged(object sender, System.EventArgs e) {
			param.pctSat = nb.Checked ? 0 : (int)sat.Value;
			bpConvert_Click(autoRecalc.Checked ? sender : null, e);
		}

		private void contrast_ValueChanged(object sender, System.EventArgs e) {
			param.pctContrast = (int)contrast.Value;
			bpConvert_Click(autoRecalc.Checked ? sender : null, e);
		}

		private void sortPal_CheckedChanged(object sender, System.EventArgs e) {
			param.sortPal = sortPal.Checked;
			bpConvert_Click(autoRecalc.Checked ? sender : null, e);
		}

		private void newMethode_CheckedChanged(object sender, System.EventArgs e) {
			param.newMethode = newMethode.Checked;
			bpConvert_Click(autoRecalc.Checked ? sender : null, e);
		}

		private void nb_CheckedChanged(object sender, System.EventArgs e) {
			bpRazSat.Enabled = sat.Enabled = !nb.Checked;
			bpConvert_Click(autoRecalc.Checked ? sender : null, e);
		}

		private void reducPal1_CheckedChanged(object sender, System.EventArgs e) {
			newReduc.Enabled = reducPal1.Checked || reducPal2.Checked;
			param.reductPal1 = reducPal1.Checked;
			param.newReduct = newReduc.Checked;
			bpConvert_Click(autoRecalc.Checked ? sender : null, e);
		}

		private void reducPal2_CheckedChanged(object sender, System.EventArgs e) {
			newReduc.Enabled = reducPal1.Checked || reducPal2.Checked;
			param.reductPal2 = reducPal2.Checked;
			param.newReduct = newReduc.Checked;
			bpConvert_Click(autoRecalc.Checked ? sender : null, e);
		}

		private void newReduc_CheckedChanged(object sender, System.EventArgs e) {
			param.newReduct = newReduc.Checked;
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

		private void bpLoadParam_Click(object sender, EventArgs e) {
			OpenFileDialog dlg = new OpenFileDialog();
			dlg.Filter = "Paramètres ConvImagesCpc (*.xml)|*.xml";
			DialogResult result = dlg.ShowDialog();
			if (result == DialogResult.OK) {
				FileStream file = File.Open(dlg.FileName, FileMode.Open);
				try {
					param = (Param)new XmlSerializer(typeof(Param)).Deserialize(file);
					// Initialisation paramètres...
					methode.SelectedIndex = param.methode;
					matrice.SelectedIndex = param.matrice - 2;
					pctTrame.Value = param.pct;
					imgCpc.lockState = param.lockState;
					lumi.Value = param.pctLumi;
					sat.Value = param.pctSat;
					contrast.Value = param.pctContrast;
					modePlus.Checked = param.cpcPlus;
					newMethode.Checked = param.newMethode;
					reducPal1.Checked = param.reductPal1;
					reducPal2.Checked = param.reductPal2;
					newReduc.Checked = param.newReduct;
					sortPal.Checked = param.sortPal;
					renderMode.SelectedIndex = param.pixMode;
					radioFit.Checked = param.sizeMode == Param.SizeMode.Fit;
					radioKeepLarger.Checked = param.sizeMode == Param.SizeMode.KeepLarger;
					radioKeepSmaller.Checked = param.sizeMode == Param.SizeMode.KeepSmaller;
				}
				catch (Exception ex) {
				}
				file.Close();
			}
		}

		private void bpSaveParam_Click(object sender, EventArgs e) {
			SaveFileDialog dlg = new SaveFileDialog();
			dlg.Filter = "Paramètres ConvImagesCpc (*.xml)|*.xml";
			DialogResult result = dlg.ShowDialog();
			if (result == DialogResult.OK) {
				FileStream file = File.Open(dlg.FileName, FileMode.Create);
				try {
					new XmlSerializer(typeof(Param)).Serialize(file, param);
				}
				catch (Exception ex) {
				}
				file.Close();
			}
		}

		private void bpSaveImage_Click(object sender, EventArgs e) {
			SaveFileDialog dlg = new SaveFileDialog();
			dlg.Filter = "Image CPC (*.scr)|*.scr";
			DialogResult result = dlg.ShowDialog();
			if (result == DialogResult.OK) {
				SauveImage.SauveEcran(dlg.FileName, imgCpc.bitmapCpc, param.cpcPlus);
			}
		}

	}
}
