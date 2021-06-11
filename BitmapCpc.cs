using System;
using System.Drawing;

namespace ConvImgCpc {
	public partial class BitmapCpc {
		private const int maxColsCpc = 96;
		private const int maxLignesCpc = 272;

		static private byte[] bufTmp = new byte[0x10000];
		public byte[] bmpCpc = new byte[0x10000];
		public byte[] imgAscii = new byte[0x1000];

		static public int[] Palette = { 1, 24, 20, 6, 26, 0, 2, 7, 10, 12, 14, 16, 18, 22, 1, 14, 1 };
		static public int[] tabOctetMode = { 0x00, 0x80, 0x08, 0x88, 0x20, 0xA0, 0x28, 0xA8, 0x02, 0x82, 0x0A, 0x8A, 0x22, 0xA2, 0x2A, 0xAA };
		public const int Lum0 = 0x00;
		public const int Lum1 = 0x66;
		public const int Lum2 = 0xFF;
		static public RvbColor[] RgbCPC = {
							new RvbColor( Lum0, Lum0, Lum0),
							new RvbColor( Lum0, Lum0, Lum1),
							new RvbColor( Lum0, Lum0, Lum2),
							new RvbColor( Lum1, Lum0, Lum0),
							new RvbColor( Lum1, Lum0, Lum1),
							new RvbColor( Lum1, Lum0, Lum2),
							new RvbColor( Lum2, Lum0, Lum0),
							new RvbColor( Lum2, Lum0, Lum1),
							new RvbColor( Lum2, Lum0, Lum2),
							new RvbColor( Lum0, Lum1, Lum0),
							new RvbColor( Lum0, Lum1, Lum1),
							new RvbColor( Lum0, Lum1, Lum2),
							new RvbColor( Lum1, Lum1, Lum0),
							new RvbColor( Lum1, Lum1, Lum1),
							new RvbColor( Lum1, Lum1, Lum2),
							new RvbColor( Lum2, Lum1, Lum0),
							new RvbColor( Lum2, Lum1, Lum1),
							new RvbColor( Lum2, Lum1, Lum2),
							new RvbColor( Lum0, Lum2, Lum0),
							new RvbColor( Lum0, Lum2, Lum1),
							new RvbColor( Lum0, Lum2, Lum2),
							new RvbColor( Lum1, Lum2, Lum0),
							new RvbColor( Lum1, Lum2, Lum1),
							new RvbColor( Lum1, Lum2, Lum2),
							new RvbColor( Lum2, Lum2, Lum0),
							new RvbColor( Lum2, Lum2, Lum1),
							new RvbColor( Lum2, Lum2, Lum2)
							};
		static public string[] modesVirtuels = {
										"Mode 0",
										"Mode 1",
										"Mode 2",
										"Mode EGX1",
										"Mode EGX2",
										"Mode X",
										"Mode <Split>",
										"Mode ASC-UT",
										"Mode ASC0",
										"Mode ASC1",
										"Mode ASC2",
										"Capture Sprites"
										};
		static public string CpcVGA = "TDU\\X]LEMVFW^@_NGORBSZY[JCK";

		static public byte[, ,] trameM1 = new byte[16, 4, 4];

		static public byte[, , ,] spritesHard = new byte[4, 16, 16, 16];
		static public int[] paletteSprite = new int[16];

		static public int modeVirtuel = 1;
		static public bool cpcPlus = false;
		static private int nbCol = 80;
		static public int NbCol { get { return nbCol; } }
		static public int TailleX {
			get { return nbCol << 3; }
			set { nbCol = value >> 3; }
		}
		static private int nbLig = 200;
		static public int NbLig { get { return nbLig; } }
		static public int TailleY {
			get { return nbLig << 1; }
			set { nbLig = value >> 1; }
		}
		static public int BitmapSize { get { return nbCol + GetAdrCpc((TailleY & 0x3F8) - 2); } }
		static public int yEgx = 0;

		public bool isCalc = false;

		public BitmapCpc() {
		}

		public BitmapCpc(byte[] source, int offset) {
			Array.Copy(source, offset, bmpCpc, 0, source.Length - offset);
		}

		public BitmapCpc(byte[] source, int tx, int ty) {
			Array.Copy(source, bmpCpc, source.Length);
			TailleX = tx;
			TailleY = ty;
		}

		public RvbColor GetColorPal(int palEntry) {
			int col = Palette[palEntry];
			return cpcPlus ? new RvbColor((byte)((col & 0x0F) * 17), (byte)(((col & 0xF00) >> 8) * 17), (byte)(((col & 0xF0) >> 4) * 17)) : RgbCPC[col < 27 ? col : 0];
		}

