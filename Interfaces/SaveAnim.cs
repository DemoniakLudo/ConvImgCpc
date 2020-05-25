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
			chk2Zone.Visible = chkDirecMem.Visible = rb1L.Visible = rb2L.Visible = rb4L.Visible = rb8L.Visible = img.modeVirtuel < 7;
		}

		private int PackWinDC(byte[] bufOut, ref int sizeDepack, int topBottom, bool razDiff, int modeLigne, bool optimSpeed) {
			int xFin = 0;
			int xDeb = img.NbCol;
			int yDeb = img.NbLig;
			int yFin = 0;
			int lStart = 0, lEnd = img.NbLig, xStart = 0, xEnd = img.NbCol;

			if (razDiff)
				Array.Clear(BufPrec, 0, BufPrec.Length);

			// Copier l'image cpc dans le buffer de travail
			img.bitmapCpc.CreeBmpCpc(img.BmpLock);

			if (chkZoneVert.Checked) {
				xStart = topBottom < 1 ? 0 : img.NbCol >> 1;
				xEnd = topBottom == 0 ? img.NbCol >> 1 : img.NbCol;
			}
			else {
				lStart = topBottom < 1 ? 0 : img.NbLig >> 1;
				lEnd = topBottom == 0 ? img.NbLig >> 1 : img.NbLig;
			}
			// Recherche les "coordonnées" de l'image différente par rapport à la précédente
			for (int l = lStart; l < lEnd; l += modeLigne) {
				int adr = img.GetAdrCpc(l << 1);
				for (int oct = xStart; oct < xEnd; oct++) {
					if (img.bitmapCpc.bmpCpc[adr + oct] != BufPrec[adr + oct]) {
						xDeb = Math.Min(xDeb, oct);
						xFin = Math.Max(xFin, oct);
						yDeb = Math.Min(yDeb, l);
						yFin = Math.Max(yFin, l);
						BufPrec[adr + oct] = img.bitmapCpc.bmpCpc[adr + oct];
					}
				}
			}

			int tailleX = xFin > xDeb ? xFin - xDeb + 1 : 0;
			int tailleY = (yFin + 1 - yDeb) / modeLigne;
			int length = tailleX * tailleY;
			if (length > 0) {
				Array.Clear(bLigne, 0, bLigne.Length);
				int pos = 0, AdrEcr;
				bLigne[pos++] = (byte)tailleX;
				bLigne[pos++] = (byte)tailleY;
				if (!optimSpeed) {
					AdrEcr = 0xC000 + xDeb + (yDeb >> 3) * img.NbCol + (yDeb & 7) * 0x800;
					bLigne[pos++] = (byte)(AdrEcr & 0xFF);
					bLigne[pos++] = (byte)(AdrEcr >> 8);
				}
				if (chkCol.Checked) {
					// passage en mode "colonne par colonne"
					for (int x = xDeb; x <= xFin; x++) {
						for (int l = 0; l < tailleY * modeLigne; l += modeLigne) {
							int offsetEcr = ((l + yDeb) >> 3) * img.NbCol + ((l + yDeb) & 7) * 0x800 + x;
							if (optimSpeed) {
								AdrEcr = 0xC000 + offsetEcr;
								bLigne[pos++] = (byte)(AdrEcr & 0xFF);
								bLigne[pos++] = (byte)(AdrEcr >> 8);
							}
							bLigne[pos++] = BufPrec[offsetEcr];
						}
					}
				}
				else {
					// Passage en mode "ligne à ligne"
					for (int l = yDeb; l <= yFin; l += modeLigne) {
						int offsetEcr = (l >> 3) * img.NbCol + (l & 7) * 0x800 + xDeb;
						if (optimSpeed) {
							AdrEcr = 0xC000 + offsetEcr;
							bLigne[pos++] = (byte)(AdrEcr & 0xFF);
							bLigne[pos++] = (byte)(AdrEcr >> 8);
						}
						Array.Copy(BufPrec, offsetEcr, bLigne, pos, tailleX);
						pos += tailleX;
					}
				}
				int lpack = PackDepack.Pack(bLigne, pos, bufOut, 0);
				sizeDepack = length + 4;
				return lpack;
			}
			else
				return 0;
		}

		private int PackDirectMem(byte[] bufOut, ref int sizeDepack, bool newMethode, bool razDiff) {
			if (razDiff)
				Array.Clear(BufPrec, 0, BufPrec.Length);

			// Copier l'image cpc dans le buffer de travail
			img.bitmapCpc.CreeBmpCpc(img.BmpLock);
			byte[] src = img.bitmapCpc.bmpCpc;

			int maxSize = (img.NbCol) + ((img.NbLig - 1) >> 3) * (img.NbCol) + ((img.NbLig - 1) & 7) * 0x800;
			if (maxSize >= 0x4000)
				maxSize += 0x3800;

			int maxDelta = newMethode ? 127 : 255;
			// Recherche les "coordonnées" de l'image différente par rapport à la précédente
			int bc = 0, posDiff = 0;
			byte deltaAdr = 0;
			for (int adr = 0; adr < maxSize; adr++) {
				byte o = BufPrec[adr];
				byte n = src[adr];
				if (deltaAdr == 127 || (o != n)) {
					if (newMethode && adr < maxSize - 256 && BufPrec[adr + 1] != src[adr + 1] && BufPrec[adr + 2] != src[adr + 2] && src[adr + 1] == n && src[adr + 2] == n) {
						DiffImage[posDiff++] = (byte)(deltaAdr | 0x80);
						bc++;
						int d = 0;
						while (d < 255 && src[adr + d] == n)
							d++;

						DiffImage[posDiff++] = (byte)d;
						DiffImage[posDiff++] = n;
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
			Array.Copy(src, BufPrec, BufPrec.Length);
			sizeDepack = posDiff + 2;
			return lPack;
		}
		/*
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
		*/
		private int PackAscii(byte[] bufOut, ref int sizeDepack, bool razDiff, bool firstFrame, bool perte = false) {
			byte[] imgAscii = new byte[2048];
			int posDiff = 0, lastPosDiff = 0, nDiff = 0;
			byte nbModif = 0;
			int tailleMax = (img.NbLig * img.NbCol) >> 3;

			if (razDiff)
				Array.Clear(OldImgAscii, 0, OldImgAscii.Length);

			if (img.modeVirtuel == 7)
				img.bitmapCpc.CreeImgAsciiMat(img.BmpLock, imgAscii);
			else
				img.bitmapCpc.CreeImgAscii(img.BmpLock, imgAscii);

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
				posDiff = Math.Min(lastPosDiff, posDiff);
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

		private int PackFrame(byte[] bufOut, ref int sizeDepack, bool razDiff, bool firstFrame, int topBottom, int modeLigne, bool optimSpeed) {
			if (img.modeVirtuel >= 7)
				return PackAscii(bufOut, ref sizeDepack, razDiff, firstFrame);
			else
				if (chkDirecMem.Checked)
					return PackDirectMem(bufOut, ref sizeDepack, true, razDiff);
				else
					return PackWinDC(bufOut, ref sizeDepack, topBottom, razDiff, modeLigne, optimSpeed);
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
			if (chkNoPtr.Checked && !gest128K)
				sw.WriteLine("	LD	HL,Delta0");
			else
				sw.WriteLine("	LD	IX,AnimDelta");

			sw.WriteLine("Boucle:");
			if (chkNoPtr.Checked && !gest128K) {
				sw.WriteLine("	LD	A,(HL)");
				sw.WriteLine("	INC	A");
			}
			else {
				sw.WriteLine("	LD	H,(IX+1)");
				sw.WriteLine("	LD	L,(IX+0)");
				sw.WriteLine("	LD	A,H");
				sw.WriteLine("	OR	H");
			}
			sw.WriteLine("	JR	Z,Debut");
			if (gest128K) {
				sw.WriteLine("	LD	B,#7F");
				sw.WriteLine("	LD	A,(IX+2)");
				sw.WriteLine("	OUT	(C),A");
			}
			sw.WriteLine("	LD	DE,Buffer");
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
			sw.WriteLine("	JP	Z,InitDraw		; Plus d'octets à traiter = fini" + Environment.NewLine);
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
			sw.WriteLine("InitDraw:");
			if (chkNoPtr.Checked && !gest128K)
				sw.WriteLine("	PUSH	HL");

			if (delai > 0) {
				sw.WriteLine("	LD	A,0");
				sw.WriteLine("	CP	" + delai.ToString());
				sw.WriteLine("	JR	C,InitDraw");
				sw.WriteLine("	XOR	A");
				sw.WriteLine("	LD	(InitDraw+1),A");
			}
		}

		private void GenereDrawDC(StreamWriter sw, int delai, bool gest128K, int addBC26, bool optimSpeed) {
			sw.WriteLine("	LD	HL,buffer");
			if (chkCol.Checked) {
				sw.WriteLine("	LD	B,(HL)		; Taille en X");
				sw.WriteLine("	INC	HL");
				sw.WriteLine("	LD	A,(HL)");
				sw.WriteLine("	INC	HL");
				sw.WriteLine("	LD	(NbLig+1),A		; Taille en Y");
				sw.WriteLine("	LD	E,(HL)");
				sw.WriteLine("	INC	HL");
				sw.WriteLine("	LD	D,(HL)		; Adresse écran");
				sw.WriteLine("	INC	HL");
				sw.WriteLine("	EX	DE,HL");
				if (delai > 0)
					sw.WriteLine("	DI");

				sw.WriteLine("	LD	(SauvSp+1),SP");
				sw.WriteLine("	LD	SP,#C0" + img.NbCol.ToString("X2"));
				sw.WriteLine("Draw1:");
				sw.WriteLine("	LD	(RecupHL+1),HL");
				sw.WriteLine("NbLig:");
				sw.WriteLine("	LD	C,0");
				sw.WriteLine("Draw2:");
				sw.WriteLine("	LD	A,(DE)");
				sw.WriteLine("	INC	DE");
				sw.WriteLine("	LD	(HL),A");
				sw.WriteLine("	LD	A,H");
				sw.WriteLine("	ADD	A,#" + (addBC26 + 1).ToString("X2"));
				sw.WriteLine("	LD	H,A");
				sw.WriteLine("	JR	NC,Draw3");
				sw.WriteLine("	ADD	HL,SP");
				sw.WriteLine("Draw3:");
				sw.WriteLine("	DEC	C");
				sw.WriteLine("	JR	NZ,Draw2");
				sw.WriteLine("RecupHL:");
				sw.WriteLine("	LD	HL,0");
				sw.WriteLine("	INC	HL");
				sw.WriteLine("	DJNZ	Draw1");
				sw.WriteLine("SauvSp:");
				sw.WriteLine("	LD	SP,0");
				if (delai > 0)
					sw.WriteLine("	EI");
			}
			else {
				sw.WriteLine("	LD	A,(HL)");
				sw.WriteLine("	INC	HL");
				sw.WriteLine("	LD	(Nbx+1),A		; Taille en X");
				if (!optimSpeed) {
					sw.WriteLine("	NEG");
					sw.WriteLine("	LD	(NbDec+1),A		; Ajustement pour 'BC26'");
					sw.WriteLine("	LD	A,(HL)			; Taille en Y");
					sw.WriteLine("	INC	HL");
					sw.WriteLine("	LD	E,(HL)");
					sw.WriteLine("	INC	HL");
					sw.WriteLine("	LD	D,(HL)			; Adresse ecran");
					sw.WriteLine("	INC	HL");
				}
				else {
					sw.WriteLine("	LD	A,(HL)			; Taille en Y");
					sw.WriteLine("	INC	HL");
				}
				sw.WriteLine("Nbx:");
				sw.WriteLine("	LD	BC,0");
				if (optimSpeed) {
					sw.WriteLine("	LD	E,(HL)");
					sw.WriteLine("	INC	HL");
					sw.WriteLine("	LD	D,(HL)			; Adresse ecran");
					sw.WriteLine("	INC	HL");
				}
				sw.WriteLine("	LDIR");
				if (!optimSpeed) {
					sw.WriteLine("	EX	DE,HL");
					sw.WriteLine("NbDec:");
					sw.WriteLine("	LD	BC,#" + addBC26.ToString("X2") + "FF			; 'BC26'");
					sw.WriteLine("	ADD	HL,BC");
					sw.WriteLine("	JR	NC,Suite");
					sw.WriteLine("	LD	BC,#C0" + img.NbCol.ToString("X2"));
					sw.WriteLine("	ADD	HL,BC");
					sw.WriteLine("Suite:");
					sw.WriteLine("	EX	DE,HL");
				}
				sw.WriteLine("	DEC	A");
				sw.WriteLine("	JR	NZ,Nbx");
			}
			if (chkNoPtr.Checked && !gest128K) {
				sw.WriteLine("	POP	HL");
				sw.WriteLine("	INC	HL");
			}
			else {
				sw.WriteLine("	INC	IX");
				sw.WriteLine("	INC	IX");
				if (gest128K)
					sw.WriteLine("	INC	IX");
			}
			sw.WriteLine("	JP	Boucle" + Environment.NewLine);
			sw.WriteLine("	Nolist");
		}

		private void GenereDrawOdin(StreamWriter sw, bool gest128K) {
			sw.WriteLine("	LD	HL,buffer");
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
			if (chkNoPtr.Checked && !gest128K) {
				sw.WriteLine("	POP	HL");
				sw.WriteLine("	INC	HL");
			}
			else {
				sw.WriteLine("	INC	IX");
				sw.WriteLine("	INC	IX");
				if (gest128K)
					sw.WriteLine("	INC	IX");
			}
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
			sw.WriteLine("	CPI");
			sw.WriteLine("	JR	DrawImgD3" + Environment.NewLine);
			sw.WriteLine("	Nolist");
		}

		private void GenereDrawAscii(StreamWriter sw, bool gest128K) {
			sw.WriteLine("	LD	HL,buffer");
			sw.WriteLine("	LD	A,(HL)");
			sw.WriteLine("	CP	'D'");
			sw.WriteLine("	JR	Z,DrawImgD");
			sw.WriteLine("	CP	'I'");
			sw.WriteLine("	JR	Z,DrawImgI");
			sw.WriteLine("	LD	BC,#C000");
			if (img.modeVirtuel == 7)
				sw.WriteLine("	LD	D,Fonte/256");

			sw.WriteLine("DrawImgO:");
			if (img.modeVirtuel > 7) {
				sw.WriteLine("	LD	D,Fonte/512");
				sw.WriteLine("	INC	HL");
				sw.WriteLine("	LD	E,(HL)			; Code ASCII");
				sw.WriteLine("	EX	DE,HL");
				sw.WriteLine("	ADD	HL,HL");
			}
			else {
				sw.WriteLine("	INC	HL");
				sw.WriteLine("	LD	A,(HL)			; Code ASCII");
				sw.WriteLine("	AND	#0F");
				sw.WriteLine("	RLCA");
				sw.WriteLine("	RLCA");
				sw.WriteLine("	LD	E,A");
				sw.WriteLine("	EX	DE,HL");
			}
			sw.WriteLine("	LD	A,(HL)");
			sw.WriteLine("	INC	HL");
			sw.WriteLine("	LD	(BC),A");
			sw.WriteLine("	SET	3,B");
			if (img.modeVirtuel == 7) {
				sw.WriteLine("	LD	A,(HL)");
				sw.WriteLine("	INC	HL");
			}
			sw.WriteLine("	LD	(BC),A");
			sw.WriteLine("	SET	4,B");
			if (img.modeVirtuel == 7) {
				sw.WriteLine("	LD	A,(HL)");
				sw.WriteLine("	INC	HL");
			}
			sw.WriteLine("	LD	(BC),A");
			sw.WriteLine("	RES	3,B");
			if (img.modeVirtuel == 7) {
				sw.WriteLine("	LD	A,(HL)");
				sw.WriteLine("	INC	HL");
			}
			sw.WriteLine("	LD	(BC),A");
			sw.WriteLine("	SET	5,B");
			if (img.modeVirtuel == 7) {
				sw.WriteLine("	LD	A,(DE)			; Code ASCII");
				sw.WriteLine("	AND	#F0");
				sw.WriteLine("	RRCA");
				sw.WriteLine("	RRCA");
				sw.WriteLine("	LD	L,A");
			}
			sw.WriteLine("	LD	A,(HL)");
			sw.WriteLine("	INC	HL");
			sw.WriteLine("	LD	(BC),A");
			sw.WriteLine("	SET	3,B");
			if (img.modeVirtuel == 7) {
				sw.WriteLine("	LD	A,(HL)");
				sw.WriteLine("	INC	HL");
			}
			sw.WriteLine("	LD	(BC),A");
			sw.WriteLine("	RES	4,B");
			if (img.modeVirtuel == 7) {
				sw.WriteLine("	LD	A,(HL)");
				sw.WriteLine("	INC	HL");
			}
			sw.WriteLine("	LD	(BC),A");
			sw.WriteLine("	RES	3,B");
			if (img.modeVirtuel == 7) {
				sw.WriteLine("	LD	A,(HL)");
				sw.WriteLine("	INC	HL");
			}
			sw.WriteLine("	LD	(BC),A");
			sw.WriteLine("	RES	5,B");
			sw.WriteLine("	EX	DE,HL");
			sw.WriteLine("	INC	BC");
			sw.WriteLine("	BIT	3,B");
			sw.WriteLine("	JR	Z,DrawImgO");
			sw.WriteLine("DrawImgI:");
			if (chkNoPtr.Checked && !gest128K) {
				sw.WriteLine("	POP	HL");
				sw.WriteLine("	INC	HL");
			}
			else {
				sw.WriteLine("	INC	IX");
				sw.WriteLine("	INC	IX");
				if (gest128K)
					sw.WriteLine("	INC	IX");
			}
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
			if (img.modeVirtuel > 7) {
				sw.WriteLine("	LD	L,(IY+4)			; Code ASCII");
				sw.WriteLine("	LD	H,Fonte/512");
				sw.WriteLine("	ADD	HL,HL");
			}
			else {
				sw.WriteLine("	LD	H,Fonte/256");
				sw.WriteLine("	LD	A,(IY+4)			; Code ASCII");
				sw.WriteLine("	AND	#0F");
				sw.WriteLine("	RLCA");
				sw.WriteLine("	RLCA");
				sw.WriteLine("	LD	L,A");
			}
			sw.WriteLine("	LD	A,(HL)");
			sw.WriteLine("	INC	HL");
			sw.WriteLine("	LD	(DE),A");
			sw.WriteLine("	SET	3,D");
			if (img.modeVirtuel == 7) {
				sw.WriteLine("	LD	A,(HL)");
				sw.WriteLine("	INC	HL");
			}
			sw.WriteLine("	LD	(DE),A");
			sw.WriteLine("	SET	4,D");
			if (img.modeVirtuel == 7) {
				sw.WriteLine("	LD	A,(HL)");
				sw.WriteLine("	INC	HL");
			}
			sw.WriteLine("	LD	(DE),A");
			sw.WriteLine("	RES	3,D");
			if (img.modeVirtuel == 7) {
				sw.WriteLine("	LD	A,(HL)");
				sw.WriteLine("	INC	HL");
			}
			sw.WriteLine("	LD	(DE),A");
			sw.WriteLine("	SET	5,D");
			if (img.modeVirtuel == 7) {
				sw.WriteLine("	LD	A,(IY+4)			; Code ASCII");
				sw.WriteLine("	AND	#F0");
				sw.WriteLine("	RRCA");
				sw.WriteLine("	RRCA");
				sw.WriteLine("	LD	L,A");
			}
			sw.WriteLine("	LD	A,(HL)");
			sw.WriteLine("	INC	HL");
			sw.WriteLine("	LD	(DE),A");
			sw.WriteLine("	SET	3,D");
			if (img.modeVirtuel == 7) {
				sw.WriteLine("	LD	A,(HL)");
				sw.WriteLine("	INC	HL");
			}
			sw.WriteLine("	LD	(DE),A");
			sw.WriteLine("	RES	4,D");
			if (img.modeVirtuel == 7) {
				sw.WriteLine("	LD	A,(HL)");
				sw.WriteLine("	INC	HL");
			}
			sw.WriteLine("	LD	(DE),A");
			sw.WriteLine("	RES	3,D");
			if (img.modeVirtuel == 7) {
				sw.WriteLine("	LD	A,(HL)");
				sw.WriteLine("	INC	HL");
			}
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
			if (img.modeVirtuel > 7) {
				sw.WriteLine("DataFnt:");
				sw.WriteLine("	DB	#00, #C0, #0C, #CC, #30, #F0, #3C, #FC");
				sw.WriteLine("	DB	#03, #C3, #0F, #CF, #33, #F3, #3F, #FF");
				sw.WriteLine("Fonte	EQU	#B000		; Doit être un multiple de 512");
			}
		}

		private void GenerePaletteOld(StreamWriter sw, ImageCpc img) {
			sw.WriteLine("Palette:");
			string line = "\tDB\t\"";
			for (int i = 0; i < 16; i++)
				line += BitmapCpc.CpcVGA[img.bitmapCpc.Palette[i]];

			// Border = couleur 0
			line += BitmapCpc.CpcVGA[img.bitmapCpc.Palette[0]] + "\",";
			line += "#" + ((img.modeVirtuel == 7 ? 1 : img.modeVirtuel & 3) | 0x8C).ToString("X2");
			sw.WriteLine(line);
		}

		private void GenerePalettePlus(StreamWriter sw, ImageCpc img) {
			sw.WriteLine("UnlockAsic:");
			sw.WriteLine("	DB	#FF, #00, #FF, #77, #B3, #51, #A8, #D4, #62, #39, #9C, #46, #2B, #15, #8A, #CD, #EE");
			sw.WriteLine("Palette:");
			string line = "\tDB\t#" + ((img.modeVirtuel == 7 ? 1 : img.modeVirtuel & 3) | 0x8C).ToString("X2");
			for (int i = 0; i < 16; i++)
				line += ",#" + ((byte)(((img.Palette[i] >> 4) & 0x0F) | (img.Palette[i] << 4))).ToString("X2") + ",#" + ((byte)(img.Palette[i] >> 8)).ToString("X2");

			// Border = couleur 0
			line += ",#" + ((byte)(((img.Palette[0] >> 4) & 0x0F) | (img.Palette[0] << 4))).ToString("X2") + ",#" + ((byte)(img.Palette[0] >> 8)).ToString("X2");
			sw.WriteLine(line);
		}

		private void GenereFin(StreamWriter sw, int ltot, bool force8000) {
			sw.WriteLine("; Taille totale animation = " + ltot.ToString() + " (#" + ltot.ToString("X4") + ")");
			sw.WriteLine("	List");
			sw.WriteLine("buffer" + (force8000 ? "	equ	#8000" : ":"));
			if (img.modeVirtuel == 7) {
				int[] ordreAdr = new int[4] { 0, 1, 3, 2 };
				sw.WriteLine("	ORG	#B000		; Doit être un multiple de 1024");
				sw.WriteLine("Fonte:");
				for (int i = 0; i < 16; i++) {
					string str = "	DB	";
					for (int ym = 0; ym < 4; ym++) {
						byte octet = 0;
						for (int xm = 0; xm < 4; xm++)
							octet |= (byte)(BitmapCpc.tabOctetMode[BitmapCpc.trameM1[i, xm, ordreAdr[ym]]] >> xm);

						str += "#" + octet.ToString("X2") + ",";
					}
					sw.WriteLine(str.Substring(0, str.Length - 1));
				}
			}
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
					sw.WriteLine("\tDB\t#" + bank[i].ToString("X2"));
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

		private void SauveDeltaPack(int adrDeb, int adrMax, int delai, int modeLigne, bool optimSpeed) {
			int sizeDepack = 0;
			int nbImages = img.main.GetMaxImages();
			byte[][] bufOut = new byte[nbImages << 1][];
			int[] lg = new int[nbImages << 1];
			int[] bank = new int[nbImages << 1];
			for (int i = 0; i < nbImages << 1; i++)
				bufOut[i] = new byte[0x8000];

			if (adrMax == 0)
				adrMax = 0xBE00;

			if (chkBoucle.Checked) {
				img.main.SelectImage(nbImages - 1, true);
				img.Convert(true, true);
				PackFrame(bufOut[0], ref sizeDepack, true, false, -1, modeLigne, optimSpeed);
			}

			img.main.SetInfo("Début sauvegarde animation assembleur...");
			// Calcule les animations
			int ltot = 0, maxDepack = 0;
			int posPack = 0;
			for (int i = 0; i < nbImages; i++) {
				img.main.SelectImage(i, true);
				img.Convert(true, true);
				Application.DoEvents();
				if (chk2Zone.Checked) {
					lg[posPack] = PackFrame(bufOut[posPack], ref sizeDepack, i == 0 && !chkBoucle.Checked, i == 0 && !chkBoucle.Checked, 0, modeLigne, optimSpeed);
					if (lg[posPack] > 0)
						ltot += lg[posPack++];

					lg[posPack] = PackFrame(bufOut[posPack], ref sizeDepack, i == 0 && !chkBoucle.Checked, i == 0 && !chkBoucle.Checked, 1, modeLigne, optimSpeed);
					if (lg[posPack] > 0)
						ltot += lg[posPack++];
				}
				else {
					lg[posPack] = PackFrame(bufOut[posPack], ref sizeDepack, i == 0 && !chkBoucle.Checked, i == 0 && !chkBoucle.Checked, -1, modeLigne, optimSpeed);
					if (lg[posPack] > 0)
						ltot += lg[posPack++];
				}
				maxDepack = Math.Max(maxDepack, sizeDepack);
			}

			// Sauvegarde
			StreamWriter sw = Save.OpenAsm(fileName, version, param);
			GenereEntete(sw, adrDeb);
			if (img.cpcPlus)
				GenereInitPlus(sw);
			else
				GenereInitOld(sw);

			bool gest128K = chk128Ko.Checked;
			if ((ltot + adrDeb < adrMax) && (ltot + adrDeb < 0xBE00 - maxDepack))
				gest128K = false;

			GenereAffichage(sw, delai, gest128K);
			if (img.modeVirtuel >= 7)
				GenereDrawAscii(sw, gest128K);
			else
				if (chkDirecMem.Checked)
					GenereDrawOdin(sw, gest128K);
				else
					GenereDrawDC(sw, delai, gest128K, modeLigne == 8 ? 0x3F : modeLigne == 4 ? 0x1F : modeLigne == 2 ? 0xF : 0x7, optimSpeed);

			if (img.cpcPlus)
				GenerePalettePlus(sw, img);
			else
				GenerePaletteOld(sw, img);

			int endBank0 = 0;
			int lbank = 0, numBank = 0xC0;
			for (int i = 0; i < posPack; i++) {
				lbank += lg[i];
				if (gest128K && lbank > (numBank == 0xC0 ? Math.Min((0xBE00 - maxDepack - adrDeb), adrMax - adrDeb) : 0x4000) && (numBank > 0xC0 || lbank + adrDeb - lg[i] > 0x4000)) {
					if (numBank == 0xC0) {
						endBank0 = lbank + adrDeb - lg[i];
						sw.WriteLine("EndBank0:");
						numBank = 0xC4;
					}
					else {
						numBank++;
						if ((numBank & 15) == 8)
							numBank += 4;

						if ((numBank & 15) == 15)
							numBank += 5;
					}
					lbank = lg[i];
					sw.WriteLine("	ORG	#4000");
					sw.WriteLine("	Write Direct -1,-1,#" + numBank.ToString("X2"));
				}
				bank[i] = numBank;
				sw.WriteLine("Delta" + i.ToString() + ":\t\t; Taille #" + lg[i].ToString("X4"));
				Save.SauveAssembleur(sw, bufOut[i], lg[i], param);
			}
			if (!chkNoPtr.Checked || gest128K)
				GenerePointeurs(sw, posPack, bank, gest128K && numBank > 0xC0);
			else {
				sw.WriteLine("	DB	#FF			; Fin de l'animation");
				ltot++;
			}
			GenereFin(sw, ltot, gest128K && endBank0 < 0x8000);
			Save.CloseAsm(sw);
			for (int i = 0; i < posPack; i++)
				bufOut[i] = null;

			img.main.SetInfo("Longueur totale données animation : " + ltot + " octets.");
			if (numBank > 0xC7 || (!chk128Ko.Checked && ltot + adrDeb >= 0xBE00 - maxDepack)) {
				MessageBox.Show("Attention ! la taille totale (animation + buffer de décompactage) dépassera " + (chk128Ko.Checked ? "112K" : "48Ko") + ", risque d'écrasement de la mémoire vidéo et plantage..."
								, "Alerte"
								, MessageBoxButtons.OK
								, MessageBoxIcon.Warning);
				img.main.SetInfo("Dépassement capacité mémoire...");
			}
			else
				img.main.SetInfo("Sauvegarde animation assembleur ok.");

			GC.Collect();
		}

		private void chkMaxMem_CheckedChanged(object sender, EventArgs e) {
			tbxAdrMax.Enabled = chkMaxMem.Checked;
		}

		private void chk128Ko_CheckedChanged(object sender, EventArgs e) {
			tbxAdrMax.Visible = chkMaxMem.Visible = chk128Ko.Checked;
			chkNoPtr.Visible = !chk128Ko.Checked;
		}

		private void chkDelai_CheckedChanged(object sender, EventArgs e) {
			numDelai.Visible = lblDelai.Visible = chkDelai.Checked;
		}

		private void chkDirecMem_CheckedChanged(object sender, EventArgs e) {
			chk2Zone.Visible = chkZoneVert.Visible = rb1L.Visible = rb2L.Visible = rb4L.Visible = rb8L.Visible = !chkDirecMem.Checked && img.modeVirtuel < 7;
			chkZoneVert.Visible = chk2Zone.Visible && chk2Zone.Checked;
		}

		private void chk2Zone_CheckedChanged(object sender, EventArgs e) {
			chkZoneVert.Visible = chk2Zone.Checked;
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

			bool optimSpeed = true;

			int modeLigne = rb8L.Checked ? 8 : rb4L.Checked ? 4 : rb2L.Checked ? 2 : 1;
			if (adrDeb > 0) {
				img.WindowState = FormWindowState.Minimized;
				img.Show();
				img.WindowState = FormWindowState.Normal;
				SauveDeltaPack(adrDeb, adrMax, chkDelai.Checked ? (int)numDelai.Value : 0, modeLigne, optimSpeed);
			}
			Close();
		}
	}
}
