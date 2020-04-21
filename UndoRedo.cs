using System.Collections.Generic;
using System.Linq;

namespace ConvImgCpc {
	public class UndoRedo {
		List<MemoPoint> lstUndoRedo = new List<MemoPoint>();
		int numRedo = 0, maxNum = 0, numPt = 0;
		public int NumRedo { get { return numRedo; } }

		public bool CanUndo { get { return numRedo > 0; } }
		public bool CanRedo { get { return numRedo < maxNum; } }

		public void Reset() {
			numRedo = maxNum = 0;
			lstUndoRedo.Clear();
		}

		public void MemoUndoRedo(int x, int y, int o, int n, bool nextDraw) {
			if (!nextDraw)
				lstUndoRedo.RemoveAll(m => m.numRedo >= numRedo);

			// Pour éviter les doublons, vérifier dernier élément <> élément à ajouter
			MemoPoint mp = lstUndoRedo.Count > 1 ? lstUndoRedo[lstUndoRedo.Count - 1] : new MemoPoint(-1, -1, -1, -1, -1, -1);
			if (mp.posx != x || mp.posy != y || mp.newColor != n)
				lstUndoRedo.Add(new MemoPoint(x, y, o, n, numRedo, numPt++));
		}

		public void EndUndoRedo() {
			numRedo++;
			maxNum = numRedo;
		}

		public List<MemoPoint> Undo() {
			List<MemoPoint> lstUndo = null;
			if (CanUndo) {
				numRedo--;
				lstUndo = lstUndoRedo.Where(m => m.numRedo == numRedo).ToList();
			}
			return lstUndo;
		}

		public List<MemoPoint> Redo() {
			List<MemoPoint> lstRedo = null;
			if (CanRedo) {
				lstRedo = lstUndoRedo.Where(m => m.numRedo == numRedo).ToList();
				numRedo++;
			}
			return lstRedo;
		}
	}

	public class MemoPoint {
		public int posx, posy, oldColor, newColor, numRedo, numPt;

		public MemoPoint(int x, int y, int o, int n, int r, int pt) {
			posx = x;
			posy = y;
			oldColor = o;
			newColor = n;
			numRedo = r;
			numPt = pt;
		}
	}
}
