namespace ConvImgCpc {
	[System.Serializable]
	public class Param {
		public enum SizeMode { Fit, KeepSmaller, KeepLarger, UserSize, Origin };

		public SizeMode sMode;
		public string methode;
		public int pct;
		public int[] lockState = new int[16];
		public int pctLumi;
		public int pctSat;
		public int pctContrast;
		public bool cpcPlus;
		public bool newMethode;
		public bool reductPal1;
		public bool reductPal2;
		public bool reductPal3;
		public bool reductPal4;
		public bool sortPal;
		public int nbCols, nbLignes;
		public int modeVirtuel;
		public bool withCode;
		public bool withPalette;
		public bool setPalCpc;
		public bool lissage;
		public int trackModeX;
		public int pctRed, pctGreen, pctBlue;
		public bool trameTc;
		public byte seuilLumR1 = 43;
		public byte seuilLumR2 = 141;
		public byte seuilLumV1 = 38;
		public byte seuilLumV2 = 167;
		public byte seuilLumB1 = 26;
		public byte seuilLumB2 = 125;
		public int coefR = 9798;
		public int coefV = 19235;
		public int coefB = 3735;
		public string lastReadPath = null;
		public string lastSavePath = null;
	}
}
