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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImageCpc));
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
			this.modeCaptureSprites = new System.Windows.Forms.CheckBox();
			this.bpCopyPal = new System.Windows.Forms.Button();
			this.pictureBox = new System.Windows.Forms.PictureBox();
			this.grpEdition.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
			this.SuspendLayout();
			// 
			// lockAllPal
			// 
			resources.ApplyResources(this.lockAllPal, "lockAllPal");
			this.lockAllPal.Name = "lockAllPal";
			this.lockAllPal.UseVisualStyleBackColor = true;
			this.lockAllPal.CheckedChanged += new System.EventHandler(this.lockAllPal_CheckedChanged);
			// 
			// modeEdition
			// 
			resources.ApplyResources(this.modeEdition, "modeEdition");
			this.modeEdition.Name = "modeEdition";
			this.modeEdition.UseVisualStyleBackColor = true;
			this.modeEdition.CheckedChanged += new System.EventHandler(this.modeEdition_CheckedChanged);
			// 
			// hScrollBar
			// 
			resources.ApplyResources(this.hScrollBar, "hScrollBar");
			this.hScrollBar.LargeChange = 32;
			this.hScrollBar.Name = "hScrollBar";
			this.hScrollBar.SmallChange = 8;
			this.hScrollBar.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollBar_Scroll);
			// 
			// vScrollBar
			// 
			resources.ApplyResources(this.vScrollBar, "vScrollBar");
			this.vScrollBar.LargeChange = 32;
			this.vScrollBar.Name = "vScrollBar";
			this.vScrollBar.SmallChange = 8;
			this.vScrollBar.Scroll += new System.Windows.Forms.ScrollEventHandler(this.vScrollBar_Scroll);
			// 
			// drawColor
			// 
			resources.ApplyResources(this.drawColor, "drawColor");
			this.drawColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.drawColor.Name = "drawColor";
			// 
			// label3
			// 
			resources.ApplyResources(this.label3, "label3");
			this.label3.Name = "label3";
			// 
			// label2
			// 
			resources.ApplyResources(this.label2, "label2");
			this.label2.Name = "label2";
			// 
			// tailleCrayon
			// 
			resources.ApplyResources(this.tailleCrayon, "tailleCrayon");
			this.tailleCrayon.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.tailleCrayon.FormattingEnabled = true;
			this.tailleCrayon.Items.AddRange(new object[] {
            resources.GetString("tailleCrayon.Items"),
            resources.GetString("tailleCrayon.Items1"),
            resources.GetString("tailleCrayon.Items2"),
            resources.GetString("tailleCrayon.Items3"),
            resources.GetString("tailleCrayon.Items4")});
			this.tailleCrayon.Name = "tailleCrayon";
			this.tailleCrayon.SelectedIndexChanged += new System.EventHandler(this.tailleCrayon_SelectedIndexChanged);
			// 
			// grpEdition
			// 
			resources.ApplyResources(this.grpEdition, "grpEdition");
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
			this.grpEdition.Name = "grpEdition";
			this.grpEdition.TabStop = false;
			// 
			// lblInfoPos
			// 
			resources.ApplyResources(this.lblInfoPos, "lblInfoPos");
			this.lblInfoPos.Name = "lblInfoPos";
			// 
			// rbCopy
			// 
			resources.ApplyResources(this.rbCopy, "rbCopy");
			this.rbCopy.Image = global::ConvImgCpc.Properties.Resources.Copy;
			this.rbCopy.Name = "rbCopy";
			this.rbCopy.UseVisualStyleBackColor = true;
			this.rbCopy.CheckedChanged += new System.EventHandler(this.rbCopy_CheckedChanged);
			// 
			// undrawColor
			// 
			resources.ApplyResources(this.undrawColor, "undrawColor");
			this.undrawColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.undrawColor.Name = "undrawColor";
			// 
			// lblZoom
			// 
			resources.ApplyResources(this.lblZoom, "lblZoom");
			this.lblZoom.Name = "lblZoom";
			// 
			// rbZoom
			// 
			resources.ApplyResources(this.rbZoom, "rbZoom");
			this.rbZoom.Image = global::ConvImgCpc.Properties.Resources.Zoom;
			this.rbZoom.Name = "rbZoom";
			this.rbZoom.UseVisualStyleBackColor = true;
			this.rbZoom.CheckedChanged += new System.EventHandler(this.rbZoom_CheckedChanged);
			// 
			// rbDraw
			// 
			resources.ApplyResources(this.rbDraw, "rbDraw");
			this.rbDraw.Checked = true;
			this.rbDraw.Image = global::ConvImgCpc.Properties.Resources.Draw;
			this.rbDraw.Name = "rbDraw";
			this.rbDraw.TabStop = true;
			this.rbDraw.UseVisualStyleBackColor = true;
			this.rbDraw.CheckedChanged += new System.EventHandler(this.rbDraw_CheckedChanged);
			// 
			// chkDoRedo
			// 
			resources.ApplyResources(this.chkDoRedo, "chkDoRedo");
			this.chkDoRedo.Name = "chkDoRedo";
			this.chkDoRedo.UseVisualStyleBackColor = true;
			// 
			// bpRedo
			// 
			resources.ApplyResources(this.bpRedo, "bpRedo");
			this.bpRedo.Name = "bpRedo";
			this.bpRedo.UseVisualStyleBackColor = true;
			this.bpRedo.Click += new System.EventHandler(this.bpRedo_Click);
			// 
			// bpUndo
			// 
			resources.ApplyResources(this.bpUndo, "bpUndo");
			this.bpUndo.Name = "bpUndo";
			this.bpUndo.UseVisualStyleBackColor = true;
			this.bpUndo.Click += new System.EventHandler(this.bpUndo_Click);
			// 
			// chkRendu
			// 
			resources.ApplyResources(this.chkRendu, "chkRendu");
			this.chkRendu.Name = "chkRendu";
			this.chkRendu.UseVisualStyleBackColor = true;
			this.chkRendu.CheckedChanged += new System.EventHandler(this.chkRendu_CheckedChanged);
			// 
			// modeCaptureSprites
			// 
			resources.ApplyResources(this.modeCaptureSprites, "modeCaptureSprites");
			this.modeCaptureSprites.Name = "modeCaptureSprites";
			this.modeCaptureSprites.UseVisualStyleBackColor = true;
			this.modeCaptureSprites.CheckedChanged += new System.EventHandler(this.modeCaptureSprites_CheckedChanged);
			// 
			// bpCopyPal
			// 
			resources.ApplyResources(this.bpCopyPal, "bpCopyPal");
			this.bpCopyPal.Name = "bpCopyPal";
			this.bpCopyPal.UseVisualStyleBackColor = true;
			this.bpCopyPal.Click += new System.EventHandler(this.bpCopyPal_Click);
			// 
			// pictureBox
			// 
			resources.ApplyResources(this.pictureBox, "pictureBox");
			this.pictureBox.Name = "pictureBox";
			this.pictureBox.TabStop = false;
			this.pictureBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TrtMouseMove);
			this.pictureBox.MouseLeave += new System.EventHandler(this.pictureBox_MouseLeave);
			this.pictureBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.TrtMouseMove);
			// 
			// ImageCpc
			// 
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
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