using System;
using System.IO;

namespace ConvImgCpc {
	static public class SauveImage {
		/*
		Conversion palette OCP+ :

		static byte CpcVGA[ 28 ] = "TDU\\X]LEMVFW^@_NGORBSZY[JCK";

												W^@_NGORBSZY[JCK = 
												FEDCBA9876543210
		(3 premiers octets /12 de la palette = composante R,B,V)
		*/

		static byte[] CodeStd = new byte[36] {  // Routine à mettre en #C7D0
			0x3A, 0xD0, 0xD7,               //      LD      A,  (#D7D0)
			0xCD, 0x1C, 0xBD,               //      CALL    #BD1C
			0x21, 0xD1, 0xD7,               //      LD      HL, #D7D1
			0x46,                           //      LD      B,  (HL)
			0x48,                           //      LD      C,  B
			0xCD, 0x38, 0xBC,               //      CALL    #BC38
			0xAF,                           //      XOR     A
			0x21, 0xD1, 0xD7,               //      LD      HL, #D7D1
			0x46,                           // BCL: LD      B,  (HL)
			0x48,                           //      LD      C,  B
			0xF5,                           //      PUSH    AF
			0xE5,                           //      PUSH    HL
			0xCD, 0x32, 0xBC,               //      CALL    #BC32
			0xE1,                           //      POP     HL
			0xF1,                           //      POP     AF
			0x23,                           //      INC     HL
			0x3C,                           //      INC     A
			0xFE, 0x10,                     //      CP      #10
			0x20, 0xF1,                     //      JR      NZ,BCL
			0xC3, 0x18, 0xBB,               //      JP      #BB18
			};

		static byte[] CodeP0 = new byte[47] {
			0xF3,						//				DI
			0x01, 0x11, 0xBC,			//				LD		BC,#BC11
			0x21, 0xD0, 0xDF,			//				LD		HL,#DFD0
			0x7E,						//	BCL1:		LD		A,(HL)
			0xED, 0x79,					//				OUT		(C),A
			0x23,						//				INC		HL
			0x0D,						//				DEC		C
			0x20, 0xF9,					//				JR		NZ,BCL1
			0x01, 0xA0, 0x7F,			//				LD		BC,#7FA0
			0x3A, 0xD0, 0xD7,			//				LD		A,(#D7D0)
			0xED, 0x79,					//				OUT		(C),A
			0xED, 0x49,					//				OUT		(C),C
			0x01, 0xB8, 0x7F,			//				LD		BC,#7FB8
			0xED, 0x49,					//				OUT		(C),C
			0x21, 0xD1, 0xD7,			//				LD		HL,#D7D1
			0x11, 0x00, 0x64,			//				LD		DE,#6400
			0x01, 0x22, 0x00,			//				LD		BC,#0022
			0xED, 0xB0,					//				LDIR
			0xCD, 0xD0, 0xCF,			//	BCL2:		CALL	WaitKey
			0x38, 0xFB,					//				JR		C,BCL2
			0xFB,						//				EI
			0xC9						//				RET
			};

		static byte[] CodeP1 = new byte[39] {
			0x01, 0x0E, 0xF4,			//	WaitKey:	LD		BC,#F40E
			0xED, 0x49,					//				OUT		(C),C
			0x01, 0xC0, 0xF6,			//				LD		BC,#F6C0
			0xED, 0x49,					//				OUT		(C),C
			0xAF,						//				XOR		A
			0xED, 0x79,					//				OUT		(C),A
			0x01, 0x92, 0xF7,			//				LD		BC,#F792
			0xED, 0x49,					//				OUT		(C),C
			0x01, 0x45, 0xF6,			//				LD		BC,#F645
			0xED, 0x49,					//				OUT		(C),C
			0x06, 0xF4,					//				LD		B,#F4
			0xED, 0x78,					//				IN		A,(C)
			0x01, 0x82, 0xF7,			//				LD		BC,#F782
			0xED, 0x49,					//				OUT		(C),C
			0x01, 0x00, 0xF6,			//				LD		BC,#F600
			0xED, 0x49,					//				OUT		(C),C
			0x17,						//				RLA
			0xC9						//				RET
			};

		// Unlock ASIC
		static byte[] CodeP3 = new byte[17] { 0xFF, 0x00, 0xFF, 0x77, 0xB3, 0x51, 0xA8, 0xD4, 0x62, 0x39, 0x9C, 0x46, 0x2B, 0x15, 0x8A, 0xCD, 0xEE };

