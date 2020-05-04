using System;

namespace ConvImgCpc {
	class DeltaPack {
		static byte[] BufOut = new byte[0x8000];
		static byte[] BufIn = new byte[0x8000];
		static byte[] BufPrec = new byte[0x8000];

		static public byte[] PackWinDC(ImageCpc img, Param param, bool razDiff = false) {
			int adr = 0, l, oct;
			int xFin = 0;
			int xDeb = img.NbCol;
			int yDeb = img.NbLig;
			int yFin = 0;

			if (razDiff)
				Array.Clear(BufPrec, 0, BufPrec.Length);

			// Copier l'image cpc dans le buffer de travail
			img.bitmapCpc.CreeBmpCpc(param, img.BmpLock);
			Array.Copy(img.bitmapCpc.bmpCpc, BufIn, BufIn.Length);

			// Recherche les "coordonnées" de l'image différente par rapport à la précédente
			for (l = 0; l < img.NbLig; l++) {
				adr = img.GetAdrCpc(l << 1);
				for (oct = 0; oct < img.NbCol; oct++) {
					if (BufIn[adr + oct] != BufPrec[adr + oct]) {
						xDeb = Math.Min(xDeb, oct);
						xFin = Math.Max(xFin, oct);
						yDeb = Math.Min(yDeb, l);
						yFin = Math.Max(yFin, l);
					}
				}
			}

			// Recopier buffer de travail dans buffer précédent
			Array.Copy(BufIn, BufPrec, BufIn.Length);

			int NbOctets = xFin - xDeb + 1;
			int Length = NbOctets * (yFin + 1 - yDeb);
			byte[] BLigne = new byte[Length + 4];
			int p = 4;
			int AdrEcr = xDeb + (yDeb >> 3) * 80 + (yDeb & 7) * 0x800;
			BLigne[0] = (byte)AdrEcr;
			BLigne[1] = (byte)(AdrEcr >> 8);
			BLigne[2] = (byte)(xFin - xDeb + 1);
			BLigne[3] = (byte)(yFin - yDeb + 1);

			// Passage en mode "ligne à ligne
			for (l = yDeb; l <= yFin; l++) {
				Array.Copy(BufIn, (l >> 3) * 80 + (l & 7) * 0x800 + xDeb, BLigne, p, NbOctets);
				p += NbOctets;
			}
			int lpack = PackDepack.Pack(BLigne, Length, BufOut);
			byte[] bufRet = new byte[lpack];
			Array.Copy(BufOut, bufRet, lpack);
			return bufRet;
		}
	}
}
