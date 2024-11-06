using System;
using System.Collections.Generic;

namespace ConvImgCpc {
	[Serializable]
	public class SplitEcran {
		private List<LigneSplit> lignesSplit = new List<LigneSplit>();
		public List<LigneSplit> LignesSplit {
			get { return lignesSplit; }
			set { lignesSplit = value; }
		}

		public SplitEcran() {
		}

		public LigneSplit GetLigne(int num) {
			return lignesSplit[num];
		}
	}

	[Serializable]
	public class LigneSplit {
		private List<Split> listeSplit = new List<Split>();
		public List<Split> ListeSplit {
			get { return listeSplit; }
			set { listeSplit = value; }
		}

		public int numPen = 0;
		public int retard = 0;
		public int newMode = 0;
		public bool changeMode = false;

		public LigneSplit() {
		}

		public Split GetSplit(int num) {
			if (num >= listeSplit.Count)
				listeSplit.Add(new Split());

			return listeSplit[num];
		}
	}

	[Serializable]
	public class Split {
		public int longueur = 32;
		public int couleur = 0;
		public bool enable = false;

		public Split() {
		}
	}
}
