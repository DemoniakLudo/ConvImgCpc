namespace ConvImgCpc {
	[System.Serializable]
	public class Param {
		public enum SizeMode { Fit, KeepSmaller, KeepLarger, UserSize, Origin };

		public SizeMode sMode;
		public int sizex, sizey, posx, posy; // Taille et position en mode 'UserSize'
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
		// Chemins de lecture et sauvegardes
		public string lastReadPath = null;
		public string lastSavePath = null;
		// Constantes pour conversion RVB CPC
		public int coefR = 9798;
		public int coefV = 19235;
		public int coefB = 3735;
		// Seuils pour conversion sur 3 niveaux RVB (palette 3^3=27 couleurs)
		public int cstR1 = 85;
		public int cstR2 = 170;
		public int cstR3 = 255;
		public int cstR4 = 340;
		public int cstV1 = 85;
		public int cstV2 = 170;
		public int cstV3 = 255;
		public int cstV4 = 340;
		public int cstB1 = 85;
		public int cstB2 = 170;
		public int cstB3 = 255;
		public int cstB4 = 340;
		public bool newReduc;
		public int bitsRVB = 24;
		public bool diffErr;
	}
}
