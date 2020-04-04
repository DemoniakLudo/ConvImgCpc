using System.Drawing;
using System.Windows.Forms;

namespace ConvImgCpc {
	public partial class EditImage: Form {
		public delegate void EventInt(int p);
		public delegate void EventNoParam();
		public event EventNoParam evtClose;
		public event EventInt evtChangeZoom, evtChangePen;

		public EditImage() {
			InitializeComponent();
			zoom.SelectedIndex = 0;
			tailleCrayon.SelectedIndex = 0;
		}

		private void zoom_SelectedIndexChanged(object sender, System.EventArgs e) {
			if (evtChangeZoom != null)
				evtChangeZoom((int.Parse(zoom.SelectedItem.ToString())));
		}

		private void crayon_SelectedIndexChanged(object sender, System.EventArgs e) {
			if (evtChangePen != null)
				evtChangePen((int.Parse(tailleCrayon.SelectedItem.ToString())));
		}

		private void EditImage_FormClosed(object sender, FormClosedEventArgs e) {
			if (evtClose != null)
				evtClose();
		}

		public void SetPenColor(RvbColor col) {
			crayonColor.BackColor = Color.FromArgb(col.blue, col.green, col.red);
		}
	}
}
