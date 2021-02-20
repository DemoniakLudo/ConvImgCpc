using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net;
using System.Reflection;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace ConvImgCpc {
	public partial class Main : Form {
		public ImageSource imgSrc = new ImageSource();
		public ImageCpc imgCpc;
		public Param param = new Param();
		private MemoryStream imageStream;
		private Informations info = new Informations();
		private Animation anim;
		private ParamInterne paramInterne;
		public Multilingue multilingue = new Multilingue();

		public Main() {
			InitializeComponent();
			imgCpc = new ImageCpc(this, Convert);
			anim = new Animation(this);
			paramInterne = new ParamInterne(this);
			paramInterne.InitValues();
			ChangeLanguage("FR");
			int i = 1;
			foreach (KeyValuePair<string, double[,]> dith in Dither.dicMat)
				methode.Items.Insert(i++, dith.Key);

			SetModes();

			nbCols.Value = BitmapCpc.TailleX >> 3;
			nbLignes.Value = BitmapCpc.TailleY >> 1;
			methode.SelectedIndex = 0;
			param.pctContrast = param.pctLumi = param.pctSat = param.pctRed = param.pctGreen = param.pctBlue = 100;
			param.withCode = withCode.Checked;
			param.withPalette = withPalette.Checked;
			Version version = Assembly.GetExecutingAssembly().GetName().Version;
			lblInfoVersion.Text = "V " + version.ToString() + " - " + new DateTime(2000, 1, 1).AddDays(version.Build).ToShortDateString();
			radioUserSize_CheckedChanged(null, null);
			string configDetault = "ConvImgCpc.xml";
			if (File.Exists(configDetault))
				ReadParam(configDetault);

			CheckVersion(version);

			anim.Show();
			imgCpc.Show();
			Show();
		}

		private void CheckVersion(Version version) {
			byte[] buffer = new byte[0x4000];
			string url = "http://ldeplanque.free.fr/ConvImgCpc/new/ConvImgCpc.exe";
			WebRequest objRequest = WebRequest.Create(url);
			WebResponse objResponse = objRequest.GetResponse();
			Stream input = objResponse.GetResponseStream();
			MemoryStream output = new MemoryStream();
			int bytesRead;
			while ((bytesRead = input.Read(buffer, 0, buffer.Length)) > 0) {
				output.Write(buffer, 0, bytesRead);
			}
			input.Close();
			input.Dispose();
			output.Close();
			byte[] lastVersion = output.ToArray();
			for (int i = 0; i < lastVersion.Length - 32; i++) {
				if (lastVersion[i] == 'V' &&
					lastVersion[i + 2] == 'e' &&
					lastVersion[i + 4] == 'r' &&
					lastVersion[i + 6] == 's' &&
					lastVersion[i + 8] == 'i' &&
					lastVersion[i + 10] == 'o' &&
					lastVersion[i + 12] == 'n' &&
					lastVersion[i + 18] == '.' &&
					lastVersion[i + 22] == '.' &&
					lastVersion[i + 32] == '.'
					) {
					int downVersion = System.Convert.ToInt32(new string(new char[4] { (char)lastVersion[i + 24], (char)lastVersion[i + 26], (char)lastVersion[i + 28], (char)lastVersion[i + 30] }));
					if (downVersion > version.Build)
						MessageBox.Show(multilingue.GetString("Main.prg.TxtInfo29") + "\n\n\r" + url);

					break;
				}
			}
			output.Dispose();
		}

		public void ChangeLanguage(Control.ControlCollection ctrl, string prefix) {
			foreach (Control c in ctrl) {
				string key = prefix + "." + c.Name;
				string text = multilingue.GetString(key);
				if (text != null)
					c.Text = text;

				if (c.Controls.Count > 0)
					ChangeLanguage(c.Controls, prefix);
			}
		}

		private void ChangeLanguage(string lang) {
			param.langue = lang;
			multilingue.SetLangue(lang);
			ChangeLanguage(Controls, "Main");
			ChangeLanguage(imgCpc.Controls, "ImageCpc");
			ChangeLanguage(anim.Controls, "Animation");
		}

		private void SetModes() {
			mode.Items.Clear();
			int mMax = modePlus.Checked ? BitmapCpc.modesVirtuels.Length : BitmapCpc.modesVirtuels.Length - 1;
			for (int i = 0; i < mMax; i++)
				mode.Items.Insert(i, BitmapCpc.modesVirtuels[i]);

			mode.SelectedIndex = BitmapCpc.modeVirtuel < mMax ? BitmapCpc.modeVirtuel : 1;
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
					param.diffErr = chkDiffErr.Checked;
					param.lockState = imgCpc.lockState;
					param.setPalCpc = chkPalCpc.Checked;
					param.trameTc = chkTrameTC.Checked;
					param.newReduc = chkNewReduc.Checked;
					DirectBitmap tmp = GetResizeBitmap();
					if (!noInfo && doConvert)
						SetInfo(multilingue.GetString("Main.Prg.TxtInfo1"));

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

		public void DisplayErreur(string msg) {
			MessageBox.Show(msg, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
			SetInfo("Erreur - " + msg);
		}

		private void ReadScreen(string fileName, bool singlePicture = false) {
			FileStream fileScr = new FileStream(fileName, FileMode.Open, FileAccess.Read);
			byte[] tabBytes = new byte[fileScr.Length];
			fileScr.Read(tabBytes, 0, tabBytes.Length);
			fileScr.Close();
			int nbImg = 0;
			try {
				bool isImp = false, isScrImp = false;
				if (CpcSystem.CheckAmsdos(tabBytes)) {
					int nbImages = 1, width = 1, height = 1;
					CpcAmsdos enteteAms = CpcSystem.GetAmsdos(tabBytes);
					// Vérifier si type scr imp
					isScrImp = (enteteAms.FileName.EndsWith("SCR") && enteteAms.FileType == 0 && enteteAms.Adress == 0x170);
					if (!isScrImp) {
						// Vérifier si type imp
						if (enteteAms.FileName.EndsWith("IMP")) {
							int l = tabBytes.Length;
							nbImages = tabBytes[l - 3];
							width = tabBytes[l - 2];
							height = tabBytes[l - 1];
							int animSize = nbImages * width * height;
							isImp = l - 131 == animSize; // 131 + 128 (Amsdos) + 3 (imp)
						}
					}
					if (isImp) {
						imgCpc.InitBitmapCpc(nbImages, imgSrc.tpsFrame);
						imgSrc.InitBitmap(nbImages);
						anim.SetNbImgs(nbImages, imgSrc.tpsFrame);
						SetInfo(multilingue.GetString("Main.Prg.TxtInfo2") + nbImages + " images.");
						BitmapCpc.TailleX = width << 3;
						BitmapCpc.TailleY = height << 1;
						int x = BitmapCpc.NbCol;
						int y = BitmapCpc.NbLig;
						int posData = 128; // Entête Amsdos
						for (int i = 0; i < nbImages; i++) {
							SelectImage(i, true);
							byte[] tempData = new byte[width * height];
							Array.Copy(tabBytes, posData, tempData, 0, tempData.Length);
							posData += tempData.Length;
							BitmapCpc bmp = new BitmapCpc(tempData, width << 3, height << 1);
							imgSrc.ImportBitmap(bmp.CreateImageFromCpc(tabBytes.Length - 0x80, param, true), i);
						}
					}
					else
						if (isScrImp) {
						BitmapCpc bmp = new BitmapCpc(tabBytes, 0x110);
						if (singlePicture)
							imgSrc.ImportBitmap(bmp.CreateImageFromCpc(tabBytes.Length - 0x80, param), imgCpc.selImage);
						else {
							BitmapCpc.modeVirtuel = param.modeVirtuel = mode.SelectedIndex = tabBytes[0x94] - 0x0E;
							BitmapCpc.TailleX = 768;
							nbLignes.Value = param.nbLignes = BitmapCpc.NbLig;
							BitmapCpc.TailleY = 544;
							nbCols.Value = param.nbCols = BitmapCpc.NbCol;
							BitmapCpc.cpcPlus = tabBytes[0xBC] != 0;
							if (BitmapCpc.cpcPlus) {
								// Palette en 0x0711;
								for (int i = 0; i < 16; i++)
									BitmapCpc.Palette[i] = ((tabBytes[0x0711 + (i << 1)] << 4) & 0xF0) + (tabBytes[0x0711 + (i << 1)] >> 4) + (tabBytes[0x0712 + (i << 1)] << 8);
							}
							else {
								// Palette en 0x7E10
								for (int i = 0; i < 16; i++)
									BitmapCpc.Palette[i] = BitmapCpc.CpcVGA.IndexOf((char)tabBytes[0x7E10 + i]);
							}
							imgSrc.InitBitmap(bmp.CreateImageFromCpc(tabBytes.Length - 0x80, param));
						}
					}
					else {
						BitmapCpc bmp = new BitmapCpc(tabBytes, 0x80);
						if (singlePicture)
							imgSrc.ImportBitmap(bmp.CreateImageFromCpc(tabBytes.Length - 0x80, param), imgCpc.selImage);
						else {
							imgSrc.InitBitmap(bmp.CreateImageFromCpc(tabBytes.Length - 0x80, param));
							nbCols.Value = param.nbCols = BitmapCpc.NbCol;
							BitmapCpc.TailleX = param.nbCols << 3;
							nbLignes.Value = param.nbLignes = BitmapCpc.NbLig;
							BitmapCpc.TailleY = param.nbLignes << 1;
							param.modeVirtuel = mode.SelectedIndex = BitmapCpc.modeVirtuel;
						}
						SetInfo(multilingue.GetString("Main.prg.TxtInfo3"));
					}
				}
				else {
					imageStream = new MemoryStream(tabBytes);
					imageStream.Position = 0;
					if (!singlePicture) {
						imgSrc.InitBitmap(imageStream);
						nbImg = imgSrc.NbImg;
						anim.SetNbImgs(nbImg, imgSrc.tpsFrame);
						chkAllPics.Visible = nbImg > 1;
						SetInfo(multilingue.GetString("Main.prg.TxtInfo4") + (nbImg > 0 ? (multilingue.GetString("Main.prg.TxtInfo5") + nbImg + " images.") : "."));
					}
					else {
						imgSrc.ImportBitmap(new Bitmap(imageStream), imgCpc.selImage);
						SetInfo(multilingue.GetString("Main.prg.TxtInfo4"));
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
				if (!singlePicture && !isImp)
					imgCpc.InitBitmapCpc(nbImg, imgSrc.tpsFrame);

				SelectImage(0);
				imgCpc.Reset(true);
				Convert(false);
			}
			catch {
				DisplayErreur(multilingue.GetString("Main.prg.TxtInfo6"));
			}
		}

		public void SelectImage(int n, bool noInfo = false) {
			if (n > -1) {
				imgSrc.SelectBitmap(n);
				imgCpc.selImage = n;
				imgCpc.Reset();
				anim.DrawImages(n);
				if (!noInfo && imgSrc.NbImg > 1)
					SetInfo(multilingue.GetString("Main.prg.TxtInfo7") + n.ToString());
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
				if (param.sMode == Param.SizeMode.UserSize) {
					tbxPosX.Text = param.posx.ToString();
					tbxPosY.Text = param.posy.ToString();
					tbxSizeX.Text = param.sizex.ToString();
					tbxSizeY.Text = param.sizey.ToString();
				}
				radioFit.Checked = param.sMode == Param.SizeMode.Fit;
				radioKeepLarger.Checked = param.sMode == Param.SizeMode.KeepLarger;
				radioKeepSmaller.Checked = param.sMode == Param.SizeMode.KeepSmaller;
				radioOrigin.Checked = param.sMode == Param.SizeMode.Origin;
				radioUserSize.Checked = param.sMode == Param.SizeMode.UserSize;
				nbCols.Value = param.nbCols;
				nbLignes.Value = param.nbLignes;
				mode.SelectedIndex = param.modeVirtuel;
				withCode.Checked = param.withCode;
				withPalette.Checked = param.withPalette;
				chkPalCpc.Checked = param.setPalCpc;
				chkLissage.Checked = param.lissage;
				paramInterne.InitValues();
				ChangeLanguage(param.langue);
				SetInfo(multilingue.GetString("Main.prg.TxtInfo8"));
			}
			catch {
				DisplayErreur(multilingue.GetString("Main.prg.TxtInfo9"));
			}
			fileParam.Close();
		}

		private void SaveParam(string fileName) {
			FileStream file = File.Open(fileName, FileMode.Create);
			try {
				param.withCode = withCode.Checked;
				param.withPalette = withPalette.Checked;
				if (param.sMode == Param.SizeMode.UserSize)
					GetSizePos(ref param.posx, ref param.posy, ref param.sizex, ref param.sizey);

				new XmlSerializer(typeof(Param)).Serialize(file, param);
				SetInfo(multilingue.GetString("Main.prg.TxtInfo10"));
			}
			catch {
				DisplayErreur(multilingue.GetString("Main.prg.TxtInfo11"));
			}
			file.Close();
		}

		public void ReadPaletteKit(string fileName, Label[] colors) {
			if (File.Exists(fileName)) {
				FileStream fileScr = new FileStream(fileName, FileMode.Open, FileAccess.Read);
				byte[] tabBytes = new byte[fileScr.Length];
				fileScr.Read(tabBytes, 0, tabBytes.Length);
				fileScr.Close();
				if (CpcSystem.CheckAmsdos(tabBytes)) {
					BitmapCpc.paletteSprite[0] = 0;
					colors[0].BackColor = Color.Black;
					colors[0].Refresh();
					for (int i = 0; i < 15; i++) {
						int kit = tabBytes[128 + (i << 1)] + (tabBytes[129 + (i << 1)] << 8);
						int col = (kit & 0xF00) + ((kit & 0x0F) << 4) + ((kit & 0xF0) >> 4);
						BitmapCpc.paletteSprite[i + 1] = col;
						colors[i + 1].BackColor = Color.FromArgb((byte)((col & 0x0F) * 17), (byte)(((col & 0xF00) >> 8) * 17), (byte)(((col & 0xF0) >> 4) * 17));
						colors[i + 1].Refresh();
					}
					SetInfo(multilingue.GetString("Main.prg.TxtInfo12"));
				}
			}
		}

		public void SavePaletteKit(string fileName, int[] palette) {
			CpcAmsdos entete = CpcSystem.CreeEntete(Path.GetFileName(fileName), -32768, 30, 0);
			BinaryWriter fp = new BinaryWriter(new FileStream(fileName, FileMode.Create));
			fp.Write(CpcSystem.AmsdosToByte(entete));
			for (int i = 0; i < 15; i++) {
				int kit = BitmapCpc.paletteSprite[i + 1];
				byte c1 = (byte)(((kit & 0x0F) << 4) + ((kit & 0xF0) >> 4));
				byte c2 = (byte)(kit >> 8);
				fp.Write(c1);
				fp.Write(c2);
			}
			fp.Close();
			SetInfo(multilingue.GetString("Main.prg.TxtInfo13"));
		}

		private void bpCreate_Click(object sender, EventArgs e) {
			CreationImages dlg = new CreationImages(this);
			dlg.ShowDialog();
			int nbImages = dlg.NbImages;
			if (nbImages != -1) {
				imgSrc.InitBitmap(nbImages);
				if (nbImages == 1)
					SetInfo(multilingue.GetString("Main.prg.TxtInfo14"));
				else {
					anim.SetNbImgs(nbImages, 100);
					SetInfo(multilingue.GetString("Main.prg.TxtInfo15") + nbImages + " images.");
				}
				SelectImage(0);
				imgCpc.InitBitmapCpc(nbImages, 100);
				imgCpc.Reset(true);
				Convert(false);
			}
		}

		private void bpImport_Click(object sender, EventArgs e) {
			OpenFileDialog dlg = new OpenFileDialog();
			dlg.Filter = multilingue.GetString("Main.prg.TxtInfo16") + " (*.bmp, *.gif, *.png, *.jpg,*.jpeg, *.scr)|*.bmp;*.gif;*.png;*.jpg;*.jpeg;*.scr|"
						+ multilingue.GetString("Main.prg.TxtInfo17") + "|*.*";
			dlg.InitialDirectory = param.lastReadPath;
			DialogResult result = dlg.ShowDialog();
			if (result == DialogResult.OK) {
				ReadScreen(dlg.FileName, true);
				param.lastReadPath = Path.GetDirectoryName(dlg.FileName);
			}
		}

		private void bpLoad_Click(object sender, EventArgs e) {
			OpenFileDialog dlg = new OpenFileDialog();
			dlg.Filter = multilingue.GetString("Main.prg.TxtInfo16") + " (*.bmp, *.gif, *.png, *.jpg,*.jpeg, *.scr, *.imp)|*.bmp;*.gif;*.png;*.jpg;*.jpeg;*.scr;*.imp|"
						+ multilingue.GetString("Main.prg.TxtInfo18") + " (*.pal)|*.pal|" + multilingue.GetString("Main.prg.TxtInfo19") + " (*.xml)|*.xml|"
						+ multilingue.GetString("Main.prg.TxtInfo17") + "|*.*";
			dlg.InitialDirectory = param.lastReadPath;
			DialogResult result = dlg.ShowDialog();
			if (result == DialogResult.OK) {
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
				param.lastReadPath = Path.GetDirectoryName(dlg.FileName);
			}
		}

		private void bpSave_Click(object sender, EventArgs e) {
			SaveFileDialog dlg = new SaveFileDialog();
			dlg.InitialDirectory = param.lastSavePath;
			string filter = multilingue.GetString("Main.prg.TxtInfo20") + " (*.scr)|*.scr|Bitmap (.png)|*.png|"
							+ multilingue.GetString("Main.prg.TxtInfo21") + " (.asm)|*.asm|"
							+ multilingue.GetString("Main.prg.TxtInfo22") + " (.asm)|*.asm|"
							+ multilingue.GetString("Main.prg.TxtInfo23") + " (.cmp)|*.cmp|"
							+ multilingue.GetString("Main.prg.TxtInfo24") + " (.asm)|*.asm|"
							+ multilingue.GetString("Main.prg.TxtInfo25") + " (.asm)|*.asm|"
							+ multilingue.GetString("Main.prg.TxtInfo26") + " (.imp)|*.imp|"
							+ multilingue.GetString("Main.prg.TxtInfo19") + "Paramètres (.xml)|*.xml|"
							+ multilingue.GetString("Main.prg.TxtInfo18") + " (.pal)|*.pal"
							+ (BitmapCpc.cpcPlus ? ("|" + multilingue.GetString("Main.prg.TxtInfo27") + " (.kit)|*.kit") : "")
							+ (BitmapCpc.modeVirtuel == 3 || BitmapCpc.modeVirtuel == 4 ? ("|" + multilingue.GetString("Main.prg.TxtInfo28") + " (.scr)|*.scr") : "");

			dlg.Filter = filter;
			DialogResult result = dlg.ShowDialog();
			if (result == DialogResult.OK) {
				switch (dlg.FilterIndex) {
					case 1:
						imgCpc.SauveScr(dlg.FileName, param);
						break;

					case 2:
						imgCpc.SauvePng(dlg.FileName, param);
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
						imgCpc.SauveDeltaPack(dlg.FileName, lblInfoVersion.Text, param, true);
						break;

					case 8:
						imgCpc.SauveImp(dlg.FileName);
						break;

					case 9:
						SaveParam(dlg.FileName);
						break;

					case 10:
						imgCpc.SauvePalette(dlg.FileName, param);
						break;

					case 11:
					case 12:
						if (BitmapCpc.cpcPlus && dlg.FilterIndex == 11)
							SavePaletteKit(dlg.FileName, BitmapCpc.Palette);
						else
							imgCpc.SauveEgx(dlg.FileName, param);
						break;
				}
				param.lastSavePath = Path.GetDirectoryName(dlg.FileName);
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
			trackModeX.Visible = mode.SelectedIndex == 5 || mode.SelectedIndex == 6;
			bpEditTrame.Visible = mode.SelectedIndex == 7;
			Convert(false);
		}

		private void InterfaceChange(object sender, EventArgs e) {
			if (autoRecalc.Checked)
				chkAllPics.Checked = false;

			if (!chkDiffErr.Visible)
				chkDiffErr.Checked = methode.SelectedItem.ToString() == "Floyd-Steinberg (2x2)";

			bpSave.Enabled = !autoRecalc.Checked;
			lblPct.Visible = pctTrame.Visible = chkDiffErr.Visible = methode.SelectedItem.ToString() != "Aucun";
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
				for (int x = bmp.Width; --x > 0;) {
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
				for (int y = bmp.Height; --y > 0;) {
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
				if (xmax > xmin)
					BitmapCpc.TailleX = xmax - xmin;

				if (ymax > ymin)
					BitmapCpc.TailleY = ymax - ymin;

				Convert(false);
			}
		}

		private void withPalette_CheckedChanged(object sender, EventArgs e) {
			param.withPalette = withPalette.Checked;
		}

		private void bpEditTrame_Click(object sender, EventArgs e) {
			EditTrameAscii dg = new EditTrameAscii(this, imgSrc, imgCpc, param);
			dg.ShowDialog();
			Convert(false);
		}


		private void bpEditSprites_Click(object sender, EventArgs e) {
			EditSprites dg = new EditSprites(this);
			dg.ShowDialog();
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

		private void chkAllPics_CheckedChanged(object sender, EventArgs e) {
			if (chkAllPics.Checked)
				autoRecalc.Checked = false;
		}

		private void chkParamInterne_CheckedChanged(object sender, EventArgs e) {
			if (chkParamInterne.Checked) {
				paramInterne.Text = chkParamInterne.Text;
				ChangeLanguage(paramInterne.Controls, "ParamInterne");
				paramInterne.Show();
			}
			else
				paramInterne.Hide();
		}

		private void chkImpDraw_CheckedChanged(object sender, EventArgs e) {
			param.modeImpDraw = chkImpDraw.Checked;
			if (Enabled)
				Convert(false);
		}

		#region Gestion des couleurs
		private void red_ValueChanged(object sender, EventArgs e) {
			param.pctRed = red.Value;
			if (Enabled)
				Convert(false);
		}

		private void green_ValueChanged(object sender, EventArgs e) {
			param.pctGreen = green.Value;
			if (Enabled)
				Convert(false);
		}

		private void blue_ValueChanged(object sender, EventArgs e) {
			param.pctBlue = blue.Value;
			if (Enabled)
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
			param.pctRed = red.Value = 100;
			Convert(false);
		}

		private void RazV_Click(object sender, EventArgs e) {
			param.pctGreen = green.Value = 100;
			Convert(false);
		}

		private void RazB_Click(object sender, EventArgs e) {
			param.pctBlue = blue.Value = 100;
			Convert(false);
		}

		private void lumi_ValueChanged(object sender, EventArgs e) {
			param.pctLumi = (int)lumi.Value;
			if (Enabled)
				Convert(false);
		}

		private void sat_ValueChanged(object sender, EventArgs e) {
			param.pctSat = nb.Checked ? 0 : (int)sat.Value;
			if (Enabled)
				Convert(false);
		}

		private void contrast_ValueChanged(object sender, EventArgs e) {
			param.pctContrast = (int)contrast.Value;
			if (Enabled)
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
			param.pctLumi = lumi.Value = 100;
			Convert(false);
		}

		private void bpRazSat_Click(object sender, EventArgs e) {
			param.pctSat = sat.Value = 100;
			Convert(false);
		}

		private void bpRazContrast_Click(object sender, EventArgs e) {
			param.pctContrast = contrast.Value = 100;
			Convert(false);
		}

		private void sortPal_CheckedChanged(object sender, EventArgs e) {
			param.sortPal = sortPal.Checked;
			Convert(false);
		}

		private void newMethode_CheckedChanged(object sender, EventArgs e) {
			param.newMethode = newMethode.Checked;
			if (Enabled)
				Convert(false);
		}

		private void nb_CheckedChanged(object sender, EventArgs e) {
			bpSatMoins.Enabled = bpSatPlus.Enabled = bpRazSat.Enabled = sat.Enabled = !nb.Checked;
			param.pctSat = nb.Checked ? 0 : (int)sat.Value;
			if (Enabled)
				Convert(false);
		}

		private void reducPal1_CheckedChanged(object sender, EventArgs e) {
			param.reductPal1 = reducPal1.Checked;
			if (Enabled)
				Convert(false);
		}

		private void reducPal2_CheckedChanged(object sender, EventArgs e) {
			param.reductPal2 = reducPal2.Checked;
			if (Enabled)
				Convert(false);
		}

		private void reducPal3_CheckedChanged(object sender, EventArgs e) {
			param.reductPal3 = reducPal3.Checked;
			if (Enabled)
				Convert(false);
		}

		private void reducPal4_CheckedChanged(object sender, EventArgs e) {
			param.reductPal4 = reducPal4.Checked;
			if (Enabled)
				Convert(false);
		}

		private void modePlus_CheckedChanged(object sender, EventArgs e) {
			bpEditSprites.Visible = modePlus.Checked;
			BitmapCpc.cpcPlus = modePlus.Checked;
			newMethode.Visible = !modePlus.Checked;
			param.cpcPlus = modePlus.Checked;
			SetModes();
			Convert(false);
		}

		private void rb24bits_CheckedChanged(object sender, EventArgs e) {
			param.bitsRVB = 24;
			if (Enabled)
				Convert(false);
		}

		private void rb12bits_CheckedChanged(object sender, EventArgs e) {
			param.bitsRVB = 12;
			Convert(false);
		}

		private void rb9bits_CheckedChanged(object sender, EventArgs e) {
			param.bitsRVB = 9;
			Convert(false);
		}

		private void rb6bits_CheckedChanged(object sender, EventArgs e) {
			param.bitsRVB = 6;
			Convert(false);
		}

		private void bpRaz_Click(object sender, EventArgs e) {
			Enabled = false;
			param.pctRed = red.Value = 100;
			param.pctGreen = green.Value = 100;
			param.pctBlue = blue.Value = 100;
			param.pctLumi = lumi.Value = 100;
			param.pctSat = sat.Value = 100;
			param.pctContrast = contrast.Value = 100;
			param.newMethode = newMethode.Checked = false;
			bpSatMoins.Enabled = bpSatPlus.Enabled = bpRazSat.Enabled = sat.Enabled = true;
			nb.Checked = false;
			param.reductPal1 = reducPal1.Checked = false;
			param.reductPal2 = reducPal2.Checked = false;
			param.reductPal3 = reducPal3.Checked = false;
			param.reductPal4 = reducPal4.Checked = false;
			newMethode.Visible = !modePlus.Checked;
			param.cpcPlus = modePlus.Checked;
			param.bitsRVB = 24;
			Enabled = true;
			Convert(false);
		}
		#endregion

		private void Main_Click(object sender, EventArgs e) {
			imgCpc.BringToFront();
		}

		private void bpFr_Click(object sender, EventArgs e) {
			ChangeLanguage("FR");
		}

		private void bpEn_Click(object sender, EventArgs e) {
			ChangeLanguage("EN");
		}

	}
}
