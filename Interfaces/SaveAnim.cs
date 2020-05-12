using System;
using System.Globalization;
using System.IO;
using System.Windows.Forms;

namespace ConvImgCpc {
	public partial class SaveAnim : Form {
		private static byte[] BufPrec = new byte[0x8000];
		private static byte[] DiffImage = new byte[0x8000];
		private static byte[] BufTmp = new byte[0x8000];
		private static byte[] bLigne = new byte[0x8000];
		private static byte[] OldImgAscii = new byte[0x800];

		private string fileName;
		private string version;
		private ImageCpc img;
		private Param param;

		public SaveAnim(string f, string v, ImageCpc i, Param p) {
			fileName = f;
			version = v;
			img = i;
			param = p;
			InitializeComponent();
			chkDirecMem.Visible = img.modeVirtuel < 8;
			chkDelai.Visible = img.modeVirtuel >= 8;
		}

		private int PackWinDC(byte[] bufOut, ref int sizeDepack, bool razDiff = false) {
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
						//BufPrec[adr + oct] = img.bitmapCpc.bmpCpc[adr + oct];
					}
				}
			}
			Array.Copy(img.bitmapCpc.bmpCpc, BufPrec, BufPrec.Length);

			int nbOctets = xFin - xDeb + 1;
			int length = nbOctets * (yFin + 1 - yDeb);
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
			int lpack = PackDepack.Pack(bLigne, length + 4, bufOut, 0);
			sizeDepack = length + 4;
			return lpack;
		}

		private int PackDirectMem(byte[] bufOut, ref int sizeDepack, bool newMethode, bool razDiff) {
			if (razDiff)
				Array.Clear(BufPrec, 0, BufPrec.Length);

			// Copier l'image cpc dans le buffer de travail
			img.bitmapCpc.CreeBmpCpc(img.BmpLock);

			int maxSize = (img.NbCol) + ((img.NbLig - 1) >> 3) * (img.NbCol) + ((img.NbLig - 1) & 7) * 0x800;
			if (maxSize >= 0x4000)
				maxSize += 0x3800;

			int maxDelta = newMethode ? 127 : 255;
			// Recherche les "coordonnées" de l'image différente par rapport à la précédente
			int bc = 0, posDiff = 0;
			byte deltaAdr = 0;
			for (int adr = 0; adr < maxSize; adr++) {
				byte o = BufPrec[adr];
				byte n = img.bitmapCpc.bmpCpc[adr];
				if (deltaAdr == 127 || (o != n)) {
					if (newMethode && adr < maxSize - 256 && BufPrec[adr + 1] != img.bitmapCpc.bmpCpc[adr + 1] && BufPrec[adr + 2] != img.bitmapCpc.bmpCpc[adr + 2] && img.bitmapCpc.bmpCpc[adr + 1] == n && img.bitmapCpc.bmpCpc[adr + 2] == n) {
						DiffImage[posDiff++] = (byte)(deltaAdr | 0x80);
						bc++;
						int d = 0;
						while (d < 255 && img.bitmapCpc.bmpCpc[adr + d] == n)
							d++;

						DiffImage[posDiff++] = (byte)d;
						DiffImage[posDiff++] = n;
						bc++;
						deltaAdr = 0;
						adr += d - 1;
					}
					else {
						DiffImage[posDiff++] = (byte)deltaAdr;
						DiffImage[posDiff++] = n;
						bc++;
						deltaAdr = 0;
						if (posDiff >= DiffImage.Length)
							break;
					}
				}
				else
					deltaAdr++;
			}
			BufTmp[0] = (byte)(bc);
			BufTmp[1] = (byte)(bc >> 8);
			Buffer.BlockCopy(DiffImage, 0, BufTmp, 2, posDiff);
			int lPack = PackDepack.Pack(BufTmp, posDiff + 2, bufOut, 0);
			Array.Copy(img.bitmapCpc.bmpCpc, BufPrec, BufPrec.Length);
			sizeDepack = posDiff + 2;
			return lPack;
		}

		private int PackWinDC4(byte[] bufOut, bool razDiff = false) {
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
					posBufOut = PackDepack.Pack(bLigne, length, bufOut, posBufOut);
					nbZone++;
				}
			}
			bufOut[0] = nbZone;
			return posBufOut;
		}

		private int GetPenColor(int x, int y) {
			int pen = 0;
			RvbColor col = img.BmpLock.GetPixelColor(x, y);
			if (img.cpcPlus) {
				for (pen = 0; pen < 16; pen++) {
					if ((col.v >> 4) == (img.Palette[pen] >> 8) && (col.r >> 4) == ((img.Palette[pen] >> 4) & 0x0F) && (col.b >> 4) == (img.Palette[pen] & 0x0F))
						break;
				}
			}
			else {
				for (pen = 0; pen < 16; pen++) {
					RvbColor fixedCol = BitmapCpc.RgbCPC[img.Palette[pen]];
					if (fixedCol.r == col.r && fixedCol.b == col.b && fixedCol.v == col.v)
						break;
				}
			}
			return pen;
		}

		private int PackAscii(byte[] bufOut, ref int sizeDepack, bool razDiff, bool firstFrame, bool perte = false) {
			byte[] imgAscii = new byte[2048];
			int posDiff = 0, lastPosDiff = 0, nDiff = 0;
			byte nbModif = 0;
			int tailleMax = (img.NbLig * img.NbCol) >> 3;

			if (razDiff)
				Array.Clear(OldImgAscii, 0, OldImgAscii.Length);

			// Première étape : convertir l'image en "blocs" de 1 octet sur 8 lignes
			int l = 0;
			for (int y = 0; y < img.NbLig << 1; y += 16)
				for (int x = 0; x < img.NbCol; x++) {
					switch (img.modeVirtuel) {
						case 8: // ASC0
							imgAscii[l++] = (byte)(GetPenColor(x << 3, y) | (GetPenColor(x << 3, y + 8) << 4));
							break;

						case 9: // ASC1
							imgAscii[l++] = (byte)(GetPenColor(x << 3, y) | (GetPenColor((x << 3) + 4, y) << 2)
												| (GetPenColor(x << 3, y + 8) << 4) | (GetPenColor((x << 3) + 4, y + 8) << 6));
							break;

						case 10: // ASC2
							imgAscii[l++] = (byte)(GetPenColor(x << 3, y) | (GetPenColor((x << 3) + 2, y) << 2)
												| (GetPenColor((x << 3) + 4, y) << 1) | (GetPenColor((x << 3) + 6, y) << 3)
												| (GetPenColor(x << 3, y + 8) << 4) | (GetPenColor((x << 3) + 2, y + 8) << 6)
												| (GetPenColor((x << 3) + 4, y + 8) << 5) | (GetPenColor((x << 3) + 6, y + 8) << 7));
							break;
					}
				}
			if (perte) {
				for (int i = img.NbCol; i < tailleMax - img.NbCol; i++)
					if (OldImgAscii[i - 1] == imgAscii[i - 1] && OldImgAscii[i + 1] == imgAscii[i + 1]
						&& OldImgAscii[i - img.NbCol] == imgAscii[i - img.NbCol] && OldImgAscii[i + img.NbCol] == imgAscii[i + img.NbCol])
						imgAscii[i] = OldImgAscii[i];
			}
			for (int i = 0; i < tailleMax; i++) {
				byte oldAsc = OldImgAscii[i];
				byte newAsc = imgAscii[i];
				if (nbModif == 255 || (oldAsc != newAsc) || firstFrame) {
					if (oldAsc != newAsc) {
						nDiff++;
						lastPosDiff = (posDiff + 2);
					}
					DiffImage[posDiff++] = (byte)nbModif;
					DiffImage[posDiff++] = newAsc;
					nbModif = 0;
					if (posDiff >= tailleMax)
						break;
				}
				else
					nbModif++;
			}
			Array.Copy(imgAscii, OldImgAscii, OldImgAscii.Length);
			sizeDepack = imgAscii.Length + 4;
			if (nDiff == 0) {
				BufTmp[0] = (byte)'I';
				return PackDepack.Pack(BufTmp, 1, bufOut, 0);
			}
			else {
				BufTmp[0] = (byte)'O';
				Array.Copy(imgAscii, 0, BufTmp, 1, tailleMax);
				int lo = PackDepack.Pack(BufTmp, tailleMax + 1, BufPrec, 0);
				BufTmp[0] = (byte)'D';
				// Optimiser les frames D
				posDiff = Math.Min(lastPosDiff, posDiff);
				// V1.03 : ajouter lSave/2 sur 2 octets
				BufTmp[1] = (byte)(posDiff >> 1);
				BufTmp[2] = (byte)(posDiff >> 9);
				Array.Copy(DiffImage, 0, BufTmp, 3, posDiff);
				int ld = PackDepack.Pack(BufTmp, posDiff + 3, bufOut, 0);
				if (lo > ld)
					return ld;
				else {
					Array.Copy(BufPrec, bufOut, lo);
					return lo;
				}
			}
		}

		private int PackFrame(byte[] bufOut, ref int sizeDepack, bool directMem, bool razDiff, bool firstFrame) {
			if (img.modeVirtuel > 7)
				return PackAscii(bufOut, ref sizeDepack, razDiff, firstFrame);
			else
				if (directMem)
					return PackDirectMem(bufOut, ref sizeDepack, true, razDiff);
				else
					return PackWinDC(bufOut, ref sizeDepack, razDiff);
		}

		#region Génération source Z80
		private void GenereEntete(StreamWriter sw, int adr) {
			sw.WriteLine("	ORG	#" + adr.ToString("X4"));
			sw.WriteLine("	RUN	$" + Environment.NewLine);
			sw.WriteLine("	DI");
		}

		private void GenereInitOld(StreamWriter sw) {
			sw.WriteLine("	LD	HL,Palette");
			sw.WriteLine("	LD	B,#7F");
			sw.WriteLine("	XOR	A");
			sw.WriteLine("SetPal:");
			sw.WriteLine("	OUT	(C),A");
			sw.WriteLine("	INC	B");
			sw.WriteLine("	OUTI");
			sw.WriteLine("	INC	A");
			sw.WriteLine("	CP	18");
			sw.WriteLine("	JR	C,SetPal");
		}

		private void GenereInitPlus(StreamWriter sw) {
			sw.WriteLine("	LD	BC,#BC11");
			sw.WriteLine("	LD	HL,UnlockAsic");
			sw.WriteLine("Unlock:");
			sw.WriteLine("	LD	A,(HL)");
			sw.WriteLine("	OUT	(C),A");
			sw.WriteLine("	INC	HL");
			sw.WriteLine("	DEC	C");
			sw.WriteLine("	JR	NZ,Unlock");
			sw.WriteLine("	LD	BC,#7FA0");
			sw.WriteLine("	LD	A,(Palette)");
			sw.WriteLine("	OUT	(C),A");
			sw.WriteLine("	OUT	(C),C");
			sw.WriteLine("	LD	BC,#7FB8");
			sw.WriteLine("	OUT	(C),C");
			sw.WriteLine("	LD	HL,Palette+1");
			sw.WriteLine("	LD	DE,#6400");
			sw.WriteLine("	LD	BC,#0022");
			sw.WriteLine("	LDIR");
			sw.WriteLine("	LD	BC,#7FA0");
			sw.WriteLine("	OUT	(C),C");
		}

		private void GenereAffichage(StreamWriter sw, int delai, bool gest128K) {
			if (delai > 0) {
				sw.WriteLine("	LD	HL,NewIrq");
				sw.WriteLine("	LD	(#39),HL");
				sw.WriteLine("	EI");
			}
			if (img.modeVirtuel > 7) {
				sw.WriteLine("	LD	BC,#F00");
				sw.WriteLine("	LD	DE,Fonte");
				sw.WriteLine("SetFonte:");
				sw.WriteLine("	LD	HL,DataFnt");
				sw.WriteLine("	LD	A,C");
				sw.WriteLine("	AND	B");
				sw.WriteLine("	ADD	A,L");
				sw.WriteLine("	LD	L,A");
				sw.WriteLine("	LD	A,(HL)");
				sw.WriteLine("	LD	(DE),A");
				sw.WriteLine("	INC	DE");
				sw.WriteLine("	LD	HL,DataFnt");
				sw.WriteLine("	LD	A,C");
				sw.WriteLine("	RRCA");
				sw.WriteLine("	RRCA");
				sw.WriteLine("	RRCA");
				sw.WriteLine("	RRCA");
				sw.WriteLine("	AND	B");
				sw.WriteLine("	ADD	A,L");
				sw.WriteLine("	LD	L,A");
				sw.WriteLine("	LD	A,(HL)");
				sw.WriteLine("	LD	(DE),A");
				sw.WriteLine("	INC	DE");
				sw.WriteLine("	INC	C");
				sw.WriteLine("	JR	NZ,SetFonte");
			}
			sw.WriteLine("Debut");
			sw.WriteLine("	LD	IX,AnimDelta");
			sw.WriteLine("Boucle:");
			sw.WriteLine("	LD	H,(IX+1)");
			sw.WriteLine("	LD	L,(IX+0)");
			sw.WriteLine("	LD	A,H");
			sw.WriteLine("	OR	H");
			sw.WriteLine("	JR	Z,Debut");
			if (gest128K) {
				sw.WriteLine("	LD	B,#7F");
				sw.WriteLine("	LD	A,(IX+2)");
				sw.WriteLine("	OUT	(C),A");
			}
			sw.WriteLine("	LD	DE,Buffer");
			sw.WriteLine("	PUSH	DE");
			sw.WriteLine("; Decompactage");
			sw.WriteLine("DepkLzw:");
			sw.WriteLine("	LD	A,(HL)			; DepackBits = InBuf[ InBytes++ ]");
			sw.WriteLine("	INC	HL");
			sw.WriteLine("	RRA				; Rotation rapide calcul seulement flag C");
			sw.WriteLine("	SET	7,A			; Positionne bit 7 en gardant flag C");
			sw.WriteLine("	LD	(BclLzw+1),A");
			sw.WriteLine("	JR	C,TstCodeLzw");
			sw.WriteLine("CopByteLzw:");
			sw.WriteLine("	LDI				; OutBuf[ OutBytes++ ] = InBuf[ InBytes++ ]" + Environment.NewLine);
			sw.WriteLine("BclLzw:");
			sw.WriteLine("	LD	A,0");
			sw.WriteLine("	RR	A			; Rotation avec calcul Flags C et Z");
			sw.WriteLine("	LD	(BclLzw+1),A");
			sw.WriteLine("	JR	NC,CopByteLzw");
			sw.WriteLine("	JR	Z,DepkLzw" + Environment.NewLine);
			sw.WriteLine("TstCodeLzw:");
			sw.WriteLine("	LD	A,(HL)			; A = InBuf[ InBytes ];");
			sw.WriteLine("	AND	A");
			sw.WriteLine("	JR	Z,InitDraw		; Plus d'octets à traiter = fini" + Environment.NewLine);
			sw.WriteLine("	INC	HL");
			sw.WriteLine("	LD	B,A			; B = InBuf[ InBytes ]");
			sw.WriteLine("	RLCA				; A & #80 ?");
			sw.WriteLine("	JR	NC,TstLzw40" + Environment.NewLine);
			sw.WriteLine("	RLCA");
			sw.WriteLine("	RLCA");
			sw.WriteLine("	RLCA");
			sw.WriteLine("	AND	7");
			sw.WriteLine("	ADD	A,3			; Longueur = 3 + ( ( InBuf[ InBytes ] >> 4 ) & 7 );");
			sw.WriteLine("	LD	C,A			; C = Longueur");
			sw.WriteLine("	LD	A,B			; B = InBuf[InBytes]");
			sw.WriteLine("	AND	#0F			; Delta = ( InBuf[ InBytes++ ] & 15 ) << 8");
			sw.WriteLine("	LD	B,A			; B = poids fort Delta");
			sw.WriteLine("	LD	A,C			; A = Length");
			sw.WriteLine("	SCF				; Repositionner flag C (pour Delta++)");
			sw.WriteLine("CopyBytes0:");
			sw.WriteLine("	LD	C,(HL)			; C = poids faible Delta (Delta |= InBuf[ InBytes++ ]);");
			sw.WriteLine("	INC	HL");
			sw.WriteLine("	PUSH	HL");
			sw.WriteLine("	LD	H,D");
			sw.WriteLine("	LD	L,E");
			sw.WriteLine("	SBC	HL,BC			; HL=HL-(BC+1)");
			sw.WriteLine("	LD	B,0");
			sw.WriteLine("CopyBytes1:");
			sw.WriteLine("	LD	C,A");
			sw.WriteLine("CopyBytes2:");
			sw.WriteLine("	LDIR");
			sw.WriteLine("CopyBytes3:");
			sw.WriteLine("	POP	HL");
			sw.WriteLine("	JR	BclLzw" + Environment.NewLine);
			sw.WriteLine("TstLzw40:");
			sw.WriteLine("	RLCA				; A & #40 ?");
			sw.WriteLine("	JR	NC,TstLzw20" + Environment.NewLine);
			sw.WriteLine("	LD	C,B");
			sw.WriteLine("	RES	6,C			; Delta = 1 + InBuf[ InBytes++ ] & #3f;");
			sw.WriteLine("	LD	B,0			; BC = Delta + 1 car flag C = 1");
			sw.WriteLine("	PUSH	HL");
			sw.WriteLine("	LD	H,D");
			sw.WriteLine("	LD	L,E");
			sw.WriteLine("	SBC	HL,BC");
			sw.WriteLine("	LDI");
			sw.WriteLine("	LDI				; Longueur = 2");
			sw.WriteLine("	JR	CopyBytes3" + Environment.NewLine);
			sw.WriteLine("TstLzw20:");
			sw.WriteLine("	RLCA				; A & #20 ?");
			sw.WriteLine("	JR	NC,TstLzw10" + Environment.NewLine);
			sw.WriteLine("	LD	A,B			; B compris entre #20 et #3F");
			sw.WriteLine("	ADD	A,#E2			; = ( A AND #1F ) + 2, et positionne carry");
			sw.WriteLine("	LD	B,0			; Longueur = 2 + ( InBuf[ InBytes++ ] & 31 );");
			sw.WriteLine("	JR	CopyBytes0" + Environment.NewLine);
			sw.WriteLine("CodeLzw0F:");
			sw.WriteLine("	LD	C,(HL)");
			sw.WriteLine("	PUSH	HL");
			sw.WriteLine("	LD	H,D");
			sw.WriteLine("	LD	L,E");
			sw.WriteLine("	CP	#F0");
			sw.WriteLine("	JR	NZ,CodeLzw02" + Environment.NewLine);
			sw.WriteLine("	XOR	A");
			sw.WriteLine("	LD	B,A");
			sw.WriteLine("	INC	BC			; Longueur = Delta = InBuf[ InBytes + 1 ] + 1;");
			sw.WriteLine("	SBC	HL,BC");
			sw.WriteLine("	LDIR");
			sw.WriteLine("	POP	HL");
			sw.WriteLine("	INC	HL			; Inbytes += 2");
			sw.WriteLine("	JR	BclLzw" + Environment.NewLine);
			sw.WriteLine("CodeLzw02:");
			sw.WriteLine("	CP	#20");
			sw.WriteLine("	JR	C,CodeLzw01" + Environment.NewLine);
			sw.WriteLine("	LD	C,B			; Longueur = Delta = InBuf[ InBytes ];");
			sw.WriteLine("	LD	B,0");
			sw.WriteLine("	SBC	HL,BC");
			sw.WriteLine("	JR	CopyBytes2" + Environment.NewLine);
			sw.WriteLine("CodeLzw01:				; Ici, B = 1");
			sw.WriteLine("	XOR	A			; Carry a zéro");
			sw.WriteLine("	DEC	H			; Longueur = Delta = 256");
			sw.WriteLine("	JR	CopyBytes1" + Environment.NewLine);
			sw.WriteLine("TstLzw10:");
			sw.WriteLine("	RLCA				; A & #10 ?");
			sw.WriteLine("	JR	NC,CodeLzw0F" + Environment.NewLine);
			sw.WriteLine("	RES	4,B			; B = Delta(high) -> ( InBuf[ InBytes++ ] & 15 ) << 8;");
			sw.WriteLine("	LD	C,(HL)			; C = Delta(low)  -> InBuf[ InBytes++ ];");
			sw.WriteLine("	INC	HL");
			sw.WriteLine("	LD	A,(HL)			; A = Longueur - 1");
			sw.WriteLine("	INC	HL");
			sw.WriteLine("	PUSH	HL");
			sw.WriteLine("	LD	H,D");
			sw.WriteLine("	LD	L,E");
			sw.WriteLine("	SBC	HL,BC			; Flag C=1 -> hl=hl-(bc+1) (Delta+1)");
			sw.WriteLine("	LD	B,0");
			sw.WriteLine("	LD	C,A");
			sw.WriteLine("	INC	BC			; BC =  Longueur = InBuf[ InBytes++ ] + 1;");
			sw.WriteLine("	JR	CopyBytes2" + Environment.NewLine);
		}

		private void GenereDrawDC(StreamWriter sw, bool gest128K) {
			sw.WriteLine("InitDraw:");
			sw.WriteLine("	POP	HL			; HL = buffer");
			sw.WriteLine("	LD	E,(HL)");
			sw.WriteLine("	INC	HL");
			sw.WriteLine("	LD	D,(HL)			; Adresse ecran");
			sw.WriteLine("	INC	HL");
			sw.WriteLine("	LD	A,(HL)");
			sw.WriteLine("	INC	HL");
			sw.WriteLine("	LD	(Nbx+1),A		; Taille en X");
			sw.WriteLine("	NEG");
			sw.WriteLine("	LD	(NbDec+1),A		; Ajustement pour 'BC26'");
			sw.WriteLine("	LD	A,(HL)			; Taille en Y");
			sw.WriteLine("	INC	HL");
			sw.WriteLine("Nbx:");
			sw.WriteLine("	LD	BC,0");
			sw.WriteLine("	LDIR");
			sw.WriteLine("	EX	DE,HL");
			sw.WriteLine("NbDec:");
			sw.WriteLine("	LD	BC,#7FF			; 'BC26'");
			sw.WriteLine("	ADD	HL,BC");
			sw.WriteLine("	JR	NC,Suite");
			sw.WriteLine("	LD	BC,#C050");
			sw.WriteLine("	ADD	HL,BC");
			sw.WriteLine("Suite:");
			sw.WriteLine("	EX	DE,HL");
			sw.WriteLine("	DEC	A");
			sw.WriteLine("	JR	NZ,Nbx");
			sw.WriteLine("	INC	IX");
			sw.WriteLine("	INC	IX");
			if (gest128K)
				sw.WriteLine("	INC	IX");

			sw.WriteLine("	JP	Boucle" + Environment.NewLine);
			sw.WriteLine("	Nolist");
		}

		private void GenereDrawOdin(StreamWriter sw, bool gest128K) {
			sw.WriteLine("InitDraw:");
			sw.WriteLine("	POP	HL			; HL = buffer");
			sw.WriteLine("	LD	DE,#C000");
			sw.WriteLine("	LD	C,(HL)");
			sw.WriteLine("	INC	HL");
			sw.WriteLine("	LD	B,(HL)");
			sw.WriteLine("	INC	HL");
			sw.WriteLine("DrawImgD1:");
			sw.WriteLine("	LD	A,(HL)			; Deplacement");
			sw.WriteLine("	INC	HL");
			sw.WriteLine("	BIT	7,A");
			sw.WriteLine("	JR	NZ,DrawImgD4");
			sw.WriteLine("	ADD	A,E");
			sw.WriteLine("	JR	NC,DrawImgD2");
			sw.WriteLine("	INC	D");
			sw.WriteLine("DrawImgD2:");
			sw.WriteLine("	LD	E,A");
			sw.WriteLine("	LDI				; Octet a copier");
			sw.WriteLine("DrawImgD3:");
			sw.WriteLine("	JP	PE,DrawImgD1");
			sw.WriteLine("	INC	IX");
			sw.WriteLine("	INC	IX");
			if (gest128K)
				sw.WriteLine("	INC	IX");

			sw.WriteLine("	JP	Boucle" + Environment.NewLine);
			sw.WriteLine("DrawImgD4:");
			sw.WriteLine("	AND	#7F");
			sw.WriteLine("	ADD	A,E");
			sw.WriteLine("	JR	NC,DrawImgD5");
			sw.WriteLine("	INC	D");
			sw.WriteLine("DrawImgD5:");
			sw.WriteLine("	LD	E,A");
			sw.WriteLine("	PUSH	BC");
			sw.WriteLine("	LD	B,(HL)");
			sw.WriteLine("	INC	HL");
			sw.WriteLine("	LD	A,(HL)");
			sw.WriteLine("DrawImgD6:");
			sw.WriteLine("	LD	(DE),A");
			sw.WriteLine("	INC	DE");
			sw.WriteLine("	DJNZ	DrawImgD6");
			sw.WriteLine("	POP	BC");
			sw.WriteLine("	DEC	BC");
			sw.WriteLine("	CPI");
			sw.WriteLine("	JR	DrawImgD3" + Environment.NewLine);
			sw.WriteLine("	Nolist");
		}

		private void GenereDrawAscii(StreamWriter sw, int delai, bool gest128K) {
			sw.WriteLine("InitDraw:");
			if (delai > 0) {
				sw.WriteLine("	LD	A,0");
				sw.WriteLine("	CP	" + delai.ToString());
				sw.WriteLine("	JR	C,InitDraw");
				sw.WriteLine("	XOR	A");
				sw.WriteLine("	LD	(InitDraw+1),A");
			}
			sw.WriteLine("	POP	HL			; HL = buffer");
			sw.WriteLine("	LD	A,(HL)");
			sw.WriteLine("	CP	'D'");
			sw.WriteLine("	JR	Z,DrawImgD");
			sw.WriteLine("	CP	'I'");
			sw.WriteLine("	JR	Z,DrawImgI");

			sw.WriteLine("	LD	BC,#C000");
			sw.WriteLine("DrawImgO:");
			sw.WriteLine("	LD	D,Fonte/512");
			sw.WriteLine("	INC	HL");
			sw.WriteLine("	LD	E,(HL)			; Code ASCII");
			sw.WriteLine("	EX	DE,HL");
			sw.WriteLine("	ADD	HL,HL");
			sw.WriteLine("	LD	A,(HL)");
			sw.WriteLine("	INC	HL");
			sw.WriteLine("	LD	(BC),A");
			sw.WriteLine("	SET	3,B");
			sw.WriteLine("	LD	(BC),A");
			sw.WriteLine("	SET	4,B");
			sw.WriteLine("	LD	(BC),A");
			sw.WriteLine("	RES	3,B");
			sw.WriteLine("	LD	(BC),A");
			sw.WriteLine("	SET	5,B");
			sw.WriteLine("	LD	A,(HL)");
			sw.WriteLine("	INC	HL");
			sw.WriteLine("	LD	(BC),A");
			sw.WriteLine("	SET	3,B");
			sw.WriteLine("	LD	(BC),A");
			sw.WriteLine("	RES	4,B");
			sw.WriteLine("	LD	(BC),A");
			sw.WriteLine("	RES	3,B");
			sw.WriteLine("	LD	(BC),A");
			sw.WriteLine("	RES	5,B");
			sw.WriteLine("	EX	DE,HL");
			sw.WriteLine("	INC	BC");
			sw.WriteLine("	BIT	3,B");
			sw.WriteLine("	JR	Z,DrawImgO");
			sw.WriteLine("DrawImgI:");
			sw.WriteLine("	INC	IX");
			sw.WriteLine("	INC	IX");
			if (gest128K)
				sw.WriteLine("	INC	IX");

			sw.WriteLine("	JP	Boucle");

			sw.WriteLine("DrawImgD:");
			sw.WriteLine("	LD	DE,#C000");
			sw.WriteLine("	LD	IY,Buffer");
			sw.WriteLine("	LD	C,(IY+1)");
			sw.WriteLine("	LD	B,(IY+2)");
			sw.WriteLine("DrawImgD1:");
			sw.WriteLine("	LD	H,0");
			sw.WriteLine("	LD	L,(IY+3)			; Déplacement");
			sw.WriteLine("	ADD	HL,DE			; Ajouter à DE");
			sw.WriteLine("	EX	DE,HL");
			sw.WriteLine("	LD	L,(IY+4)			; Code ASCII");
			sw.WriteLine("	LD	H,Fonte/512");
			sw.WriteLine("	ADD	HL,HL");
			sw.WriteLine("	LD	A,(HL)");
			sw.WriteLine("	INC	HL");
			sw.WriteLine("	LD	(DE),A");
			sw.WriteLine("	SET	3,D");
			sw.WriteLine("	LD	(DE),A");
			sw.WriteLine("	SET	4,D");
			sw.WriteLine("	LD	(DE),A");
			sw.WriteLine("	RES	3,D");
			sw.WriteLine("	LD	(DE),A");
			sw.WriteLine("	SET	5,D");
			sw.WriteLine("	LD	A,(HL)");
			sw.WriteLine("	INC	HL");
			sw.WriteLine("	LD	(DE),A");
			sw.WriteLine("	SET	3,D");
			sw.WriteLine("	LD	(DE),A");
			sw.WriteLine("	RES	4,D");
			sw.WriteLine("	LD	(DE),A");
			sw.WriteLine("	RES	3,D");
			sw.WriteLine("	LD	(DE),A");
			sw.WriteLine("	RES	5,D");
			sw.WriteLine("	INC	IY");
			sw.WriteLine("	INC	IY");
			sw.WriteLine("	INC	DE");
			sw.WriteLine("	DEC	BC");
			sw.WriteLine("	LD	A,B");
			sw.WriteLine("	OR	C");
			sw.WriteLine("	JR	NZ,DrawImgD1");
			sw.WriteLine("	JR	DrawImgI");

			sw.WriteLine("	Nolist");
			if (delai > 0) {
				sw.WriteLine("NewIrq:");
				sw.WriteLine("	PUSH	AF");
				sw.WriteLine("	LD	A,(InitDraw+1)");
				sw.WriteLine("	INC	A");
				sw.WriteLine("	LD	(InitDraw+1),A");
				sw.WriteLine("	POP	AF");
				sw.WriteLine("	EI");
				sw.WriteLine("	RET");
			}
			sw.WriteLine("DataFnt:");
			sw.WriteLine("	DB	#00, #C0, #0C, #CC, #30, #F0, #3C, #FC");
			sw.WriteLine("	DB	#03, #C3, #0F, #CF, #33, #F3, #3F, #FF");
			sw.WriteLine("Fonte	EQU	#B000");
		}

		private void GenerePaletteOld(StreamWriter sw, ImageCpc img) {
			sw.WriteLine("Palette:");
			string line = "\tDB\t\"";
			for (int i = 0; i < 16; i++)
				line += BitmapCpc.CpcVGA[img.bitmapCpc.Palette[i]];

			// Border = couleur 0
			line += BitmapCpc.CpcVGA[img.bitmapCpc.Palette[0]] + "\",";
			line += "#" + ((img.modeVirtuel & 3) | 0x8C).ToString("X2");
			sw.WriteLine(line);
		}

		private void GenerePalettePlus(StreamWriter sw, ImageCpc img) {
			sw.WriteLine("UnlockAsic:");
			sw.WriteLine("	DB	#FF, #00, #FF, #77, #B3, #51, #A8, #D4, #62, #39, #9C, #46, #2B, #15, #8A, #CD, #EE");
			sw.WriteLine("Palette:");
			string line = "\tDB\t#" + ((img.modeVirtuel & 3) | 0x8C).ToString("X2");
			for (int i = 0; i < 16; i++)
				line += ",#" + ((byte)(((img.Palette[i] >> 4) & 0x0F) | (img.Palette[i] << 4))).ToString("X2") + ",#" + ((byte)(img.Palette[i] >> 8)).ToString("X2");

			// Border = couleur 0
			line += ",#" + ((byte)(((img.Palette[0] >> 4) & 0x0F) | (img.Palette[0] << 4))).ToString("X2") + ",#" + ((byte)(img.Palette[0] >> 8)).ToString("X2");
			sw.WriteLine(line);
		}

		private void GenereFin(StreamWriter sw) {
			sw.WriteLine("	List");
			sw.WriteLine("buffer:");
		}

		private void GenerePointeurs(StreamWriter sw, int nbImages, int[] bank, bool gest128K) {
			string line = "\tDW\t";
			int nbFramesWrite = 0;
			if (gest128K) {
				sw.WriteLine("	ORG EndBank0");
				sw.WriteLine("	Write Direct -1,-1,#C0");
			}
			sw.WriteLine("AnimDelta:");
			for (int i = 0; i < nbImages; i++) {
				if (gest128K) {
					sw.WriteLine("\tDW\tDelta" + i.ToString());
					sw.WriteLine("\tDB\t#C" + bank[i]);
				}
				else {
					line += "Delta" + i.ToString() + ",";
					if (++nbFramesWrite >= Math.Min(16, param.nbCols)) {
						sw.WriteLine(line.Substring(0, line.Length - 1));
						line = "\tDW\t";
						nbFramesWrite = 0;
					}
				}
			}
			sw.WriteLine(line + "0");
		}
		#endregion

		private void SauveDeltaPack(int adrDeb, bool reboucle, bool gest128K, bool directMem, int adrMax, int delai) {
			int sizeDepack = 0;
			int nbImages = img.main.GetMaxImages();
			byte[][] bufOut = new byte[nbImages][];
			int[] lg = new int[nbImages];
			int[] bank = new int[nbImages];
			for (int i = 0; i < nbImages; i++)
				bufOut[i] = new byte[0x8000];

			if (adrMax == 0)
				adrMax = 0xBE00;

			if (reboucle) {
				img.main.SelectImage(nbImages - 1);
				img.Convert(true);
				PackFrame(bufOut[0], ref sizeDepack, directMem, true, false);
			}
			

			// Calcule les animations
			int ltot = 0, maxDepack = 0;
			for (int i = 0; i < nbImages; i++) {
				img.main.SelectImage(i);
				img.Convert(true);
				Application.DoEvents();
				lg[i] = PackFrame(bufOut[i], ref sizeDepack, directMem, i == 0 && !reboucle, i == 0 && !reboucle);
				ltot += lg[i];
				maxDepack = Math.Max(maxDepack, sizeDepack);
			}
			if ((ltot + adrDeb < adrMax) && (ltot + adrDeb < 0xBE00 - maxDepack))
				gest128K = false;

			// Sauvegarde
			StreamWriter sw = Save.OpenAsm(fileName, version, param);
			GenereEntete(sw, adrDeb);
			if (img.cpcPlus)
				GenereInitPlus(sw);
			else
				GenereInitOld(sw);

			GenereAffichage(sw, delai, gest128K);
			if (img.modeVirtuel > 7)
				GenereDrawAscii(sw, delai, gest128K);
			else
				if (directMem)
					GenereDrawOdin(sw, gest128K);
				else
					GenereDrawDC(sw, gest128K);

			if (img.cpcPlus)
				GenerePalettePlus(sw, img);
			else
				GenerePaletteOld(sw, img);

			int lbank = 0, numBank = 0;
			for (int i = 0; i < nbImages; i++) {
				lbank += lg[i];
				if (gest128K && lbank > (numBank == 0 ? Math.Min((0xBE00 - maxDepack - adrDeb), adrMax - adrDeb) : 0x4000) && (numBank > 0 || lbank + adrDeb - lg[i] >= 0x8000)) {
					if (numBank == 0) {
						sw.WriteLine("EndBank0:");
						numBank = 4;
					}
					else
						numBank++;

					lbank = lg[i];
					sw.WriteLine("	ORG	#4000");
					sw.WriteLine("	Write Direct -1,-1,#C" + numBank);
				}
				bank[i] = numBank;
				sw.WriteLine("Delta" + i.ToString() + ":\t\t; Taille #" + lg[i].ToString("X4"));
				Save.SauveAssembleur(sw, bufOut[i], lg[i], param);
			}
			GenerePointeurs(sw, nbImages, bank, gest128K && numBank > 0);
			GenereFin(sw);
			Save.CloseAsm(sw);
			for (int i = 0; i < nbImages; i++)
				bufOut[i] = null;

			img.main.SetInfo("Longueur totale données animation : " + ltot + " octets.");
			if (numBank > 7 || (!gest128K && ltot + adrDeb >= 0xBE00 - maxDepack)) {
				MessageBox.Show("Attention ! la taille totale (animation + buffer de décompactage) dépassera " + (gest128K ? "112K" : "48Ko") + ", risque d'écrasement de la mémoire vidéo et plantage..."
								, "Alerte"
								, MessageBoxButtons.OK
								, MessageBoxIcon.Warning);
				img.main.SetInfo("Dépassement capacité mémoire...");
			}
			else
				img.main.SetInfo("Sauvegarde animation assembleur ok.");

			GC.Collect();
		}

		private void bpSave_Click(object sender, EventArgs e) {
			string adrTxt = txbAdrDeb.Text;
			int adrDeb = 0, adrMax = 0;
			try {
				adrDeb = int.Parse(adrTxt.Substring(1), (adrTxt[0] == '#' || adrTxt[0] == '&') ? NumberStyles.HexNumber : NumberStyles.Integer);
			}
			catch (FormatException ex) {
				MessageBox.Show("L'adresse saisie [" + adrTxt + "] est erronée");
			}
			if (chkMaxMem.Checked) {
				adrTxt = tbxAdrMax.Text;
				try {
					adrMax = int.Parse(adrTxt.Substring(1), (adrTxt[0] == '#' || adrTxt[0] == '&') ? NumberStyles.HexNumber : NumberStyles.Integer);
				}
				catch (FormatException ex) {
					MessageBox.Show("L'adresse saisie [" + adrTxt + "] est erronée");
				}
			}
			if (adrDeb > 0) {
				img.WindowState = FormWindowState.Minimized;
				img.Show();
				img.WindowState = FormWindowState.Normal;
				SauveDeltaPack(adrDeb, chkBoucle.Checked, chk128Ko.Checked, chkDirecMem.Checked, adrMax, chkDelai.Checked ? (int)numDelai.Value : 0);
			}
			Close();
		}

		private void chkMaxMem_CheckedChanged(object sender, EventArgs e) {
			tbxAdrMax.Enabled = chkMaxMem.Checked;
		}

		private void chk128Ko_CheckedChanged(object sender, EventArgs e) {
			tbxAdrMax.Visible = chkMaxMem.Visible = chk128Ko.Checked;
		}

		private void chkDelai_CheckedChanged(object sender, EventArgs e) {
			numDelai.Visible = lblDelai.Visible = chkDelai.Checked;
		}
	}
}
