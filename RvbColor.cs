namespace ConvImgCpc {
	public class RvbColor {
		public byte r, v, b;

		public RvbColor(byte compR, byte compV, byte compB) {
			r = compR;
			v = compV;
			b = compB;
		}

		public RvbColor(int value) {
			r = (byte)value;
			v = (byte)(value >> 8);
			b = (byte)(value >> 16);
		}

		public int GetColor { get { return r + (v << 8) + (b << 16); } }
		public int GetColorArgb { get { return r + (v << 8) + (b << 16) + (255 << 24); } }
	}
}
