using System.Drawing;
using System.Windows.Forms;

namespace ConvImgCpc {
	public partial class Rendu : Form {
		public PictureBox Picture { get { return pictureBox; } }

		public Rendu(Bitmap b) {
			InitializeComponent();
			pictureBox.Image = b;
		}
	}
}
