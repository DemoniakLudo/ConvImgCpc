using System;
using System.IO;

namespace ConvImgCpc {
	public class SaveAsm {
		static public StreamWriter OpenAsm(string fileName, string version) {
			StreamWriter sw = File.CreateText(fileName);
			sw.WriteLine("; Généré par ConvImgCpc" + version.Replace('\n', ' ') + " - le " + DateTime.Now.ToString("dd/MM/yyyy (HH mm ss)"));
			sw.WriteLine("; Mode écran - " + BitmapCpc.modesVirtuels[BitmapCpc.modeVirtuel]);
			sw.WriteLine("; Taille (nbColsxNbLignes) " + BitmapCpc.NbCol.ToString() + "x" + BitmapCpc.NbLig.ToString());
			return sw;
		}

		static public void GenereDatas(StreamWriter sw, byte[] tabByte, int length, int nbOctetsLigne) {
			string line = "\tDB\t";
			int nbOctets = 0;
			for (int i = 0; i < length; i++) {
				line += "#" + tabByte[i].ToString("X2") + ",";
				if (++nbOctets >= Math.Min(nbOctetsLigne, BitmapCpc.NbCol)) {
					sw.WriteLine(line.Substring(0, line.Length - 1));
					line = "\tDB\t";
					nbOctets = 0;
				}
			}
			if (nbOctets > 0)
				sw.WriteLine(line.Substring(0, line.Length - 1));
		}

		static public void GenereDZ80(StreamWriter sw, string jumpLabel = null) {
			sw.WriteLine("; Decompactage");
			sw.WriteLine("DepkLzw:");
			sw.WriteLine("	ld	bc, #ffff	; preserve default offset 1");
			sw.WriteLine("	push    bc");
			sw.WriteLine("	inc     bc");
			sw.WriteLine("	ld      a, #80");
			sw.WriteLine("dzx0s_literals:");
			sw.WriteLine("	call    dzx0s_elias             ; obtain length");
			sw.WriteLine("	ldir                            ; copy literals");
			sw.WriteLine("	add     a, a                    ; copy from last offset or new offset?");
			sw.WriteLine("	jr      c, dzx0s_new_offset");
			sw.WriteLine("	call    dzx0s_elias             ; obtain length");
			sw.WriteLine("dzx0s_copy:");
			sw.WriteLine("	ex      (sp), hl                ; preserve source, restore offset");
			sw.WriteLine("	push    hl                      ; preserve offset");
			sw.WriteLine("	add     hl, de                  ; calculate destination - offset");
			sw.WriteLine("	ldir                            ; copy from offset");
			sw.WriteLine("	pop     hl                      ; restore offset");
			sw.WriteLine("	ex      (sp), hl                ; preserve offset, restore source");
			sw.WriteLine("	add     a, a                    ; copy from literals or new offset?");
			sw.WriteLine("	jr      nc, dzx0s_literals");
			sw.WriteLine("dzx0s_new_offset:");
			sw.WriteLine("	call    dzx0s_elias             ; obtain offset MSB");
			sw.WriteLine("	ld b,a");
			sw.WriteLine("	pop     af                      ; discard last offset");
			sw.WriteLine("	xor     a                       ; adjust for negative offset");
			sw.WriteLine("	sub     c");
			sw.WriteLine((jumpLabel != null ? ("	JP	Z," + jumpLabel) : "	RET	Z") + "		; Plus d'octets à traiter = fini" + Environment.NewLine);
			sw.WriteLine("	ld      c,a");
			sw.WriteLine("	ld      a,b");
			sw.WriteLine("	ld      b,c");
			sw.WriteLine("	ld      c, (hl)                 ; obtain offset LSB");
			sw.WriteLine("	inc     hl");
			sw.WriteLine("	rr      b                       ; last offset bit becomes first length bit");
			sw.WriteLine("	rr      c");
			sw.WriteLine("	push    bc                      ; preserve new offset");
			sw.WriteLine("	ld      bc, 1                   ; obtain length");
			sw.WriteLine("	call    nc, dzx0s_elias_backtrack");
			sw.WriteLine("	inc     bc");
			sw.WriteLine("	jr      dzx0s_copy");
			sw.WriteLine("dzx0s_elias:");
			sw.WriteLine("	inc     c                       ; interlaced Elias gamma coding");
			sw.WriteLine("dzx0s_elias_loop:");
			sw.WriteLine("	add     a, a");
			sw.WriteLine("	jr      nz, dzx0s_elias_skip");
			sw.WriteLine("	ld      a, (hl)                 ; load another group of 8 bits");
			sw.WriteLine("	inc     hl");
			sw.WriteLine("	rla");
			sw.WriteLine("dzx0s_elias_skip:");
			sw.WriteLine("	ret 	c");
			sw.WriteLine("dzx0s_elias_backtrack:");
			sw.WriteLine("	add     a, a");
			sw.WriteLine("	rl      c");
			sw.WriteLine("	rl      b");
			sw.WriteLine("	jr      dzx0s_elias_loop");
		}

