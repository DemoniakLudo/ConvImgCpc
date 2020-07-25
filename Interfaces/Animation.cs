using System;
using System.Windows.Forms;

namespace ConvImgCpc {
	public partial class Animation : Form {
		private Main main;

		public int SelImage { get { return (int)numImage.Value; } }
		public int MaxImage { get { return (int)numImage.Maximum; } }

		public Animation(Main m) {
			main = m;
			InitializeComponent();
		}

		public void SetNbImgs(int nbImg) {
			lblMaxImage.Text = "Nbre images:" + nbImg;
			lblMaxImage.Visible = lblNumImage.Visible = numImage.Visible = nbImg > 1;
			numImage.Maximum = nbImg - 1;
			numImage.Value = hScrollBar1.Value = 0;
			if (nbImg > 1)
				Show();
			else
				Hide();
		}

		public void DrawImages(int startImg) {
			PictureBox[] tabPb = new PictureBox[] { pictureBox1, pictureBox2, pictureBox3, pictureBox4, pictureBox5 };
			Button[] tabButton = new Button[] { bpSup1, bpSup2, bpSup3, bpSup4, bpSup5 };
			int endImg = Math.Min(startImg + 4, (int)numImage.Maximum);
			for (int i = startImg; i <= endImg; i++) {
				tabPb[i - startImg].Image = main.imgSrc.GetBitmap(i);
				tabPb[i - startImg].Refresh();
				tabButton[i - startImg].Visible = startImg + i > 0;
			}
			for (int i = endImg - startImg + 1; i < 5; i++) {
				tabPb[i].Image = null;
				tabPb[i].Refresh();
				tabButton[i].Visible = false;
			}
			hScrollBar1.Visible = numImage.Maximum > 5;
			hScrollBar1.Maximum = (int)(numImage.Maximum);
			Application.DoEvents();
		}

		private void numImage_ValueChanged(object sender, EventArgs e) {
			main.SelectImage((int)numImage.Value);
			hScrollBar1.Value = (int)numImage.Value;
			main.imgCpc.SetImgCopy();
			main.Convert(false);
		}

		private void hScrollBar1_Scroll(object sender, ScrollEventArgs e) {
			numImage.Value = hScrollBar1.Value;
			//SelectImage(hScrollBar1.Value);
		}

		private void bpSup_Click(object sender, EventArgs e) {
			int num = (int)numImage.Value;
			int index = System.Convert.ToInt32(((Button)sender).Tag) + num;
			main.imgSrc.DeleteImage(index);
			numImage.Maximum = main.imgSrc.NbImg - 1;
			main.SetInfo("Suppression image " + index);
			lblMaxImage.Text = "Nbre images:" + main.imgSrc.NbImg;
			main.SelectImage(num > main.imgSrc.NbImg - 1 ? main.imgSrc.NbImg - 1 : num);
		}
	}
}
