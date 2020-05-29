using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace ConvImgCpc {
	public partial class Main : Form {
		private ImageSource imgSrc;
		private ImageCpc imgCpc;
		private Param param = new Param();
		private string lastReadPath = null;
		private string lastSavePath = null;
		private MemoryStream imageStream;
		private Image selImage;
		private FrameDimension dimension;

		public Main() {
			InitializeComponent();
			mode.Items.Insert(0, "Mode 0");
			mode.Items.Insert(1, "Mode 1");
			mode.Items.Insert(2, "Mode 2");
			mode.Items.Insert(3, "Mode EGX1");
			mode.Items.Insert(4, "Mode EGX2");
			mode.Items.Insert(5, "Mode X");
			mode.Items.Insert(6, "Mode 16");
			mode.Items.Insert(7, "Mode ASC-UT");
			mode.Items.Insert(8, "Mode ASC0");
			mode.Items.Insert(9, "Mode ASC1");
			mode.Items.Insert(10, "Mode ASC2");
			imgSrc = new ImageSource();
			imgCpc = new ImageCpc(this, Convert);
			nbCols.Value = imgCpc.TailleX >> 3;
			nbLignes.Value = imgCpc.TailleY >> 1;
			mode.SelectedIndex = imgCpc.modeVirtuel;
			methode.SelectedIndex = 0;
			param.pctContrast = param.pctLumi = param.pctSat = param.pctRed = param.pctGreen = param.pctBlue = 100;
			param.withCode = withCode.Checked;
			param.withPalette = withPalette.Checked;
			imgCpc.Visible = true;
			lblInfoVersion.Text = "Version Béta\n" + Assembly.GetExecutingAssembly().GetName().Version.ToString();
			radioUserSize_CheckedChanged(null, null);
			string configDetault = "ConvImgCpc.xml";
			if (File.Exists(configDetault))
				ReadParam(configDetault);
		}

		private void checkImageSource_CheckedChanged(object sender, EventArgs e) {
			try {
				imgSrc.Visible = checkImageSource.Checked;
			}
			catch (Exception ex) {
				MessageBox.Show(ex.StackTrace, ex.Message);
			}
		}

		private void Convert(bool doConvert, bool noInfo = false) {
			if (imgSrc.GetImage != null && (autoRecalc.Checked || doConvert)) {
				bpSave.Enabled = bpConvert.Enabled = false;
				param.sMode = radioKeepLarger.Checked ? Param.SizeMode.KeepLarger : radioKeepSmaller.Checked ? Param.SizeMode.KeepSmaller : radioFit.Checked ? Param.SizeMode.Fit : Param.SizeMode.UserSize;
				param.methode = methode.SelectedItem.ToString();
				param.pct = (int)pctTrame.Value;
				param.lockState = imgCpc.lockState;
				param.motif = chkMotif.Checked;
				param.motif2 = chkMotif2.Checked;
				param.setPalCpc = chkPalCpc.Checked;
				DirectBitmap tmp = new DirectBitmap(imgCpc.TailleX, imgCpc.TailleY);
				Graphics g = Graphics.FromImage(tmp.Bitmap);
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
						g.DrawImage(imgSrc.GetImage, 0, 0, imgCpc.TailleX, imgCpc.TailleY);
						break;

					case Param.SizeMode.UserSize:
					case Param.SizeMode.Origin:
						int posx = 0, posy = 0, tx = imgCpc.TailleX, ty = imgCpc.TailleY;
						GetSizePos(ref posx, ref posy, ref tx, ref ty);
						g.DrawImage(imgSrc.GetImage, -(posx << 1), -(posy << 1), tx << 1, ty << 1);
						break;
				}
				if (!noInfo)
					SetInfo("Conversion en cours...");

				Conversion.Convert(tmp, imgCpc, param, noInfo);
				bpSave.Enabled = bpConvert.Enabled = true;
				tmp.Dispose();
			}
			imgCpc.Render();
		}

		public void GetSizePos(ref int posx, ref int posy, ref int sizex, ref int sizey) {
			int.TryParse(tbxPosX.Text, out posx);
			int.TryParse(tbxPosY.Text, out posy);
			int.TryParse(tbxSizeX.Text, out sizex);
			int.TryParse(tbxSizeY.Text, out sizey);
		}

		public void SetSizePos(int posx, int posy, int sizex, int sizey, bool doConvert = false) {
			tbxPosX.Text = posx.ToString();
			tbxPosY.Text = posy.ToString();
			tbxSizeX.Text = sizex.ToString();
			tbxSizeY.Text = sizey.ToString();
			if (doConvert)
				Convert(false);
		}

		private void bpConvert_Click(object sender, EventArgs e) {
			Convert(true);
		}

		private void ReadScreen(string fileName) {
			FileStream fileScr = new FileStream(fileName, FileMode.Open, FileAccess.Read);
			byte[] tabBytes = new byte[fileScr.Length];
			fileScr.Read(tabBytes, 0, tabBytes.Length);
			fileScr.Close();
			bool bitmapOk = false;
			try {
				if (CpcSystem.CheckAmsdos(tabBytes)) {
					BitmapCpc bmp = new BitmapCpc(tabBytes);
					imgSrc.SetBitmap(bmp.CreateImageFromCpc(tabBytes), checkImageSource.Checked);
					nbCols.Value = param.nbCols = bmp.NbCol;
					imgCpc.TailleX = param.nbCols << 3;
					nbLignes.Value = param.nbLignes = bmp.NbLig;
					imgCpc.TailleY = param.nbLignes << 1;
					imgCpc.modeVirtuel = param.modeVirtuel = mode.SelectedIndex = bmp.modeVirtuel;
					SetInfo("Lecture image de type CPC");
				}
				else {
					imageStream = new MemoryStream(tabBytes);
					imageStream.Position = 0;
					selImage = new Bitmap(imageStream);
					dimension = new FrameDimension(selImage.FrameDimensionsList[0]);
					int nbImg = GetMaxImages();
					numImage.Maximum = nbImg - 1;
					lblMaxImage.Text = "Nbre images:" + nbImg;
					lblMaxImage.Visible = lblNumImage.Visible = numImage.Visible = nbImg > 1;
					numImage.Value = 0;
					SetInfo("Lecture image PC" + (nbImg > 0 ? (" de type animation avec " + nbImg + " images.") : "."));
					SelectImage(0);
				}
				bitmapOk = true;
			}
			catch (Exception ex) {
				MessageBox.Show("Impossible de lire l'image (format inconnu ???)");
				SetInfo("Impossible de lire l'image (format inconnu ???)");
			}
			if (bitmapOk) {
				radioUserSize.Enabled = radioOrigin.Enabled = true;
				Text = "ConvImgCPC - " + Path.GetFileName(fileName);
				if (radioOrigin.Checked) {
					tbxSizeX.Text = imgSrc.GetImage.Width.ToString();
					tbxSizeY.Text = imgSrc.GetImage.Height.ToString();
					tbxPosX.Text = "0";
					tbxPosY.Text = "0";
				}
				imgCpc.Reset();
				Convert(false);
			}
		}

		public void SelectImage(int n, bool noInfo = false) {
			selImage.SelectActiveFrame(dimension, n);
			imgSrc.SetBitmap(new Bitmap(selImage), checkImageSource.Checked);
			if (!noInfo)
				SetInfo("Image sélectionnée: " + n.ToString());
		}

		public int GetMaxImages() {
			return selImage != null ? selImage.GetFrameCount(dimension) : 0;
		}

		public void SetInfo(string txt) {
			listInfo.Items.Add(DateTime.Now.ToString() + " - " + txt);
			listInfo.SelectedIndex = listInfo.Items.Count - 1;
		}

		private void ReadParam(string fileName) {
			FileStream fileParam = File.Open(fileName, FileMode.Open);
			try {
				param = (Param)new XmlSerializer(typeof(Param)).Deserialize(fileParam);
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
				mode.SelectedIndex = param.modeVirtuel;
				withCode.Checked = param.withCode;
				withPalette.Checked = param.withPalette;
				chkMotif.Checked = param.motif;
				chkMotif2.Checked = param.motif2;
				chkPalCpc.Checked = param.setPalCpc;
				chkLissage.Checked = param.lissage;
				SetInfo("Lecture paramètres ok.");
			}
			catch (Exception ex) {
				MessageBox.Show(ex.StackTrace, ex.Message);
				SetInfo("Erreur lecture paramètres...");
			}
			fileParam.Close();
		}

		private void SaveParam(string fileName) {
			FileStream file = File.Open(fileName, FileMode.Create);
			//try {
			param.withCode = withCode.Checked;
			param.withPalette = withPalette.Checked;
			new XmlSerializer(typeof(Param)).Serialize(file, param);
			SetInfo("Sauvegarde paramètres ok.");
			//}
			//catch (Exception ex) {
			//	MessageBox.Show(ex.StackTrace, ex.Message);
			//	SetInfo("Erreur sauvegarde paramètres...");
			//}
			file.Close();
		}

		private void bpLoad_Click(object sender, EventArgs e) {
			OpenFileDialog dlg = new OpenFileDialog();
			dlg.Filter = "Images (*.bmp, *.gif, *.png, *.jpg,*.jpeg, *.scr)|*.bmp;*.gif;*.png;*.jpg;*.jpeg;*.scr|Palette (*.pal)|*.pal|Paramètres ConvImagesCpc (*.xml)|*.xml|Tous fichiers|*.*";
			dlg.InitialDirectory = lastReadPath;
			DialogResult result = dlg.ShowDialog();
			if (result == DialogResult.OK) {
				lastReadPath = Path.GetDirectoryName(dlg.FileName);
				switch (dlg.FilterIndex) {
					case 1:
					case 4:
						ReadScreen(dlg.FileName);
						break;

					case 2:
						imgCpc.LirePalette(dlg.FileName, param);
						break;

					case 3:
						ReadParam(dlg.FileName);
						break;
				}
			}
		}

		private void bpSave_Click(object sender, EventArgs e) {
			SaveFileDialog dlg = new SaveFileDialog();
			dlg.InitialDirectory = lastSavePath;
			dlg.Filter = "Image CPC (*.scr)|*.scr|Image Bitmap (.png)|*.png|Sprite assembleur (.asm)|*.asm|Sprite assembleur compacté (.asm)|*.asm|Ecran compacté (.cmp)|*.cmp|Palette (.pal)|*.pal|Animation DeltaPack (.asm)|*.asm|Paramètres (.xml)|*.xml";
			DialogResult result = dlg.ShowDialog();
			if (result == DialogResult.OK) {
				lastSavePath = Path.GetDirectoryName(dlg.FileName);
				switch (dlg.FilterIndex) {
					case 1:
						imgCpc.SauveScr(dlg.FileName, param);
						break;

					case 2:
						imgCpc.SauvePng(dlg.FileName);
						break;

					case 3:
						imgCpc.SauveSprite(dlg.FileName, lblInfoVersion.Text, param);
						break;

					case 4:
						imgCpc.SauveSpriteCmp(dlg.FileName, lblInfoVersion.Text, param);
						break;

					case 5:
						imgCpc.SauveCmp(dlg.FileName, param);
						break;

					case 6:
						imgCpc.SauvePalette(dlg.FileName, param);
						break;

					case 7:
						imgCpc.SauveDeltaPack(dlg.FileName, lblInfoVersion.Text, param, true);
						break;

					case 8:
						SaveParam(dlg.FileName);
						break;
				}
			}
		}

		private void nbCols_ValueChanged(object sender, EventArgs e) {
			param.nbCols = (int)nbCols.Value;
			imgCpc.TailleX = param.nbCols << 3;
			imgCpc.Reset();
			Convert(false);
		}

		private void nbLignes_ValueChanged(object sender, EventArgs e) {
			param.nbLignes = (int)nbLignes.Value;
			imgCpc.TailleY = param.nbLignes << 1;
			imgCpc.Reset();
			Convert(false);
		}

		private void mode_SelectedIndexChanged(object sender, EventArgs e) {
			imgCpc.modeVirtuel = param.modeVirtuel = mode.SelectedIndex;
			imgCpc.Reset();
			trackModeX.Visible = mode.SelectedIndex == 5;
			bpEditTrame.Visible = mode.SelectedIndex == 7;
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
			param.methode = methode.SelectedItem.ToString();
			param.lissage = chkLissage.Checked;
			param.trackModeX = trackModeX.Value;
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
			lumi.Value = 100;
		}

		private void bpRazSat_Click(object sender, EventArgs e) {
			sat.Value = 100;
		}

		private void bpRazContrast_Click(object sender, EventArgs e) {
			contrast.Value = 100;
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
			bpSatMoins.Enabled = bpSatPlus.Enabled = bpRazSat.Enabled = sat.Enabled = !nb.Checked;
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

		private void radioUserSize_CheckedChanged(object sender, EventArgs e) {
			tbxPosX.Visible = tbxPosY.Visible = tbxSizeX.Visible = tbxSizeY.Visible = label5.Visible = label7.Visible = radioUserSize.Checked || radioOrigin.Checked;
			if (radioOrigin.Checked || (radioUserSize.Checked && tbxSizeX.Text == "" && tbxSizeY.Text == ""))
				SetSizePos(0, 0, imgSrc.GetImage.Width, imgSrc.GetImage.Height);

			tbxSizeX.Enabled = tbxSizeY.Enabled = tbxPosX.Enabled = tbxPosY.Enabled = !radioOrigin.Checked;
			Convert(false);
		}

		private void bpOverscan_Click(object sender, EventArgs e) {
			nbLignes.Value = 272;
			nbCols.Value = 96;
		}

		private void bpStandard_Click(object sender, EventArgs e) {
			nbLignes.Value = 200;
			nbCols.Value = 80;
		}

		private void withPalette_CheckedChanged(object sender, EventArgs e) {
			param.withPalette = withPalette.Checked;
		}

		private void chkMotif_CheckedChanged(object sender, EventArgs e) {
			if (chkMotif.Checked)
				chkMotif2.Checked = false;

			Convert(false);
		}

		private void chkMotif2_CheckedChanged(object sender, EventArgs e) {
			if (chkMotif2.Checked)
				chkMotif.Checked = false;

			Convert(false);
		}

		private void numImage_ValueChanged(object sender, EventArgs e) {
			SelectImage((int)numImage.Value);
			Convert(false);
		}

		private void bpEditTrame_Click(object sender, EventArgs e) {
			EditTrameAscii dg = new EditTrameAscii(imgCpc.bitmapCpc);
			dg.ShowDialog();
		}

		private void red_ValueChanged(object sender, EventArgs e) {
			param.pctRed = red.Value;
			Convert(false);
		}

		private void green_ValueChanged(object sender, EventArgs e) {
			param.pctGreen = green.Value;
			Convert(false);
		}

		private void blue_ValueChanged(object sender, EventArgs e) {
			param.pctBlue = blue.Value;
			Convert(false);
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
			red.Value = 100;
		}

		private void RazV_Click(object sender, EventArgs e) {
			green.Value = 100;
		}

		private void RazB_Click(object sender, EventArgs e) {
			blue.Value = 100;
		}
	}
}
