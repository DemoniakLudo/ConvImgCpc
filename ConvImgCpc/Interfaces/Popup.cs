using System.Diagnostics;
using System.Windows.Forms;

namespace ConvImgCpc {
	public partial class Popup : Form {
		public Popup(string label, string link) {
			InitializeComponent();
			label1.Text = label;
			linkLabel1.Text = link;
		}

		private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
			Process.Start(linkLabel1.Text);
		}

		private void bpOk_Click(object sender, System.EventArgs e) {
			Close();
		}
	}
}
