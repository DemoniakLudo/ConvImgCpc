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
		public bool motif;
		public bool motif2;
		public bool setPalCpc;
		public bool lissage;
		public int trackModeX;
		public int pctRed, pctGreen, pctBlue;
	}
}
