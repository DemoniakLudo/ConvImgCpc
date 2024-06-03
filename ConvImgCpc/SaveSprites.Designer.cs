namespace ConvImgCpc {
	partial class SaveSprites {
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
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.txbLabelSpr = new System.Windows.Forms.TextBox();
			this.txbLabelPtr = new System.Windows.Forms.TextBox();
			this.chkZeroPtr = new System.Windows.Forms.CheckBox();
			this.bpOk = new System.Windows.Forms.Button();
			this.label3 = new System.Windows.Forms.Label();
			this.txbLabelPalette = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 20);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(68, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Sprites Label";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(12, 73);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(104, 13);
			this.label2.TabIndex = 1;
			this.label2.Text = "Sprites Pointer Label";
			// 
			// txbLabelSpr
			// 
			this.txbLabelSpr.Location = new System.Drawing.Point(148, 20);
			this.txbLabelSpr.Name = "txbLabelSpr";
			this.txbLabelSpr.Size = new System.Drawing.Size(339, 20);
			this.txbLabelSpr.TabIndex = 2;
			// 
			// txbLabelPtr
			// 
			this.txbLabelPtr.Location = new System.Drawing.Point(148, 73);
			this.txbLabelPtr.Name = "txbLabelPtr";
			this.txbLabelPtr.Size = new System.Drawing.Size(339, 20);
			this.txbLabelPtr.TabIndex = 3;
			// 
			// chkZeroPtr
			// 
			this.chkZeroPtr.AutoSize = true;
			this.chkZeroPtr.Location = new System.Drawing.Point(12, 102);
			this.chkZeroPtr.Name = "chkZeroPtr";
			this.chkZeroPtr.Size = new System.Drawing.Size(172, 17);
			this.chkZeroPtr.TabIndex = 4;
			this.chkZeroPtr.Text = "Add 0 in the end onf pointer list";
			this.chkZeroPtr.UseVisualStyleBackColor = true;
			// 
			// bpOk
			// 
			this.bpOk.Location = new System.Drawing.Point(409, 99);
			this.bpOk.Name = "bpOk";
			this.bpOk.Size = new System.Drawing.Size(75, 23);
			this.bpOk.TabIndex = 5;
			this.bpOk.Text = "Ok";
			this.bpOk.UseVisualStyleBackColor = true;
			this.bpOk.Click += new System.EventHandler(this.bpOk_Click);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(12, 46);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(104, 13);
			this.label3.TabIndex = 0;
			this.label3.Text = "Sprites Palette Label";
			// 
			// txbLabelPalette
			// 
			this.txbLabelPalette.Location = new System.Drawing.Point(148, 46);
			this.txbLabelPalette.Name = "txbLabelPalette";
			this.txbLabelPalette.Size = new System.Drawing.Size(339, 20);
			this.txbLabelPalette.TabIndex = 2;
			// 
			// SaveSprites
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(496, 127);
			this.Controls.Add(this.bpOk);
			this.Controls.Add(this.chkZeroPtr);
			this.Controls.Add(this.txbLabelPtr);
			this.Controls.Add(this.txbLabelPalette);
			this.Controls.Add(this.txbLabelSpr);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "SaveSprites";
			this.Text = "Save Sprites (asm format)";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox txbLabelSpr;
		private System.Windows.Forms.TextBox txbLabelPtr;
		private System.Windows.Forms.CheckBox chkZeroPtr;
		private System.Windows.Forms.Button bpOk;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox txbLabelPalette;
	}
}