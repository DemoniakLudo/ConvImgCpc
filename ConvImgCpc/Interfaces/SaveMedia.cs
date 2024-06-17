using System;
using System.Windows.Forms;

namespace ConvImgCpc {
	public partial class SaveMedia : Form {
		public bool saveMediaOk = false;
		public string LabelMedia { get { return chkLabelMedia.Checked ? txbLabelMedia.Text : ""; } }
		public string LabelPal { get { return chkLabelPalette.Checked ? txbLabelPalette.Text : ""; } }
		public string LabelPtr { get { return chkLabelPtr.Checked ? txbLabelPtr.Text : ""; } }
		public bool ZeroPtr { get { return chkZeroPtr.Checked; } }

		public SaveMedia(string typeMedia, string fileName, bool withPalette, bool withPtr = false) {
			InitializeComponent();
			Text = "Save " + typeMedia + " (asm format)";
			chkLabelMedia.Text = typeMedia + " Label";
			txbLabelMedia.Text = fileName;

			chkLabelPalette.Text = typeMedia + " Palette Label";
			txbLabelPalette.Text = "Palette" + fileName;
			txbLabelPalette.Visible = chkLabelPalette.Visible = withPalette;

			chkLabelPtr.Text = typeMedia + " Pointer Label";
			txbLabelPtr.Text = fileName + "Ptr";
			txbLabelPtr.Visible = chkZeroPtr.Visible = chkLabelPtr.Visible = withPtr;
		}

		private void bpOk_Click(object sender, EventArgs e) {
			saveMediaOk = true;
			Close();
		}

		private void ChkLabelMedia_CheckedChanged(object sender, EventArgs e) {
			txbLabelMedia.Enabled = chkLabelMedia.Checked;
		}

		private void ChkLabelPalette_CheckedChanged(object sender, EventArgs e) {
			txbLabelPalette.Enabled = chkLabelPalette.Checked;
		}

		private void ChkLabelPtr_CheckedChanged(object sender, EventArgs e) {
			txbLabelPtr.Enabled = chkLabelPtr.Checked;
		}
	}
}
