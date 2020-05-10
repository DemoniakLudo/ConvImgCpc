namespace ConvImgCpc {
	partial class Information {
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
			this.listInfo.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listInfo.FormattingEnabled = true;
			this.listInfo.Location = new System.Drawing.Point(0, 0);
			this.listInfo.Name = "listInfo";
			this.listInfo.Size = new System.Drawing.Size(681, 211);
			this.listInfo.TabIndex = 0;
			// 
			// Information
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(681, 211);
			this.Controls.Add(this.listInfo);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "Information";
			this.Text = "Information";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Information_FormClosed);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ListBox listInfo;
	}
}