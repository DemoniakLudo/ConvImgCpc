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
		public Animation anim;
		private ParamInterne paramInterne;
		public Multilingue multilingue = new Multilingue();
		public enum PackMethode { None = 0, Standard, ZX0, ZX1 };
		private PackMethode pkMethode = PackMethode.None;
		private Version version = Assembly.GetExecutingAssembly().GetName().Version;
		public GestDSK dsk;
		public enum OutputFormat { Binary = 0, Assembler, DSK, SNA };
		private bool doNotReset = false;

		public Main(string[] args) {
			InitializeComponent();
			imgCpc = new ImageCpc(this, Convert);
			anim = new Animation(this);
			paramInterne = new ParamInterne(this);
			dsk = new GestDSK(this);
			paramInterne.InitValues();
			ChangeLanguage("FR");
			int i = 1;
			foreach (KeyValuePair<string, double[,]> dith in Dither.dicMat)
				methode.Items.Insert(i++, dith.Key);

			SetModes();
			Cpc.CopyTrame(0);     // Copier par défaut la trame 0	

			nbCols.Value = Cpc.TailleX >> 3;
			nbLignes.Value = Cpc.TailleY >> 1;
			methode.SelectedIndex = 0;
			param.pctContrast = param.pctLumi = param.pctSat = param.pctRed = param.pctGreen = param.pctBlue = 100;
			param.withCode = withCode.Checked;
			param.withPalette = withPalette.Checked;
			lblInfoVersion.Text = "V " + version.ToString() + " - " + new DateTime(2000, 1, 1).AddDays(version.Build).ToShortDateString();
			radioUserSize_CheckedChanged(null, null);
			string configDetault = "ConvImgCpc.xml";
			if (File.Exists(configDetault))
				ReadParam(configDetault);

			anim.Show();
			imgCpc.Show();
			Show();
			if (args.Length > 0) {
				bool isAsm = false, lockFirst = false;
				Enabled = false;
				chkInfo.Checked = true;
				info.Show();
				foreach (string p in args) {
					/*
-DX	Valeur	Indique le déplacement relatif en X à appliquer à l'image source. Par défaut = 0.
-DY	Valeur	Indique le déplacement relatif en Y à appliquer à l'image source. Par défaut = 0.
-NX	Valeur	Indique la nouvelle taille en X à utiliser pour les images sources.
-NY	Valeur	Indique la nouvelle taille en Y à utiliser pour les images sources.
-O	(Aucun)	Indique qu'il faut garder la taille d'origine pour les images sources.
 */
					int v;
					string pUp = p.ToUpper();
					if (pUp[0] == '-') {
						string arg = pUp.Substring(2);
						switch (pUp[1]) {
							// -A sauvegarde en assembleur
							case 'A':
								isAsm = true;
								break;

							// -BpctBleu
							case 'B':
								if (int.TryParse(arg, out v) && v >= 0 && v <= 800)
									param.pctBlue = v;

								break;

							// -CFicPalette
							case 'C':
								imgCpc.LirePalette(arg, param);
								for (v = 0; v < 16; v++)
									imgCpc.lockState[v] = param.lockState[v] = 1;

								break;

							// -Ffichiers
							case 'F':
								string file = Path.GetFileName(arg);
								string path = Path.GetDirectoryName(arg);
								if (path == "")
									path = ".";

								string[] tabFiles = Directory.GetFiles(path, file);
								foreach (string f in tabFiles) {
									info.SetInfos("Chargement " + f);
									ReadScreen(f);
									Convert(true);
									if (isAsm && pkMethode == PackMethode.None) {
										string fs = f.Substring(0, f.IndexOf(Path.GetExtension(f))) + ".ASM";
										info.SetInfos("Sauvegarde " + fs);
										imgCpc.SauveSprite(fs, lblInfoVersion.Text);
									}
									else {
										string fs = f.Substring(0, f.IndexOf(Path.GetExtension(f))) + (isAsm ? ".ASM" : pkMethode == PackMethode.None ? ".SCR" : ".CMP");
										info.SetInfos("Sauvegarde " + fs);
										if (!isAsm && pkMethode == PackMethode.None)
											imgCpc.SauveScr(fs, OutputFormat.Binary);
										else {
											OutputFormat format = isAsm ? OutputFormat.Assembler : OutputFormat.Binary;
											imgCpc.SauveCmp(fs, pkMethode, format, lblInfoVersion.Text);
										}
									}
									if (lockFirst) {
										for (v = 0; v < 16; v++)
											imgCpc.lockState[v] = param.lockState[v] = 1;
									}
								}
								break;

							// -I Keep Smaller
							case 'I':
								radioKeepSmaller.Checked = true;
								InterfaceChange(null, null);
								break;

							// -Kpack (methode de compactage: 0=aucun,1=standard,2=ZX0,3=ZX1)
							case 'K':
								if (int.TryParse(arg, out v) && v >= 0 && v <= 3)
									pkMethode = (PackMethode)v;

								break;

							// -L lock palette
							case 'L':
								lockFirst = true;
								break;

							// -Mmode
							case 'M':
								if (int.TryParse(arg, out v) && v >= 0 && v <= 10)
									Cpc.modeVirtuel = param.modeVirtuel = mode.SelectedIndex = v;

								break;

							// -PpctTrame
							case 'P':
								if (int.TryParse(arg, out v) && v >= 0 && v <= 400)
									param.pct = v;

								break;

							// -RpctRouge
							case 'R':
								if (int.TryParse(arg, out v) && v >= 0 && v <= 800)
									param.pctRed = v;

								break;

							// -S Keep Larger
							case 'S':
								radioKeepLarger.Checked = true;
								InterfaceChange(null, null);
								break;

							// -Ttrame
							case 'T':
								if (int.TryParse(arg, out v) && v >= 0 && v < Dither.dicMat.Count) {
									methode.SelectedItem = v;
									InterfaceChange(null, null);
								}
								break;

							// -U0 ou -U1 (palette OLD ou PLUS)
							case 'U':
								if (int.TryParse(arg, out v) && (v == 0 || v == 1)) {
									modePlus.Checked = v != 0;
									modePlus_CheckedChanged(null, null);
								}
								break;

							// -VpctVert
							case 'V':
								if (int.TryParse(arg, out v) && v >= 0 && v <= 800)
									param.pctGreen = v;

								break;

							// -X Diffusion d'erreur pour tramage
							case 'X':
								chkDiffErr.Checked = true;
								InterfaceChange(null, null);
								break;

							// -Z0 ou =Z1 (standard ou overscan)
							case 'Z':
								if (int.TryParse(arg, out v) && (v == 0 || v == 1)) {
									nbLignes.Value = v > 0 ? 272 : 200;
									nbCols.Value = v > 0 ? 96 : 80;
								}
								break;
						}
					}
				}
				Enabled = true;
			}
			else
				comboPackMethode.SelectedItem = "ZX0";

			//		int tpsStart = Environment.TickCount;
			//		bool internet = CheckForInternetConnection();
			//		int tpsElapsed = Environment.TickCount - tpsStart;
			//		if (tpsElapsed < 500 && internet)
			//			CheckMaj(true);
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
			int mMax = modePlus.Checked ? Cpc.modesVirtuels.Length : Cpc.modesVirtuels.Length - 1;
			for (int i = 0; i < mMax; i++)
				mode.Items.Insert(i, Cpc.modesVirtuels[i]);

			mode.SelectedIndex = Cpc.modeVirtuel < mMax ? Cpc.modeVirtuel : 1;
		}

		public DirectBitmap GetResizeBitmap() {
			DirectBitmap tmp = new DirectBitmap(Cpc.TailleX, Cpc.TailleY);
			Graphics g = Graphics.FromImage(tmp.Bitmap);
			g.FillRectangle(new SolidBrush(Color.FromArgb((int)(0xFF000000 + Cpc.GetPalCPC(Cpc.Palette[0])))), 0, 0, Cpc.TailleX, Cpc.TailleY);
			double ratio = imgSrc.GetImage.Width * Cpc.TailleY / (double)(imgSrc.GetImage.Height * Cpc.TailleX);
			switch (param.sMode) {
				case Param.SizeMode.KeepSmaller:
					if (ratio < 1) {
						int newW = (int)(Cpc.TailleX * ratio);
						g.DrawImage(imgSrc.GetImage, (Cpc.TailleX - newW) >> 1, 0, newW, Cpc.TailleY);
					}
					else {
						int newH = (int)(Cpc.TailleY / ratio);
						g.DrawImage(imgSrc.GetImage, 0, (Cpc.TailleY - newH) >> 1, Cpc.TailleX, newH);
					}
					break;

				case Param.SizeMode.KeepLarger:
					if (ratio < 1) {
						int newY = (int)(Cpc.TailleY / ratio);
						g.DrawImage(imgSrc.GetImage, 0, (Cpc.TailleY - newY) >> 1, Cpc.TailleX, newY);
					}
					else {
						int newX = (int)(Cpc.TailleX * ratio);
						g.DrawImage(imgSrc.GetImage, (Cpc.TailleX - newX) >> 1, 0, newX, Cpc.TailleY);
					}
					break;

				case Param.SizeMode.Fit:
					g.DrawImage(imgSrc.GetImage, 0, 0, Cpc.TailleX, Cpc.TailleY);
					break;

				case Param.SizeMode.UserSize:
				case Param.SizeMode.Origin:
					int posx = 0, posy = 0, tx = Cpc.TailleX, ty = Cpc.TailleY;
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
			imgCpc.SetImpDrawMode(param.modeImpDraw);
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
				if (Cpc.CheckAmsdos(tabBytes)) {
					int nbImages = 1, width = 1, height = 1;
					CpcAmsdos enteteAms = Cpc.GetAmsdos(tabBytes);
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
						Cpc.TailleX = width << 3;
						Cpc.TailleY = height << 1;
						int x = Cpc.NbCol;
						int y = Cpc.NbLig;
						int posData = 128; // Entête Amsdos
						for (int i = 0; i < nbImages; i++) {
							SelectImage(i, true);
							byte[] tempData = new byte[width * height];
							Array.Copy(tabBytes, posData, tempData, 0, tempData.Length);
							posData += tempData.Length;
							BitmapCpc bmp = new BitmapCpc(tempData, width << 3, height << 1);
							imgSrc.ImportBitmap(bmp.CreateImageFromCpc(tabBytes.Length - 0x80, param, pkMethode, true, imgCpc).Bitmap, i);
						}
					}
					else
						if (isScrImp) {
							BitmapCpc bmp = new BitmapCpc(tabBytes, 0x110);
							if (singlePicture)
								imgSrc.ImportBitmap(bmp.CreateImageFromCpc(tabBytes.Length - 0x80, param, pkMethode, false, imgCpc).Bitmap, imgCpc.selImage);
							else {
								doNotReset = true;
								Cpc.modeVirtuel = param.modeVirtuel = mode.SelectedIndex = tabBytes[0x94] - 0x0E;
								Cpc.TailleX = 768;
								nbLignes.Value = param.nbLignes = Cpc.NbLig;
								Cpc.TailleY = 544;
								nbCols.Value = param.nbCols = Cpc.NbCol;
								Cpc.cpcPlus = tabBytes[0xBC] != 0;
								if (Cpc.cpcPlus) {
									// Palette en 0x0711;
									for (int i = 0; i < 16; i++)
										Cpc.Palette[i] = ((tabBytes[0x0711 + (i << 1)] << 4) & 0xF0) + (tabBytes[0x0711 + (i << 1)] >> 4) + (tabBytes[0x0712 + (i << 1)] << 8);
								}
								else {
									// Palette en 0x7E10
									for (int i = 0; i < 16; i++)
										Cpc.Palette[i] = Cpc.CpcVGA.IndexOf((char)tabBytes[0x7E10 + i]);
								}
								imgSrc.InitBitmap(bmp.CreateImageFromCpc(tabBytes.Length - 0x80, param, pkMethode, false, imgCpc).Bitmap);
							}
						}
						else {
							BitmapCpc bmp = new BitmapCpc(tabBytes, 0x80);
							if (singlePicture)
								imgSrc.ImportBitmap(bmp.CreateImageFromCpc(tabBytes.Length - 0x80, param, pkMethode, false, imgCpc).Bitmap, imgCpc.selImage);
							else {
								doNotReset = true;
								imgSrc.InitBitmap(bmp.CreateImageFromCpc(tabBytes.Length - 0x80, param, pkMethode, false, imgCpc).Bitmap);
								nbCols.Value = param.nbCols = Cpc.NbCol;
								Cpc.TailleX = param.nbCols << 3;
								nbLignes.Value = param.nbLignes = Cpc.NbLig;
								Cpc.TailleY = param.nbLignes << 1;
								param.modeVirtuel = mode.SelectedIndex = Cpc.modeVirtuel;
								modePlus.Checked = Cpc.cpcPlus;
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

				if (!doNotReset) {
					if (!singlePicture && !isImp)
						imgCpc.InitBitmapCpc(nbImg, imgSrc.tpsFrame);

					SelectImage(0);
					imgCpc.Reset(true);
				}
				else
					imgCpc.Reset(false);

				Convert(false);
				doNotReset = false;
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
				for (int i = 0; i < 16; i++)
					param.lockState[i] = 0;

				radioFit.Checked = param.sMode == Param.SizeMode.Fit;
				radioKeepLarger.Checked = param.sMode == Param.SizeMode.KeepLarger;
				radioKeepSmaller.Checked = param.sMode == Param.SizeMode.KeepSmaller;
				radioOrigin.Checked = param.sMode == Param.SizeMode.Origin;
				sortPal.Checked = param.sortPal;
				radioUserSize.Checked = param.sMode == Param.SizeMode.UserSize;
				nbCols.Value = param.nbCols;
				nbLignes.Value = param.nbLignes;
				mode.SelectedIndex = param.modeVirtuel;
				withCode.Checked = param.withCode;
				withPalette.Checked = param.withPalette;
				chkPalCpc.Checked = param.setPalCpc;
				chkLissage.Checked = param.lissage;
				modePlus.Checked = param.cpcPlus;
				paramInterne.InitValues();
				ChangeLanguage(param.langue);
				SetInfo(multilingue.GetString("Main.prg.TxtInfo8"));
				lumi.Value = param.pctLumi;
				sat.Value = param.pctSat;
				contrast.Value = param.pctContrast;
				trackModeX.Value = param.trackModeX;
				switch (param.bitsRVB) {
					case 24:
						rb24bits.Checked = true;
						break;

					case 12:
						rb12bits.Checked = true;
						break;

					case 9:
						rb9bits.Checked = true;
						break;

					case 6:
						rb6bits.Checked = true;
						break;
				}
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

		public void ReadPaletteSprite(string fileName, Label[] colors) {
			if (File.Exists(fileName)) {
				FileStream fileScr = new FileStream(fileName, FileMode.Open, FileAccess.Read);
				byte[] tabBytes = new byte[fileScr.Length];
				fileScr.Read(tabBytes, 0, tabBytes.Length);
				fileScr.Close();
				if (Cpc.CheckAmsdos(tabBytes)) {
					Cpc.paletteSprite[0] = 0;
					colors[0].BackColor = Color.Black;
					colors[0].Refresh();
					for (int i = 0; i < 15; i++) {
						int kit = tabBytes[128 + (i << 1)] + (tabBytes[129 + (i << 1)] << 8);
						int col = (kit & 0xF00) + ((kit & 0x0F) << 4) + ((kit & 0xF0) >> 4);
						Cpc.paletteSprite[i + 1] = col;
						colors[i + 1].BackColor = Color.FromArgb((byte)((col & 0x0F) * 17), (byte)(((col & 0xF00) >> 8) * 17), (byte)(((col & 0xF0) >> 4) * 17));
						colors[i + 1].Refresh();
					}
					SetInfo(multilingue.GetString("Main.prg.TxtInfo12"));
				}
			}
		}

		public void SavePaletteKit(string fileName, bool isImage = false) {
			CpcAmsdos entete = Cpc.CreeEntete(Path.GetFileName(fileName), -32768, 30, 0);
			BinaryWriter fp = new BinaryWriter(new FileStream(fileName, FileMode.Create));
			fp.Write(Cpc.AmsdosToByte(entete));
			for (int i = isImage ? 0 : 1; i < 16; i++) {
				int kit = isImage ? Cpc.Palette[i] : Cpc.paletteSprite[i];
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
				chkAllPics.Visible = nbImages > 1;
				imgCpc.InitBitmapCpc(nbImages, 100);
				SelectImage(0);
				imgCpc.Reset(true);
				Convert(false);
			}
		}

		private void bpImport_Click(object sender, EventArgs e) {
			OpenFileDialog dlg = new OpenFileDialog();
			dlg.Filter = multilingue.GetString("Main.prg.TxtInfo16") + " (*.bmp, *.gif, *.png, *.jpg, *.jpeg, *.jfif, *.scr)|*.bmp;*.gif;*.png;*.jpg;*.jpeg;*.jfif;*.scr|"
						+ multilingue.GetString("Main.prg.TxtInfo17") + "|*.*";
			dlg.InitialDirectory = param.lastReadPath;
			if (dlg.ShowDialog() == DialogResult.OK) {
				imgCpc.ResetX2();
				ReadScreen(dlg.FileName, true);
				param.lastReadPath = Path.GetDirectoryName(dlg.FileName);
			}
		}

		private void bpLoad_Click(object sender, EventArgs e) {
			Enabled = false;
			OpenFileDialog dlg = new OpenFileDialog();
			dlg.Filter = multilingue.GetString("Main.prg.TxtInfo16") + " (*.bmp, *.gif, *.png, *.jpg,*.jpeg, *.jfif, *.scr, *.imp)|*.bmp;*.gif;*.png;*.jpg;*.jpeg;*.jfif;*.scr;*.imp|"
						+ multilingue.GetString("Main.prg.TxtInfo18") + " (*.pal)|*.pal|"
						+ multilingue.GetString("Main.prg.TxtInfo19") + " (*.xml)|*.xml|"
						+ (Cpc.cpcPlus ? (multilingue.GetString("Main.prg.TxtInfo39") + " (.kit)|*.kit|") : "")
						+ multilingue.GetString("Main.prg.TxtInfo17") + "|*.*";
			dlg.InitialDirectory = param.lastReadPath;
			if (dlg.ShowDialog() == DialogResult.OK) {
				imgCpc.ResetX2();
				switch (dlg.FilterIndex) {
					case 1:
					case 4:
					case 5:
						if (dlg.FilterIndex == 4 && Cpc.cpcPlus)
							imgCpc.LirePaletteKit(dlg.FileName, param);
						else
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
			Enabled = true;
		}

		private void bpSave_Click(object sender, EventArgs e) {
			Enabled = false;
			SaveFileDialog dlg = new SaveFileDialog();
			dlg.InitialDirectory = param.lastSavePath;
			string filter = multilingue.GetString("Main.prg.TxtInfo20") + " (*.scr)|*.scr|Bitmap (.png)|*.png|"             //	1 & 2
							+ multilingue.GetString("Main.prg.TxtInfo21") + " (.asm)|*.asm|"                                //	3
							+ multilingue.GetString("Main.prg.TxtInfo22") + " (.asm)|*.asm|"                                //	4
							+ multilingue.GetString("Main.prg.TxtInfo23") + " (.cmp)|*.cmp|"                                //	5
							+ multilingue.GetString("Main.prg.TxtInfo24") + " (.asm)|*.asm|"                                //	6
							+ multilingue.GetString("Main.prg.TxtInfo31") + " (.dsk)|*.dsk|"                                //	7
							+ multilingue.GetString("Main.prg.TxtInfo32") + " (.dsk)|*.dsk|"                                //	8
							+ multilingue.GetString("Main.prg.TxtInfo25") + " (.asm)|*.asm|"                                //	9
							+ multilingue.GetString("Main.prg.TxtInfo26") + " (.imp)|*.imp|"                                //	10
							+ multilingue.GetString("Main.prg.TxtInfo36") + " (.imp)|*.imp|"                                //	11
							+ multilingue.GetString("Main.prg.TxtInfo37") + " (.imp)|*.imp|"                                //	12
							+ multilingue.GetString("Main.prg.TxtInfo38") + " (.imp)|*.imp|"                                //	13
							+ multilingue.GetString("Main.prg.TxtInfo19") + "Paramètres (.xml)|*.xml|"                      //	14
							+ multilingue.GetString("Main.prg.TxtInfo18") + " (.pal)|*.pal"                                 //	15
							+ (Cpc.cpcPlus ? ("|" + multilingue.GetString("Main.prg.TxtInfo27") + " (.kit)|*.kit") : "")  //	16
							+ (Cpc.modeVirtuel == 3 || Cpc.modeVirtuel == 4 ? ("|" + multilingue.GetString("Main.prg.TxtInfo28") + " (.scr)|*.scr") : "")   //	17
							+ "|Bump (*.asm)| *.asm"  //	15
							+ "|DiffAnim (*.asm)| *.asm";

			dlg.Filter = filter;
			if (dlg.ShowDialog() == DialogResult.OK) {
				imgCpc.ResetGrille();
				switch (dlg.FilterIndex) {
					case 1:
						imgCpc.SauveScr(dlg.FileName, OutputFormat.Binary);
						break;

					case 2:
						imgCpc.SauvePng(dlg.FileName);
						break;

					case 3:
						imgCpc.SauveSprite(dlg.FileName, lblInfoVersion.Text);
						break;

					case 4:
						imgCpc.SauveSpriteCmp(dlg.FileName, lblInfoVersion.Text, pkMethode);
						break;

					case 5:
						imgCpc.SauveCmp(dlg.FileName, pkMethode, OutputFormat.Binary);
						break;

					case 6:
						imgCpc.SauveCmp(dlg.FileName, pkMethode, OutputFormat.Assembler, lblInfoVersion.Text);
						break;

					case 7:
						imgCpc.SauveScr(dlg.FileName, OutputFormat.DSK);
						break;

					case 8:
						imgCpc.SauveCmp(dlg.FileName, pkMethode, OutputFormat.DSK);
						break;

					case 9:
						imgCpc.SauveDeltaPack(dlg.FileName, lblInfoVersion.Text, true, pkMethode);
						break;

					case 10:
						imgCpc.SauveImp(dlg.FileName);
						break;

					case 11:
						imgCpc.SauveTiles(dlg.FileName, 16, 16, param);
						break;

					case 12:
						imgCpc.SauveTiles(dlg.FileName, 32, 16, param);
						break;

					case 13:
						imgCpc.SauveTiles(dlg.FileName, 32, 32, param);
						break;

					case 14:
						SaveParam(dlg.FileName);
						break;

					case 15:
						imgCpc.SauvePalette(dlg.FileName, param);
						break;

					case 16:
					case 17:
						if (Cpc.cpcPlus && dlg.FilterIndex == 16)
							SavePaletteKit(dlg.FileName, true);
						else
							if (Cpc.modeVirtuel == 3 || Cpc.modeVirtuel == 4)
								imgCpc.SauveEgx(dlg.FileName);
							else
								//imgCpc.SauvBump(dlg.FileName, lblInfoVersion.Text);
								imgCpc.SauveDiffImage(dlg.FileName, lblInfoVersion.Text, false, PackMethode.None);
						break;
				}
				param.lastSavePath = Path.GetDirectoryName(dlg.FileName);
			}
			Enabled = true;
		}

		private void nbCols_ValueChanged(object sender, EventArgs e) {
			param.nbCols = (int)nbCols.Value;
			if (!doNotReset) {
				Cpc.TailleX = param.nbCols << 3;
				imgCpc.Reset(true);
				Convert(false);
			}
		}

		private void nbLignes_ValueChanged(object sender, EventArgs e) {
			param.nbLignes = (int)nbLignes.Value;
			if (!doNotReset) {
				Cpc.TailleY = param.nbLignes << 1;
				imgCpc.Reset(true);
				Convert(false);
			}
		}

		private void mode_SelectedIndexChanged(object sender, EventArgs e) {
			Cpc.modeVirtuel = param.modeVirtuel = mode.SelectedIndex;
			trackModeX.Visible = mode.SelectedIndex == 5 || mode.SelectedIndex == 6;
			bpEditTrame.Visible = mode.SelectedIndex == 7;
			chkSwapEgx.Visible = mode.SelectedIndex == 3 || mode.SelectedIndex == 4;
			if (!doNotReset) {
				imgCpc.Reset(true);
				Convert(false);
			}
		}

		private void InterfaceChange(object sender, EventArgs e) {
			if (autoRecalc.Checked)
				chkAllPics.Checked = false;

			if (!chkDiffErr.Visible)
				chkDiffErr.Checked = methode.SelectedItem.ToString() == "Floyd-Steinberg (2x2)";

			if (radioUserSize.Checked) {
				int x = 0, y = 0;
				if (int.TryParse(tbxSizeX.Text, out x) && int.TryParse(tbxSizeY.Text, out y)) {
					bpXDiv2.Enabled = x >= 16;
					bpXMul2.Enabled = x <= 1920;
					bpYDiv2.Enabled = y >= 16;
					bpYMul2.Enabled = y <= 1080;
				}
			}

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
			bpXDiv2.Visible = bpXMul2.Visible = bpYDiv2.Visible = bpYMul2.Visible = radioUserSize.Checked;
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
				if (xmax > xmin)
					Cpc.TailleX = xmax - xmin;

				if (ymax > ymin)
					Cpc.TailleY = ymax - ymin;

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
			EditSprites dg = new EditSprites(this, pkMethode);
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
			Cpc.cpcPlus = modePlus.Checked;
			newMethode.Visible = !modePlus.Checked;
			param.cpcPlus = modePlus.Checked;
			SetModes();
			if (!Cpc.cpcPlus) {
				int[] memoLock = new int[16];
				for (int i = 0; i < 16; i++) {
					memoLock[i] = imgCpc.lockState[i];
					imgCpc.lockState[i] = 0;
				}
				Convert(false);
				for (int i = 0; i < 16; i++)
					imgCpc.lockState[i] = memoLock[i];
			}
			else {
				for (int i = 0; i < 16; i++) {
					if (Cpc.Palette[i] < 27) {
						RvbColor col = Cpc.RgbCPC[Cpc.Palette[i]];
						Cpc.Palette[i] = ((col.b >> 4) << 4) + ((col.v >> 4) << 8) + (col.r >> 4);
					}
					else
						Cpc.Palette[i] = 0;
				}
				Convert(false);
			}
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

		private void comboPackMethode_SelectedIndexChanged(object sender, EventArgs e) {
			switch (comboPackMethode.SelectedItem.ToString()) {
				case "Standard":
					pkMethode = PackMethode.Standard;
					break;

				case "ZX0":
					pkMethode = PackMethode.ZX0;
					break;

				case "ZX1":
					pkMethode = PackMethode.ZX1;
					break;
			}
		}

		private bool CheckForInternetConnection() {
			try {
				HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://google.com/generate_204");
				request.Timeout = 1000;
				HttpWebResponse response = (HttpWebResponse)request.GetResponse();
				Stream receiveStream = response.GetResponseStream();
				StreamReader readStream = new StreamReader(receiveStream, System.Text.Encoding.UTF8);
				string ret = readStream.ReadToEnd();
				response.Close();
				readStream.Close();
				return true;
			}
			catch {
				return false;
			}
		}

		private void CheckMaj(bool noResponseOk = false) {
			Enabled = noResponseOk;
			Application.DoEvents();
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
			bool found = false;
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
						found = true;

					break;
				}
			}
			output.Dispose();
			Popup pop = new Popup(multilingue.GetString(found ? "Main.prg.TxtInfo29" : "Main.prg.TxtInfo30"), found ? url : "");
			if (found || !noResponseOk)
				pop.Show();

			Enabled = true;
		}

		private void bpCheckMaj_Click(object sender, EventArgs e) {
			CheckMaj();
		}

		private void chkSwapEGX_CheckedChanged(object sender, EventArgs e) {
			Cpc.yEgx = chkSwapEgx.Checked ? 2 : 0;
			Convert(false);
		}

		private void bpXDiv2_Click(object sender, EventArgs e) {
			int v = 0;
			if (int.TryParse(tbxSizeX.Text, out v)) {
				tbxSizeX.Text = (v >> 1).ToString();
				InterfaceChange(sender, e);
			}
		}

		private void bpXMul2_Click(object sender, EventArgs e) {
			int v = 0;
			if (int.TryParse(tbxSizeX.Text, out v)) {
				tbxSizeX.Text = (v << 1).ToString();
				InterfaceChange(sender, e);
			}
		}

		private void bpYDiv2_Click(object sender, EventArgs e) {
			int v = 0;
			if (int.TryParse(tbxSizeY.Text, out v)) {
				tbxSizeY.Text = (v >> 1).ToString();
				InterfaceChange(sender, e);
			}
		}

		private void bpYMul2_Click(object sender, EventArgs e) {
			int v = 0;
			if (int.TryParse(tbxSizeY.Text, out v)) {
				tbxSizeY.Text = (v << 1).ToString();
				InterfaceChange(sender, e);
			}
		}

		private void bpTest_Click(object sender, EventArgs e) {
			Enabled = false;
			SaveFileDialog dlg = new SaveFileDialog();
			dlg.InitialDirectory = param.lastSavePath;
			string filter = "Sauvegarde matrice assembleur (.asm)|*.asm";
			dlg.Filter = filter;
			if (dlg.ShowDialog() == DialogResult.OK) {
				imgCpc.ResetGrille();
				imgCpc.SauveMatrice(dlg.FileName, lblInfoVersion.Text, pkMethode);
				param.lastSavePath = Path.GetDirectoryName(dlg.FileName);
			}
			Enabled = true;
		}
	}
}