		private void SetPalette(byte[] palStart, int startAdr, bool plus) {
			modeVirtuel = palStart[startAdr] & 0x03;
			for (int i = 0; i < 16; i++)
				Palette[i] = plus ? ((palStart[startAdr + 1 + (i << 1)] << 4) & 0xF0) + (palStart[startAdr + 1 + (i << 1)] >> 4) + (palStart[startAdr + 2 + (i << 1)] << 8) : palStart[startAdr + i + 1];
		}

		private bool InitDatas() {
			bool Ret = false;
			// Si sauvegardé avec ConvImgCpc, alors la palette se trouve dans l'image...
			// CPC OLD, écran standard
			if (bmpCpc[0x7D0] == 0x3A && bmpCpc[0x7D1] == 0xD0 && bmpCpc[0x7D2] == 0xD7 && bmpCpc[0x7D3] == 0xCD) {
				cpcPlus = false;
				nbCol = 80;
				nbLig = 200;
				SetPalette(bmpCpc, 0x17D0, cpcPlus);
				Ret = true;
			}
			else
				// CPC +, écran standard
				if (bmpCpc[0x7D0] == 0xF3 && bmpCpc[0x7D1] == 0x01 && bmpCpc[0x7D2] == 0x11 && bmpCpc[0x7D3] == 0xBC) {
					cpcPlus = true;
					nbCol = 80;
					nbLig = 200;
					SetPalette(bmpCpc, 0x17D0, cpcPlus);
					Ret = true;
				}
				else
					// CPC OLD, écran overscan
					if (bmpCpc[0x611] == 0x21 && bmpCpc[0x612] == 0x47 && bmpCpc[0x613] == 0x08 && bmpCpc[0x614] == 0xCD) {
						cpcPlus = false;
						nbCol = maxColsCpc;
						nbLig = maxLignesCpc;
						SetPalette(bmpCpc, 0x600, cpcPlus);
						Ret = true;
					}
					else
						// CPC +, écran overscan
						if (bmpCpc[0x621] == 0xF3 && bmpCpc[0x622] == 0x01 && bmpCpc[0x623] == 0x11 && bmpCpc[0x624] == 0xBC) {
							cpcPlus = true;
							nbCol = maxColsCpc;
							nbLig = maxLignesCpc;
							SetPalette(bmpCpc, 0x600, cpcPlus);
							Ret = true;
						}
			return (Ret);
		}

		private void DepactOCP() {
			int PosIn = 0, PosOut = 0;
			int LgOut, CntBlock = 0;

			bmpCpc.CopyTo(bufTmp, 0x10000);
			while (PosOut < 0x4000) {
				if (bufTmp[PosIn] == 'M' && bufTmp[PosIn + 1] == 'J' && bufTmp[PosIn + 2] == 'H') {
					PosIn += 3;
					LgOut = bufTmp[PosIn++];
					LgOut += (bufTmp[PosIn++] << 8);
					CntBlock = 0;
					while (CntBlock < LgOut) {
						if (bufTmp[PosIn] == 'M' && bufTmp[PosIn + 1] == 'J' && bufTmp[PosIn + 2] == 'H')
							break;

						byte a = bufTmp[PosIn++];
						if (a == 1) { // MARKER_OCP
							int c = bufTmp[PosIn++];
							a = bufTmp[PosIn++];
							if (c == 0)
								c = 0x100;

							for (int i = 0; i < c && CntBlock < LgOut; i++) {
								bmpCpc[PosOut++] = a;
								CntBlock++;
							}
						}
						else {
							bmpCpc[PosOut++] = a;
							CntBlock++;
						}
					}
				}
				else
					PosOut = 0x4000;
			}
		}

		static public int GetAdrCpc(int y) {
			int adrCPC = (y >> 4) * nbCol + (y & 14) * 0x400;
			if (y > 255 && (nbCol * nbLig > 0x4000))
				adrCPC += 0x3800;

			return adrCPC;
		}

		static public int CalcTx(int y = 0) {
			return 8 >> (modeVirtuel == 11 ? 2 : modeVirtuel == 8 ? 0 : modeVirtuel > 8 ? modeVirtuel - 8 : modeVirtuel >= 5 ? 2 : modeVirtuel > 2 ? ((y & 2) == yEgx ? modeVirtuel - 1 : modeVirtuel - 2) : modeVirtuel + 1);
		}

