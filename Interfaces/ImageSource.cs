using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;

namespace ConvImgCpc {
	public partial class ImageSource : Form {
		private Bitmap[] tabImage;
		public Bitmap GetImage { get { return (Bitmap)pictureBox.Image; } }
		private int nbImg;
		public int NbImg { get { return nbImg; } }

		public ImageSource() {
			InitializeComponent();
		}

		private void ClearAll() {
			for (int n = 0; n < nbImg; n++)
				tabImage[n].Dispose();
		}

		public void InitBitmap(Bitmap b) {
			ClearAll();
			tabImage = new Bitmap[1];
			tabImage[0] = new Bitmap(b);
		}

		public void InitBitmap(MemoryStream imageStream) {
			ClearAll();
			Image selImage = new Bitmap(imageStream);
			FrameDimension dimension = new FrameDimension(selImage.FrameDimensionsList[0]);
			nbImg = selImage.GetFrameCount(dimension);
			tabImage = new Bitmap[nbImg];
			for (int n = 0; n < nbImg; n++) {
				selImage.SelectActiveFrame(dimension, n);
				tabImage[n] = new Bitmap(selImage);
			}
		}

		public void InitBitmap(int nbImg) {
			ClearAll();
			this.nbImg = nbImg;
			tabImage = new Bitmap[nbImg];
			for (int n = 0; n < nbImg; n++) 
				tabImage[n] = new Bitmap(768, 544);
		}

		public void ImportBitmap(Bitmap bmp, int numImage) {
			tabImage[numImage] = new Bitmap(bmp);
		}

		public void SelectBitmap(int num, bool visible) {
			Bitmap b = tabImage[num];
			pictureBox.Image = b;
			int x = Width = b.Width;
			int y = Height = b.Height;
			Visible = false;
			// Resize "auto" de la fenêtre
			while ((pictureBox.Width < b.Width || pictureBox.Height < b.Height) && x - 1 > Width && y - 1 > Height) {
				if (pictureBox.Width < b.Width)
					Width = ++x;

				if (pictureBox.Height < b.Height)
					Height = ++y;

				if (x > Screen.PrimaryScreen.Bounds.Width || y > Screen.PrimaryScreen.Bounds.Height)
					break;
			}
			Visible = visible;
		}

		public void Render() {
			pictureBox.Refresh();
		}
	}
}
