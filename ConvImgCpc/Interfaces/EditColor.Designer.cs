namespace ConvImgCpc {
	partial class EditColor {
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
			this.selColor = new System.Windows.Forms.Label();
			this.lblNumColor = new System.Windows.Forms.Label();
			this.bpValide = new System.Windows.Forms.Button();
			this.bpAnnule = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// selColor
			// 
			this.selColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.selColor.Location = new System.Drawing.Point(165, 0);
			this.selColor.Name = "selColor";
			this.selColor.Size = new System.Drawing.Size(100, 75);
			this.selColor.TabIndex = 0;
			// 
			// lblNumColor
			// 
			this.lblNumColor.AutoSize = true;
			this.lblNumColor.Location = new System.Drawing.Point(95, 34);
			this.lblNumColor.Name = "lblNumColor";
			this.lblNumColor.Size = new System.Drawing.Size(0, 13);
			this.lblNumColor.TabIndex = 1;
			// 
			// bpValide
			// 
			this.bpValide.Location = new System.Drawing.Point(12, 208);
			this.bpValide.Name = "bpValide";
			this.bpValide.Size = new System.Drawing.Size(75, 23);
			this.bpValide.TabIndex = 2;
			this.bpValide.UseVisualStyleBackColor = true;
			this.bpValide.Click += new System.EventHandler(this.bpValide_Click);
			// 
			// bpAnnule
			// 
			this.bpAnnule.Location = new System.Drawing.Point(348, 208);
			this.bpAnnule.Name = "bpAnnule";
			this.bpAnnule.Size = new System.Drawing.Size(75, 23);
			this.bpAnnule.TabIndex = 2;
			this.bpAnnule.UseVisualStyleBackColor = true;
			this.bpAnnule.Click += new System.EventHandler(this.bpAnnule_Click);
			// 
			// EditColor
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(435, 243);
			this.ControlBox = false;
			this.Controls.Add(this.bpAnnule);
			this.Controls.Add(this.bpValide);
			this.Controls.Add(this.lblNumColor);
			this.Controls.Add(this.selColor);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "EditColor";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "EditColor";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label selColor;
		private System.Windows.Forms.Label lblNumColor;
		private System.Windows.Forms.Button bpValide;
		private System.Windows.Forms.Button bpAnnule;
	}
}