		static public int MaxPen(int y = 0) {
			switch (modeVirtuel) {
				case 0:
				case 1:
				case 2:
					return 1 << (4 >> modeVirtuel);
				case 3:
				case 4:
					return 1 << (4 >> ((y & 2) == yEgx ? modeVirtuel - 2 : modeVirtuel - 3));
				case 5:
					return 4;
				case 6:
					return 16;
				case 7:
					return 4;
				case 8:
					return 16;
				case 9:
					return 4;
				case 10:
					return 2;
			}
			return 16;
		}

		private void DepactPK(Main.PackMethode pkMethode) {
			byte[] Palette = new byte[0x100];

			// Valeurs par défaut
			cpcPlus = false;
			nbCol = 80;
			nbLig = 200;

			//
			//PKSL -> 320x200 STD
			//PKS3 -> 320x200 Mode 3
			//PKSP -> 320x200 PLUS
			//PKVL -> Overscan STD
			//PKVP -> Overscan PLUS
			//

			cpcPlus = (bmpCpc[3] == 'P') || (bmpCpc[2] == 'O');
			bool Overscan = (bmpCpc[2] == 'V') || (bmpCpc[3] == 'V');
			bool Std = (bmpCpc[2] == 'S' && bmpCpc[3] == 'L');
			if (Std)
				for (int i = 0; i < 17; i++)
					Palette[i] = bmpCpc[i + 4];

			int l = new PackModule().Depack(bmpCpc, Std ? 21 : 4, bufTmp, Main.PackMethode.Standard);
			if (Std) {
				int i = 0;
				for (int x = 0; x < 80; x++)
					for (int y = 0; y < 200; y++)
						bmpCpc[x + GetAdrCpc(y << 1)] = bufTmp[i++];
			}
			else
				Array.Copy(bufTmp, bmpCpc, l);
			if (Overscan) {
				nbCol = maxColsCpc;
				nbLig = maxLignesCpc;
				SetPalette(bmpCpc, 0x600, cpcPlus);
			}
			else {
				if (Std)
					SetPalette(Palette, 0, cpcPlus);
				else
					SetPalette(bmpCpc, 0x17D0, cpcPlus);
			}
		}

		private int GetPalCPC(int c) {
			if (cpcPlus) {
				byte b = (byte)((c & 0x0F) * 17);
				byte r = (byte)(((c & 0xF0) >> 4) * 17);
				byte v = (byte)(((c & 0xF00) >> 8) * 17);
				return (int)(r + (v << 8) + (b << 16) + 0xFF000000);
			}
			return RgbCPC[c < 27 ? c : 0].GetColor;
		}

		public void CreeBmpCpc(DirectBitmap bmpLock, int[,] colMode5, bool egx = false, int lignestart = 0) {
			Array.Clear(bmpCpc, 0, bmpCpc.Length);
			for (int y = 0; y < TailleY; y += 2) {
				int adrCPC = GetAdrCpc(y);
				int tx = CalcTx(y);
				int maxPen = MaxPen(y);
				for (int x = 0; x < TailleX; x += 8) {
					byte pen = 0, octet = 0, decal = 0;
					if (!egx || ((y >> 1) & 1) == lignestart) {
						for (int p = 0; p < 8; p += tx) {
							RvbColor col = bmpLock.GetPixelColor(x + p, y);
							for (pen = 0; pen < maxPen; pen++) {
								if (cpcPlus) {
									if ((col.v >> 4) == (Palette[pen] >> 8) && (col.b >> 4) == ((Palette[pen] >> 4) & 0x0F) && (col.r >> 4) == (Palette[pen] & 0x0F))
										break;
								}
								else {
									RvbColor fixedCol = RgbCPC[modeVirtuel == 5 || modeVirtuel == 6 ? colMode5[y >> 1, pen] : Palette[pen]];
									if (fixedCol.r == col.r && fixedCol.b == col.b && fixedCol.v == col.v)
										break;
								}
							}
							octet |= (byte)(tabOctetMode[pen % 16] >> (decal++));
						}
						bmpCpc[adrCPC + (x >> 3)] = octet;
					}
				}
			}
		}

