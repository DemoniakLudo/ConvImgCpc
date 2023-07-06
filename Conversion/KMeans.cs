using System;

namespace ConvImgCpc {
	public class KMeans {
		public enum Distance {
			DISTANCE_SUP = 0,
			DISTANCE_EUCLIDE = 1,
			DISTANCE_MANHATTAN = 2,
		};

		public Niveau[] niveaux;
		private Distance distance;
		private double seuil;
		private int nbCols;

		public KMeans(int c, Distance d, double s) {
			nbCols = c;
			distance = d;
			seuil = s;
		}

		public void Palettiser(DirectBitmap img) {
			niveaux = new Niveau[nbCols];
			for (int n = 0; n < nbCols; n++) {
				double gris = (n / (double)(nbCols - 1));
				niveaux[n] = new Niveau(gris, gris, gris);
			}

			int nbIterations = 0;
			bool continuer = true;
			while (continuer) {
				for (int n = 0; n < nbCols; n++)
					niveaux[n].Vider();

				for (int x = 0; x < img.Width; x++) {
					for (int y = 0; y < img.Height; y++) {
						RvbDouble coul = new RvbDouble(img.GetPixelColor(x, y));
						NiveauAdequat(coul).AjouterCouleur(coul);
					}
				}
				continuer = false;
				for (int n = 0; n < nbCols; n++) {
					double delta = calculerDistance(niveaux[n].couleurNiveau, niveaux[n].couleurMoyenne);
					niveaux[n].couleurNiveau = niveaux[n].couleurMoyenne;
					if (delta > seuil)
						continuer = true;
				}
				nbIterations++;
			}

			for (int x = 0; x < img.Width; x++)
				for (int y = 0; y < img.Height; y++)
					img.SetPixel(x, y, NiveauAdequat(new RvbDouble(img.GetPixelColor(x, y))).couleurNiveau.ToRvbColor().GetColorArgb);
		}

		double calculerDistance(RvbDouble coul1, RvbDouble coul2) {
			switch (distance) {
				case Distance.DISTANCE_EUCLIDE:
					return Math.Sqrt(Math.Pow(coul1.r - coul2.r, 2) + Math.Pow(coul1.v - coul2.v, 2) + Math.Pow(coul1.b - coul2.b, 2));

				case Distance.DISTANCE_SUP:
					return Math.Max(Math.Abs(coul1.r - coul2.r), Math.Max(Math.Abs(coul1.v - coul2.v), Math.Abs(coul1.b - coul2.b)));

				default:
					return Math.Abs(coul1.r - coul2.r) + Math.Abs(coul1.v - coul2.v) + Math.Abs(coul1.b - coul2.b);
			}
		}

		public Niveau NiveauAdequat(RvbDouble coul) {
			double distanceMinimale = 0;
			Niveau retNiveau = niveaux[0];
			bool first = true;
			for (int n = 0; n < niveaux.Length; n++) {
				double cetteDistance = calculerDistance(coul, niveaux[n].couleurNiveau);
				if (first || cetteDistance < distanceMinimale) {
					retNiveau = niveaux[n];
					distanceMinimale = cetteDistance;
					first = false;
				}
			}
			return retNiveau;
		}
	}

	public class RvbDouble {
		public double r, v, b;

		public RvbDouble(double _r, double _v, double _b) {
			SetRgb(_r, _v, _b);
		}

		public RvbDouble(RvbColor col) {
			r = col.r / 255.0;
			v = col.v / 255.0;
			b = col.b / 255.0;
		}

		public void SetRgb(double _r, double _v, double _b) {
			r = _r;
			v = _v;
			b = _b;
		}

		public RvbColor ToRvbColor() {
			return new RvbColor((byte)(255 * r), (byte)(255 * v), (byte)(255 * b));
		}
	};

	public class Niveau {
		public RvbDouble couleurNiveau, couleurMoyenne;
		private int nbPixels;

		public Niveau(double r, double v, double b) {
			couleurNiveau = new RvbDouble(r, v, b);
			couleurMoyenne = new RvbDouble(0, 0, 0);
		}

		public void Vider() {
			couleurMoyenne.SetRgb(0, 0, 0);
			nbPixels = 0;
		}

		public void AjouterCouleur(RvbDouble coul) {
			couleurMoyenne.r = (nbPixels * couleurMoyenne.r + coul.r) / (nbPixels + 1);
			couleurMoyenne.v = (nbPixels * couleurMoyenne.v + coul.v) / (nbPixels + 1);
			couleurMoyenne.b = (nbPixels * couleurMoyenne.b + coul.b) / (nbPixels + 1);
			nbPixels++;
		}
	};
}