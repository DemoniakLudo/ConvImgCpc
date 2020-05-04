using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace ConvImgCpc {
	public partial class ImageSource: Form {
		private LockBitmap bmpLock;

		public Bitmap GetImage { get { return (Bitmap)pictureBox.Image; } }

		public ImageSource() {
			InitializeComponent();
		}

		public void SetBitmap(Bitmap bmp, bool visible) {
			if (pictureBox.Image != null)
				pictureBox.Image.Dispose();

			pictureBox.Image = bmp;
			pictureBox.Image.SelectActiveFrame(FrameDimension.Page, 0);
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

				if (x > Screen.PrimaryScreen.Bounds.Width|| y > Screen.PrimaryScreen.Bounds.Height)
					break;
			}
			bmpLock.UnlockBits();
			Visible = visible;
		}

		public void Render() {
			pictureBox.Refresh();
		}
	}
}
