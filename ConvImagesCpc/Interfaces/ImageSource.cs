using ConvImgCpc;
using System.Drawing;
using System.Windows.Forms;

namespace CpcConvImg {
	public partial class ImageSource: Form {
		private LockBitmap bmpLock;

		public int GetWidth { get { return bmpLock.Width; } }
		public int GetHeight { get { return bmpLock.Height; } }
		public Bitmap GetImage { get { return (Bitmap)pictureBox.Image; } }

		public ImageSource() {
			InitializeComponent();
		}

		public void SetBitmap(Bitmap bmp, bool visible) {
			pictureBox.Image = bmp;
			int x = Width = bmp.Width;
			int y = Height = bmp.Height;
			Visible = false;
			bmpLock = new LockBitmap(bmp);
			bmpLock.LockBits();
			// Resize "auto" de la fenêtre
			while ((pictureBox.Width < bmp.Width || pictureBox.Height < bmp.Height) && x - 1 > Width && y - 1 > Height) {
				if (pictureBox.Width < bmp.Width)
					Width = ++x;

				if (pictureBox.Height < bmp.Height)
					Height = ++y;
			}
			bmpLock.UnlockBits();
			Visible = visible;
		}

		public void Render() {
			pictureBox.Refresh();
		}
	}
}
