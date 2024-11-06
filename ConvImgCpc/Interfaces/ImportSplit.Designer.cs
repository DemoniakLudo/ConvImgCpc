namespace ConvImgCpc {
	partial class ImportSplit {
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
			this.pictureSplit = new System.Windows.Forms.PictureBox();
			this.bpLire = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.numPen = new System.Windows.Forms.NumericUpDown();
			this.listErr = new System.Windows.Forms.ListBox();
			((System.ComponentModel.ISupportInitialize)(this.pictureSplit)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numPen)).BeginInit();
			this.SuspendLayout();
			// 
			// pictureSplit
			// 
			this.pictureSplit.Location = new System.Drawing.Point(115, 2);
			this.pictureSplit.Name = "pictureSplit";
			this.pictureSplit.Size = new System.Drawing.Size(768, 544);
			this.pictureSplit.TabIndex = 0;
			this.pictureSplit.TabStop = false;
			// 
			// bpLire
			// 
			this.bpLire.Location = new System.Drawing.Point(7, 12);
			this.bpLire.Name = "bpLire";
			this.bpLire.Size = new System.Drawing.Size(92, 23);
			this.bpLire.TabIndex = 1;
			this.bpLire.Text = "Lire image";
			this.bpLire.UseVisualStyleBackColor = true;
			this.bpLire.Click += new System.EventHandler(this.BpLire_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(4, 50);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(45, 13);
			this.label1.TabIndex = 3;
			this.label1.Text = "N° Stylo";
			// 
			// numPen
			// 
			this.numPen.Location = new System.Drawing.Point(55, 47);
			this.numPen.Maximum = new decimal(new int[] {
            15,
            0,
            0,
            0});
			this.numPen.Name = "numPen";
			this.numPen.Size = new System.Drawing.Size(45, 20);
			this.numPen.TabIndex = 4;
			this.numPen.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
			// 
			// listErr
			// 
			this.listErr.FormattingEnabled = true;
			this.listErr.Location = new System.Drawing.Point(7, 548);
			this.listErr.Name = "listErr";
			this.listErr.Size = new System.Drawing.Size(875, 134);
			this.listErr.TabIndex = 5;
			// 
			// ImportSplit
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(885, 684);
			this.Controls.Add(this.listErr);
			this.Controls.Add(this.numPen);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.bpLire);
			this.Controls.Add(this.pictureSplit);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ImportSplit";
			this.Text = "ImportSplit";
			((System.ComponentModel.ISupportInitialize)(this.pictureSplit)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numPen)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.PictureBox pictureSplit;
		private System.Windows.Forms.Button bpLire;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.NumericUpDown numPen;
		private System.Windows.Forms.ListBox listErr;
	}
}