using ConvImgCpc;
using System.Drawing;
using System.Windows.Forms;

namespace CpcConvImg {
	public partial class ImageCpc: Form {
		private Bitmap bmp;
		private LockBitmap bmpLock;
		public BitmapCpc bitmapCpc;
		private Label[] colors = new Label[16];
		private CheckBox[] lockColors = new CheckBox[16];
		public int[] lockState = new int[16];

		public ImageCpc() {
			InitializeComponent();
			bmp = new Bitmap(pictureBox.Width, pictureBox.Height);
			pictureBox.Image = bmp;
			bmpLock = new LockBitmap(bmp);
			bitmapCpc = new BitmapCpc(768, 544, 1); // ###
			for (int i = 0; i < 16; i++) {
				// Générer les contrôles de "couleurs"
				colors[i] = new Label();
				colors[i].BorderStyle = BorderStyle.FixedSingle;
				colors[i].Location = new Point(4 + i * 48, 550);
				colors[i].Size = new Size(40, 32);
				colors[i].Tag = i;
				colors[i].Click += ClickColor;
				Controls.Add(colors[i]);
				// Générer les contrôles de "bloquage couleur"
				lockColors[i] = new CheckBox();
				lockColors[i].Location = new Point(16 + i * 48, 586);
				lockColors[i].Size = new Size(20, 20);
				lockColors[i].Tag = i;
				lockColors[i].Click += ClickLock;
				Controls.Add(lockColors[i]);
				lockColors[i].Update();
			}
			Render();
		}

		// Click sur un "lock"
		void ClickLock(object sender, System.EventArgs e) {
			CheckBox colorLock = sender as CheckBox;
			int numLock = colorLock.Tag != null ? (int)colorLock.Tag : 0;
			lockState[numLock] = colorLock.Checked ? 1 : 0;
		}

		void ClickColor(object sender, System.EventArgs e) {
			Label colorClick = sender as Label;
			int numCol = colorClick.Tag != null ? (int)colorClick.Tag : 0;
			EditColor ed = new EditColor(numCol, bitmapCpc.Palette[numCol], bitmapCpc.GetPaletteColor(numCol).GetColorArgb, bitmapCpc.cpcPlus);
			ed.ShowDialog();
			if (ed.isValide) {
				bitmapCpc.SetPalette(numCol, ed.ValColor);
				UpdatePalette();
			}
		}

		public void Reset() {
			bmpLock.LockBits();
			for (int x = 0; x < bmpLock.Width; x++)
				for (int y = 0; y < bmpLock.Height; y++)
					bmpLock.SetPixel(x, y, 0);
			bmpLock.UnlockBits();
		}

		public void SetPixelCpc(int pixelX, int pixelY, int pix) {
			bitmapCpc.SetPixelCpc(pixelX, pixelY, pix);
		}

		public void UpdatePalette() {
			for (int i = 0; i < 16; i++) {
				colors[i].BackColor = System.Drawing.Color.FromArgb(bitmapCpc.GetPaletteColor(i).GetColorArgb);
				colors[i].Refresh();
			}
		}

		public void SetPalette(int entree, int valeur) {
			bitmapCpc.SetPalette(entree, valeur);
			UpdatePalette();
		}

		public void Render() {
			bitmapCpc.Render(bmpLock, bitmapCpc.ModeCPC, 1, 0, 0, false);
			pictureBox.Refresh();
			UpdatePalette();
		}

		private void lockAllPal_CheckedChanged(object sender, System.EventArgs e) {
			for (int i = 0; i < 16; i++) {
				lockColors[i].Checked = lockAllPal.Checked;
				lockState[i] = lockAllPal.Checked ? 1 : 0;
			}
		}
	}
}
