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
		public byte seuilR1 = 43;
		public byte seuilR2 = 168;
		public byte seuilV1 = 38;
		public byte seuilV2 = 175;
		public byte seuilB1 = 26;
		public byte seuilB2 = 125;
		public int coefR = 9798;
		public int coefV = 19235;
		public int coefB = 3735;
		public string lastReadPath = null;
		public string lastSavePath = null;
	}
}
