using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;

namespace ConvImgCpc {
	public partial class Animation : Form {
		private Main main;
		private bool displaySrc = true;
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
				tabPb[i - startImg].Image = displaySrc ? main.imgSrc.GetBitmap(i) : main.imgCpc.bmpLock[i].Bitmap;
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

		private void pictureBox_Click(object sender, EventArgs e) {
			int num = (int)numImage.Value;
			int index = System.Convert.ToInt32(((PictureBox)sender).Tag) + num;
			if (index <= numImage.Maximum)
				numImage.Value = index;
		}

		private void rbSource_CheckedChanged(object sender, EventArgs e) {
			bpSaveGif.Visible = false;
			displaySrc = true;
			DrawImages(main.imgCpc.selImage);
		}

		private void rvCalculee_CheckedChanged(object sender, EventArgs e) {
			bpSaveGif.Visible = true;
			displaySrc = false;
			DrawImages(main.imgCpc.selImage);
		}

		private void bpSaveGif_Click(object sender, EventArgs e) {
			SaveFileDialog dlg = new SaveFileDialog();
			dlg.Filter = "Gif animé (*.gif)|*.gif";
			DialogResult result = dlg.ShowDialog();
			if (result == DialogResult.OK) {
				byte[] GifAnimation = { 33, 255, 11, 78, 69, 84, 83, 67, 65, 80, 69, 50, 46, 48, 3, 1, 0, 0, 0 };
				MemoryStream ms = new MemoryStream();
				BinaryWriter bWr = new BinaryWriter(new FileStream(dlg.FileName, FileMode.Create));
				Bitmap b = GetBitmap(main.imgCpc.bmpLock[0].Bitmap);
				b.Save(ms, ImageFormat.Gif);
				b.Dispose();
				byte[] tabByte = ms.ToArray();
				tabByte[10] = (byte)(tabByte[10] & 0X78); //No global color table
				bWr.Write(tabByte, 0, 13);
				bWr.Write(GifAnimation);
				WriteGifImg(tabByte, bWr);
				for (int i = 1; i <= numImage.Maximum; i++) {
					ms.SetLength(0);
					b = GetBitmap(main.imgCpc.bmpLock[i].Bitmap);
					b.Save(ms, ImageFormat.Gif);
					tabByte = ms.ToArray();
					WriteGifImg(tabByte, bWr);
					b.Dispose();
				}
				bWr.Write(tabByte[tabByte.Length - 1]);
				bWr.Close();
				ms.Dispose();
			}
		}


		private Bitmap GetBitmap(Bitmap image) {
			Bitmap bitmap = new Bitmap(BitmapCpc.TailleX, BitmapCpc.TailleY, PixelFormat.Format24bppRgb);
				using (Graphics g = Graphics.FromImage(bitmap)) {
					g.DrawImage(image, new Rectangle(0, 0, bitmap.Width, bitmap.Height), new Rectangle(0,0,bitmap.Width,bitmap.Height),GraphicsUnit.Pixel);
				}
			return bitmap;
		}

		private void WriteGifImg(byte[] tabByte, BinaryWriter bWr) {
			tabByte[785] = 25; //0.5 secs delay
			tabByte[786] = 0;
			tabByte[798] = (byte)(tabByte[798] | 0X87);
			bWr.Write(tabByte, 781, 18);
			bWr.Write(tabByte, 13, 768);
			bWr.Write(tabByte, 799, tabByte.Length - 800);
		}
	}
}
