using System;

namespace ConvImgCpc {
	[Serializable]
	public class Raster {
		public int[] line = new int[272];
		public int minLine = 0, maxLine = 0;
	}
}
