namespace ConvImgCpc {
	public partial class BitmapBase {
		private const int maxColsCpc = 96;
		private const int maxLignesCpc = 272;

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

		static public byte[,,] trameM1 = new byte[16, 4, 4];

		static public byte[,,,] spritesHard = new byte[4, 16, 16, 16];
		static public int[] paletteSprite = new int[16];

		static public int modeVirtuel = 1;
		static public bool cpcPlus = false;
		static protected int nbCol = 80;
		static public int NbCol { get { return nbCol; } }
		static public int TailleX {
			get { return nbCol << 3; }
			set { nbCol = value >> 3; }
		}
		static protected int nbLig = 200;
		static public int NbLig { get { return nbLig; } }
		static public int TailleY {
			get { return nbLig << 1; }
			set { nbLig = value >> 1; }
		}
		static public int BitmapSize { get { return nbCol + GetAdrCpc((TailleY & 0x3F8) - 2); } }
		static public int yEgx = 0;

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
	}
}
