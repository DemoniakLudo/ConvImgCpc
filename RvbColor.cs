namespace ConvImgCpc {
	public class RvbColor {
		public byte r, v, b;

		public RvbColor(byte compR, byte compV, byte compB) {
			r = compR;
			v = compV;
			b = compB;
		}

		public RvbColor(int value) {
			r = (byte)(value >> 16);
			v = (byte)(value >> 8);
			b = (byte)value;
		}

		public int GetColor { get { return b + (v << 8) + (r << 16); } }
		public int GetColorArgb { get { return b + (v << 8) + (r << 16) + (255 << 24); } }
	}
}
