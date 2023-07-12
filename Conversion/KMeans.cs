using System;

namespace ConvImgCpc {
	public class Niveau {
		public RvbColor couleurNiveau;
		private int nbR, nbV, nbB, nbPixels;

		public Niveau(byte r, byte v, byte b) {
			couleurNiveau = new RvbColor(r, v, b);
			Reset();
		}

		public void Reset() {
			nbR = nbV = nbB = nbPixels = 0;
		}

		public void AjouterCouleur(RvbColor coul) {
			nbR += coul.r;
			nbV += coul.v;
			nbB += coul.b;
			nbPixels++;
		}

		public void SetNiveauMoyen() {
			couleurNiveau.r = nbPixels > 0 ? (byte)(nbR / nbPixels) : (byte)0;
			couleurNiveau.v = nbPixels > 0 ? (byte)(nbV / nbPixels) : (byte)0;
			couleurNiveau.b = nbPixels > 0 ? (byte)(nbB / nbPixels) : (byte)0;
		}
	}
}