		static byte[] CodeOv = new byte[83] {
			0x21, 0x47, 0x08,			//				LD		HL,#847
			0xCD, 0x36, 0x08,			//				CALL	SetRegs
			0x3A, 0x00, 0x08,			//				LD		A,(#0800)
			0xCD, 0x1C, 0xBD,			//				CALL	#BD1C
			0x21, 0x01, 0x08,			//				LD		HL,#0801
			0xAF,						//				XOR		A
			0x4E,						//	SetPal:		LD		C,(HL)
			0x41,						//				LD		B,C
			0xF5,						//				PUSH	AF
			0xE5,						//				PUSH	HL
			0xCD, 0x32, 0xBC,			//				CALL	#BC32
			0xE1,						//				POP		HL
			0xF1,						//				POP		AF
			0x23,						//				INC		HL
			0x3C,						//				INC		A
			0xFE, 0x10,					//				CP		#10
			0x20, 0xF1,					//				JR		NZ,SetPal
			0xCD, 0x18, 0xBB,			//				CALL	#BB18
			0x21, 0x57, 0x08,			//				LD		HL,#0857 // Remise aux valeurs originales
			0x01, 0x00, 0xBC,			//	SetRegs:	LD		BC,#BC00
			0x7E,						//	SetRegs1:	LD		A,(HL)
			0xA7,						//				AND		A
			0xC8,						//				RET		Z
			0xED, 0x79,					//				OUT		(C),A
			0x04,						//				INC		B
			0x23,						//				INC		HL
			0x7E,						//				LD		A,(HL)
			0xED, 0x79,					//				OUC		(C),A
			0x23,						//				INC		HL
			0x05,						//				DEC		B
			0x18, 0xF2,					//				JR		SetRegs1
			0x01, 0x30, 0x02, 0x32, 0x03, 0x89, 0x06, 0x22,
			0x07, 0x23, 0x0C, 0x0D, 0x0D, 0x00, 0x00, 0x00,
			0x01, 0x28, 0x02, 0x2E, 0x03, 0x8E, 0x06, 0x19,
			0x07, 0x1E, 0x0C, 0x30, 0x00
			};

		static byte[] CodeOvP = new byte[142] {
			0xF3,						//				DI
			0x01, 0x11, 0xBC,			//				LD		BC,#BC11
			0x21, 0x86, 0x08,			//				LD		HL,#0886
			0x04,						//	SetAsic:	INC		B
			0xED, 0xA3,					//				OUTI
			0x0D,						//				DEC		C
			0x20, 0xFA,					//				JR		NZ,SetAsic
			0x21, 0x97, 0x08,			//				LD		HL,#897
			0xCD, 0x75, 0x08,			//				CALL	SetReg
			0x01, 0xB8, 0x7F,			//				LD		BC,#7FB8
			0x3A, 0x00, 0x08,			//				LD		A,(#0800)
			0xED, 0x49,					//				OUT		(C),C
			0xED, 0x79,					//				OUT		(C),A
			0x21, 0x01, 0x08,			//				LD		HL,#0801
			0x11, 0x00, 0x64,			//				LD		DE,#6400
			0x01, 0x20, 0x00,			//				LD		BC,#0020
			0xED, 0xB0,					//				LDIR
			0xAF,						//	WaitKey:	XOR		A
			0x01, 0x0E, 0xF4,			//				LD		BC,#F40E
			0xED, 0x49,					//				OUT		(C),C
			0x01, 0xC0, 0xF6,			//				LD		BC,#F6C0
			0xED, 0x49,					//				OUT		(C),C
			0xED, 0x79,					//				OUT		(C),A
			0x01, 0x92, 0xF7,			//				LD		BC,#F792
			0xED, 0x49,					//				OUT		(C),C
			0x01, 0x45, 0xF6,			//				LD		BC,#F645
			0xED, 0x49,					//				OUT		(C),C
			0x06, 0xF4,					//				LD		B,#F4
			0xED, 0x78,					//				IN		A,(C)
			0x01, 0x82, 0xF7,			//				LD		BC,#F782
			0xED, 0x49,					//				OUT		(C),C
			0x17,						//				RLA
			0x38, 0xDD,					//				JR		C,WaitKey
			0x01, 0xA0, 0x7F,			//				LD		BC,#7FA0
			0xED, 0x49,					//				OUT		(C),C
			0xFB,						//				EI
			0x21, 0xA5, 0x08,			//				LD		HL,#08A5
			0x01, 0x00, 0xBC,			//	SetReg:		LD		BC,#BC00
			0x7E,						//	SetReg1:	LD		A,(HL)
			0xA7,						//				AND		A
			0xC8,						//				RET		Z
			0xED, 0x79,					//				OUT		(C),A
			0x04,						//				INC		B
			0x23,						//				INC		HL
			0x7E,						//				LD		A,(HL)
			0xED, 0x79,					//				OUT		(C),A
			0x23,						//				INC		HL
			0x05,						//				DEC		B
			0x18, 0xF2,					//				JR		SetReg1
			0xFF, 0x00, 0xFF, 0x77, 0xB3, 0x51, 0xA8, 0xD4,
			0x62, 0x39, 0x9C, 0x46, 0x2B, 0x15, 0x8A, 0xCD,
			0xEE, 0x01, 0x30, 0x02, 0x32, 0x06, 0x22, 0x07,
			0x23, 0x0C, 0x0D, 0x0D, 0x00, 0x00, 0x00, 0x01,
			0x28, 0x02, 0x2E, 0x06, 0x19, 0x07, 0x1E, 0x0C,
			0x30
			};

