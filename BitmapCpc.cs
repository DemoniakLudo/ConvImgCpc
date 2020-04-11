﻿using System.Drawing;
namespace ConvImgCpc {
	class BitmapCpc {
		private const int maxColsCpc = 96;
		private const int maxLignesCpc = 272;

		static private byte[] bufTmp = new byte[0x10000];
		static private byte[] bmpCpc = new byte[0x10000];

		static public int nbCol = 80, nbLig = 200, modeCPC = 1;
		static bool cpcPlus = false;
		static int[] Palette = new int[17];

		static private void SetPalette(byte[] palStart, int startAdr, bool plus) {
			modeCPC = palStart[startAdr] & 0x03;
			for (int i = 0; i < 16; i++)
				Palette[i] = plus ? palStart[startAdr + 1 + (i << 1)] + (palStart[startAdr + 2 + (i << 1)] << 8) : palStart[startAdr + i + 1];
		}

		static private bool InitDatas(byte[] bmpCpc) {
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

		static private void DepactOCP(byte[] bmpCpc) {
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

		static private int GetAdrCpc(int y) {
			int adrCPC = (y >> 4) * nbCol + (y & 14) * 0x400;
			if (y > 255 && (nbCol * nbLig > 0x3FFF))
				adrCPC += 0x3800;

			return adrCPC;
		}



		static private void DepactPK(byte[] bmpCpc) {
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

			PackDepack.Depack(bmpCpc, Std ? 21 : 4, bufTmp);
			System.Array.Copy(bufTmp, bmpCpc, 0x10000);
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

		static private int GetPalCPC(int c) {
			if (cpcPlus) {
				byte b = (byte)((c & 0x0F) * 17);
				byte r = (byte)(((c & 0xF0) >> 4) * 17);
				byte v = (byte)(((c & 0xF00) >> 8) * 17);
				return (int)(r + (v << 8) + (b << 16) + 0xFF000000);
			}
			return ImageCpc.RgbCPC[c < 27 ? c : 0].GetColor;
		}


		static public Bitmap CreateImageFromCpc(byte[] source) {
			System.Array.Copy(source, 0x80, bmpCpc, 0, source.Length - 0x80);
			if (bmpCpc[0] == 'M' && bmpCpc[1] == 'J' && bmpCpc[2] == 'H')
				DepactOCP(bmpCpc);
			else
				if (bmpCpc[0] == 'P' && bmpCpc[1] == 'K' && (bmpCpc[2] == 'O' || bmpCpc[2] == 'V' || bmpCpc[2] == 'S')) {
					DepactPK(bmpCpc);
					//GetPalMode = 0;
					//memcpy(p, Palette, sizeof(Palette));
				}
				else
					InitDatas(bmpCpc);
			//memcpy(Palette, p, sizeof(Palette));
			//if (GetPalMode) {
			//	if (InitDatas(out Mode))
			//		memcpy(p, Palette, sizeof(Palette));
			//}


			Bitmap bmp = new Bitmap(nbCol << 3, nbLig * 2);
			LockBitmap loc = new LockBitmap(bmp);
			loc.LockBits();
			// Rendu dans un bitmap PC
			for (int y = 0; y < nbLig << 1; y += 2) {
				int adrCPC = GetAdrCpc(y);
				int tx = 4 >> modeCPC;
				int xBitmap = 0;

				for (int x = 0; x < nbCol; x++) {
					byte octet = bmpCpc[adrCPC + x];

					switch (modeCPC) {
						case 0:
							loc.SetPixel(xBitmap, y, GetPalCPC(Palette[(octet >> 7) + ((octet & 0x20) >> 3) + ((octet & 0x08) >> 2) + ((octet & 0x02) << 2)]));
							loc.SetPixel(xBitmap + 1, y, GetPalCPC(Palette[(octet >> 7) + ((octet & 0x20) >> 3) + ((octet & 0x08) >> 2) + ((octet & 0x02) << 2)]));
							loc.SetPixel(xBitmap + 2, y, GetPalCPC(Palette[(octet >> 7) + ((octet & 0x20) >> 3) + ((octet & 0x08) >> 2) + ((octet & 0x02) << 2)]));
							loc.SetPixel(xBitmap + 3, y, GetPalCPC(Palette[(octet >> 7) + ((octet & 0x20) >> 3) + ((octet & 0x08) >> 2) + ((octet & 0x02) << 2)]));
							loc.SetPixel(xBitmap + 4, y, GetPalCPC(Palette[((octet & 0x40) >> 6) + ((octet & 0x10) >> 2) + ((octet & 0x04) >> 1) + ((octet & 0x01) << 3)]));
							loc.SetPixel(xBitmap + 5, y, GetPalCPC(Palette[((octet & 0x40) >> 6) + ((octet & 0x10) >> 2) + ((octet & 0x04) >> 1) + ((octet & 0x01) << 3)]));
							loc.SetPixel(xBitmap + 6, y, GetPalCPC(Palette[((octet & 0x40) >> 6) + ((octet & 0x10) >> 2) + ((octet & 0x04) >> 1) + ((octet & 0x01) << 3)]));
							loc.SetPixel(xBitmap + 7, y, GetPalCPC(Palette[((octet & 0x40) >> 6) + ((octet & 0x10) >> 2) + ((octet & 0x04) >> 1) + ((octet & 0x01) << 3)]));
							xBitmap += (1 << 3);
							break;

						case 1:
							loc.SetPixel(xBitmap, y, GetPalCPC(Palette[((octet >> 7) & 1) + ((octet >> 2) & 2)]));
							loc.SetPixel(xBitmap + 1, y, GetPalCPC(Palette[((octet >> 7) & 1) + ((octet >> 2) & 2)]));
							loc.SetPixel(xBitmap + 2, y, GetPalCPC(Palette[((octet >> 6) & 1) + ((octet >> 1) & 2)]));
							loc.SetPixel(xBitmap + 3, y, GetPalCPC(Palette[((octet >> 6) & 1) + ((octet >> 1) & 2)]));
							loc.SetPixel(xBitmap + 4, y, GetPalCPC(Palette[((octet >> 5) & 1) + ((octet >> 0) & 2)]));
							loc.SetPixel(xBitmap + 5, y, GetPalCPC(Palette[((octet >> 5) & 1) + ((octet >> 0) & 2)]));
							loc.SetPixel(xBitmap + 6 * 1, y, GetPalCPC(Palette[((octet >> 4) & 1) + ((octet << 1) & 2)]));
							loc.SetPixel(xBitmap + 7 * 1, y, GetPalCPC(Palette[((octet >> 4) & 1) + ((octet << 1) & 2)]));
							xBitmap += (1 << 3);
							break;

						case 2:
							for (int i = 8; i-- > 0; ) {
								loc.SetPixel(xBitmap, y, GetPalCPC(Palette[(octet >> i) & 1]));
								xBitmap += 1;
							}
							break;

					}

				}
			}
			loc.UnlockBits();
			return (bmp);
		}
	}
}