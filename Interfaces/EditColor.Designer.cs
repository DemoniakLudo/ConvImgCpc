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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditColor));
			this.selColor = new System.Windows.Forms.Label();
			this.lblNumColor = new System.Windows.Forms.Label();
			this.bpValide = new System.Windows.Forms.Button();
			this.bpAnnule = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// selColor
			// 
			this.selColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			resources.ApplyResources(this.selColor, "selColor");
			this.selColor.Name = "selColor";
			// 
			// lblNumColor
			// 
			resources.ApplyResources(this.lblNumColor, "lblNumColor");
			this.lblNumColor.Name = "lblNumColor";
			// 
			// bpValide
			// 
			resources.ApplyResources(this.bpValide, "bpValide");
			this.bpValide.Name = "bpValide";
			this.bpValide.UseVisualStyleBackColor = true;
			this.bpValide.Click += new System.EventHandler(this.bpValide_Click);
			// 
			// bpAnnule
			// 
			resources.ApplyResources(this.bpAnnule, "bpAnnule");
			this.bpAnnule.Name = "bpAnnule";
			this.bpAnnule.UseVisualStyleBackColor = true;
			this.bpAnnule.Click += new System.EventHandler(this.bpAnnule_Click);
			// 
			// EditColor
			// 
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ControlBox = false;
			this.Controls.Add(this.bpAnnule);
			this.Controls.Add(this.bpValide);
			this.Controls.Add(this.lblNumColor);
			this.Controls.Add(this.selColor);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "EditColor";
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