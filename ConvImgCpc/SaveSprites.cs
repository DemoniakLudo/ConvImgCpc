using System;
using System.Windows.Forms;

namespace ConvImgCpc {
	public partial class SaveSprites : Form {
		public bool saveSpritesOk = false;
		public string LabelSpr { get { return txbLabelSpr.Text; } }
		public string LabelPtr { get { return txbLabelPtr.Text; } }
		public string LabelPal { get { return txbLabelPalette.Text; } }
		public bool ZeroPtr { get { return chkZeroPtr.Checked; } }

		public SaveSprites(string fileName, bool withPtr) {
			InitializeComponent();
			txbLabelPtr.Enabled = chkZeroPtr.Visible = withPtr;
			txbLabelSpr.Text = fileName;
			txbLabelPalette.Text = "Palette" + fileName;
			if (withPtr)
				txbLabelPtr.Text = fileName + "Ptr";
		}

		private void bpOk_Click(object sender, EventArgs e) {
			saveSpritesOk = true;
			Close();
		}
	}
}
