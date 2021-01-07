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
			this.lockAllPal = new System.Windows.Forms.CheckBox();
			this.modeEdition = new System.Windows.Forms.CheckBox();
			this.hScrollBar = new System.Windows.Forms.HScrollBar();
			this.vScrollBar = new System.Windows.Forms.VScrollBar();
			this.drawColor = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.tailleCrayon = new System.Windows.Forms.ComboBox();
			this.grpEdition = new System.Windows.Forms.GroupBox();
			this.lblInfoPos = new System.Windows.Forms.Label();
			this.rbCopy = new System.Windows.Forms.RadioButton();
			this.undrawColor = new System.Windows.Forms.Label();
			this.lblZoom = new System.Windows.Forms.Label();
			this.rbZoom = new System.Windows.Forms.RadioButton();
			this.rbDraw = new System.Windows.Forms.RadioButton();
			this.chkDoRedo = new System.Windows.Forms.CheckBox();
			this.bpRedo = new System.Windows.Forms.Button();
			this.bpUndo = new System.Windows.Forms.Button();
			this.chkRendu = new System.Windows.Forms.CheckBox();
			this.pictureBox = new System.Windows.Forms.PictureBox();
			this.modeCaptureSprites = new System.Windows.Forms.CheckBox();
			this.bpCopyPal = new System.Windows.Forms.Button();
			this.grpEdition.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
			this.SuspendLayout();
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
			this.hScrollBar.Visible = false;
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
			this.vScrollBar.Visible = false;
			this.vScrollBar.Scroll += new System.Windows.Forms.ScrollEventHandler(this.vScrollBar_Scroll);
			// 
			// drawColor
			// 
			this.drawColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.drawColor.Location = new System.Drawing.Point(6, 243);
			this.drawColor.Name = "drawColor";
			this.drawColor.Size = new System.Drawing.Size(70, 70);
			this.drawColor.TabIndex = 7;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(17, 217);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(84, 13);
			this.label3.TabIndex = 4;
			this.label3.Text = "Couleur crayon :";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(7, 182);
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
			this.tailleCrayon.Location = new System.Drawing.Point(96, 179);
			this.tailleCrayon.Name = "tailleCrayon";
			this.tailleCrayon.Size = new System.Drawing.Size(31, 21);
			this.tailleCrayon.TabIndex = 3;
			this.tailleCrayon.SelectedIndexChanged += new System.EventHandler(this.tailleCrayon_SelectedIndexChanged);
			// 
			// grpEdition
			// 
			this.grpEdition.Controls.Add(this.lblInfoPos);
			this.grpEdition.Controls.Add(this.rbCopy);
			this.grpEdition.Controls.Add(this.undrawColor);
			this.grpEdition.Controls.Add(this.lblZoom);
			this.grpEdition.Controls.Add(this.rbZoom);
			this.grpEdition.Controls.Add(this.rbDraw);
			this.grpEdition.Controls.Add(this.chkDoRedo);
			this.grpEdition.Controls.Add(this.bpRedo);
			this.grpEdition.Controls.Add(this.bpUndo);
			this.grpEdition.Controls.Add(this.chkRendu);
			this.grpEdition.Controls.Add(this.drawColor);
			this.grpEdition.Controls.Add(this.label3);
			this.grpEdition.Controls.Add(this.label2);
			this.grpEdition.Controls.Add(this.tailleCrayon);
			this.grpEdition.Location = new System.Drawing.Point(790, 35);
			this.grpEdition.Name = "grpEdition";
			this.grpEdition.Size = new System.Drawing.Size(149, 497);
			this.grpEdition.TabIndex = 9;
			this.grpEdition.TabStop = false;
			this.grpEdition.Visible = false;
			// 
			// lblInfoPos
			// 
			this.lblInfoPos.AutoSize = true;
			this.lblInfoPos.Location = new System.Drawing.Point(7, 151);
			this.lblInfoPos.Name = "lblInfoPos";
			this.lblInfoPos.Size = new System.Drawing.Size(0, 13);
			this.lblInfoPos.TabIndex = 17;
			// 
			// rbCopy
			// 
			this.rbCopy.Image = global::ConvImgCpc.Properties.Resources.Copy;
			this.rbCopy.Location = new System.Drawing.Point(10, 103);
			this.rbCopy.Name = "rbCopy";
			this.rbCopy.Size = new System.Drawing.Size(48, 32);
			this.rbCopy.TabIndex = 16;
			this.rbCopy.UseVisualStyleBackColor = true;
			this.rbCopy.CheckedChanged += new System.EventHandler(this.rbCopy_CheckedChanged);
			// 
			// undrawColor
			// 
			this.undrawColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.undrawColor.Location = new System.Drawing.Point(6, 325);
			this.undrawColor.Name = "undrawColor";
			this.undrawColor.Size = new System.Drawing.Size(70, 70);
			this.undrawColor.TabIndex = 15;
			// 
			// lblZoom
			// 
			this.lblZoom.AutoSize = true;
			this.lblZoom.Location = new System.Drawing.Point(65, 75);
			this.lblZoom.Name = "lblZoom";
			this.lblZoom.Size = new System.Drawing.Size(22, 13);
			this.lblZoom.TabIndex = 14;
			this.lblZoom.Text = "1:1";
			// 
			// rbZoom
			// 
			this.rbZoom.Image = global::ConvImgCpc.Properties.Resources.Zoom;
			this.rbZoom.Location = new System.Drawing.Point(10, 69);
			this.rbZoom.Name = "rbZoom";
			this.rbZoom.Size = new System.Drawing.Size(48, 32);
			this.rbZoom.TabIndex = 13;
			this.rbZoom.UseVisualStyleBackColor = true;
			this.rbZoom.CheckedChanged += new System.EventHandler(this.rbZoom_CheckedChanged);
			// 
			// rbDraw
			// 
			this.rbDraw.Checked = true;
			this.rbDraw.Image = global::ConvImgCpc.Properties.Resources.Draw;
			this.rbDraw.Location = new System.Drawing.Point(10, 35);
			this.rbDraw.Name = "rbDraw";
			this.rbDraw.Size = new System.Drawing.Size(48, 32);
			this.rbDraw.TabIndex = 12;
			this.rbDraw.TabStop = true;
			this.rbDraw.UseVisualStyleBackColor = true;
			this.rbDraw.CheckedChanged += new System.EventHandler(this.rbDraw_CheckedChanged);
			// 
			// chkDoRedo
			// 
			this.chkDoRedo.AutoSize = true;
			this.chkDoRedo.Location = new System.Drawing.Point(19, 460);
			this.chkDoRedo.Name = "chkDoRedo";
			this.chkDoRedo.Size = new System.Drawing.Size(108, 30);
			this.chkDoRedo.TabIndex = 11;
			this.chkDoRedo.Text = "Garder retouches\naprès recalcul";
			this.chkDoRedo.UseVisualStyleBackColor = true;
			// 
			// bpRedo
			// 
			this.bpRedo.Enabled = false;
			this.bpRedo.Location = new System.Drawing.Point(36, 431);
			this.bpRedo.Name = "bpRedo";
			this.bpRedo.Size = new System.Drawing.Size(75, 23);
			this.bpRedo.TabIndex = 10;
			this.bpRedo.Text = "Redo";
			this.bpRedo.UseVisualStyleBackColor = true;
			this.bpRedo.Click += new System.EventHandler(this.bpRedo_Click);
			// 
			// bpUndo
			// 
			this.bpUndo.Enabled = false;
			this.bpUndo.Location = new System.Drawing.Point(36, 402);
			this.bpUndo.Name = "bpUndo";
			this.bpUndo.Size = new System.Drawing.Size(75, 23);
			this.bpUndo.TabIndex = 10;
			this.bpUndo.Text = "Undo";
			this.bpUndo.UseVisualStyleBackColor = true;
			this.bpUndo.Click += new System.EventHandler(this.bpUndo_Click);
			// 
			// chkRendu
			// 
			this.chkRendu.AutoSize = true;
			this.chkRendu.Location = new System.Drawing.Point(6, 10);
			this.chkRendu.Name = "chkRendu";
			this.chkRendu.Size = new System.Drawing.Size(107, 17);
			this.chkRendu.TabIndex = 9;
			this.chkRendu.Text = "Fenêtre de rendu";
			this.chkRendu.UseVisualStyleBackColor = true;
			this.chkRendu.CheckedChanged += new System.EventHandler(this.chkRendu_CheckedChanged);
			// 
			// pictureBox
			// 
			this.pictureBox.Location = new System.Drawing.Point(0, 0);
			this.pictureBox.Name = "pictureBox";
			this.pictureBox.Size = new System.Drawing.Size(768, 544);
			this.pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.pictureBox.TabIndex = 0;
			this.pictureBox.TabStop = false;
			this.pictureBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TrtMouseMove);
			this.pictureBox.MouseLeave += new System.EventHandler(this.pictureBox_MouseLeave);
			this.pictureBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.TrtMouseMove);
			// 
			// modeCaptureSprites
			// 
			this.modeCaptureSprites.AutoSize = true;
			this.modeCaptureSprites.Location = new System.Drawing.Point(796, 12);
			this.modeCaptureSprites.Name = "modeCaptureSprites";
			this.modeCaptureSprites.Size = new System.Drawing.Size(111, 17);
			this.modeCaptureSprites.TabIndex = 10;
			this.modeCaptureSprites.Text = "Capture de sprites";
			this.modeCaptureSprites.UseVisualStyleBackColor = true;
			this.modeCaptureSprites.Visible = false;
			this.modeCaptureSprites.CheckedChanged += new System.EventHandler(this.modeCaptureSprites_CheckedChanged);
			// 
			// bpCopyPal
			// 
			this.bpCopyPal.Location = new System.Drawing.Point(800, 547);
			this.bpCopyPal.Name = "bpCopyPal";
			this.bpCopyPal.Size = new System.Drawing.Size(107, 49);
			this.bpCopyPal.TabIndex = 11;
			this.bpCopyPal.Text = "Copier palette dans presse-papier";
			this.bpCopyPal.UseVisualStyleBackColor = true;
			this.bpCopyPal.Click += new System.EventHandler(this.bpCopyPal_Click);
			// 
			// ImageCpc
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(942, 620);
			this.ControlBox = false;
			this.Controls.Add(this.bpCopyPal);
			this.Controls.Add(this.modeCaptureSprites);
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
			this.ShowIcon = false;
			this.Text = "Image CPC";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ImageCpc_FormClosing);
			this.grpEdition.ResumeLayout(false);
			this.grpEdition.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.PictureBox pictureBox;
		private System.Windows.Forms.CheckBox lockAllPal;
		private System.Windows.Forms.CheckBox modeEdition;
		private System.Windows.Forms.HScrollBar hScrollBar;
		private System.Windows.Forms.VScrollBar vScrollBar;
		private System.Windows.Forms.Label drawColor;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ComboBox tailleCrayon;
		private System.Windows.Forms.GroupBox grpEdition;
		private System.Windows.Forms.CheckBox chkRendu;
		private System.Windows.Forms.Button bpRedo;
		private System.Windows.Forms.Button bpUndo;
		private System.Windows.Forms.CheckBox chkDoRedo;
		private System.Windows.Forms.RadioButton rbDraw;
		private System.Windows.Forms.RadioButton rbZoom;
		private System.Windows.Forms.Label lblZoom;
		private System.Windows.Forms.Label undrawColor;
		private System.Windows.Forms.RadioButton rbCopy;
		private System.Windows.Forms.Label lblInfoPos;
		private System.Windows.Forms.CheckBox modeCaptureSprites;
		private System.Windows.Forms.Button bpCopyPal;

	}
}