		static byte[] ModePal = new byte[48];

		static char[] CpcVGA = new char[27] { 'T', 'D', 'U', '\\', 'X', ']', 'L', 'E', 'M', 'V', 'F', 'W', '^', '@', '_', 'N', 'G', 'O', 'R', 'B', 'S', 'Z', 'Y', '[', 'J', 'C', 'K' };

		static public int SauveEcran(string NomFic, ImageCpc bitmap, bool CpcPlus) {
			bool Overscan = (bitmap.NbLig * bitmap.NbCol > 0x3F00);
			bool WithCode = true; // ###
			if (CpcPlus) {
				ModePal[0] = (byte)(bitmap.ModeCPC | 0x8C);
				int k = 1;
				for (int i = 0; i < 16; i++) {
					ModePal[k++] = (byte)((bitmap.Palette[i] >> 4) | (bitmap.Palette[i] << 4));
					ModePal[k++] = (byte)(bitmap.Palette[i] >> 8);
				}
			}
			else {
				ModePal[0] = (byte)bitmap.ModeCPC;
				for (int i = 0; i < 16; i++)
					if (bitmap.ModeCPC < 3)
						ModePal[1 + i] = (byte)bitmap.Palette[i];
					else
						ModePal[1 + i] = (byte)CpcVGA[bitmap.Palette[i]];
			}

			byte[] imgCpc = bitmap.bmpCpc;
			if (WithCode) {
				if (!Overscan) {
					Buffer.BlockCopy(ModePal, 0, imgCpc, 0x17D0, ModePal.Length);
					if (CpcPlus) {
						Buffer.BlockCopy(CodeP0, 0, imgCpc, 0x07D0, CodeP0.Length);
						Buffer.BlockCopy(CodeP1, 0, imgCpc, 0x0FD0, CodeP1.Length);
						Buffer.BlockCopy(CodeP3, 0, imgCpc, 0x1FD0, CodeP3.Length);
					}
					else
						Buffer.BlockCopy(CodeStd, 0, imgCpc, 0x07D0, CodeStd.Length);
				}
				else {
					if (bitmap.NbLig == 272 && bitmap.NbCol == 96) {
						Buffer.BlockCopy(ModePal, 0, imgCpc, 0x600, ModePal.Length);
						if (CpcPlus)
							Buffer.BlockCopy(CodeOvP, 0, imgCpc, 0x621, CodeOvP.Length);
						else
							Buffer.BlockCopy(CodeOv, 0, imgCpc, 0x611, CodeOv.Length);
					}
				}
			}

			int Lg = bitmap.BitmapSize;
			BinaryWriter fp = new BinaryWriter(new FileStream(NomFic, FileMode.Create));
			CpcAmsdos entete = CpcSystem.CreeEntete(NomFic, (short)(Overscan ? 0x200 : 0xC000), (short)Lg, (short)(Overscan ? CpcPlus ? 0x821 : 0x811 : 0xC7D0));
			fp.Write(CpcSystem.AmsdosToByte(entete));
			fp.Write(bitmap.bmpCpc, 0, Lg);
			fp.Close();
			return (Lg);
		}
	}
}
