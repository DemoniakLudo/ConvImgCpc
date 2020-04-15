//#define TRY_CATCH

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
			mode.Items.Insert(0, "Mode 0");
			mode.Items.Insert(1, "Mode 1");
			mode.Items.Insert(2, "Mode 2");
			mode.Items.Insert(3, "Mode EGX1");
			mode.Items.Insert(4, "Mode EGX2");
			mode.Items.Insert(5, "Mode X");
			imgSrc = new ImageSource();
			imgCpc = new ImageCpc(Convert);
			nbCols.Value = imgCpc.TailleX >> 3;
			nbLignes.Value = imgCpc.TailleY >> 1;
			mode.SelectedIndex = imgCpc.modeVirtuel;
			methode.SelectedIndex = 0;
			param.pctContrast = param.pctLumi = param.pctSat = 100;
			imgCpc.Visible = true;
			lblInfoVersion.Text = "Version " + Assembly.GetExecutingAssembly().GetName().Version.ToString();
			radioUserSize_CheckedChanged(null, null);
		}

		private void checkImageSource_CheckedChanged(object sender, EventArgs e) {
			imgSrc.Visible = checkImageSource.Checked;
		}

		private void Convert(bool doConvert) {
			if (imgSrc.GetImage != null && (autoRecalc.Checked || doConvert)) {
#if  TRY_CATCH
				try {
#endif
				bpSaveImage.Enabled = bpConvert.Enabled = false;
				imgCpc.Reset();
				param.sMode = radioKeepLarger.Checked ? Param.SizeMode.KeepLarger : radioKeepSmaller.Checked ? Param.SizeMode.KeepSmaller : radioFit.Checked ? Param.SizeMode.Fit : Param.SizeMode.UserSize;
				param.methode = methode.SelectedItem.ToString();
				param.pct = (int)pctTrame.Value;
				param.lockState = imgCpc.lockState;
				param.withCode = withCode.Checked;
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

					case Param.SizeMode.UserSize:
						int posx = 0, posy = 0, tx = imgCpc.TailleX, ty = imgCpc.TailleY;
						int.TryParse(tbxSizeX.Text, out tx);
						int.TryParse(tbxSizeY.Text, out ty);
						int.TryParse(tbxPosX.Text, out posx);
						int.TryParse(tbxPosY.Text, out posy);
						g.DrawImage(imgSrc.GetImage, posx, posy, tx, ty);
						break;
				}
				imgCpc.SetNbColors(Conversion.Convert(tmp, imgCpc, param));
				bpSaveImage.Enabled = bpConvert.Enabled = true;
#if TRY_CATCH
				}
				catch (Exception ex) {
					MessageBox.Show(ex.StackTrace, ex.Message);
				}
#endif
			}
			imgCpc.Render();
		}

		private void bpConvert_Click(object sender, EventArgs e) {
			Convert(true);
		}

		private void bpReadSrc_Click(object sender, EventArgs e) {
			OpenFileDialog dlg = new OpenFileDialog();
			dlg.Filter = "Images (*.bmp, *.gif, *.png, *.jpg, *.scr)|*.bmp;*.gif;*.png;*.jpg;*.scr|Tous fichiers|*.*";
			DialogResult result = dlg.ShowDialog();
			if (result == DialogResult.OK) {
#if TRY_CATCH
				try {
#endif
				FileStream file = new FileStream(dlg.FileName, FileMode.Open, FileAccess.Read);
				byte[] tabBytes = new byte[file.Length];
				file.Read(tabBytes, 0, tabBytes.Length);
				file.Close();
				bool bitmapOk = false;
				try {
					if (CpcSystem.CheckAmsdos(tabBytes)) {
						BitmapCpc bmp = new BitmapCpc(tabBytes);
						imgSrc.SetBitmap(bmp.CreateImageFromCpc(tabBytes), checkImageSource.Checked);
						nbCols.Value = param.nbCols = bmp.nbCol;
						imgCpc.TailleX = param.nbCols << 3;
						nbLignes.Value = param.nbLignes = bmp.nbLig;
						imgCpc.TailleY = param.nbLignes << 1;
						imgCpc.modeVirtuel = param.modeVirtuel = mode.SelectedIndex = bmp.modeCPC;
					}
					else {
						MemoryStream ms = new MemoryStream(tabBytes);
						imgSrc.SetBitmap(new Bitmap(ms), checkImageSource.Checked);
						ms.Dispose();
					}
					bitmapOk = true;
				}
				catch (Exception ex) {
					MessageBox.Show("Impossible de lire l'image (format inconnu ???)");
				}

				if (bitmapOk) {
					Text = "ConvImgCPC - " + Path.GetFileName(dlg.FileName);
					tbxSizeX.Text = imgSrc.GetImage.Width.ToString();
					tbxSizeY.Text = imgSrc.GetImage.Height.ToString();
					Convert(false);
				}
#if TRY_CATCH
				}
				catch (Exception ex) {
					MessageBox.Show(ex.StackTrace, ex.Message);
				}
#endif
			}
		}

		private void bpLoadParam_Click(object sender, EventArgs e) {
			OpenFileDialog dlg = new OpenFileDialog();
			dlg.Filter = "Paramètres ConvImagesCpc (*.xml)|*.xml";
			DialogResult result = dlg.ShowDialog();
			if (result == DialogResult.OK) {
				FileStream file = File.Open(dlg.FileName, FileMode.Open);
#if TRY_CATCH
				try {
#endif
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
				sortPal.Checked = param.sortPal;
				radioFit.Checked = param.sMode == Param.SizeMode.Fit;
				radioKeepLarger.Checked = param.sMode == Param.SizeMode.KeepLarger;
				radioKeepSmaller.Checked = param.sMode == Param.SizeMode.KeepSmaller;
				nbCols.Value = param.nbCols;
				nbLignes.Value = param.nbLignes;
				mode.SelectedItem = param.modeVirtuel;
#if TRY_CATCH
				}
				catch (Exception ex) {
					MessageBox.Show(ex.StackTrace, ex.Message);
				}
#endif
				file.Close();
			}
		}

		private void bpSaveParam_Click(object sender, EventArgs e) {
			SaveFileDialog dlg = new SaveFileDialog();
			dlg.Filter = "Paramètres ConvImagesCpc (*.xml)|*.xml";
			DialogResult result = dlg.ShowDialog();
			if (result == DialogResult.OK) {
				FileStream file = File.Open(dlg.FileName, FileMode.Create);
#if TRY_CATCH
				try {
#endif
				new XmlSerializer(typeof(Param)).Serialize(file, param);
#if TRY_CATCH
				}
				catch (Exception ex) {
					MessageBox.Show(ex.StackTrace, ex.Message);
				}
#endif
				file.Close();
			}
		}

		private void bpSaveImage_Click(object sender, EventArgs e) {
#if TRY_CATCH
			try {
#endif
			SaveFileDialog dlg = new SaveFileDialog();
			dlg.Filter = "Image CPC (*.scr)|*.scr|Image Bitmap (.bmp)|*.bmp|Sprite assembleur (.asm)|*.asm|Compacté (.cmp)|*.cmp";
			DialogResult result = dlg.ShowDialog();
			if (result == DialogResult.OK)
				switch (dlg.FilterIndex) {
					case 1:
						imgCpc.SauveScr(dlg.FileName, param);
						break;

					case 2:
						imgCpc.SauveBmp(dlg.FileName);
						break;

					case 3:
						imgCpc.SauveSprite(dlg.FileName, lblInfoVersion.Text, param);
						break;

					case 4:
						imgCpc.SauveCmp(dlg.FileName, param);
						break;
				}
#if TRY_CATCH
			}
			catch (Exception ex) {
				MessageBox.Show(ex.StackTrace, ex.Message);
			}
#endif
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
			imgCpc.modeVirtuel = param.modeVirtuel = mode.SelectedIndex;
			Convert(false);
		}

		private void modePlus_CheckedChanged(object sender, EventArgs e) {
			imgCpc.cpcPlus = modePlus.Checked;
			newMethode.Visible = !modePlus.Checked;
			reducPal1.Visible = reducPal2.Visible = modePlus.Checked;
			param.cpcPlus = modePlus.Checked;
			Convert(false);
		}

		private void InterfaceChange(object sender, EventArgs e) {
			lblPct.Visible = pctTrame.Visible = methode.SelectedItem.ToString() != "Aucun";
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
			param.reductPal1 = reducPal1.Checked;
			Convert(false);
		}

		private void reducPal2_CheckedChanged(object sender, EventArgs e) {
			param.reductPal2 = reducPal2.Checked;
			Convert(false);
		}

		private void withCode_CheckedChanged(object sender, EventArgs e) {
			param.withCode = withCode.Checked;
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

		private void radioUserSize_CheckedChanged(object sender, EventArgs e) {
			tbxPosX.Visible = tbxPosY.Visible = tbxSizeX.Visible = tbxSizeY.Visible = label5.Visible = label7.Visible = radioUserSize.Checked;
		}

		private void bpOverscan_Click(object sender, EventArgs e) {
			nbLignes.Value = 272;
			nbCols.Value = 96;
		}

	}
}
