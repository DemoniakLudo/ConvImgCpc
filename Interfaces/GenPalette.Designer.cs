namespace ConvImgCpc {
	partial class GenPalette {
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
			this.lblStartText = new System.Windows.Forms.Label();
			this.lblStartColor = new System.Windows.Forms.Label();
			this.trkStartR = new System.Windows.Forms.TrackBar();
			this.trkEndV = new System.Windows.Forms.TrackBar();
			this.trkEndR = new System.Windows.Forms.TrackBar();
			this.trkEndB = new System.Windows.Forms.TrackBar();
			this.trkStartB = new System.Windows.Forms.TrackBar();
			this.trkStartV = new System.Windows.Forms.TrackBar();
			this.lblEndText = new System.Windows.Forms.Label();
			this.lblEndColor = new System.Windows.Forms.Label();
			this.txbStartR = new System.Windows.Forms.TextBox();
			this.txbStartV = new System.Windows.Forms.TextBox();
			this.txbStartB = new System.Windows.Forms.TextBox();
			this.txbEndB = new System.Windows.Forms.TextBox();
			this.txbEndV = new System.Windows.Forms.TextBox();
			this.txbEndR = new System.Windows.Forms.TextBox();
			this.lblFrom = new System.Windows.Forms.Label();
			this.lblTo = new System.Windows.Forms.Label();
			this.txbTo = new System.Windows.Forms.TextBox();
			this.txbFrom = new System.Windows.Forms.TextBox();
			this.bpGenerate = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.bpGetCol = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.trkStartR)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.trkEndV)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.trkEndR)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.trkEndB)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.trkStartB)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.trkStartV)).BeginInit();
			this.SuspendLayout();
			// 
			// lblStartText
			// 
			this.lblStartText.AutoSize = true;
			this.lblStartText.Location = new System.Drawing.Point(23, 69);
			this.lblStartText.Name = "lblStartText";
			this.lblStartText.Size = new System.Drawing.Size(55, 13);
			this.lblStartText.TabIndex = 0;
			this.lblStartText.Text = "Start color";
			// 
			// lblStartColor
			// 
			this.lblStartColor.BackColor = System.Drawing.Color.Black;
			this.lblStartColor.Location = new System.Drawing.Point(106, 41);
			this.lblStartColor.Name = "lblStartColor";
			this.lblStartColor.Size = new System.Drawing.Size(64, 64);
			this.lblStartColor.TabIndex = 1;
			// 
			// trkStartR
			// 
			this.trkStartR.Location = new System.Drawing.Point(38, 124);
			this.trkStartR.Maximum = 15;
			this.trkStartR.Name = "trkStartR";
			this.trkStartR.Size = new System.Drawing.Size(104, 45);
			this.trkStartR.TabIndex = 2;
			this.trkStartR.Scroll += new System.EventHandler(this.trkStartR_Scroll);
			// 
			// trkEndV
			// 
			this.trkEndV.Location = new System.Drawing.Point(300, 195);
			this.trkEndV.Maximum = 15;
			this.trkEndV.Name = "trkEndV";
			this.trkEndV.Size = new System.Drawing.Size(104, 45);
			this.trkEndV.TabIndex = 3;
			this.trkEndV.Scroll += new System.EventHandler(this.trkEndV_Scroll);
			// 
			// trkEndR
			// 
			this.trkEndR.Location = new System.Drawing.Point(300, 124);
			this.trkEndR.Maximum = 15;
			this.trkEndR.Name = "trkEndR";
			this.trkEndR.Size = new System.Drawing.Size(104, 45);
			this.trkEndR.TabIndex = 4;
			this.trkEndR.Scroll += new System.EventHandler(this.trkEndR_Scroll);
			// 
			// trkEndB
			// 
			this.trkEndB.Location = new System.Drawing.Point(300, 266);
			this.trkEndB.Maximum = 15;
			this.trkEndB.Name = "trkEndB";
			this.trkEndB.Size = new System.Drawing.Size(104, 45);
			this.trkEndB.TabIndex = 5;
			this.trkEndB.Scroll += new System.EventHandler(this.trkEndB_Scroll);
			// 
			// trkStartB
			// 
			this.trkStartB.Location = new System.Drawing.Point(38, 266);
			this.trkStartB.Maximum = 15;
			this.trkStartB.Name = "trkStartB";
			this.trkStartB.Size = new System.Drawing.Size(104, 45);
			this.trkStartB.TabIndex = 6;
			this.trkStartB.Scroll += new System.EventHandler(this.trkStartB_Scroll);
			// 
			// trkStartV
			// 
			this.trkStartV.Location = new System.Drawing.Point(38, 195);
			this.trkStartV.Maximum = 15;
			this.trkStartV.Name = "trkStartV";
			this.trkStartV.Size = new System.Drawing.Size(104, 45);
			this.trkStartV.TabIndex = 7;
			this.trkStartV.Scroll += new System.EventHandler(this.trkStartV_Scroll);
			// 
			// lblEndText
			// 
			this.lblEndText.AutoSize = true;
			this.lblEndText.Location = new System.Drawing.Point(285, 69);
			this.lblEndText.Name = "lblEndText";
			this.lblEndText.Size = new System.Drawing.Size(52, 13);
			this.lblEndText.TabIndex = 0;
			this.lblEndText.Text = "End color";
			// 
			// lblEndColor
			// 
			this.lblEndColor.BackColor = System.Drawing.Color.Black;
			this.lblEndColor.Location = new System.Drawing.Point(368, 41);
			this.lblEndColor.Name = "lblEndColor";
			this.lblEndColor.Size = new System.Drawing.Size(64, 64);
			this.lblEndColor.TabIndex = 1;
			// 
			// txbStartR
			// 
			this.txbStartR.Location = new System.Drawing.Point(140, 124);
			this.txbStartR.MaxLength = 2;
			this.txbStartR.Name = "txbStartR";
			this.txbStartR.Size = new System.Drawing.Size(30, 20);
			this.txbStartR.TabIndex = 8;
			this.txbStartR.Text = "0";
			this.txbStartR.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.txbStartR.TextChanged += new System.EventHandler(this.txbStartR_TextChanged);
			// 
			// txbStartV
			// 
			this.txbStartV.Location = new System.Drawing.Point(140, 195);
			this.txbStartV.MaxLength = 2;
			this.txbStartV.Name = "txbStartV";
			this.txbStartV.Size = new System.Drawing.Size(30, 20);
			this.txbStartV.TabIndex = 8;
			this.txbStartV.Text = "0";
			this.txbStartV.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.txbStartV.TextChanged += new System.EventHandler(this.txbStartV_TextChanged);
			// 
			// txbStartB
			// 
			this.txbStartB.Location = new System.Drawing.Point(140, 266);
			this.txbStartB.MaxLength = 2;
			this.txbStartB.Name = "txbStartB";
			this.txbStartB.Size = new System.Drawing.Size(30, 20);
			this.txbStartB.TabIndex = 8;
			this.txbStartB.Text = "0";
			this.txbStartB.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.txbStartB.TextChanged += new System.EventHandler(this.txbStartB_TextChanged);
			// 
			// txbEndB
			// 
			this.txbEndB.Location = new System.Drawing.Point(402, 266);
			this.txbEndB.MaxLength = 2;
			this.txbEndB.Name = "txbEndB";
			this.txbEndB.Size = new System.Drawing.Size(30, 20);
			this.txbEndB.TabIndex = 8;
			this.txbEndB.Text = "0";
			this.txbEndB.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.txbEndB.TextChanged += new System.EventHandler(this.txbEndB_TextChanged);
			// 
			// txbEndV
			// 
			this.txbEndV.Location = new System.Drawing.Point(402, 195);
			this.txbEndV.MaxLength = 2;
			this.txbEndV.Name = "txbEndV";
			this.txbEndV.Size = new System.Drawing.Size(30, 20);
			this.txbEndV.TabIndex = 8;
			this.txbEndV.Text = "0";
			this.txbEndV.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.txbEndV.TextChanged += new System.EventHandler(this.txbEndV_TextChanged);
			// 
			// txbEndR
			// 
			this.txbEndR.Location = new System.Drawing.Point(402, 124);
			this.txbEndR.MaxLength = 2;
			this.txbEndR.Name = "txbEndR";
			this.txbEndR.Size = new System.Drawing.Size(30, 20);
			this.txbEndR.TabIndex = 8;
			this.txbEndR.Text = "0";
			this.txbEndR.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.txbEndR.TextChanged += new System.EventHandler(this.txbEndR_TextChanged);
			// 
			// lblFrom
			// 
			this.lblFrom.AutoSize = true;
			this.lblFrom.Location = new System.Drawing.Point(23, 9);
			this.lblFrom.Name = "lblFrom";
			this.lblFrom.Size = new System.Drawing.Size(56, 13);
			this.lblFrom.TabIndex = 9;
			this.lblFrom.Text = "From color";
			// 
			// lblTo
			// 
			this.lblTo.AutoSize = true;
			this.lblTo.Location = new System.Drawing.Point(285, 9);
			this.lblTo.Name = "lblTo";
			this.lblTo.Size = new System.Drawing.Size(46, 13);
			this.lblTo.TabIndex = 9;
			this.lblTo.Text = "To color";
			// 
			// txbTo
			// 
			this.txbTo.Location = new System.Drawing.Point(371, 6);
			this.txbTo.MaxLength = 2;
			this.txbTo.Name = "txbTo";
			this.txbTo.Size = new System.Drawing.Size(30, 20);
			this.txbTo.TabIndex = 8;
			this.txbTo.Text = "15";
			this.txbTo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// txbFrom
			// 
			this.txbFrom.Location = new System.Drawing.Point(109, 6);
			this.txbFrom.MaxLength = 2;
			this.txbFrom.Name = "txbFrom";
			this.txbFrom.Size = new System.Drawing.Size(30, 20);
			this.txbFrom.TabIndex = 8;
			this.txbFrom.Text = "0";
			this.txbFrom.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// bpGenerate
			// 
			this.bpGenerate.Location = new System.Drawing.Point(183, 316);
			this.bpGenerate.Name = "bpGenerate";
			this.bpGenerate.Size = new System.Drawing.Size(75, 23);
			this.bpGenerate.TabIndex = 10;
			this.bpGenerate.Text = "Generate";
			this.bpGenerate.UseVisualStyleBackColor = true;
			this.bpGenerate.Click += new System.EventHandler(this.bpGenerate_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(17, 127);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(15, 13);
			this.label1.TabIndex = 11;
			this.label1.Text = "R";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(279, 127);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(15, 13);
			this.label2.TabIndex = 11;
			this.label2.Text = "R";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(18, 202);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(14, 13);
			this.label3.TabIndex = 12;
			this.label3.Text = "V";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(279, 202);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(14, 13);
			this.label4.TabIndex = 12;
			this.label4.Text = "V";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(18, 273);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(14, 13);
			this.label5.TabIndex = 13;
			this.label5.Text = "B";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(279, 269);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(14, 13);
			this.label6.TabIndex = 13;
			this.label6.Text = "B";
			// 
			// bpGetCol
			// 
			this.bpGetCol.Location = new System.Drawing.Point(145, 6);
			this.bpGetCol.Name = "bpGetCol";
			this.bpGetCol.Size = new System.Drawing.Size(59, 23);
			this.bpGetCol.TabIndex = 14;
			this.bpGetCol.Text = "Get color";
			this.bpGetCol.UseVisualStyleBackColor = true;
			this.bpGetCol.Click += new System.EventHandler(this.bpGetCol_Click);
			// 
			// GenPalette
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(445, 351);
			this.Controls.Add(this.bpGetCol);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.bpGenerate);
			this.Controls.Add(this.lblTo);
			this.Controls.Add(this.lblFrom);
			this.Controls.Add(this.txbEndR);
			this.Controls.Add(this.txbEndV);
			this.Controls.Add(this.txbEndB);
			this.Controls.Add(this.txbStartB);
			this.Controls.Add(this.txbStartV);
			this.Controls.Add(this.txbFrom);
			this.Controls.Add(this.txbTo);
			this.Controls.Add(this.txbStartR);
			this.Controls.Add(this.trkStartV);
			this.Controls.Add(this.trkStartB);
			this.Controls.Add(this.trkEndB);
			this.Controls.Add(this.trkEndR);
			this.Controls.Add(this.trkEndV);
			this.Controls.Add(this.trkStartR);
			this.Controls.Add(this.lblEndColor);
			this.Controls.Add(this.lblEndText);
			this.Controls.Add(this.lblStartColor);
			this.Controls.Add(this.lblStartText);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "GenPalette";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "GenPalette";
			((System.ComponentModel.ISupportInitialize)(this.trkStartR)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.trkEndV)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.trkEndR)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.trkEndB)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.trkStartB)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.trkStartV)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label lblStartText;
		private System.Windows.Forms.Label lblStartColor;
		private System.Windows.Forms.TrackBar trkStartR;
		private System.Windows.Forms.TrackBar trkEndV;
		private System.Windows.Forms.TrackBar trkEndR;
		private System.Windows.Forms.TrackBar trkEndB;
		private System.Windows.Forms.TrackBar trkStartB;
		private System.Windows.Forms.TrackBar trkStartV;
		private System.Windows.Forms.Label lblEndText;
		private System.Windows.Forms.Label lblEndColor;
		private System.Windows.Forms.TextBox txbStartR;
		private System.Windows.Forms.TextBox txbStartV;
		private System.Windows.Forms.TextBox txbStartB;
		private System.Windows.Forms.TextBox txbEndB;
		private System.Windows.Forms.TextBox txbEndV;
		private System.Windows.Forms.TextBox txbEndR;
		private System.Windows.Forms.Label lblFrom;
		private System.Windows.Forms.Label lblTo;
		private System.Windows.Forms.TextBox txbTo;
		private System.Windows.Forms.TextBox txbFrom;
		private System.Windows.Forms.Button bpGenerate;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Button bpGetCol;
	}
}