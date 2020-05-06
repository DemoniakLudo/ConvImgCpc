using System;

namespace ConvImgCpc {
	class DeltaPack {
		static byte[] BufPrec = new byte[0x8000];
		static byte[] DiffAscii = new byte[0x8000];
		static byte[] BufTmp = new byte[0x8000];

		static private int PackOdin(ImageCpc img, Param param, byte[] BufOut, bool razDiff = false) {
			if (razDiff)
				Array.Clear(BufPrec, 0, BufPrec.Length);

			// Copier l'image cpc dans le buffer de travail
			img.bitmapCpc.CreeBmpCpc(img.BmpLock);

			int maxSize = (img.NbCol) + ((img.NbLig - 1) >> 3) * (img.NbCol) + ((img.NbLig - 1) & 7) * 0x800;
			if (maxSize >= 0x4000)
				maxSize += 0x3800;

			// Recherche les "coordonnées" de l'image différente par rapport à la précédente
			int bc = 0, posDiff = 0;
			byte deltaAdr = 0;
			for (int adr = 0; adr < maxSize; adr++) {
				byte o = BufPrec[adr];
				byte n = img.bitmapCpc.bmpCpc[adr];
				if (deltaAdr == 127 || (o != n)) {
					if (adr < maxSize - 256 && BufPrec[adr + 1] != img.bitmapCpc.bmpCpc[adr + 1] && BufPrec[adr + 2] != img.bitmapCpc.bmpCpc[adr + 2] && img.bitmapCpc.bmpCpc[adr + 1] == n && img.bitmapCpc.bmpCpc[adr + 2] == n) {
						DiffAscii[posDiff++] = (byte)(deltaAdr | 0x80);
						bc++;
						int d = 0;
						while (d < 255 && img.bitmapCpc.bmpCpc[adr + d] == n)
							d++;

						DiffAscii[posDiff++] = (byte)d;
						DiffAscii[posDiff++] = n;
						bc++;
						deltaAdr = 0;
						adr += d - 1;
					}
					else {
						DiffAscii[posDiff++] = (byte)deltaAdr;
						DiffAscii[posDiff++] = n;
						bc++;
						deltaAdr = 0;
						if (posDiff >= DiffAscii.Length)
							break;
					}
				}
				else
					deltaAdr++;
			}
			BufTmp[0] = (byte)'D';
			//// V1.03 : ajouter l/2 sur 2 octets
			//BufTmp[1] = (byte)(posDiff >> 1);
			//BufTmp[2] = (byte)(posDiff >> 9);
			// V1.03 : ajouter l/2 sur 2 octets
			BufTmp[1] = (byte)(bc);
			BufTmp[2] = (byte)(bc >> 8);
			Buffer.BlockCopy(DiffAscii, 0, BufTmp, 3, posDiff);
			int lSave = PackDepack.Pack(BufTmp, posDiff + 2, BufOut, 0);
			Array.Copy(img.bitmapCpc.bmpCpc, BufPrec, BufPrec.Length);
			return lSave;
		}

		static private int PackWinDC(ImageCpc img, Param param, byte[] BufOut, bool razDiff = false) {
			int xFin = 0;
			int xDeb = img.NbCol;
			int yDeb = img.NbLig;
			int yFin = 0;

			if (razDiff)
				Array.Clear(BufPrec, 0, BufPrec.Length);

			// Copier l'image cpc dans le buffer de travail
			img.bitmapCpc.CreeBmpCpc(img.BmpLock);

			// Recherche les "coordonnées" de l'image différente par rapport à la précédente
			for (int l = 0; l < img.NbLig; l++) {
				int adr = img.GetAdrCpc(l << 1);
				for (int oct = 0; oct < img.NbCol; oct++) {
					if (img.bitmapCpc.bmpCpc[adr + oct] != BufPrec[adr + oct]) {
						xDeb = Math.Min(xDeb, oct);
						xFin = Math.Max(xFin, oct);
						yDeb = Math.Min(yDeb, l);
						yFin = Math.Max(yFin, l);
						BufPrec[adr + oct] = img.bitmapCpc.bmpCpc[adr + oct];
					}
				}
			}

			int nbOctets = xFin - xDeb + 1;
			int length = nbOctets * (yFin + 1 - yDeb);
			byte[] bLigne = new byte[length + 4];
			Array.Clear(bLigne, 0, bLigne.Length);
			int AdrEcr = 0xC000 + xDeb + (yDeb >> 3) * img.NbCol + (yDeb & 7) * 0x800;
			bLigne[0] = (byte)AdrEcr;
			bLigne[1] = (byte)(AdrEcr >> 8);
			bLigne[2] = (byte)(xFin - xDeb + 1);
			bLigne[3] = (byte)(yFin - yDeb + 1);
			// Passage en mode "ligne à ligne
			int pos = 4;
			for (int l = yDeb; l <= yFin; l++) {
				Array.Copy(BufPrec, (l >> 3) * img.NbCol + (l & 7) * 0x800 + xDeb, bLigne, pos, nbOctets);
				pos += nbOctets;
			}
			int lpack = PackDepack.Pack(bLigne, length, BufOut, 0);
			return lpack;
		}

