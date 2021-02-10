namespace ConvImgCpc {
	partial class EditTrameAscii {
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
			this.pictEditMatrice = new System.Windows.Forms.PictureBox();
			this.pictAllMatrice = new System.Windows.Forms.PictureBox();
			this.bpPrev = new System.Windows.Forms.Button();
			this.bpSuiv = new System.Windows.Forms.Button();
			this.lblPen0 = new System.Windows.Forms.Label();
			this.lblPen1 = new System.Windows.Forms.Label();
			this.lblPen2 = new System.Windows.Forms.Label();
			this.lblPen3 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.lblPenLeft = new System.Windows.Forms.Label();
			this.lblPenRight = new System.Windows.Forms.Label();
			this.bpRead = new System.Windows.Forms.Button();
			this.bpSave = new System.Windows.Forms.Button();
			this.bpAutoGene = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.pictEditMatrice)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictAllMatrice)).BeginInit();
			this.SuspendLayout();
			// 
			// pictEditMatrice
			// 
			this.pictEditMatrice.Location = new System.Drawing.Point(160, 64);
			this.pictEditMatrice.Name = "pictEditMatrice";
			this.pictEditMatrice.Size = new System.Drawing.Size(320, 320);
			this.pictEditMatrice.TabIndex = 0;
			this.pictEditMatrice.TabStop = false;
			this.pictEditMatrice.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictEditMatrice_MouseMove);
			this.pictEditMatrice.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictEditMatrice_MouseMove);
			// 
			// pictAllMatrice
			// 
			this.pictAllMatrice.Location = new System.Drawing.Point(64, 16);
			this.pictAllMatrice.Name = "pictAllMatrice";
			this.pictAllMatrice.Size = new System.Drawing.Size(512, 32);
			this.pictAllMatrice.TabIndex = 1;
			this.pictAllMatrice.TabStop = false;
			this.pictAllMatrice.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictAllMatrice_MouseDown);
			// 
			// bpPrev
			// 
			this.bpPrev.Location = new System.Drawing.Point(64, 64);
			this.bpPrev.Name = "bpPrev";
			this.bpPrev.Size = new System.Drawing.Size(75, 23);
			this.bpPrev.TabIndex = 2;
			this.bpPrev.UseVisualStyleBackColor = true;
			this.bpPrev.Visible = false;
			this.bpPrev.Click += new System.EventHandler(this.bpPrev_Click);
			// 
			// bpSuiv
			// 
			this.bpSuiv.Location = new System.Drawing.Point(501, 64);
			this.bpSuiv.Name = "bpSuiv";
			this.bpSuiv.Size = new System.Drawing.Size(75, 23);
			this.bpSuiv.TabIndex = 2;
			this.bpSuiv.UseVisualStyleBackColor = true;
			this.bpSuiv.Click += new System.EventHandler(this.bpSuiv_Click);
			// 
			// lblPen0
			// 
			this.lblPen0.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lblPen0.Location = new System.Drawing.Point(160, 396);
			this.lblPen0.Name = "lblPen0";
			this.lblPen0.Size = new System.Drawing.Size(48, 48);
			this.lblPen0.TabIndex = 3;
			this.lblPen0.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lblPen0_Click);
			// 
			// lblPen1
			// 
			this.lblPen1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lblPen1.Location = new System.Drawing.Point(250, 396);
			this.lblPen1.Name = "lblPen1";
			this.lblPen1.Size = new System.Drawing.Size(48, 48);
			this.lblPen1.TabIndex = 3;
			this.lblPen1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lblPen1_Click);
			// 
			// lblPen2
			// 
			this.lblPen2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lblPen2.Location = new System.Drawing.Point(340, 396);
			this.lblPen2.Name = "lblPen2";
			this.lblPen2.Size = new System.Drawing.Size(48, 48);
			this.lblPen2.TabIndex = 3;
			this.lblPen2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lblPen2_Click);
			// 
			// lblPen3
			// 
			this.lblPen3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lblPen3.Location = new System.Drawing.Point(430, 396);
			this.lblPen3.Name = "lblPen3";
			this.lblPen3.Size = new System.Drawing.Size(48, 48);
			this.lblPen3.TabIndex = 3;
			this.lblPen3.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lblPen3_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 174);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(62, 13);
			this.label1.TabIndex = 4;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(12, 239);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(52, 13);
			this.label2.TabIndex = 4;
			// 
			// lblPenLeft
			// 
			this.lblPenLeft.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lblPenLeft.Location = new System.Drawing.Point(91, 158);
			this.lblPenLeft.Name = "lblPenLeft";
			this.lblPenLeft.Size = new System.Drawing.Size(48, 48);
			this.lblPenLeft.TabIndex = 5;
			// 
			// lblPenRight
			// 
			this.lblPenRight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lblPenRight.Location = new System.Drawing.Point(91, 222);
			this.lblPenRight.Name = "lblPenRight";
			this.lblPenRight.Size = new System.Drawing.Size(48, 48);
			this.lblPenRight.TabIndex = 5;
			// 
			// bpRead
			// 
			this.bpRead.Location = new System.Drawing.Point(12, 396);
			this.bpRead.Name = "bpRead";
			this.bpRead.Size = new System.Drawing.Size(85, 23);
			this.bpRead.TabIndex = 6;
			this.bpRead.UseVisualStyleBackColor = true;
			this.bpRead.Click += new System.EventHandler(this.bpRead_Click);
			// 
			// bpSave
			// 
			this.bpSave.Location = new System.Drawing.Point(12, 425);
			this.bpSave.Name = "bpSave";
			this.bpSave.Size = new System.Drawing.Size(85, 23);
			this.bpSave.TabIndex = 6;
			this.bpSave.UseVisualStyleBackColor = true;
			this.bpSave.Click += new System.EventHandler(this.bpSave_Click);
			// 
			// bpAutoGene
			// 
			this.bpAutoGene.Location = new System.Drawing.Point(501, 396);
			this.bpAutoGene.Name = "bpAutoGene";
			this.bpAutoGene.Size = new System.Drawing.Size(119, 45);
			this.bpAutoGene.TabIndex = 7;
			this.bpAutoGene.UseVisualStyleBackColor = true;
			this.bpAutoGene.Click += new System.EventHandler(this.bpAutoGene_Click);
			// 
			// EditTrameAscii
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(632, 453);
			this.Controls.Add(this.bpAutoGene);
			this.Controls.Add(this.bpSave);
			this.Controls.Add(this.bpRead);
			this.Controls.Add(this.lblPenRight);
			this.Controls.Add(this.lblPenLeft);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.lblPen3);
			this.Controls.Add(this.lblPen2);
			this.Controls.Add(this.lblPen1);
			this.Controls.Add(this.lblPen0);
			this.Controls.Add(this.bpSuiv);
			this.Controls.Add(this.bpPrev);
			this.Controls.Add(this.pictAllMatrice);
			this.Controls.Add(this.pictEditMatrice);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "EditTrameAscii";
			this.ShowIcon = false;
			this.Text = "Edition Trames Asc-ut";
			((System.ComponentModel.ISupportInitialize)(this.pictEditMatrice)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictAllMatrice)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.PictureBox pictEditMatrice;
		private System.Windows.Forms.PictureBox pictAllMatrice;
		private System.Windows.Forms.Button bpPrev;
		private System.Windows.Forms.Button bpSuiv;
		private System.Windows.Forms.Label lblPen0;
		private System.Windows.Forms.Label lblPen1;
		private System.Windows.Forms.Label lblPen2;
		private System.Windows.Forms.Label lblPen3;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label lblPenLeft;
		private System.Windows.Forms.Label lblPenRight;
		private System.Windows.Forms.Button bpRead;
		private System.Windows.Forms.Button bpSave;
		private System.Windows.Forms.Button bpAutoGene;
	}
}