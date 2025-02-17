namespace ConvImgCpc {
	partial class Rendu {
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
			this.chkX2 = new System.Windows.Forms.CheckBox();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
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
			// 
			// chkX2
			// 
			this.chkX2.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.chkX2.AutoSize = true;
			this.chkX2.Location = new System.Drawing.Point(774, 240);
			this.chkX2.Name = "chkX2";
			this.chkX2.Size = new System.Drawing.Size(37, 17);
			this.chkX2.TabIndex = 1;
			this.chkX2.Text = "x2";
			this.chkX2.UseVisualStyleBackColor = true;
			this.chkX2.CheckedChanged += new System.EventHandler(this.ChkX2_CheckedChanged);
			// 
			// Rendu
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(809, 544);
			this.ControlBox = false;
			this.Controls.Add(this.chkX2);
			this.Controls.Add(this.pictureBox);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.MaximizeBox = false;
			this.Name = "Rendu";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.Text = "Rendu";
			((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.PictureBox pictureBox;
		private System.Windows.Forms.CheckBox chkX2;

	}
}