namespace CpcConvImg {
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
			((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
			this.SuspendLayout();
			// 
			// pictureBox
			// 
			this.pictureBox.Location = new System.Drawing.Point(0, 0);
			this.pictureBox.Name = "pictureBox";
			this.pictureBox.Size = new System.Drawing.Size(768, 544);
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
			// ImageCpc
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(879, 620);
			this.ControlBox = false;
			this.Controls.Add(this.vScrollBar);
			this.Controls.Add(this.hScrollBar);
			this.Controls.Add(this.modeEdition);
			this.Controls.Add(this.lockAllPal);
			this.Controls.Add(this.pictureBox);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ImageCpc";
			this.Text = "ImageCPC";
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

	}
}