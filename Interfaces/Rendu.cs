using System.Drawing;
using System.Windows.Forms;

namespace ConvImgCpc {
	public partial class Rendu : Form {
		public PictureBox Picture { get { return pictureBox; } }

		public Rendu(Bitmap b) {
			InitializeComponent();
			pictureBox.Image = b;
		}

		private void chkX2_CheckedChanged(object sender, System.EventArgs e) {
			if (chkX2.Checked) {
				this.Width = 1593;
				this.Height = 1127;
				pictureBox.Width = 1536;
				pictureBox.Height = 1088;
			}
			else {
				this.Width = 825;
				this.Height = 583;
				pictureBox.Width = 768;
				pictureBox.Height = 544;
			}
			pictureBox.Refresh();
		}
	}
}
