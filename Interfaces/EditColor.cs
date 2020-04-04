using System.Drawing;
using System.Windows.Forms;

namespace ConvImgCpc {
	public partial class EditColor: Form {
		private Label[] colors = new Label[27];
		private Label lblValColor = new Label();
		private TextBox[] tabVal = new TextBox[3];
		private TrackBar[] tabTrack = new TrackBar[3];
		private Label[] tabLabel = new Label[3];
		private int valColor;
		public int ValColor { get { return valColor; } }
		public bool isValide;

		public EditColor(int numColor, int val, int rgbColor, bool cpcPlus) {
			InitializeComponent();
			selColor.BackColor = Color.FromArgb(rgbColor);
			lblNumColor.Text = "Couleur " + numColor;
			if (cpcPlus) {
				for (int i = 0; i < 3; i++) {
					tabLabel[i] = new Label();
					tabVal[i] = new TextBox();
					tabTrack[i] = new TrackBar();
					tabLabel[i].Location = new Point(277, 8 + 40 * i);
					tabVal[i].Location = new Point(298, 5 + 40 * i);
					tabTrack[i].Location = new Point(331, 5 + 40 * i);
					tabLabel[i].AutoSize = true;
					tabVal[i].Size = new Size(27, 20);
					tabTrack[i].Size = new Size(104, 42);
					tabVal[i].TextChanged += val_TextChanged;
					tabTrack[i].Scroll += track_Scroll;
					tabTrack[i].Maximum = 15;
					tabVal[i].Tag = tabTrack[i].Tag = i;
					tabLabel[i].Text = "RVB".Substring(i, 1);
					tabVal[i].Text = (((rgbColor >> (2 - i) * 8) & 0xFF) / 17).ToString();
					Controls.Add(tabLabel[i]);
					Controls.Add(tabTrack[i]);
					Controls.Add(tabVal[i]);
				}
			}
			else {
				valColor = val;
				int i = 0;
				for (int y = 0; y < 3; y++)
					for (int x = 0; x < 9; x++) {
						colors[i] = new Label();
						colors[i].BorderStyle = BorderStyle.FixedSingle;
						colors[i].Location = new Point(4 + x * 48, 80 + y * 40);
						colors[i].Size = new Size(40, 32);
						colors[i].Tag = i;
						colors[i].BackColor = Color.FromArgb(ImageCpc.RgbCPC[i].GetColorArgb);
						colors[i].Click += ClickColor;
						colors[i].DoubleClick += DblClickColor;
						Controls.Add(colors[i++]);
					}
				lblValColor.AutoSize = true;
				lblValColor.Location = new Point(271, 34);
				lblValColor.Text = "=" + val;
				Controls.Add(lblValColor);
			}
		}

		private void ClickColor(object sender, System.EventArgs e) {
			Label colorClick = sender as Label;
			valColor = colorClick.Tag != null ? (int)colorClick.Tag : 0;
			lblValColor.Text = "=" + valColor;
			selColor.BackColor = colorClick.BackColor;
		}

		private void DblClickColor(object sender, System.EventArgs e) {
			ClickColor(sender, e);
			bpValide_Click(sender, e);
		}

		private void NewColor() {
			try {
				int r = int.Parse(tabVal[0].Text);
				int v = int.Parse(tabVal[1].Text);
				int b = int.Parse(tabVal[2].Text);
				valColor = (v << 8) + (b << 4) + r;
				selColor.BackColor = Color.FromArgb(r * 17, v * 17, b * 17);
			}
			catch { }
		}

		private void track_Scroll(object sender, System.EventArgs e) {
			int i = (int)((TrackBar)sender).Tag;
			tabVal[i].Text = tabTrack[i].Value.ToString();
		}

		private void val_TextChanged(object sender, System.EventArgs e) {
			try {
				int i = (int)((TextBox)sender).Tag;
				tabTrack[i].Value = int.Parse(tabVal[i].Text);
				NewColor();
			}
			catch { }
		}

		private void bpValide_Click(object sender, System.EventArgs e) {
			isValide = true;
			Close();
		}

		private void bpAnnule_Click(object sender, System.EventArgs e) {
			Close();
		}
	}
}