		public void CreeBmpCpcForceMode1(DirectBitmap bmpLock) {
			Array.Clear(bmpCpc, 0, bmpCpc.Length);
			for (int y = 0; y < TailleY; y += 2) {
				int adrCPC = GetAdrCpc(y);
				int tx = CalcTx(y);
				int maxPen = MaxPen(y);
				for (int x = 0; x < TailleX; x += 8) {
					byte pen = 0, octet = 0, decal = 0;
					for (int p = 0; p < 8; p += tx) {
						RvbColor col = bmpLock.GetPixelColor(x + p, y);
						for (pen = 0; pen < maxPen; pen++) {
							if (cpcPlus) {
								if ((col.v >> 4) == (Palette[pen] >> 8) && (col.b >> 4) == ((Palette[pen] >> 4) & 0x0F) && (col.r >> 4) == (Palette[pen] & 0x0F))
									break;
							}
							else {
								RvbColor fixedCol = RgbCPC[Palette[pen]];
								if (fixedCol.r == col.r && fixedCol.b == col.b && fixedCol.v == col.v)
									break;
							}
						}
						octet |= (byte)(tabOctetMode[Math.Min(3, (int)pen)] >> (decal++));
					}
					bmpCpc[adrCPC + (x >> 3)] = octet;
				}
			}
		}

		private int GetPenColor(DirectBitmap bmpLock, int x, int y) {
			int pen = 0;
			RvbColor col = bmpLock.GetPixelColor(x, y);
			if (cpcPlus) {
				for (pen = 0; pen < 16; pen++) {
					if ((col.v >> 4) == (Palette[pen] >> 8) && (col.r >> 4) == ((Palette[pen] >> 4) & 0x0F) && (col.b >> 4) == (Palette[pen] & 0x0F))
						break;
				}
			}
			else {
				for (pen = 0; pen < 16; pen++) {
					RvbColor fixedCol = RgbCPC[Palette[pen]];
					if (fixedCol.r == col.r && fixedCol.b == col.b && fixedCol.v == col.v)
						break;
				}
			}
			return pen;
		}

		private void CreeImgAscii(DirectBitmap bmpLock) {
			int l = 0;
			for (int y = 0; y < NbLig << 1; y += 16)
				for (int x = 0; x < NbCol; x++) {
					switch (modeVirtuel) {
						case 8: // ASC0
							imgAscii[l++] = (byte)(GetPenColor(bmpLock, x << 3, y) | (GetPenColor(bmpLock, x << 3, y + 8) << 4));
							break;

						case 9: // ASC1
							imgAscii[l++] = (byte)(GetPenColor(bmpLock, x << 3, y) | (GetPenColor(bmpLock, (x << 3) + 4, y) << 2)
												| (GetPenColor(bmpLock, x << 3, y + 8) << 4) | (GetPenColor(bmpLock, (x << 3) + 4, y + 8) << 6));
							break;

						case 10: // ASC2
							imgAscii[l++] = (byte)(GetPenColor(bmpLock, x << 3, y) | (GetPenColor(bmpLock, (x << 3) + 2, y) << 2)
												| (GetPenColor(bmpLock, (x << 3) + 4, y) << 1) | (GetPenColor(bmpLock, (x << 3) + 6, y) << 3)
												| (GetPenColor(bmpLock, x << 3, y + 8) << 4) | (GetPenColor(bmpLock, (x << 3) + 2, y + 8) << 6)
												| (GetPenColor(bmpLock, (x << 3) + 4, y + 8) << 5) | (GetPenColor(bmpLock, (x << 3) + 6, y + 8) << 7));
							break;
					}
				}
		}

		private int GetAsciiMat(DirectBitmap bmpLock, int x, int y) {
			for (int i = 0; i < 16; i++) {
				bool found = true;
				for (int ym = 0; ym < 4; ym++) {
					for (int xm = 0; xm < 4; xm++) {
						RvbColor pix = bmpLock.GetPixelColor(x + (xm << 1), y + (ym << 1));
						RvbColor c = GetColorPal(trameM1[i, xm, ym]);
						if (c.r != pix.r || c.v != pix.v || c.b != pix.b) {
							found = false;
							break;
						}
					}
				}
				if (found)
					return i;
			}
			return 0;
		}

		private void CreeImgAsciiMat(DirectBitmap bmpLock) {
			int l = 0;
			for (int y = 0; y < NbLig << 1; y += 16)
				for (int x = 0; x < NbCol; x++)
					imgAscii[l++] = (byte)(GetAsciiMat(bmpLock, x << 3, y) | (GetAsciiMat(bmpLock, x << 3, y + 8) << 4));
		}

		public void ConvertAscii(DirectBitmap bmpLock) {
			if (modeVirtuel == 7)
				CreeImgAsciiMat(bmpLock);
			else
				CreeImgAscii(bmpLock);
		}

