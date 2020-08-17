﻿using System;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace ConvImgCpc {
	public partial class Main : Form {
		public ImageSource imgSrc;
		public ImageCpc imgCpc;
		public Param param = new Param();
		private string lastReadPath = null;
		private string lastSavePath = null;
		private MemoryStream imageStream;
		private Informations info = new Informations();
		private Animation anim;
		private EffetsPalette efPalette;

		public Main() {
			anim = new Animation(this);
			anim.Show();
			efPalette = new EffetsPalette(this);
			efPalette.Show();
			InitializeComponent();

			for (int i = 0; i < BitmapCpc.modesVirtuels.Length; i++)
				mode.Items.Insert(i, BitmapCpc.modesVirtuels[i]);

			imgSrc = new ImageSource();
			imgCpc = new ImageCpc(this, Convert);
			nbCols.Value = BitmapCpc.TailleX >> 3;
			nbLignes.Value = BitmapCpc.TailleY >> 1;
			mode.SelectedIndex = BitmapCpc.modeVirtuel;
			methode.SelectedIndex = 0;
			param.pctContrast = param.pctLumi = param.pctSat = param.pctRed = param.pctGreen = param.pctBlue = 100;
			param.withCode = withCode.Checked;
			param.withPalette = withPalette.Checked;
			imgCpc.Visible = true;
			lblInfoVersion.Text = "Version Béta - " + Assembly.GetExecutingAssembly().GetName().Version.ToString();
			radioUserSize_CheckedChanged(null, null);
			string configDetault = "ConvImgCpc.xml";
			if (File.Exists(configDetault))
				ReadParam(configDetault);
		}

		public DirectBitmap GetResizeBitmap() {
			DirectBitmap tmp = new DirectBitmap(BitmapCpc.TailleX, BitmapCpc.TailleY);
			Graphics g = Graphics.FromImage(tmp.Bitmap);
			double ratio = imgSrc.GetImage.Width * BitmapCpc.TailleY / (double)(imgSrc.GetImage.Height * BitmapCpc.TailleX);
			switch (param.sMode) {
				case Param.SizeMode.KeepSmaller:
					if (ratio < 1) {
						int newW = (int)(BitmapCpc.TailleX * ratio);
						g.DrawImage(imgSrc.GetImage, (BitmapCpc.TailleX - newW) >> 1, 0, newW, BitmapCpc.TailleY);
					}
					else {
						int newH = (int)(BitmapCpc.TailleY / ratio);
						g.DrawImage(imgSrc.GetImage, 0, (BitmapCpc.TailleY - newH) >> 1, BitmapCpc.TailleX, newH);
					}
					break;

				case Param.SizeMode.KeepLarger:
					if (ratio < 1) {
						int newY = (int)(BitmapCpc.TailleY / ratio);
						g.DrawImage(imgSrc.GetImage, 0, (BitmapCpc.TailleY - newY) >> 1, BitmapCpc.TailleX, newY);
					}
					else {
						int newX = (int)(BitmapCpc.TailleX * ratio);
						g.DrawImage(imgSrc.GetImage, (BitmapCpc.TailleX - newX) >> 1, 0, newX, BitmapCpc.TailleY);
					}
					break;

				case Param.SizeMode.Fit:
					g.DrawImage(imgSrc.GetImage, 0, 0, BitmapCpc.TailleX, BitmapCpc.TailleY);
					break;

				case Param.SizeMode.UserSize:
				case Param.SizeMode.Origin:
					int posx = 0, posy = 0, tx = BitmapCpc.TailleX, ty = BitmapCpc.TailleY;
					GetSizePos(ref posx, ref posy, ref tx, ref ty);
					g.DrawImage(imgSrc.GetImage, -(posx << 1), -(posy << 1), tx << 1, ty << 1);
					break;
			}
			return tmp;
		}

		public void Convert(bool doConvert, bool noInfo = false) {
			if (imgSrc.GetImage != null && (autoRecalc.Checked || doConvert) && !noInfo) {
				int imgSel = anim.SelImage;
				int startImg = chkAllPics.Checked ? 0 : imgSel;
				int endImg = chkAllPics.Checked ? anim.MaxImage : imgSel;
				for (int i = startImg; i <= endImg; i++) {
					SelectImage(i, true);
					bpSave.Enabled = bpConvert.Enabled = false;
					param.sMode = radioKeepLarger.Checked ? Param.SizeMode.KeepLarger : radioKeepSmaller.Checked ? Param.SizeMode.KeepSmaller : radioFit.Checked ? Param.SizeMode.Fit : Param.SizeMode.UserSize;
					param.methode = methode.SelectedItem.ToString();
					param.pct = (int)pctTrame.Value;
					param.lockState = imgCpc.lockState;
					param.motif = chkMotif.Checked;
					param.motif2 = chkMotif2.Checked;
					param.setPalCpc = chkPalCpc.Checked;
					DirectBitmap tmp = GetResizeBitmap();
					if (!noInfo && doConvert)
						SetInfo("Conversion en cours...");

					Conversion.Convert(tmp, imgCpc, param, !doConvert || noInfo);
					bpSave.Enabled = bpConvert.Enabled = true;
					tmp.Dispose();
				}
				SelectImage(imgSel);
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

		private void ReadScreen(string fileName, bool singlePicture = false) {
			FileStream fileScr = new FileStream(fileName, FileMode.Open, FileAccess.Read);
			byte[] tabBytes = new byte[fileScr.Length];
			fileScr.Read(tabBytes, 0, tabBytes.Length);
			fileScr.Close();
			int nbImg = 0;
			try {
				if (CpcSystem.CheckAmsdos(tabBytes)) {
					BitmapCpc bmp = new BitmapCpc(tabBytes);
					if (singlePicture)
						imgSrc.ImportBitmap(bmp.CreateImageFromCpc(tabBytes), imgCpc.selImage);
					else {
						imgSrc.InitBitmap(bmp.CreateImageFromCpc(tabBytes));
						nbCols.Value = param.nbCols = BitmapCpc.NbCol;
						BitmapCpc.TailleX = param.nbCols << 3;
						nbLignes.Value = param.nbLignes = BitmapCpc.NbLig;
						BitmapCpc.TailleY = param.nbLignes << 1;
						param.modeVirtuel = mode.SelectedIndex = BitmapCpc.modeVirtuel;
					}
					SetInfo("Lecture image de type CPC.");
				}
				else {
					imageStream = new MemoryStream(tabBytes);
					imageStream.Position = 0;
					if (!singlePicture) {
						imgSrc.InitBitmap(imageStream);
						nbImg = imgSrc.NbImg;
						anim.SetNbImgs(nbImg);
						chkAllPics.Visible = nbImg > 1;
						SetInfo("Lecture image PC" + (nbImg > 0 ? (" de type animation avec " + nbImg + " images.") : "."));
					}
					else {
						imgSrc.ImportBitmap(new Bitmap(imageStream), imgCpc.selImage);
						SetInfo("Lecture image PC.");
					}
				}
				radioUserSize.Enabled = radioOrigin.Enabled = true;
				Text = "ConvImgCPC - " + Path.GetFileName(fileName);
				if (radioOrigin.Checked) {
					tbxSizeX.Text = imgSrc.GetImage.Width.ToString();
					tbxSizeY.Text = imgSrc.GetImage.Height.ToString();
					tbxPosX.Text = "0";
					tbxPosY.Text = "0";
				}
				if (!singlePicture)
					imgCpc.InitBitmapCpc(nbImg);

				SelectImage(0);
				imgCpc.Reset(true);
				Convert(false);
			}
			catch (Exception ex) {
				MessageBox.Show("Impossible de lire l'image (format inconnu ???)");
				SetInfo("Impossible de lire l'image (format inconnu ???)");
			}
		}

		public void SelectImage(int n, bool noInfo = false) {
			if (n > -1) {
				imgSrc.SelectBitmap(n);
				imgCpc.selImage = n;
				imgCpc.Reset();
				anim.DrawImages(n);
				if (!noInfo)
					SetInfo("Image sélectionnée: " + n.ToString());
			}
		}

		public int GetMaxImages() {
			return 1 + anim.MaxImage;
		}

		public void SetInfo(string txt) {
			info.SetInfos(txt);
		}

		private void ReadParam(string fileName) {
			FileStream fileParam = File.Open(fileName, FileMode.Open);
			try {
				param = (Param)new XmlSerializer(typeof(Param)).Deserialize(fileParam);
				// Initialisation paramètres...
				methode.SelectedItem = param.methode;
				pctTrame.Value = param.pct;
				imgCpc.lockState = param.lockState;
				/*
				lumi.Value = param.pctLumi;
				sat.Value = param.pctSat;
				contrast.Value = param.pctContrast;
				modePlus.Checked = param.cpcPlus;
				newMethode.Checked = param.newMethode;
				reducPal1.Checked = param.reductPal1;
				reducPal2.Checked = param.reductPal2;
				reducPal3.Checked = param.reductPal3;
				reducPal4.Checked = param.reductPal4;
				sortPal.Checked = param.sortPal;
				*/
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

		private void bpCreate_Click(object sender, EventArgs e) {
			CreationImages dlg = new CreationImages();
			dlg.ShowDialog();
			int nbImages = dlg.NbImages;
			if (nbImages != -1) {
				imgSrc.InitBitmap(nbImages);
				if (nbImages == 1)
					SetInfo("Création image vierge");
				else {
					anim.SetNbImgs(nbImages);
					SetInfo("Création animation avec " + nbImages + " images.");
				}
				SelectImage(0);
				imgCpc.InitBitmapCpc(nbImages);
				imgCpc.Reset(true);
				Convert(false);
			}
		}

		private void bpImport_Click(object sender, EventArgs e) {
			OpenFileDialog dlg = new OpenFileDialog();
			dlg.Filter = "Images (*.bmp, *.gif, *.png, *.jpg,*.jpeg, *.scr)|*.bmp;*.gif;*.png;*.jpg;*.jpeg;*.scr|Tous fichiers|*.*";
			dlg.InitialDirectory = lastReadPath;
			DialogResult result = dlg.ShowDialog();
			if (result == DialogResult.OK) {
				lastReadPath = Path.GetDirectoryName(dlg.FileName);
				ReadScreen(dlg.FileName, true);
			}
		}

		private void bpLoad_Click(object sender, EventArgs e) {
			OpenFileDialog dlg = new OpenFileDialog();
			dlg.Filter = "Images (*.bmp, *.gif, *.png, *.jpg,*.jpeg, *.scr)|*.bmp;*.gif;*.png;*.jpg;*.jpeg;*.scr|Palette (*.pal)|*.pal|Paramètres ConvImgCpc (*.xml)|*.xml|Tous fichiers|*.*";
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
			dlg.Filter = "Image CPC (*.scr)|*.scr|Image Bitmap (.png)|*.png|Sprite assembleur (.asm)|*.asm|Sprite assembleur compacté (.asm)|*.asm|Ecran compacté (.cmp)|*.cmp|Ecran assembleur compacté (.asm)|*.asm|Palette (.pal)|*.pal|Animation DeltaPack (.asm)|*.asm|Paramètres (.xml)|*.xml";
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
						imgCpc.SauveSprite(dlg.FileName, lblInfoVersion.Text);
						break;

					case 4:
						imgCpc.SauveSpriteCmp(dlg.FileName, lblInfoVersion.Text);
						break;

					case 5:
						imgCpc.SauveCmp(dlg.FileName, param);
						break;

					case 6:
						imgCpc.SauveCmp(dlg.FileName, param, lblInfoVersion.Text);
						break;

					case 7:
						imgCpc.SauvePalette(dlg.FileName, param);
						break;

					case 8:
						imgCpc.SauveDeltaPack(dlg.FileName, lblInfoVersion.Text, param, true);
						break;

					case 9:
						SaveParam(dlg.FileName);
						break;
				}
			}
		}

		private void nbCols_ValueChanged(object sender, EventArgs e) {
			param.nbCols = (int)nbCols.Value;
			BitmapCpc.TailleX = param.nbCols << 3;
			imgCpc.Reset(true);
			Convert(false);
		}

		private void nbLignes_ValueChanged(object sender, EventArgs e) {
			param.nbLignes = (int)nbLignes.Value;
			BitmapCpc.TailleY = param.nbLignes << 1;
			imgCpc.Reset(true);
			Convert(false);
		}

		private void mode_SelectedIndexChanged(object sender, EventArgs e) {
			BitmapCpc.modeVirtuel = param.modeVirtuel = mode.SelectedIndex;
			imgCpc.Reset(true);
			trackModeX.Visible = mode.SelectedIndex == 5;
			bpEditTrame.Visible = mode.SelectedIndex == 7;
			Convert(false);
		}

		private void InterfaceChange(object sender, EventArgs e) {
			bpSave.Enabled = !autoRecalc.Checked;
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

		private void withCode_CheckedChanged(object sender, EventArgs e) {
			param.withCode = withCode.Checked;
			if (withCode.Checked)
				withPalette.Checked = true;
		}

		private void radioUserSize_CheckedChanged(object sender, EventArgs e) {
			tbxPosX.Visible = tbxPosY.Visible = tbxSizeX.Visible = tbxSizeY.Visible = label5.Visible = label7.Visible = radioUserSize.Checked || radioOrigin.Checked;
			if (radioOrigin.Checked || (radioUserSize.Checked && tbxSizeX.Text == "" && tbxSizeY.Text == ""))
				SetSizePos(0, 0, imgSrc.GetImage.Width, imgSrc.GetImage.Height);

			tbxSizeX.Enabled = tbxSizeY.Enabled = tbxPosX.Enabled = tbxPosY.Enabled = !radioOrigin.Checked;
			//bpCalcSprite.Visible = radioUserSize.Checked;
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

		private void bpCalcSprite_Click(object sender, EventArgs e) {
			Bitmap bmp = imgSrc.GetImage;
			if (bmp != null) {


				int xmin = 0, ymin = 0, xmax = 0, ymax = 0;
				// Calcule xMin;
				for (int x = 0; x < bmp.Width; x++) {
					for (int y = 0; y < bmp.Height; y++) {
						if ((bmp.GetPixel(x, y).ToArgb() & 0xFFFFFF) > 0) {
							y = bmp.Height;
							x = bmp.Width;
						}
					}
					if (x < bmp.Width)
						xmin = x;
				}
				// Calcule yMin;
				for (int y = 0; y < bmp.Height; y++) {
					for (int x = 0; x < bmp.Width; x++) {
						if ((bmp.GetPixel(x, y).ToArgb() & 0xFFFFFF) > 0) {
							y = bmp.Height;
							x = bmp.Width;
						}
					}
					if (y < bmp.Height)
						ymin = y;
				}
				// Calcule xMax
				for (int x = bmp.Width; --x > 0; ) {
					for (int y = 0; y < bmp.Height; y++) {
						if ((bmp.GetPixel(x, y).ToArgb() & 0xFFFFFF) > 0) {
							y = bmp.Height;
							x = 0;
						}
					}
					if (x > 0)
						xmax = x;
				}
				// Calcule yMax;
				for (int y = bmp.Height; --y > 0; ) {
					for (int x = 0; x < bmp.Width; x++) {
						if ((bmp.GetPixel(x, y).ToArgb() & 0xFFFFFF) > 0) {
							y = 0;
							x = bmp.Width;
						}
					}
					if (y > 0)
						ymax = y;
				}
				SetSizePos(xmin, ymin, bmp.Width, bmp.Height);
				BitmapCpc.TailleX = xmax - xmin;
				BitmapCpc.TailleY = ymax - ymin;
				Convert(false);
			}
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

		private void bpEditTrame_Click(object sender, EventArgs e) {
			EditTrameAscii dg = new EditTrameAscii(this, imgSrc, imgCpc, param);
			dg.ShowDialog();
			Convert(false);
		}

		private void chkInfo_CheckedChanged(object sender, EventArgs e) {
			if (chkInfo.Checked)
				info.Show();
			else
				info.Hide();
		}

		private void Main_DragEnter(object sender, DragEventArgs e) {
			if (e.Data.GetDataPresent(DataFormats.FileDrop))
				e.Effect = DragDropEffects.Copy;
		}

		private void Main_DragDrop(object sender, DragEventArgs e) {
			string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
			switch (Path.GetExtension(files[0]).ToLower()) {
				case ".pal":
					imgCpc.LirePalette(files[0], param);
					break;

				case ".xml":
					ReadParam(files[0]);
					break;

				default:
					ReadScreen(files[0]);
					break;
			}
		}
	}
}
