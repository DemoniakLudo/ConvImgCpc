using System;
using System.Windows.Forms;

namespace ConvImgCpc {
	public partial class SaveMedia : Form {
		public bool saveMediaOk = false;
		public string LabelMedia { get { return chkLabelMedia.Checked ? txbLabelMedia.Text : ""; } }
		public string LabelPal { get { return chkLabelPalette.Checked ? txbLabelPalette.Text : ""; } }
		public string LabelPtr { get { return chkLabelPtr.Checked ? txbLabelPtr.Text : ""; } }
		public bool ZeroPtr { get { return chkZeroPtr.Checked; } }

		public SaveMedia(string typeMedia, string fileName, bool withPtr = false) {
			InitializeComponent();
			Text = "Save " + typeMedia + " (asm format)";
			chkLabelMedia.Text = typeMedia + " Label";
			chkLabelPtr.Text = typeMedia + " Pointer Label";
			chkLabelPalette.Text = typeMedia + " Palette Label";
			txbLabelPtr.Visible = chkZeroPtr.Visible = chkLabelPtr.Visible = withPtr;
			txbLabelMedia.Text = fileName;
			txbLabelPalette.Text = "Palette" + fileName;
			if (withPtr)
				txbLabelPtr.Text = fileName + "Ptr";
		}

		private void bpOk_Click(object sender, EventArgs e) {
			saveMediaOk = true;
			Close();
		}

		private void chkLabelMedia_CheckedChanged(object sender, EventArgs e) {
			txbLabelMedia.Enabled = chkLabelMedia.Checked;
		}

		private void chkLabelPalette_CheckedChanged(object sender, EventArgs e) {
			txbLabelPalette.Enabled = chkLabelPalette.Checked;
		}

		private void chkLabelPtr_CheckedChanged(object sender, EventArgs e) {
			txbLabelPtr.Enabled = chkLabelPtr.Checked;
		}
	}
}