		// Rendu d'une image CPC dans un bitmap PC
		public DirectBitmap DrawBitmap(int nbCol, int nbLig, bool isSprite = false, ImageCpc imgCpc = null) {
			DirectBitmap loc = new DirectBitmap(nbCol << 3, nbLig << 1);
			for (int y = 0; y < nbLig << 1; y += 2) {
				int mode = (modeVirtuel >= 5 ? 1 : modeVirtuel >= 3 ? (y & 2) ==yEgx ? modeVirtuel - 2 : modeVirtuel - 3 : modeVirtuel);
				int adrCPC = isSprite ? nbCol * (y >> 1) : GetAdrCpc(y);
				int xBitmap = 0;
				int p0, p1, p2, p3;
				for (int x = 0; x < nbCol; x++) {
					byte octet = bmpCpc[adrCPC + x];
					switch (mode) {
						case 0:
							p0 = (octet >> 7) + ((octet & 0x20) >> 3) + ((octet & 0x08) >> 2) + ((octet & 0x02) << 2);
							p1 = ((octet & 0x40) >> 6) + ((octet & 0x10) >> 2) + ((octet & 0x04) >> 1) + ((octet & 0x01) << 3);
							loc.SetHorLineDouble(xBitmap, y, 4, GetPalCPC(Palette[p0]));
							loc.SetHorLineDouble(xBitmap + 4, y, 4, GetPalCPC(Palette[p1]));
							if (imgCpc != null) {
								imgCpc.SetPixelCpc(xBitmap, y, p0, 4);
								imgCpc.SetPixelCpc(xBitmap + 4, y, p1, 4);
							}
							xBitmap += 8;
							break;

						case 1:
							p0 = ((octet >> 7) & 1) + ((octet >> 2) & 2);
							p1 = ((octet >> 6) & 1) + ((octet >> 1) & 2);
							p2 = ((octet >> 5) & 1) + ((octet >> 0) & 2);
							p3 = ((octet >> 4) & 1) + ((octet << 1) & 2);
							loc.SetHorLineDouble(xBitmap, y, 2, GetPalCPC(Palette[p0]));
							loc.SetHorLineDouble(xBitmap + 2, y, 2, GetPalCPC(Palette[p1]));
							loc.SetHorLineDouble(xBitmap + 4, y, 2, GetPalCPC(Palette[p2]));
							loc.SetHorLineDouble(xBitmap + 6, y, 2, GetPalCPC(Palette[p3]));
							if (imgCpc != null) {
								imgCpc.SetPixelCpc(xBitmap, y, p0, 2);
								imgCpc.SetPixelCpc(xBitmap + 2, y, p1, 2);
								imgCpc.SetPixelCpc(xBitmap + 4, y, p2, 2);
								imgCpc.SetPixelCpc(xBitmap + 6, y, p3, 2);
							}
							xBitmap += 8;
							break;

						case 2:
							for (int i = 8; i-- > 0; ) {
								p0 = (octet >> i) & 1;
								loc.SetPixel(xBitmap, y, GetPalCPC(Palette[p0]));
								loc.SetPixel(xBitmap++, y + 1, GetPalCPC(Palette[p0]));
								if (imgCpc != null)
									imgCpc.SetPixelCpc(xBitmap, y, p0, 1);
							}
							break;
					}
				}
			}
			if (imgCpc != null) {
				imgCpc.bitmapCpc.isCalc = true;
				imgCpc.Render();
			}
			return (loc);
		}

		public DirectBitmap CreateImageFromCpc(int length, Param par, Main.PackMethode pkMethode, bool isSprite = false, ImageCpc imgCpc = null) {
			if (!isSprite) {
				if (bmpCpc[0] == 'M' && bmpCpc[1] == 'J' && bmpCpc[2] == 'H')
					DepactOCP();
				else
					if (bmpCpc[0] == 'P' && bmpCpc[1] == 'K' && (bmpCpc[2] == 'O' || bmpCpc[2] == 'V' || bmpCpc[2] == 'S'))
						DepactPK(pkMethode);
					else {
						if (!InitDatas()) {
							if (length == 16384) {
								nbCol = 80;
								nbLig = 200;
							}
							else
								if (length < 32000) {
									try {
										int l = new PackModule().Depack(bmpCpc, 0, bufTmp, Main.PackMethode.Standard);
										Array.Copy(bufTmp, bmpCpc, l);
									}
									catch (Exception) { }
									if (!InitDatas()) {
										cpcPlus = false;
										nbCol = maxColsCpc;
										nbLig = maxLignesCpc;
										SetPalette(bmpCpc, 0x600, cpcPlus);
									}
								}
								else {
									if (length > 0x4000) {
										nbCol = maxColsCpc;
										nbLig = maxLignesCpc;
									}
								}
						}
					}
			}
			par.cpcPlus = cpcPlus;
			return DrawBitmap(nbCol, nbLig, isSprite, imgCpc);
		}
	}
}
