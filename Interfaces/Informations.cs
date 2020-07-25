using System;
using System.Windows.Forms;

namespace ConvImgCpc {
	public partial class Informations : Form {
		public Informations() {
			InitializeComponent();
		}

		public void SetInfos(string txt) {
			listInfo.Items.Add(DateTime.Now.ToString() + " - " + txt);
			listInfo.SelectedIndex = listInfo.Items.Count - 1;
		}

		public void ClearInfos() {
			listInfo.Items.Clear();
		}
	}
}
