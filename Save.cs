using System;
using System.IO;

namespace ConvImgCpc {
	public class Save {
		static public StreamWriter OpenAsm(string fileName, string version, Param param = null) {
			StreamWriter sw = File.CreateText(fileName);
			sw.WriteLine("; Généré par ConvImgCpc" + version.Replace('\n', ' '));
			if (param != null) {
				sw.WriteLine("; mode écran " + param.modeVirtuel);
				sw.WriteLine("; Taille (nbColsxNbLignes) " + param.nbCols.ToString() + "x" + param.nbLignes.ToString());
			}
			return sw;
		}

		static public void SauveAssembleur(StreamWriter sw, byte[] tabByte, int length, Param param) {
			string line = "\tDB\t";
			int nbOctets = 0;
			for (int i = 0; i < length; i++) {
				line += "#" + tabByte[i].ToString("X2") + ",";
				if (++nbOctets >= Math.Min(16, param.nbCols)) {
					sw.WriteLine(line.Substring(0, line.Length - 1));
					line = "\tDB\t";
					nbOctets = 0;
				}
			}
			if (nbOctets > 0)
				sw.WriteLine(line.Substring(0, line.Length - 1));
		}

		static public void CloseAsm(StreamWriter sw) {
			sw.Close();
			sw.Dispose();
		}
	}
}