		static public void GenereDepack(StreamWriter sw, string jumpLabel = null) {
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
			sw.WriteLine((jumpLabel != null ? ("	JP	Z," + jumpLabel) : "	RET	Z") + "		; Plus d'octets à traiter = fini" + Environment.NewLine);
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

		static public void GenereFormatEcran(StreamWriter sw) {
			if (BitmapCpc.NbCol != 80) {
				sw.WriteLine("	LD	HL,#" + ((BitmapCpc.NbCol + 1) >> 1).ToString("X2") + (26 + (BitmapCpc.NbCol >> 2)).ToString("X2"));
				sw.WriteLine("	LD	BC,#BC01");
				sw.WriteLine("	OUT	(C),C");
				sw.WriteLine("	INC	B");
				sw.WriteLine("	OUT	(C),H");
				sw.WriteLine("	DEC	B");
				sw.WriteLine("	INC	C");
				sw.WriteLine("	OUT	(C),C");
				sw.WriteLine("	INC	B");
				sw.WriteLine("	OUT	(C),L");
			}
			if (BitmapCpc.NbLig != 200) {
				sw.WriteLine("	LD	HL,#" + ((BitmapCpc.NbLig + 7) >> 3).ToString("X2") + (18 + (BitmapCpc.NbLig >> 4)).ToString("X2"));
				sw.WriteLine("	LD	BC,#BC06");
				sw.WriteLine("	OUT	(C),C");
				sw.WriteLine("	INC	B");
				sw.WriteLine("	OUT	(C),H");
				sw.WriteLine("	DEC	B");
				sw.WriteLine("	INC	C");
				sw.WriteLine("	OUT	(C),C");
				sw.WriteLine("	INC	B");
				sw.WriteLine("	OUT	(C),L");
			}
			if (BitmapCpc.NbLig * BitmapCpc.NbCol > 0x4000) {
				sw.WriteLine("	LD	BC,#BC0C");
				sw.WriteLine("	OUT	(C),C");
				sw.WriteLine("	INC	B");
				sw.WriteLine("	INC	C");
				sw.WriteLine("	OUT	(C),C");
			}
		}

		static public void GenereEntete(StreamWriter sw, int adr) {
			sw.WriteLine("	ORG	#" + adr.ToString("X4"));
			sw.WriteLine("	RUN	$" + Environment.NewLine);
			sw.WriteLine("	DI");
			GenereFormatEcran(sw);
		}

		static public void GenereInitOld(StreamWriter sw) {
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

		static public void GenereInitPlus(StreamWriter sw) {
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

		static public void GenereAffichage(StreamWriter sw, bool delai, bool reboucle, bool gest128K, bool imageMode, Main.PackMethode methode) {
			if (delai) {
				sw.WriteLine("	LD	HL,NewIrq");
				sw.WriteLine("	LD	(#39),HL");
				sw.WriteLine("	EI");
			}
			if (BitmapCpc.modeVirtuel > 7) {
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

			if (!imageMode) {
				sw.WriteLine("Boucle:");
				if (gest128K) {
					sw.WriteLine("	LD	BC,#7FC0");
					sw.WriteLine("	OUT	(C),C");
				}
				sw.WriteLine("	LD	H,(IX+1)");
				sw.WriteLine("	LD	L,(IX+0)");
				sw.WriteLine("	LD	A,H");
				sw.WriteLine("	OR	H");
				if (reboucle) {
					sw.WriteLine("	JR	NZ,Boucle2");
					sw.WriteLine("	LD	IX,AnimDelta+" + (2 + (gest128K ? 1 : 0) + (delai ? 1 : 0)).ToString());
					sw.WriteLine("	JR	Boucle");

					sw.WriteLine("Boucle2:");
				}
				else
					sw.WriteLine("	JR	Z,Debut");
			}
			if (delai) {
				sw.WriteLine("	LD	A,(IX+2)");
				sw.WriteLine("	LD	(SyncFrame+3),A");
			}
			if (gest128K) {
				sw.WriteLine("	LD	B,#7F");
				sw.WriteLine("	LD	A,(IX+3)");
				sw.WriteLine("	OUT	(C),A");
			}
			sw.WriteLine("	LD	DE,Buffer");
			if (methode == Main.PackMethode.Standard)
				GenereDepack(sw, "InitDraw");
			else
				GenereDZ80(sw, "InitDraw");

			if (delai) {
				sw.WriteLine("NewIrq:");
				sw.WriteLine("	PUSH	AF");
				sw.WriteLine("	LD	A,(SyncFrame+1)");
				sw.WriteLine("	INC	A");
				sw.WriteLine("	LD	(SyncFrame+1),A");
				sw.WriteLine("	POP	AF");
				sw.WriteLine("	EI");
				sw.WriteLine("	RET");
			}
			sw.WriteLine("InitDraw:");
			if (delai) {
				sw.WriteLine("SyncFrame:");
				sw.WriteLine("	LD	A,0");
				sw.WriteLine("	CP	1");
				sw.WriteLine("	JR	C,SyncFrame");
				sw.WriteLine("	XOR	A");
				sw.WriteLine("	LD	(SyncFrame+1),A");
			}
		}

		static public void GenereDrawDC(StreamWriter sw, bool delai, bool modeCol, bool gest128K, int addBC26, bool optimSpeed) {
			sw.WriteLine("	LD	HL,buffer");
			if (modeCol) {
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
				if (delai)
					sw.WriteLine("	DI");

				sw.WriteLine("	LD	(SauvSp+1),SP");
				sw.WriteLine("	LD	SP,#C0" + BitmapCpc.NbCol.ToString("X2"));
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
				if (delai)
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
					sw.WriteLine("	LD	BC,#C0" + BitmapCpc.NbCol.ToString("X2"));
					sw.WriteLine("	ADD	HL,BC");
					sw.WriteLine("Suite:");
					sw.WriteLine("	EX	DE,HL");
				}
				sw.WriteLine("	DEC	A");
				sw.WriteLine("	JR	NZ,Nbx");
			}
			sw.WriteLine("	INC	IX");
			sw.WriteLine("	INC	IX");
			if (delai)
				sw.WriteLine("	INC	IX");

			if (gest128K)
				sw.WriteLine("	INC	IX");

			sw.WriteLine("	JP	Boucle" + Environment.NewLine);
			sw.WriteLine("	Nolist");
		}

		static private void GenereTspace(StreamWriter sw, bool wait) {
			sw.WriteLine("TstSpace:");
			sw.WriteLine("	LD	BC,#F40E");
			sw.WriteLine("	OUT	(C),C");
			sw.WriteLine("	LD	BC,#F6C0");
			sw.WriteLine("	OUT	(C),C");
			sw.WriteLine("	XOR	A");
			sw.WriteLine("	OUT	(C),A");
			sw.WriteLine("	LD	BC,#F792");
			sw.WriteLine("	OUT	(C),C");
			sw.WriteLine("	LD	BC,#F645");
			sw.WriteLine("	OUT	(C),C");
			sw.WriteLine("	LD	B,#F4");
			sw.WriteLine("	IN	A,(C)");
			sw.WriteLine("	LD	BC,#F782");
			sw.WriteLine("	OUT	(C),C");
			sw.WriteLine("	LD	BC,#F600");
			sw.WriteLine("	OUT	(C),C");
			sw.WriteLine("	INC	A");
			if (wait)
				sw.WriteLine("	JR	Z,TstSpace");

			sw.WriteLine("	RET");
		}

		static public void GenereDrawDirect(StreamWriter sw, bool gest128K) {
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
			sw.WriteLine("	INC	IX");
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
			sw.WriteLine("	CPI");
			sw.WriteLine("	JR	DrawImgD3" + Environment.NewLine);
			sw.WriteLine("	Nolist");
		}

		static public void GenereDrawAscii(StreamWriter sw, bool frameFull, bool frameO, bool frameD, bool gest128K, bool imageMode, bool withSpeed) {
			if (frameFull && !imageMode) {
				sw.WriteLine("	LD	HL,Buffer");
				sw.WriteLine("	LD	A,(HL)");
				sw.WriteLine("	CP	'D'");
				sw.WriteLine("	JR	Z,DrawImgD");
				sw.WriteLine("	CP	'I'");
				sw.WriteLine("	JR	Z,DrawImgI");
			}
			else
				if (frameO)
					sw.WriteLine("	LD	HL,Buffer");

			if (!frameD) {
				sw.WriteLine("	LD	BC,#C000");
				if (BitmapCpc.modeVirtuel == 7)
					sw.WriteLine("	LD	D,Fonte/256");

				sw.WriteLine("DrawImgO:");
				if (BitmapCpc.modeVirtuel > 7) {
					sw.WriteLine("	LD	D,Fonte/512");
					if (!frameO)
						sw.WriteLine("	INC	HL");

					sw.WriteLine("	LD	E,(HL)			; Code ASCII");
					sw.WriteLine("	EX	DE,HL");
					sw.WriteLine("	ADD	HL,HL");
				}
				else {
					if (!frameO)
						sw.WriteLine("	INC	HL");

					sw.WriteLine("	LD	A,(HL)			; Code ASCII");
					sw.WriteLine("	AND	#0F");
					sw.WriteLine("	RLCA");
					sw.WriteLine("	RLCA");
					sw.WriteLine("	LD	E,A");
					sw.WriteLine("	EX	DE,HL");
				}
				sw.WriteLine("	LD	A,(HL)");
				sw.WriteLine("	INC	L");
				sw.WriteLine("	LD	(BC),A");
				sw.WriteLine("	SET	3,B");
				if (BitmapCpc.modeVirtuel == 7) {
					sw.WriteLine("	LD	A,(HL)");
					sw.WriteLine("	INC	L");
				}
				sw.WriteLine("	LD	(BC),A");
				sw.WriteLine("	SET	4,B");
				if (BitmapCpc.modeVirtuel == 7) {
					sw.WriteLine("	LD	A,(HL)");
					sw.WriteLine("	INC	L");
				}
				sw.WriteLine("	LD	(BC),A");
				sw.WriteLine("	RES	3,B");
				if (BitmapCpc.modeVirtuel == 7) {
					sw.WriteLine("	LD	A,(HL)");
					sw.WriteLine("	INC	L");
				}
				sw.WriteLine("	LD	(BC),A");
				sw.WriteLine("	SET	5,B");
				if (BitmapCpc.modeVirtuel == 7) {
					sw.WriteLine("	LD	A,(DE)			; Code ASCII");
					sw.WriteLine("	AND	#F0");
					sw.WriteLine("	RRCA");
					sw.WriteLine("	RRCA");
					sw.WriteLine("	ADD	A,3");
					sw.WriteLine("	LD	L,A");
				}
				sw.WriteLine("	LD	A,(HL)");
				if (BitmapCpc.modeVirtuel == 7)
					sw.WriteLine("	DEC	L");
				else
					sw.WriteLine("	INC	HL");

				sw.WriteLine("	LD	(BC),A");
				sw.WriteLine("	SET	3,B");
				if (BitmapCpc.modeVirtuel == 7) {
					sw.WriteLine("	LD	A,(HL)");
					sw.WriteLine("	DEC	L");
				}
				sw.WriteLine("	LD	(BC),A");
				sw.WriteLine("	RES	4,B");
				if (BitmapCpc.modeVirtuel == 7) {
					sw.WriteLine("	LD	A,(HL)");
					sw.WriteLine("	DEC	L");
				}
				sw.WriteLine("	LD	(BC),A");
				sw.WriteLine("	RES	3,B");
				if (BitmapCpc.modeVirtuel == 7) {
					sw.WriteLine("	LD	A,(HL)");
				}
				sw.WriteLine("	LD	(BC),A");
				sw.WriteLine("	RES	5,B");
				sw.WriteLine("	EX	DE,HL");
				if (frameO)
					sw.WriteLine("	INC	HL");

				sw.WriteLine("	INC	BC");
				sw.WriteLine("	BIT	3,B");
				sw.WriteLine("	JR	Z,DrawImgO");
			}
			if (!frameD) {
				if (!imageMode) {
					sw.WriteLine("DrawImgI:");
					sw.WriteLine("	INC	IX");
					sw.WriteLine("	INC	IX");
					if (withSpeed)
						sw.WriteLine("	INC	IX");

					if (gest128K)
						sw.WriteLine("	INC	IX");

					sw.WriteLine("	JP	Boucle");
				}
				else
					GenereTspace(sw, true);
			}
			if (!frameO) {
				if (!frameD)
					sw.WriteLine("DrawImgD:");

				sw.WriteLine("	LD	HL,#C000");
				if (frameD)
					sw.WriteLine("	LD	IY,Buffer");
				else
					sw.WriteLine("	LD	IY,Buffer+1");

				sw.WriteLine("	LD	C,(IY+0)");
				sw.WriteLine("	LD	B,(IY+1)");
				sw.WriteLine("DrawImgD1:");
				sw.WriteLine("	LD	D,0");
				sw.WriteLine("	LD	E,(IY+2)			; Déplacement");
				sw.WriteLine("	ADD	HL,DE			; Ajouter à aresse écran");
				sw.WriteLine("	EX	DE,HL");
				if (BitmapCpc.modeVirtuel > 7) {
					sw.WriteLine("	LD	H,Fonte/512");
					sw.WriteLine("	LD	L,(IY+3)			; Code ASCII");
					sw.WriteLine("	ADD	HL,HL");
				}
				else {
					sw.WriteLine("	LD	H,Fonte/256");
					sw.WriteLine("	LD	A,(IY+3)			; Code ASCII");
					sw.WriteLine("	AND	#0F");
					sw.WriteLine("	RLCA");
					sw.WriteLine("	RLCA");
					sw.WriteLine("	LD	L,A");
				}
				sw.WriteLine("	EX	DE,HL");
				sw.WriteLine("	LD	A,(DE)");
				sw.WriteLine("	INC	E");
				sw.WriteLine("	LD	(HL),A");
				sw.WriteLine("	SET	3,H");
				if (BitmapCpc.modeVirtuel == 7) {
					sw.WriteLine("	LD	A,(DE)");
					sw.WriteLine("	INC	E");
				}
				sw.WriteLine("	LD	(HL),A");
				sw.WriteLine("	SET	4,H");
				if (BitmapCpc.modeVirtuel == 7) {
					sw.WriteLine("	LD	A,(DE)");
					sw.WriteLine("	INC	E");
				}
				sw.WriteLine("	LD	(HL),A");
				sw.WriteLine("	RES	3,H");
				if (BitmapCpc.modeVirtuel == 7) {
					sw.WriteLine("	LD	A,(DE)");
					sw.WriteLine("	INC	E");
				}
				sw.WriteLine("	LD	(HL),A");
				sw.WriteLine("	SET	5,H");
				if (BitmapCpc.modeVirtuel == 7) {
					sw.WriteLine("	LD	A,(IY+3)			; Code ASCII");
					sw.WriteLine("	AND	#F0");
					sw.WriteLine("	RRCA");
					sw.WriteLine("	RRCA");
					sw.WriteLine("	ADD	A,3");
					sw.WriteLine("	LD	E,A");
				}
				sw.WriteLine("	LD	A,(DE)");
				if (BitmapCpc.modeVirtuel == 7)
					sw.WriteLine("	DEC	E");
				else
					sw.WriteLine("	INC	DE");

				sw.WriteLine("	LD	(HL),A");
				sw.WriteLine("	SET	3,H");
				if (BitmapCpc.modeVirtuel == 7) {
					sw.WriteLine("	LD	A,(DE)");
					sw.WriteLine("	DEC E");
				}
				sw.WriteLine("	LD	(HL),A");
				sw.WriteLine("	RES	4,H");
				if (BitmapCpc.modeVirtuel == 7) {
					sw.WriteLine("	LD	A,(DE)");
					sw.WriteLine("	DEC	E");
				}
				sw.WriteLine("	LD	(HL),A");
				sw.WriteLine("	RES	3,H");
				if (BitmapCpc.modeVirtuel == 7) {
					sw.WriteLine("	LD	A,(DE)");
				}
				sw.WriteLine("	LD	(HL),A");
				sw.WriteLine("	RES	5,H");
				sw.WriteLine("	INC	IY");
				sw.WriteLine("	INC	IY");
				sw.WriteLine("	CPI");
				sw.WriteLine("	JP	PE,DrawImgD1");
				if (frameD) {
					if (!imageMode) {
						sw.WriteLine("	INC	IX");
						sw.WriteLine("	INC	IX");
						if (withSpeed)
							sw.WriteLine("	INC	IX");

						if (gest128K)
							sw.WriteLine("	INC	IX");

						sw.WriteLine("	JP	Boucle");
					}
					else
						GenereTspace(sw, true);
				}
				else
					sw.WriteLine("	JR	DrawImgI");
			}
			sw.WriteLine("	Nolist");
			if (BitmapCpc.modeVirtuel > 7) {
				sw.WriteLine("DataFnt:");
				sw.WriteLine("	DB	#00,#C0,#0C,#CC,#30,#F0,#3C,#FC,#03,#C3,#0F,#CF,#33,#F3,#3F,#FF");
			}
		}

		static public void GenerePaletteOld(StreamWriter sw, ImageCpc img) {
			sw.WriteLine("Palette:");
			string line = "\tDB\t";
			for (int i = 0; i < 17; i++)
				line += "#" + ((int)BitmapCpc.CpcVGA[BitmapCpc.Palette[i < BitmapCpc.MaxPen() ? i : 0]]).ToString("X2") + ",";

			line += "#" + ((BitmapCpc.modeVirtuel == 7 ? 1 : BitmapCpc.modeVirtuel & 3) | 0x8C).ToString("X2");
			sw.WriteLine(line);
		}

		static public void GenerePalettePlus(StreamWriter sw, ImageCpc img) {
			sw.WriteLine("UnlockAsic:");
			sw.WriteLine("	DB	#FF,#00,#FF,#77,#B3,#51,#A8,#D4,#62,#39,#9C,#46,#2B,#15,#8A,#CD,#EE");
			sw.WriteLine("Palette:");
			string line = "\tDB\t#" + ((BitmapCpc.modeVirtuel == 7 ? 1 : BitmapCpc.modeVirtuel & 3) | 0x8C).ToString("X2");
			for (int i = 0; i < 17; i++) {
				int c = BitmapCpc.Palette[i < BitmapCpc.MaxPen() ? i : 0];
				line += ",#" + ((byte)(((c >> 4) & 0x0F) | (c << 4))).ToString("X2") + ",#" + ((byte)(c >> 8)).ToString("X2");
			}
			sw.WriteLine(line);
		}

		static public void GenereFin(StreamWriter sw, int ltot, bool force8000) {
			sw.WriteLine("; Taille totale animation = " + ltot.ToString() + " (#" + ltot.ToString("X4") + ")");
			sw.WriteLine("_EndCode:");
			sw.WriteLine("	List");
			if (BitmapCpc.modeVirtuel >= 7) {
				int align = BitmapCpc.modeVirtuel == 7 ? 256 : 512;
				sw.WriteLine("	Align	" + align.ToString() + "		; Doit être un multiple de " + align.ToString());
				sw.WriteLine("Fonte:");
				if (BitmapCpc.modeVirtuel == 7) {
					int[] ordreAdr = new int[4] { 0, 1, 3, 2 };
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
				else
					sw.WriteLine("	DS	" + align.ToString() + Environment.NewLine);
			}
			sw.WriteLine("buffer" + (force8000 ? "	equ	#8000" : ":"));
		}

		static public void GenerePointeurs(StreamWriter sw, int nbImages, int[] bank, int[] speed, bool gest128K) {
			if (gest128K) {
				sw.WriteLine("	ORG EndBank0");
				sw.WriteLine("	Write Direct -1,-1,#C0");
			}
			sw.WriteLine("AnimDelta:");
			for (int i = 0; i < nbImages; i++) {
				sw.WriteLine("\tDW\tDelta" + i.ToString());
				if (speed != null)
					sw.WriteLine("\tDB\t#" + Convert.ToInt32(Math.Max(1, Math.Min(255, speed[i] / 3.333333))).ToString("X2"));

				if (gest128K)
					sw.WriteLine("\tDB\t#" + bank[i].ToString("X2"));
			}
			sw.WriteLine("\tDW\t#0");
		}

		// #### A revoir...
		static public void GenereAfficheStd(StreamWriter sw, ImageCpc img, int mode, int[] palette, bool overscan, Main.PackMethode methode) {
			sw.WriteLine("	DI");
			if (BitmapCpc.cpcPlus)
				GenereInitPlus(sw);
			else
				GenereInitOld(sw);

			GenereFormatEcran(sw);
			sw.WriteLine("	LD	HL,ImageCmp");
			sw.WriteLine("	LD	DE,#" + (overscan ? "0200" : "C000"));
			sw.WriteLine("	CALL	DepkLzw");
			GenereTspace(sw, false);

			sw.WriteLine("	LD	BC,#BC0C");
			sw.WriteLine("	LD	A,#30");
			sw.WriteLine("	OUT	(C),C");
			sw.WriteLine("	INC	B");
			sw.WriteLine("	OUT	(C),A");
			sw.WriteLine("	EI");
			sw.WriteLine("	RET");

			if (methode == Main.PackMethode.Standard)
				GenereDepack(sw);
			else
				GenereDZ80(sw);

			if (BitmapCpc.cpcPlus)
				GenerePalettePlus(sw, img);
			else
				GenerePaletteOld(sw, img);
		}

		static public void GenereAfficheModeX(StreamWriter sw, int[,] colMode5, bool isOverscan, Main.PackMethode methode) {
			sw.WriteLine("	LD	HL,ImageCmp");
			sw.WriteLine("	LD	DE,#" + (isOverscan ? "0200" : "C000"));
			sw.WriteLine("	CALL	DepkLzw");
			sw.WriteLine("	DI");
			sw.WriteLine("	LD	HL,#C9FB");
			sw.WriteLine("	LD	(#38),HL");
			sw.WriteLine("	EI");
			sw.WriteLine("WaitVbl:");
			sw.WriteLine("	LD	B,#F5");
			sw.WriteLine("	IN	A,(C)");
			sw.WriteLine("	RRA");
			sw.WriteLine("	JR	NC,WaitVbl");
			sw.WriteLine("WaitEnd:");
			sw.WriteLine("	IN	A,(C)");
			sw.WriteLine("	RRA");
			sw.WriteLine("	JR	C,WaitEnd");
			sw.WriteLine("	LD	HL,Color01			; Initialiser les couleurs fixes de l'image (0 et 1)");
			sw.WriteLine("	LD	BC,#7F00");
			sw.WriteLine("	OUT	(C),C			; Sélection pen 0");
			sw.WriteLine("	OUTI				; Mise à jour couleur");
			sw.WriteLine("	INC	C");
			sw.WriteLine("	OUT	(C),C			; Sélection pen 1");
			sw.WriteLine("	OUTI				; Mise à jour couleur");
			if (isOverscan) {
				sw.WriteLine("	LD	BC,#BC0C");
				sw.WriteLine("	OUT	(C),C");
				sw.WriteLine("	INC	B");
				sw.WriteLine("	INC	C");
				sw.WriteLine("	OUT	(C),C");
			}
			sw.WriteLine("	HALT");
			sw.WriteLine("	HALT");
			sw.WriteLine("	HALT");
			sw.WriteLine("	HALT");
			sw.WriteLine("	HALT");
			sw.WriteLine("	HALT");
			sw.WriteLine("	DI");
			sw.WriteLine("	LD	BC," + (BitmapCpc.NbLig == 200 ? 634 : 268).ToString());
			sw.WriteLine("	DEC	BC");
			sw.WriteLine("	LD	A,B");
			sw.WriteLine("	OR	C");
			sw.WriteLine("	JR	NZ,$-3			; Attendre première ligne visible de l'écran");
			sw.WriteLine("Boucle:");
			sw.WriteLine("	LD	HL,ColorModeX		; Initaliser les couleurs variables");
			sw.WriteLine("	LD	DE," + BitmapCpc.NbLig + "			; par lignes de l'image (2 et 3)");
			sw.WriteLine("LoopLineX:");
			sw.WriteLine("	LD	BC,#7F02");
			sw.WriteLine("	OUT	(C),C			; Sélection pen 2");
			sw.WriteLine("	OUTI				; Mise à jour couleur");
			sw.WriteLine("	INC C");
			sw.WriteLine("	OUT	(C),C			; Sélection pen 3");
			sw.WriteLine("	OUTI				; Mise à jour couleur");
			sw.WriteLine("	LD	B,8");
			sw.WriteLine("	NEG");
			sw.WriteLine("	DJNZ	$");
			sw.WriteLine("	DEC	DE");
			sw.WriteLine("	LD	A,D");
			sw.WriteLine("	OR	E");
			sw.WriteLine("	JR	NZ,LoopLineX");

			sw.WriteLine("	LD	BC," + (BitmapCpc.NbLig == 200 ? 1022 : 364).ToString());
			sw.WriteLine("	DEC	BC");
			sw.WriteLine("	LD	A,B");
			sw.WriteLine("	OR	C");
			sw.WriteLine("	JR	NZ,$-3");
			sw.WriteLine("	CP	(HL)");
			if (BitmapCpc.NbLig == 200)
				sw.WriteLine("	CP	(HL)");

			sw.WriteLine("	JR	Boucle");

			// Code du décompacteur avant les datas
			if (methode == Main.PackMethode.Standard)
				GenereDepack(sw);
			else
				GenereDZ80(sw);

			sw.WriteLine("Color01:");
			sw.WriteLine("	DB	'" + BitmapCpc.CpcVGA[colMode5[0, 0]].ToString() + BitmapCpc.CpcVGA[colMode5[0, 1]].ToString() + "'");
			sw.WriteLine("ColorModeX:");
			string line = "\tDB\t'";
			int nbOctets = 0;
			for (int y = 0; y < 272; y++) {
				line += BitmapCpc.CpcVGA[colMode5[y, 2]].ToString() + BitmapCpc.CpcVGA[colMode5[y, 3]].ToString();
				if (++nbOctets >= 16) {
					sw.WriteLine(line + "'");
					line = "\tDB\t'";
					nbOctets = 0;
				}
			}
			if (nbOctets > 0)
				sw.WriteLine(line + "'");
		}

		static public void GenereAfficheModeEgx(StreamWriter sw, int[] palette, bool overscan, Main.PackMethode methode) {
			sw.WriteLine("	LD	HL,Palette");
			sw.WriteLine("	LD	B,(HL)");
			sw.WriteLine("	LD	C,B");
			sw.WriteLine("	CALL	#BC38");
			sw.WriteLine("	XOR	A");
			sw.WriteLine("	LD	HL,Palette");
			sw.WriteLine("SetPalette:");
			sw.WriteLine("	LD	B,(HL)");
			sw.WriteLine("	LD	C,B");
			sw.WriteLine("	PUSH	AF");
			sw.WriteLine("	PUSH	HL");
			sw.WriteLine("	CALL	#BC32");
			sw.WriteLine("	POP	HL");
			sw.WriteLine("	POP	AF");
			sw.WriteLine("	INC	HL");
			sw.WriteLine("	INC	A");
			sw.WriteLine("	CP	#10");
			sw.WriteLine("	JR	NZ,SetPalette");

			GenereFormatEcran(sw);
			sw.WriteLine("	LD	HL,ImageCmp");
			sw.WriteLine("	LD	DE,#" + (overscan ? "0200" : "C000"));
			sw.WriteLine("	CALL	DepkLzw");

			sw.WriteLine("	DI");
			sw.WriteLine("WaitVbl:");
			sw.WriteLine("	LD	B,#F5");
			sw.WriteLine("	IN	A,(C)");
			sw.WriteLine("	RRA");
			sw.WriteLine("	JR	NC,WaitVbl");
			sw.WriteLine("WaitEnd:");
			sw.WriteLine("	IN	A,(C)");
			sw.WriteLine("	RRA");
			sw.WriteLine("	JR	C,WaitEnd");
			sw.WriteLine("	LD	HL,#012F");
			sw.WriteLine("	LD	DE,#8C01");
			sw.WriteLine("SetMode:");
			sw.WriteLine("	LD	B,#7F");
			sw.WriteLine("	OUT	(C),D			; Premier mode pour les lignes paires");
			sw.WriteLine("	LD	A,D");
			sw.WriteLine("	XOR	E				; Inversion du mode pour la prochaine ligne");
			sw.WriteLine("	LD	D,A");
			sw.WriteLine("	LD	B,11");
			sw.WriteLine("WaitNextLine:");
			sw.WriteLine("	DJNZ	WaitNextLine");
			sw.WriteLine("	BIT	0,(HL)			; + 4 Nops");
			sw.WriteLine("	DEC	HL");
			sw.WriteLine("	LD	A,H");
			sw.WriteLine("	OR	L");
			sw.WriteLine("	JR	NZ,SetMode");
			sw.WriteLine("	CALL	TstSpace");
			sw.WriteLine("	JR	Z,WaitVbl");

			sw.WriteLine("	LD	BC,#BC0C");
			sw.WriteLine("	LD	A,#30");
			sw.WriteLine("	OUT	(C),C");
			sw.WriteLine("	INC	B");
			sw.WriteLine("	OUT	(C),A");
			sw.WriteLine("	EI");
			sw.WriteLine("	RET");

			GenereTspace(sw, false);
			if (methode == Main.PackMethode.Standard)
				GenereDepack(sw);
			else
				GenereDZ80(sw);

			string line = "\tDB\t";
			for (int y = 0; y < 16; y++)
				line += palette[y].ToString() + ",";

			sw.WriteLine("Palette:");
			sw.WriteLine(line.Substring(0, line.Length - 1));
		}

		static public void CloseAsm(StreamWriter sw) {
			sw.Close();
			sw.Dispose();
		}
	}
}
