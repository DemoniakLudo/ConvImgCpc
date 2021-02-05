namespace ConvImgCpc {
	partial class Animation {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			this.lblNumImage = new System.Windows.Forms.Label();
			this.lblMaxImage = new System.Windows.Forms.Label();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.pictureBox2 = new System.Windows.Forms.PictureBox();
			this.pictureBox3 = new System.Windows.Forms.PictureBox();
			this.pictureBox4 = new System.Windows.Forms.PictureBox();
			this.pictureBox5 = new System.Windows.Forms.PictureBox();
			this.hScrollBar1 = new System.Windows.Forms.HScrollBar();
			this.bpSup1 = new System.Windows.Forms.Button();
			this.bpSup2 = new System.Windows.Forms.Button();
			this.bpSup3 = new System.Windows.Forms.Button();
			this.bpSup4 = new System.Windows.Forms.Button();
			this.bpSup5 = new System.Windows.Forms.Button();
			this.numImage = new System.Windows.Forms.NumericUpDown();
			this.rbSource = new System.Windows.Forms.RadioButton();
			this.rvCalculee = new System.Windows.Forms.RadioButton();
			this.bpSaveGif = new System.Windows.Forms.Button();
			this.txbTps1 = new System.Windows.Forms.TextBox();
			this.txbTps2 = new System.Windows.Forms.TextBox();
			this.txbTps3 = new System.Windows.Forms.TextBox();
			this.txbTps4 = new System.Windows.Forms.TextBox();
			this.txbTps5 = new System.Windows.Forms.TextBox();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numImage)).BeginInit();
			this.SuspendLayout();
			// 
			// lblNumImage
			// 
			this.lblNumImage.AutoSize = true;
			this.lblNumImage.Location = new System.Drawing.Point(5, 6);
			this.lblNumImage.Name = "lblNumImage";
			this.lblNumImage.Size = new System.Drawing.Size(51, 13);
			this.lblNumImage.TabIndex = 53;
			this.lblNumImage.Text = "N° Image";
			this.lblNumImage.Visible = false;
			// 
			// lblMaxImage
			// 
			this.lblMaxImage.AutoSize = true;
			this.lblMaxImage.Location = new System.Drawing.Point(127, 5);
			this.lblMaxImage.Name = "lblMaxImage";
			this.lblMaxImage.Size = new System.Drawing.Size(60, 13);
			this.lblMaxImage.TabIndex = 53;
			this.lblMaxImage.Text = "maxImages";
			this.lblMaxImage.Visible = false;
			// 
			// pictureBox1
			// 
			this.pictureBox1.Location = new System.Drawing.Point(2, 29);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(142, 100);
			this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.pictureBox1.TabIndex = 60;
			this.pictureBox1.TabStop = false;
			this.pictureBox1.Tag = "0";
			this.pictureBox1.Click += new System.EventHandler(this.pictureBox_Click);
			// 
			// pictureBox2
			// 
			this.pictureBox2.Location = new System.Drawing.Point(145, 29);
			this.pictureBox2.Name = "pictureBox2";
			this.pictureBox2.Size = new System.Drawing.Size(142, 100);
			this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.pictureBox2.TabIndex = 61;
			this.pictureBox2.TabStop = false;
			this.pictureBox2.Tag = "1";
			this.pictureBox2.Click += new System.EventHandler(this.pictureBox_Click);
			// 
			// pictureBox3
			// 
			this.pictureBox3.Location = new System.Drawing.Point(288, 29);
			this.pictureBox3.Name = "pictureBox3";
			this.pictureBox3.Size = new System.Drawing.Size(142, 100);
			this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.pictureBox3.TabIndex = 62;
			this.pictureBox3.TabStop = false;
			this.pictureBox3.Tag = "2";
			this.pictureBox3.Click += new System.EventHandler(this.pictureBox_Click);
			// 
			// pictureBox4
			// 
			this.pictureBox4.Location = new System.Drawing.Point(431, 29);
			this.pictureBox4.Name = "pictureBox4";
			this.pictureBox4.Size = new System.Drawing.Size(142, 100);
			this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.pictureBox4.TabIndex = 63;
			this.pictureBox4.TabStop = false;
			this.pictureBox4.Tag = "3";
			this.pictureBox4.Click += new System.EventHandler(this.pictureBox_Click);
			// 
			// pictureBox5
			// 
			this.pictureBox5.Location = new System.Drawing.Point(574, 29);
			this.pictureBox5.Name = "pictureBox5";
			this.pictureBox5.Size = new System.Drawing.Size(142, 100);
			this.pictureBox5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.pictureBox5.TabIndex = 64;
			this.pictureBox5.TabStop = false;
			this.pictureBox5.Tag = "4";
			this.pictureBox5.Click += new System.EventHandler(this.pictureBox_Click);
			// 
			// hScrollBar1
			// 
			this.hScrollBar1.LargeChange = 1;
			this.hScrollBar1.Location = new System.Drawing.Point(2, 201);
			this.hScrollBar1.Maximum = 5;
			this.hScrollBar1.Name = "hScrollBar1";
			this.hScrollBar1.Size = new System.Drawing.Size(714, 20);
			this.hScrollBar1.TabIndex = 65;
			this.hScrollBar1.Visible = false;
			this.hScrollBar1.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollBar1_Scroll);
			// 
			// bpSup1
			// 
			this.bpSup1.Location = new System.Drawing.Point(36, 176);
			this.bpSup1.Name = "bpSup1";
			this.bpSup1.Size = new System.Drawing.Size(75, 22);
			this.bpSup1.TabIndex = 66;
			this.bpSup1.Tag = "0";
			this.bpSup1.UseVisualStyleBackColor = true;
			this.bpSup1.Visible = false;
			this.bpSup1.Click += new System.EventHandler(this.bpSup_Click);
			// 
			// bpSup2
			// 
			this.bpSup2.Location = new System.Drawing.Point(179, 176);
			this.bpSup2.Name = "bpSup2";
			this.bpSup2.Size = new System.Drawing.Size(75, 22);
			this.bpSup2.TabIndex = 66;
			this.bpSup2.Tag = "1";
			this.bpSup2.UseVisualStyleBackColor = true;
			this.bpSup2.Visible = false;
			this.bpSup2.Click += new System.EventHandler(this.bpSup_Click);
			// 
			// bpSup3
			// 
			this.bpSup3.Location = new System.Drawing.Point(322, 176);
			this.bpSup3.Name = "bpSup3";
			this.bpSup3.Size = new System.Drawing.Size(75, 22);
			this.bpSup3.TabIndex = 66;
			this.bpSup3.Tag = "2";
			this.bpSup3.UseVisualStyleBackColor = true;
			this.bpSup3.Visible = false;
			this.bpSup3.Click += new System.EventHandler(this.bpSup_Click);
			// 
			// bpSup4
			// 
			this.bpSup4.Location = new System.Drawing.Point(465, 176);
			this.bpSup4.Name = "bpSup4";
			this.bpSup4.Size = new System.Drawing.Size(75, 22);
			this.bpSup4.TabIndex = 66;
			this.bpSup4.Tag = "3";
			this.bpSup4.UseVisualStyleBackColor = true;
			this.bpSup4.Visible = false;
			this.bpSup4.Click += new System.EventHandler(this.bpSup_Click);
			// 
			// bpSup5
			// 
			this.bpSup5.Location = new System.Drawing.Point(608, 176);
			this.bpSup5.Name = "bpSup5";
			this.bpSup5.Size = new System.Drawing.Size(75, 22);
			this.bpSup5.TabIndex = 66;
			this.bpSup5.Tag = "4";
			this.bpSup5.UseVisualStyleBackColor = true;
			this.bpSup5.Visible = false;
			this.bpSup5.Click += new System.EventHandler(this.bpSup_Click);
			// 
			// numImage
			// 
			this.numImage.Location = new System.Drawing.Point(62, 2);
			this.numImage.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.numImage.Name = "numImage";
			this.numImage.Size = new System.Drawing.Size(59, 20);
			this.numImage.TabIndex = 54;
			this.numImage.Visible = false;
			this.numImage.ValueChanged += new System.EventHandler(this.numImage_ValueChanged);
			// 
			// rbSource
			// 
			this.rbSource.AutoSize = true;
			this.rbSource.Checked = true;
			this.rbSource.Location = new System.Drawing.Point(277, 3);
			this.rbSource.Name = "rbSource";
			this.rbSource.Size = new System.Drawing.Size(132, 17);
			this.rbSource.TabIndex = 67;
			this.rbSource.TabStop = true;
			this.rbSource.UseVisualStyleBackColor = true;
			this.rbSource.CheckedChanged += new System.EventHandler(this.rbSource_CheckedChanged);
			// 
			// rvCalculee
			// 
			this.rvCalculee.AutoSize = true;
			this.rvCalculee.Location = new System.Drawing.Point(420, 4);
			this.rvCalculee.Name = "rvCalculee";
			this.rvCalculee.Size = new System.Drawing.Size(145, 17);
			this.rvCalculee.TabIndex = 68;
			this.rvCalculee.UseVisualStyleBackColor = true;
			this.rvCalculee.CheckedChanged += new System.EventHandler(this.rvCalculee_CheckedChanged);
			// 
			// bpSaveGif
			// 
			this.bpSaveGif.Location = new System.Drawing.Point(596, 3);
			this.bpSaveGif.Name = "bpSaveGif";
			this.bpSaveGif.Size = new System.Drawing.Size(120, 23);
			this.bpSaveGif.TabIndex = 69;
			this.bpSaveGif.UseVisualStyleBackColor = true;
			this.bpSaveGif.Visible = false;
			this.bpSaveGif.Click += new System.EventHandler(this.bpSaveGif_Click);
			// 
			// txbTps1
			// 
			this.txbTps1.Location = new System.Drawing.Point(36, 135);
			this.txbTps1.Name = "txbTps1";
			this.txbTps1.Size = new System.Drawing.Size(75, 20);
			this.txbTps1.TabIndex = 70;
			this.txbTps1.Visible = false;
			this.txbTps1.Tag = "0";
			this.txbTps1.TextChanged += new System.EventHandler(this.txbTps_TextChanged);
			// 
			// txbTps2
			// 
			this.txbTps2.Location = new System.Drawing.Point(179, 135);
			this.txbTps2.Name = "txbTps2";
			this.txbTps2.Size = new System.Drawing.Size(75, 20);
			this.txbTps2.TabIndex = 70;
			this.txbTps2.Visible = false;
			this.txbTps2.Tag = "1";
			this.txbTps2.TextChanged += new System.EventHandler(this.txbTps_TextChanged);
			// 
			// txbTps3
			// 
			this.txbTps3.Location = new System.Drawing.Point(322, 135);
			this.txbTps3.Name = "txbTps3";
			this.txbTps3.Size = new System.Drawing.Size(75, 20);
			this.txbTps3.TabIndex = 70;
			this.txbTps3.Visible = false;
			this.txbTps3.Tag = "2";
			this.txbTps3.TextChanged += new System.EventHandler(this.txbTps_TextChanged);
			// 
			// txbTps4
			// 
			this.txbTps4.Location = new System.Drawing.Point(465, 135);
			this.txbTps4.Name = "txbTps4";
			this.txbTps4.Size = new System.Drawing.Size(75, 20);
			this.txbTps4.TabIndex = 70;
			this.txbTps4.Visible = false;
			this.txbTps4.Tag = "3";
			this.txbTps4.TextChanged += new System.EventHandler(this.txbTps_TextChanged);
			// 
			// txbTps5
			// 
			this.txbTps5.Location = new System.Drawing.Point(608, 135);
			this.txbTps5.Name = "txbTps5";
			this.txbTps5.Size = new System.Drawing.Size(75, 20);
			this.txbTps5.TabIndex = 70;
			this.txbTps5.Visible = false;
			this.txbTps5.Tag = "4";
			this.txbTps5.TextChanged += new System.EventHandler(this.txbTps_TextChanged);
			// 
			// Animation
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(718, 225);
			this.ControlBox = false;
			this.Controls.Add(this.txbTps5);
			this.Controls.Add(this.txbTps4);
			this.Controls.Add(this.txbTps3);
			this.Controls.Add(this.txbTps2);
			this.Controls.Add(this.txbTps1);
			this.Controls.Add(this.bpSaveGif);
			this.Controls.Add(this.rvCalculee);
			this.Controls.Add(this.rbSource);
			this.Controls.Add(this.lblNumImage);
			this.Controls.Add(this.numImage);
			this.Controls.Add(this.lblMaxImage);
			this.Controls.Add(this.bpSup5);
			this.Controls.Add(this.bpSup4);
			this.Controls.Add(this.bpSup3);
			this.Controls.Add(this.bpSup2);
			this.Controls.Add(this.bpSup1);
			this.Controls.Add(this.hScrollBar1);
			this.Controls.Add(this.pictureBox5);
			this.Controls.Add(this.pictureBox4);
			this.Controls.Add(this.pictureBox3);
			this.Controls.Add(this.pictureBox2);
			this.Controls.Add(this.pictureBox1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "Animation";
			this.ShowIcon = false;
			this.Text = "Image / animation";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Animation_FormClosing);
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numImage)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label lblNumImage;
		private System.Windows.Forms.NumericUpDown numImage;
		private System.Windows.Forms.Label lblMaxImage;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.PictureBox pictureBox2;
		private System.Windows.Forms.PictureBox pictureBox3;
		private System.Windows.Forms.PictureBox pictureBox4;
		private System.Windows.Forms.PictureBox pictureBox5;
		private System.Windows.Forms.HScrollBar hScrollBar1;
		private System.Windows.Forms.Button bpSup1;
		private System.Windows.Forms.Button bpSup2;
		private System.Windows.Forms.Button bpSup3;
		private System.Windows.Forms.Button bpSup4;
		private System.Windows.Forms.Button bpSup5;
		private System.Windows.Forms.RadioButton rbSource;
		private System.Windows.Forms.RadioButton rvCalculee;
		private System.Windows.Forms.Button bpSaveGif;
		private System.Windows.Forms.TextBox txbTps1;
		private System.Windows.Forms.TextBox txbTps2;
		private System.Windows.Forms.TextBox txbTps3;
		private System.Windows.Forms.TextBox txbTps4;
		private System.Windows.Forms.TextBox txbTps5;
	}
}