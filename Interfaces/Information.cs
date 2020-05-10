using System.Windows.Forms;

namespace ConvImgCpc {
	public partial class Information : Form {
		private Main main;
		public Information(Main m) {
			InitializeComponent();
			main = m;
		}


		public void Clear() {
			listInfo.Items.Clear();
		}

		public void AddInfo(string txt) {
			listInfo.Items.Add(txt);
			listInfo.SelectedIndex = listInfo.Items.Count - 1;
		}

		private void Information_FormClosed(object sender, FormClosedEventArgs e) {
			main.InfoClosed();
		}
	}
}
