using System.Drawing;
namespace ConvImgCpc {
	class BitmapCpc {
		private const int maxColsCpc = 96;
		private const int maxLignesCpc = 272;

		private byte[] bufTmp = new byte[0x10000];
		private byte[] bmpCpc = new byte[0x10000];

		public int nbCol = 80, nbLig = 200, modeCPC = 1;
		bool cpcPlus = false;
		int[] Palette = new int[17];

		public BitmapCpc(byte[] source) {
			System.Array.Copy(source, 0x80, bmpCpc, 0, source.Length - 0x80);
		}

		private void SetPalette(byte[] palStart, int startAdr, bool plus) {
			modeCPC = palStart[startAdr] & 0x03;
			for (int i = 0; i < 16; i++)
				Palette[i] = plus ? palStart[startAdr + 1 + (i << 1)] + (palStart[startAdr + 2 + (i << 1)] << 8) : palStart[startAdr + i + 1];
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

		private int GetAdrCpc(int y) {
			int adrCPC = (y >> 4) * nbCol + (y & 14) * 0x400;
			if (y > 255 && (nbCol * nbLig > 0x3FFF))
				adrCPC += 0x3800;

			return adrCPC;
		}

		private void DepactPK() {
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

			int l = PackDepack.Depack(bmpCpc, Std ? 21 : 4, bufTmp);
			if (Std) {
				int i = 0;
				for (int x = 0; x < 80; x++)
					for (int y = 0; y < 200; y++)
						bmpCpc[x + GetAdrCpc(y << 1)] = bufTmp[i++];
			}
			else
				System.Array.Copy(bufTmp, bmpCpc, l);
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
			return ImageCpc.RgbCPC[c < 27 ? c : 0].GetColor;
		}

		public Bitmap CreateImageFromCpc(byte[] source) {
			if (bmpCpc[0] == 'M' && bmpCpc[1] == 'J' && bmpCpc[2] == 'H')
				DepactOCP();
			else
				if (bmpCpc[0] == 'P' && bmpCpc[1] == 'K' && (bmpCpc[2] == 'O' || bmpCpc[2] == 'V' || bmpCpc[2] == 'S'))
					DepactPK();
				else
					InitDatas();

			// Rendu dans un bitmap PC
			Bitmap bmp = new Bitmap(nbCol << 3, nbLig * 2);
			LockBitmap loc = new LockBitmap(bmp);
			loc.LockBits();
			for (int y = 0; y < nbLig << 1; y += 2) {
				int mode = (modeCPC == 5 ? 1 : modeCPC >= 3 ? (y & 2) == 0 ? modeCPC - 2 : modeCPC - 3 : modeCPC);
				int adrCPC = GetAdrCpc(y);
				int xBitmap = 0;
				for (int x = 0; x < nbCol; x++) {
					byte octet = bmpCpc[adrCPC + x];
					switch (mode) {
						case 0:
							loc.SetHorLine(xBitmap, y, 4, GetPalCPC(Palette[(octet >> 7) + ((octet & 0x20) >> 3) + ((octet & 0x08) >> 2) + ((octet & 0x02) << 2)]));
							loc.SetHorLine(xBitmap + 4, y, 4, GetPalCPC(Palette[((octet & 0x40) >> 6) + ((octet & 0x10) >> 2) + ((octet & 0x04) >> 1) + ((octet & 0x01) << 3)]));
							xBitmap += 8;
							break;

						case 1:
							loc.SetHorLine(xBitmap, y, 2, GetPalCPC(Palette[((octet >> 7) & 1) + ((octet >> 2) & 2)]));
							loc.SetHorLine(xBitmap + 2, y, 2, GetPalCPC(Palette[((octet >> 6) & 1) + ((octet >> 1) & 2)]));
							loc.SetHorLine(xBitmap + 4, y, 2, GetPalCPC(Palette[((octet >> 5) & 1) + ((octet >> 0) & 2)]));
							loc.SetHorLine(xBitmap + 6, y, 2, GetPalCPC(Palette[((octet >> 4) & 1) + ((octet << 1) & 2)]));
							xBitmap += 8;						break;

						case 2:
							for (int i = 8; i-- > 0; )
								loc.SetPixel(xBitmap++, y, GetPalCPC(Palette[(octet >> i) & 1]));

							break;
					}
				}
			}
			loc.UnlockBits();
			//bmp.Save("bmptmp.bmp", System.Drawing.Imaging.ImageFormat.Bmp);
			return (bmp);
		}
	}
}
