namespace ConvImgCpc {
	partial class Informations {
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
			this.listInfo = new System.Windows.Forms.ListBox();
			this.SuspendLayout();
			// 
			// listInfo
			// 
			this.listInfo.FormattingEnabled = true;
			this.listInfo.Location = new System.Drawing.Point(0, 1);
			this.listInfo.Name = "listInfo";
			this.listInfo.Size = new System.Drawing.Size(567, 160);
			this.listInfo.TabIndex = 0;
			// 
			// Informations
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(568, 162);
			this.ControlBox = false;
			this.Controls.Add(this.listInfo);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.MaximizeBox = false;
			this.Name = "Informations";
			this.Text = "Informations";
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ListBox listInfo;
	}
}