using System;
using System.Windows.Forms;

namespace ConvImgCpc {
	public partial class CreationImages : Form {
		private int ret = 0;
		public int NbImages { get { return ret; } }

		public CreationImages() {
			InitializeComponent();
		}

		private void bpCreer_Click(object sender, EventArgs e) {
			ret = (int)nbImages.Value;
			Close();
		}
	}
}
