using System;
using System.Drawing;
using System.IO;
using System.Reflection;
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
			lblInfoVersion.Text = "Version " + Assembly.GetExecutingAssembly().GetName().Version.ToString();
		}

		private void checkImageSource_CheckedChanged(object sender, EventArgs e) {
			imgSrc.Visible = checkImageSource.Checked;
		}

		private void Convert(bool doConvert) {
			if (imgSrc.GetImage != null && (autoRecalc.Checked || doConvert)) {
				try {
					bpConvert.Enabled = false;
					imgCpc.Reset();
					param.sMode = radioKeepLarger.Checked ? Param.SizeMode.KeepLarger : radioKeepSmaller.Checked ? Param.SizeMode.KeepSmaller : Param.SizeMode.Fit;
					param.methode = methode.SelectedItem.ToString();
					param.pct = (int)pctTrame.Value;
					param.lockState = imgCpc.lockState;
					Bitmap tmp = new Bitmap(imgCpc.TailleX, imgCpc.TailleY);
					Graphics g = Graphics.FromImage(tmp);
					double ratio = imgSrc.GetImage.Width * imgCpc.TailleY / (double)(imgSrc.GetImage.Height * imgCpc.TailleX);
					switch (param.sMode) {
						case Param.SizeMode.KeepSmaller:
							if (ratio < 1) {
								int newW = (int)(imgCpc.TailleX * ratio);
								g.DrawImage(imgSrc.GetImage, (imgCpc.TailleX - newW) >> 1, 0, newW, imgCpc.TailleY);
							}
							else {
								int newH = (int)(imgCpc.TailleY / ratio);
								g.DrawImage(imgSrc.GetImage, 0, (imgCpc.TailleY - newH) >> 1, imgCpc.TailleX, newH);
							}
							break;

						case Param.SizeMode.KeepLarger:
							if (ratio < 1) {
								int newY = (int)(imgCpc.TailleY / ratio);
								g.DrawImage(imgSrc.GetImage, 0, (imgCpc.TailleY - newY) >> 1, imgCpc.TailleX, newY);
							}
							else {
								int newX = (int)(imgCpc.TailleX * ratio);
								g.DrawImage(imgSrc.GetImage, (imgCpc.TailleX - newX) >> 1, 0, newX, imgCpc.TailleY);
							}
							break;

						case Param.SizeMode.Fit:
							tmp = new Bitmap(imgSrc.GetImage, imgCpc.TailleX, imgCpc.TailleY);
							break;
					}
					Conversion.Convert(tmp, imgCpc, param);
					bpSaveImage.Enabled = bpConvert.Enabled = true;
				}
				catch (Exception ex) {
					MessageBox.Show(ex.StackTrace, ex.Message);
				}
			}
			imgCpc.Render();
		}

		private void bpConvert_Click(object sender, EventArgs e) {
			Convert(true);
		}

		private void bpReadSrc_Click(object sender, EventArgs e) {
			OpenFileDialog dlg = new OpenFileDialog();
			dlg.Filter = "Images (*.bmp, *.gif, *.png, *.jpg)|*.bmp;*.gif;*.png;*.jpg";
			DialogResult result = dlg.ShowDialog();
			if (result == DialogResult.OK) {
				try {
					Bitmap bmp = new Bitmap(dlg.FileName);
					imgSrc.SetBitmap(bmp, checkImageSource.Checked);
					Text = "ConvImgCPC - " + Path.GetFileName(dlg.FileName);
					Convert(false);
				}
				catch (Exception ex) {
					MessageBox.Show(ex.StackTrace, ex.Message);
				}
			}
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
					radioFit.Checked = param.sMode == Param.SizeMode.Fit;
					radioKeepLarger.Checked = param.sMode == Param.SizeMode.KeepLarger;
					radioKeepSmaller.Checked = param.sMode == Param.SizeMode.KeepSmaller;
					nbCols.Value = param.nbCols;
					nbLignes.Value = param.nbLignes;
					mode.SelectedItem = param.modeCpc;
				}
				catch (Exception ex) {
					MessageBox.Show(ex.StackTrace, ex.Message);
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
					MessageBox.Show(ex.StackTrace, ex.Message);
				}
				file.Close();
			}
		}

		private void bpSaveImage_Click(object sender, EventArgs e) {
			try {
				SaveFileDialog dlg = new SaveFileDialog();
				dlg.Filter = "Image CPC (*.scr)|*.scr|Image Bitmap (.bmp)|*.bmp";
				DialogResult result = dlg.ShowDialog();
				if (result == DialogResult.OK)
					switch (dlg.FilterIndex) {
						case 1:
							imgCpc.SauveScr(dlg.FileName, param);
							break;

						case 2:
							imgCpc.SauveBmp(dlg.FileName);
							break;
					}
			}
			catch (Exception ex) {
				MessageBox.Show(ex.StackTrace, ex.Message);
			}
		}

		private void nbCols_ValueChanged(object sender, EventArgs e) {
			param.nbCols = (int)nbCols.Value;
			imgCpc.TailleX = param.nbCols << 3;
			Convert(false);
		}

		private void nbLignes_ValueChanged(object sender, EventArgs e) {
			param.nbLignes = (int)nbLignes.Value;
			imgCpc.TailleY = param.nbLignes << 1;
			Convert(false);
		}

		private void mode_SelectedIndexChanged(object sender, EventArgs e) {
			param.modeCpc = mode.SelectedItem.ToString();
			imgCpc.ModeCPC = int.Parse(param.modeCpc.Substring(0, 1));
			Convert(false);
		}

		private void modePlus_CheckedChanged(object sender, EventArgs e) {
			imgCpc.cpcPlus = modePlus.Checked;
			newMethode.Enabled = !modePlus.Checked;
			reducPal1.Enabled = reducPal2.Enabled = newReduc.Enabled = modePlus.Checked;
			param.cpcPlus = modePlus.Checked;
			Convert(false);
		}

		private void InterfaceChange(object sender, EventArgs e) {
			Convert(false);
		}

		private void pctTrame_ValueChanged(object sender, EventArgs e) {
			param.pct = (int)pctTrame.Value;
			Convert(false);
		}

		private void lumi_ValueChanged(object sender, EventArgs e) {
			param.pctLumi = (int)lumi.Value;
			Convert(false);
		}

		private void sat_ValueChanged(object sender, EventArgs e) {
			param.pctSat = nb.Checked ? 0 : (int)sat.Value;
			Convert(false);
		}

		private void contrast_ValueChanged(object sender, EventArgs e) {
			param.pctContrast = (int)contrast.Value;
			Convert(false);
		}

		private void sortPal_CheckedChanged(object sender, EventArgs e) {
			param.sortPal = sortPal.Checked;
			Convert(false);
		}

		private void newMethode_CheckedChanged(object sender, EventArgs e) {
			param.newMethode = newMethode.Checked;
			Convert(false);
		}

		private void nb_CheckedChanged(object sender, EventArgs e) {
			bpRazSat.Enabled = sat.Enabled = !nb.Checked;
			param.pctSat = nb.Checked ? 0 : (int)sat.Value;
			Convert(false);
		}

		private void reducPal1_CheckedChanged(object sender, EventArgs e) {
			newReduc.Enabled = reducPal1.Checked || reducPal2.Checked;
			param.reductPal1 = reducPal1.Checked;
			param.newReduct = newReduc.Checked;
			Convert(false);
		}

		private void reducPal2_CheckedChanged(object sender, EventArgs e) {
			newReduc.Enabled = reducPal1.Checked || reducPal2.Checked;
			param.reductPal2 = reducPal2.Checked;
			param.newReduct = newReduc.Checked;
			Convert(false);
		}

		private void newReduc_CheckedChanged(object sender, EventArgs e) {
			param.newReduct = newReduc.Checked;
			Convert(false);
		}

		private void bpRazLumi_Click(object sender, EventArgs e) {
			lumi.Value = 100;
		}

		private void bpRazSat_Click(object sender, EventArgs e) {
			sat.Value = 100;
		}

		private void bpRazContrast_Click(object sender, EventArgs e) {
			contrast.Value = 100;
		}

		private void chkOverscan_CheckedChanged(object sender, EventArgs e) {
			nbLignes.Value = 272;
			nbCols.Value = 96;
		}
	}
}
