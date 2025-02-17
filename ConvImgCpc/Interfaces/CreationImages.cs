using System;
using System.Windows.Forms;

namespace ConvImgCpc {
	public partial class CreationImages : Form {
		private int ret = -1;
		public int NbImages { get { return ret; } }

		public CreationImages(Main m) {
			InitializeComponent();
			m.ChangeLanguage(Controls, "CreationImages");
		}

		private void BpCreer_Click(object sender, EventArgs e) {
			ret = (int)nbImages.Value;
			Close();
		}
	}
}