		static private int PackWinDC4(ImageCpc img, Param param, byte[] BufOut, bool razDiff = false) {
			int[] xFin = { 0, 0, 0, 0 };
			int[] xDeb = { img.NbCol, img.NbCol, img.NbCol, img.NbCol };
			int[] yDeb = { img.NbLig, img.NbLig, img.NbLig, img.NbLig };
			int[] yFin = { 0, 0, 0, 0 };
			int[] ld = { 0, 0, img.NbLig >> 1, img.NbLig >> 1 };
			int[] lf = { img.NbLig >> 1, img.NbLig >> 1, img.NbLig, img.NbLig };
			int[] od = { 0, img.NbCol >> 1, 0, img.NbCol >> 1 };
			int[] of = { img.NbCol >> 1, img.NbCol, img.NbCol >> 1, img.NbCol };
			if (razDiff)
				Array.Clear(BufPrec, 0, BufPrec.Length);

			// Copier l'image cpc dans le buffer de travail
			img.bitmapCpc.CreeBmpCpc(img.BmpLock);

			int posBufOut = 1;
			byte nbZone = 0;
			// Recherche les "coordonnées" de l'image différente par rapport à la précédente
			for (int i = 0; i < 4; i++) {
				for (int l = ld[i]; l < lf[i]; l++) {
					int adr = img.GetAdrCpc(l << 1);
					for (int oct = od[i]; oct < of[i]; oct++) {
						if (img.bitmapCpc.bmpCpc[adr + oct] != BufPrec[adr + oct]) {
							xDeb[i] = Math.Min(xDeb[i], oct);
							xFin[i] = Math.Max(xFin[i], oct);
							yDeb[i] = Math.Min(yDeb[i], l);
							yFin[i] = Math.Max(yFin[i], l);
							BufPrec[adr + oct] = img.bitmapCpc.bmpCpc[adr + oct];
						}
					}
				}
				if (xFin[i] >= xDeb[i] && yFin[i] >= yDeb[i]) {
					int nbOctets = xFin[i] - xDeb[i] + 1;
					int length = nbOctets * (yFin[i] + 1 - yDeb[i]);
					byte[] bLigne = new byte[length + 4];
					Array.Clear(bLigne, 0, bLigne.Length);
					int AdrEcr = xDeb[i] + (yDeb[i] >> 3) * img.NbCol + (yDeb[i] & 7) * 0x800;
					bLigne[0] = (byte)AdrEcr;
					bLigne[1] = (byte)(AdrEcr >> 8);
					bLigne[2] = (byte)(xFin[i] - xDeb[i] + 1);
					bLigne[3] = (byte)(yFin[i] - yDeb[i] + 1);
					// Passage en mode "ligne à ligne
					int pos = 4;
					for (int l = yDeb[i]; l <= yFin[i]; l++) {
						Array.Copy(BufPrec, (l >> 3) * img.NbCol + (l & 7) * 0x800 + xDeb[i], bLigne, pos, nbOctets);
						pos += nbOctets;
					}
					posBufOut = PackDepack.Pack(bLigne, length, BufOut, posBufOut);
					nbZone++;
				}
			}
			BufOut[0] = nbZone;

			return posBufOut;
		}

		static public int Pack(ImageCpc img, Param param, byte[] BufOut, bool razDiff = false) {
			return PackWinDC(img, param, BufOut, razDiff);
			//return PackOdin(img, param, BufOut, razDiff);
		}
	}
}
