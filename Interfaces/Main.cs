using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace ConvImgCpc {
	public partial class Main : Form {
		private ImageSource imgSrc;
		private ImageCpc imgCpc;
		private Param param = new Param();

		public Main() {
			InitializeComponent();
			imgSrc = new ImageSource();
			imgCpc = new ImageCpc(Convert);
			nbCols.Value = imgCpc.TailleX >> 3;
			nbLignes.Value = imgCpc.TailleY >> 1;
			mode.SelectedIndex = imgCpc.ModeCPC;
			methode.SelectedIndex = 0;
			param.pctContrast = param.pctLumi = param.pctSat = 100;
			imgCpc.Visible = true;
		}

		private void checkImageSource_CheckedChanged(object sender, System.EventArgs e) {
			imgSrc.Visible = checkImageSource.Checked;
		}

		private void UpdateImgCPC() {
			imgCpc.Render();
		}

		private void Convert(bool doConvert) {
			if (imgSrc.GetImage != null && (autoRecalc.Checked || doConvert)) {
				bpConvert.Enabled = false;
				imgCpc.Reset();
				param.sizeMode = radioKeepLarger.Checked ? Param.SizeMode.KeepLarger : radioKeepSmaller.Checked ? Param.SizeMode.KeepSmaller : Param.SizeMode.Fit;
				param.methode = methode.SelectedItem.ToString();
				param.lockState = imgCpc.lockState;

				int tailleX = imgCpc.TailleX;
				int tailleY = imgCpc.TailleY;
				Bitmap tmp = new Bitmap(tailleX, tailleY);
				Graphics g = Graphics.FromImage(tmp);
				double ratio = imgSrc.GetImage.Width * tailleY / (double)(imgSrc.GetImage.Height * tailleX);
				switch (param.sizeMode) {
					case Param.SizeMode.KeepSmaller:
						if (ratio < 1) {
							int newW = (int)(tailleX * ratio);
							g.DrawImage(imgSrc.GetImage, (tailleX - newW) >> 1, 0, newW, tailleY);
						}
						else {
							int newH = (int)(tailleY / ratio);
							g.DrawImage(imgSrc.GetImage, 0, (tailleY - newH) >> 1, tailleX, newH);
						}
						break;

					case Param.SizeMode.KeepLarger:
						if (ratio < 1) {
							int newY = (int)(tailleY / ratio);
							g.DrawImage(imgSrc.GetImage, 0, (tailleY - newY) >> 1, tailleX, newY);
						}
						else {
							int newX = (int)(tailleX * ratio);
							g.DrawImage(imgSrc.GetImage, (tailleX - newX) >> 1, 0, newX, tailleY);
						}
						break;

					case Param.SizeMode.Fit:
						tmp = new Bitmap(imgSrc.GetImage, tailleX, tailleY);
						break;
				}
				Conversion.Convert(tmp, imgCpc, param);
				bpConvert.Enabled = true;
			}
			UpdateImgCPC();
		}

		private void bpConvert_Click(object sender, System.EventArgs e) {
			Convert(true);
		}

		private void bpReadSrc_Click(object sender, System.EventArgs e) {
			OpenFileDialog dlg = new OpenFileDialog();
			dlg.Filter = "Images (*.bmp, *.gif, *.png, *.jpg)|*.bmp;*.gif;*.png;*.jpg";
			DialogResult result = dlg.ShowDialog();
			if (result == DialogResult.OK) {
				imgSrc.SetBitmap(new Bitmap(dlg.FileName), checkImageSource.Checked);
				Text = "ConvImgCPC - " + Path.GetFileName(dlg.FileName);
				Convert(false);
			}
		}

		private void nbCols_ValueChanged(object sender, System.EventArgs e) {
			param.nbCols = (int)nbCols.Value;
			imgCpc.TailleX = param.nbCols << 3;
			imgCpc.Reset();
			Convert(false);
		}

		private void nbLignes_ValueChanged(object sender, System.EventArgs e) {
			param.nbLignes = (int)nbLignes.Value;
			imgCpc.TailleY = param.nbLignes << 1;
			imgCpc.Reset();
			Convert(false);
		}

		private void mode_SelectedIndexChanged(object sender, System.EventArgs e) {
			param.modeCpc = mode.SelectedItem.ToString();
			imgCpc.ModeCPC = int.Parse(mode.SelectedItem.ToString().Substring(0, 1));
			imgCpc.Reset();
			Convert(false);
		}

		private void modePlus_CheckedChanged(object sender, System.EventArgs e) {
			imgCpc.cpcPlus = modePlus.Checked;
			newMethode.Enabled = !modePlus.Checked;
			reducPal1.Enabled = reducPal2.Enabled = newReduc.Enabled = modePlus.Checked;
			param.cpcPlus = modePlus.Checked;
			Convert(false);
		}

		private void radioFit_CheckedChanged(object sender, System.EventArgs e) {
			Convert(false);
		}

		private void radioKeepSmaller_CheckedChanged(object sender, System.EventArgs e) {
			Convert(false);
		}

		private void radioKeepLarger_CheckedChanged(object sender, System.EventArgs e) {
			Convert(false);
		}

		private void pctTrame_ValueChanged(object sender, System.EventArgs e) {
			param.pct = (int)pctTrame.Value;
			Convert(false);
		}

		private void methode_SelectedIndexChanged(object sender, System.EventArgs e) {
			Convert(false);
		}

		private void matrice_SelectedIndexChanged(object sender, System.EventArgs e) {
			Convert(false);
		}

		private void lumi_ValueChanged(object sender, System.EventArgs e) {
			param.pctLumi = (int)lumi.Value;
			Convert(false);
		}

		private void sat_ValueChanged(object sender, System.EventArgs e) {
			param.pctSat = nb.Checked ? 0 : (int)sat.Value;
			Convert(false);
		}

		private void contrast_ValueChanged(object sender, System.EventArgs e) {
			param.pctContrast = (int)contrast.Value;
			Convert(false);
		}

		private void sortPal_CheckedChanged(object sender, System.EventArgs e) {
			param.sortPal = sortPal.Checked;
			Convert(false);
		}

		private void newMethode_CheckedChanged(object sender, System.EventArgs e) {
			param.newMethode = newMethode.Checked;
			Convert(false);
		}

		private void nb_CheckedChanged(object sender, System.EventArgs e) {
			bpRazSat.Enabled = sat.Enabled = !nb.Checked;
			param.pctSat = nb.Checked ? 0 : (int)sat.Value;
			Convert(false);
		}

		private void reducPal1_CheckedChanged(object sender, System.EventArgs e) {
			newReduc.Enabled = reducPal1.Checked || reducPal2.Checked;
			param.reductPal1 = reducPal1.Checked;
			param.newReduct = newReduc.Checked;
			Convert(false);
		}

		private void reducPal2_CheckedChanged(object sender, System.EventArgs e) {
			newReduc.Enabled = reducPal1.Checked || reducPal2.Checked;
			param.reductPal2 = reducPal2.Checked;
			param.newReduct = newReduc.Checked;
			Convert(false);
		}

		private void newReduc_CheckedChanged(object sender, System.EventArgs e) {
			param.newReduct = newReduc.Checked;
			Convert(false);
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
			Convert(false);
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
					methode.SelectedItem = param.methode;
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
					radioFit.Checked = param.sizeMode == Param.SizeMode.Fit;
					radioKeepLarger.Checked = param.sizeMode == Param.SizeMode.KeepLarger;
					radioKeepSmaller.Checked = param.sizeMode == Param.SizeMode.KeepSmaller;
					nbCols.Value = param.nbCols;
					nbLignes.Value = param.nbLignes;
					mode.SelectedItem = param.modeCpc;
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
				//SauveImage.SauveEcran(dlg.FileName, imgCpc, param.cpcPlus);
			}
		}
	}
}
