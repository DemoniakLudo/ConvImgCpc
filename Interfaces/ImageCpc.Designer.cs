namespace ConvImgCpc {
	partial class ImageCpc {
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
			this.pictureBox = new System.Windows.Forms.PictureBox();
			this.lockAllPal = new System.Windows.Forms.CheckBox();
			this.modeEdition = new System.Windows.Forms.CheckBox();
			this.hScrollBar = new System.Windows.Forms.HScrollBar();
			this.vScrollBar = new System.Windows.Forms.VScrollBar();
			this.crayonColor = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.tailleCrayon = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.comboZoom = new System.Windows.Forms.ComboBox();
			this.grpEdition = new System.Windows.Forms.GroupBox();
			this.chkRendu = new System.Windows.Forms.CheckBox();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
			this.grpEdition.SuspendLayout();
			this.SuspendLayout();
			// 
			// pictureBox
			// 
			this.pictureBox.Location = new System.Drawing.Point(0, 0);
			this.pictureBox.Name = "pictureBox";
			this.pictureBox.Size = new System.Drawing.Size(768, 544);
			this.pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.pictureBox.TabIndex = 0;
			this.pictureBox.TabStop = false;
			this.pictureBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseDown);
			this.pictureBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseMove);
			// 
			// lockAllPal
			// 
			this.lockAllPal.AutoSize = true;
			this.lockAllPal.Location = new System.Drawing.Point(784, 602);
			this.lockAllPal.Name = "lockAllPal";
			this.lockAllPal.Size = new System.Drawing.Size(93, 17);
			this.lockAllPal.TabIndex = 1;
			this.lockAllPal.Text = "Tout vérouiller";
			this.lockAllPal.UseVisualStyleBackColor = true;
			this.lockAllPal.CheckedChanged += new System.EventHandler(this.lockAllPal_CheckedChanged);
			// 
			// modeEdition
			// 
			this.modeEdition.AutoSize = true;
			this.modeEdition.Location = new System.Drawing.Point(797, 12);
			this.modeEdition.Name = "modeEdition";
			this.modeEdition.Size = new System.Drawing.Size(84, 17);
			this.modeEdition.TabIndex = 2;
			this.modeEdition.Text = "Editer image";
			this.modeEdition.UseVisualStyleBackColor = true;
			this.modeEdition.CheckedChanged += new System.EventHandler(this.modeEdition_CheckedChanged);
			// 
			// hScrollBar
			// 
			this.hScrollBar.LargeChange = 32;
			this.hScrollBar.Location = new System.Drawing.Point(0, 547);
			this.hScrollBar.Name = "hScrollBar";
			this.hScrollBar.Size = new System.Drawing.Size(768, 16);
			this.hScrollBar.SmallChange = 8;
			this.hScrollBar.TabIndex = 1;
			this.hScrollBar.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollBar_Scroll);
			// 
			// vScrollBar
			// 
			this.vScrollBar.LargeChange = 32;
			this.vScrollBar.Location = new System.Drawing.Point(771, 0);
			this.vScrollBar.Name = "vScrollBar";
			this.vScrollBar.Size = new System.Drawing.Size(16, 544);
			this.vScrollBar.SmallChange = 8;
			this.vScrollBar.TabIndex = 0;
			this.vScrollBar.Scroll += new System.Windows.Forms.ScrollEventHandler(this.vScrollBar_Scroll);
			// 
			// crayonColor
			// 
			this.crayonColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.crayonColor.Location = new System.Drawing.Point(40, 121);
			this.crayonColor.Name = "crayonColor";
			this.crayonColor.Size = new System.Drawing.Size(100, 75);
			this.crayonColor.TabIndex = 7;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(47, 99);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(84, 13);
			this.label3.TabIndex = 4;
			this.label3.Text = "Couleur crayon :";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(14, 211);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(73, 13);
			this.label2.TabIndex = 5;
			this.label2.Text = "Taille crayon :";
			// 
			// tailleCrayon
			// 
			this.tailleCrayon.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.tailleCrayon.Enabled = false;
			this.tailleCrayon.FormattingEnabled = true;
			this.tailleCrayon.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "8"});
			this.tailleCrayon.Location = new System.Drawing.Point(93, 208);
			this.tailleCrayon.Name = "tailleCrayon";
			this.tailleCrayon.Size = new System.Drawing.Size(31, 21);
			this.tailleCrayon.TabIndex = 3;
			this.tailleCrayon.SelectedIndexChanged += new System.EventHandler(this.tailleCrayon_SelectedIndexChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(14, 34);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(40, 13);
			this.label1.TabIndex = 6;
			this.label1.Text = "Zoom :";
			// 
			// comboZoom
			// 
			this.comboZoom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboZoom.Enabled = false;
			this.comboZoom.FormattingEnabled = true;
			this.comboZoom.Items.AddRange(new object[] {
            "1",
            "2",
            "4",
            "8"});
			this.comboZoom.Location = new System.Drawing.Point(68, 31);
			this.comboZoom.Name = "comboZoom";
			this.comboZoom.Size = new System.Drawing.Size(33, 21);
			this.comboZoom.TabIndex = 8;
			this.comboZoom.SelectedIndexChanged += new System.EventHandler(this.comboZoom_SelectedIndexChanged);
			// 
			// grpEdition
			// 
			this.grpEdition.Controls.Add(this.chkRendu);
			this.grpEdition.Controls.Add(this.comboZoom);
			this.grpEdition.Controls.Add(this.crayonColor);
			this.grpEdition.Controls.Add(this.label3);
			this.grpEdition.Controls.Add(this.label2);
			this.grpEdition.Controls.Add(this.tailleCrayon);
			this.grpEdition.Controls.Add(this.label1);
			this.grpEdition.Location = new System.Drawing.Point(790, 35);
			this.grpEdition.Name = "grpEdition";
			this.grpEdition.Size = new System.Drawing.Size(174, 252);
			this.grpEdition.TabIndex = 9;
			this.grpEdition.TabStop = false;
			this.grpEdition.Visible = false;
			// 
			// chkRendu
			// 
			this.chkRendu.AutoSize = true;
			this.chkRendu.Location = new System.Drawing.Point(17, 67);
			this.chkRendu.Name = "chkRendu";
			this.chkRendu.Size = new System.Drawing.Size(107, 17);
			this.chkRendu.TabIndex = 9;
			this.chkRendu.Text = "Fenêtre de rendu";
			this.chkRendu.UseVisualStyleBackColor = true;
			this.chkRendu.CheckedChanged += new System.EventHandler(this.chkRendu_CheckedChanged);
			// 
			// ImageCpc
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(968, 620);
			this.ControlBox = false;
			this.Controls.Add(this.grpEdition);
			this.Controls.Add(this.vScrollBar);
			this.Controls.Add(this.hScrollBar);
			this.Controls.Add(this.modeEdition);
			this.Controls.Add(this.lockAllPal);
			this.Controls.Add(this.pictureBox);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ImageCpc";
			this.Text = "Image CPC";
			((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
			this.grpEdition.ResumeLayout(false);
			this.grpEdition.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.PictureBox pictureBox;
		private System.Windows.Forms.CheckBox lockAllPal;
		private System.Windows.Forms.CheckBox modeEdition;
		private System.Windows.Forms.HScrollBar hScrollBar;
		private System.Windows.Forms.VScrollBar vScrollBar;
		private System.Windows.Forms.Label crayonColor;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ComboBox tailleCrayon;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox comboZoom;
		private System.Windows.Forms.GroupBox grpEdition;
		private System.Windows.Forms.CheckBox chkRendu;

	}
}