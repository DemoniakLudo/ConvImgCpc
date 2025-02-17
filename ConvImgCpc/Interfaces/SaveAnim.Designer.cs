namespace ConvImgCpc {
	partial class SaveAnim {
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
			this.bpSave = new System.Windows.Forms.Button();
			this.chk128Ko = new System.Windows.Forms.CheckBox();
			this.chkBoucle = new System.Windows.Forms.CheckBox();
			this.label1 = new System.Windows.Forms.Label();
			this.txbAdrDeb = new System.Windows.Forms.TextBox();
			this.chkMaxMem = new System.Windows.Forms.CheckBox();
			this.tbxAdrMax = new System.Windows.Forms.TextBox();
			this.chkDelai = new System.Windows.Forms.CheckBox();
			this.rb1L = new System.Windows.Forms.RadioButton();
			this.rb2L = new System.Windows.Forms.RadioButton();
			this.rb4L = new System.Windows.Forms.RadioButton();
			this.rb8L = new System.Windows.Forms.RadioButton();
			this.chk2Zone = new System.Windows.Forms.CheckBox();
			this.chkZoneVert = new System.Windows.Forms.CheckBox();
			this.chkCol = new System.Windows.Forms.CheckBox();
			this.grpGenereLigne = new System.Windows.Forms.GroupBox();
			this.grpAscii = new System.Windows.Forms.GroupBox();
			this.rbFrameFull = new System.Windows.Forms.RadioButton();
			this.rbFrameD = new System.Windows.Forms.RadioButton();
			this.rbFrameO = new System.Windows.Forms.RadioButton();
			this.chkDataBrut = new System.Windows.Forms.CheckBox();
			this.comboMethode = new System.Windows.Forms.ComboBox();
			this.grpGenereLigne.SuspendLayout();
			this.grpAscii.SuspendLayout();
			this.SuspendLayout();
			// 
			// bpSave
			// 
			this.bpSave.Location = new System.Drawing.Point(408, 207);
			this.bpSave.Name = "bpSave";
			this.bpSave.Size = new System.Drawing.Size(75, 23);
			this.bpSave.TabIndex = 1;
			this.bpSave.UseVisualStyleBackColor = true;
			this.bpSave.Click += new System.EventHandler(this.BpSave_Click);
			// 
			// chk128Ko
			// 
			this.chk128Ko.AutoSize = true;
			this.chk128Ko.Location = new System.Drawing.Point(25, 35);
			this.chk128Ko.Name = "chk128Ko";
			this.chk128Ko.Size = new System.Drawing.Size(15, 14);
			this.chk128Ko.TabIndex = 2;
			this.chk128Ko.UseVisualStyleBackColor = true;
			this.chk128Ko.CheckedChanged += new System.EventHandler(this.Chk128Ko_CheckedChanged);
			// 
			// chkBoucle
			// 
			this.chkBoucle.AutoSize = true;
			this.chkBoucle.Location = new System.Drawing.Point(25, 12);
			this.chkBoucle.Name = "chkBoucle";
			this.chkBoucle.Size = new System.Drawing.Size(15, 14);
			this.chkBoucle.TabIndex = 3;
			this.chkBoucle.UseVisualStyleBackColor = true;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(329, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(0, 13);
			this.label1.TabIndex = 4;
			// 
			// txbAdrDeb
			// 
			this.txbAdrDeb.Location = new System.Drawing.Point(431, 12);
			this.txbAdrDeb.Name = "txbAdrDeb";
			this.txbAdrDeb.Size = new System.Drawing.Size(53, 20);
			this.txbAdrDeb.TabIndex = 5;
			this.txbAdrDeb.Text = "#200";
			this.txbAdrDeb.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// chkMaxMem
			// 
			this.chkMaxMem.AutoSize = true;
			this.chkMaxMem.Location = new System.Drawing.Point(25, 58);
			this.chkMaxMem.Name = "chkMaxMem";
			this.chkMaxMem.Size = new System.Drawing.Size(15, 14);
			this.chkMaxMem.TabIndex = 6;
			this.chkMaxMem.UseVisualStyleBackColor = true;
			this.chkMaxMem.Visible = false;
			this.chkMaxMem.CheckedChanged += new System.EventHandler(this.ChkMaxMem_CheckedChanged);
			// 
			// tbxAdrMax
			// 
			this.tbxAdrMax.Enabled = false;
			this.tbxAdrMax.Location = new System.Drawing.Point(227, 55);
			this.tbxAdrMax.Name = "tbxAdrMax";
			this.tbxAdrMax.Size = new System.Drawing.Size(53, 20);
			this.tbxAdrMax.TabIndex = 7;
			this.tbxAdrMax.Text = "#A600";
			this.tbxAdrMax.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.tbxAdrMax.Visible = false;
			// 
			// chkDelai
			// 
			this.chkDelai.AutoSize = true;
			this.chkDelai.Location = new System.Drawing.Point(25, 207);
			this.chkDelai.Name = "chkDelai";
			this.chkDelai.Size = new System.Drawing.Size(15, 14);
			this.chkDelai.TabIndex = 9;
			this.chkDelai.UseVisualStyleBackColor = true;
			// 
			// rb1L
			// 
			this.rb1L.AutoSize = true;
			this.rb1L.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
			this.rb1L.Checked = true;
			this.rb1L.Location = new System.Drawing.Point(6, 11);
			this.rb1L.Name = "rb1L";
			this.rb1L.Size = new System.Drawing.Size(14, 13);
			this.rb1L.TabIndex = 12;
			this.rb1L.TabStop = true;
			this.rb1L.UseVisualStyleBackColor = true;
			// 
			// rb2L
			// 
			this.rb2L.AutoSize = true;
			this.rb2L.Location = new System.Drawing.Point(6, 34);
			this.rb2L.Name = "rb2L";
			this.rb2L.Size = new System.Drawing.Size(14, 13);
			this.rb2L.TabIndex = 13;
			this.rb2L.UseVisualStyleBackColor = true;
			// 
			// rb4L
			// 
			this.rb4L.AutoSize = true;
			this.rb4L.Location = new System.Drawing.Point(6, 57);
			this.rb4L.Name = "rb4L";
			this.rb4L.Size = new System.Drawing.Size(14, 13);
			this.rb4L.TabIndex = 14;
			this.rb4L.UseVisualStyleBackColor = true;
			// 
			// rb8L
			// 
			this.rb8L.AutoSize = true;
			this.rb8L.Location = new System.Drawing.Point(6, 80);
			this.rb8L.Name = "rb8L";
			this.rb8L.Size = new System.Drawing.Size(14, 13);
			this.rb8L.TabIndex = 15;
			this.rb8L.UseVisualStyleBackColor = true;
			// 
			// chk2Zone
			// 
			this.chk2Zone.AutoSize = true;
			this.chk2Zone.Location = new System.Drawing.Point(330, 133);
			this.chk2Zone.Name = "chk2Zone";
			this.chk2Zone.Size = new System.Drawing.Size(15, 14);
			this.chk2Zone.TabIndex = 16;
			this.chk2Zone.UseVisualStyleBackColor = true;
			this.chk2Zone.CheckedChanged += new System.EventHandler(this.Chk2Zone_CheckedChanged);
			// 
			// chkZoneVert
			// 
			this.chkZoneVert.AutoSize = true;
			this.chkZoneVert.Location = new System.Drawing.Point(330, 156);
			this.chkZoneVert.Name = "chkZoneVert";
			this.chkZoneVert.Size = new System.Drawing.Size(15, 14);
			this.chkZoneVert.TabIndex = 17;
			this.chkZoneVert.UseVisualStyleBackColor = true;
			this.chkZoneVert.Visible = false;
			// 
			// chkCol
			// 
			this.chkCol.AutoSize = true;
			this.chkCol.Location = new System.Drawing.Point(330, 180);
			this.chkCol.Name = "chkCol";
			this.chkCol.Size = new System.Drawing.Size(15, 14);
			this.chkCol.TabIndex = 19;
			this.chkCol.UseVisualStyleBackColor = true;
			// 
			// grpGenereLigne
			// 
			this.grpGenereLigne.Controls.Add(this.rb1L);
			this.grpGenereLigne.Controls.Add(this.rb2L);
			this.grpGenereLigne.Controls.Add(this.rb4L);
			this.grpGenereLigne.Controls.Add(this.rb8L);
			this.grpGenereLigne.Location = new System.Drawing.Point(36, 97);
			this.grpGenereLigne.Name = "grpGenereLigne";
			this.grpGenereLigne.Size = new System.Drawing.Size(152, 104);
			this.grpGenereLigne.TabIndex = 20;
			this.grpGenereLigne.TabStop = false;
			// 
			// grpAscii
			// 
			this.grpAscii.Controls.Add(this.rbFrameFull);
			this.grpAscii.Controls.Add(this.rbFrameD);
			this.grpAscii.Controls.Add(this.rbFrameO);
			this.grpAscii.Location = new System.Drawing.Point(30, 104);
			this.grpAscii.Name = "grpAscii";
			this.grpAscii.Size = new System.Drawing.Size(138, 89);
			this.grpAscii.TabIndex = 21;
			this.grpAscii.TabStop = false;
			// 
			// rbFrameFull
			// 
			this.rbFrameFull.AutoSize = true;
			this.rbFrameFull.Checked = true;
			this.rbFrameFull.Location = new System.Drawing.Point(5, 10);
			this.rbFrameFull.Name = "rbFrameFull";
			this.rbFrameFull.Size = new System.Drawing.Size(14, 13);
			this.rbFrameFull.TabIndex = 1;
			this.rbFrameFull.TabStop = true;
			this.rbFrameFull.UseVisualStyleBackColor = true;
			// 
			// rbFrameD
			// 
			this.rbFrameD.AutoSize = true;
			this.rbFrameD.Location = new System.Drawing.Point(5, 56);
			this.rbFrameD.Name = "rbFrameD";
			this.rbFrameD.Size = new System.Drawing.Size(14, 13);
			this.rbFrameD.TabIndex = 0;
			this.rbFrameD.UseVisualStyleBackColor = true;
			// 
			// rbFrameO
			// 
			this.rbFrameO.AutoSize = true;
			this.rbFrameO.Location = new System.Drawing.Point(5, 33);
			this.rbFrameO.Name = "rbFrameO";
			this.rbFrameO.Size = new System.Drawing.Size(14, 13);
			this.rbFrameO.TabIndex = 0;
			this.rbFrameO.UseVisualStyleBackColor = true;
			// 
			// chkDataBrut
			// 
			this.chkDataBrut.AutoSize = true;
			this.chkDataBrut.Location = new System.Drawing.Point(329, 97);
			this.chkDataBrut.Name = "chkDataBrut";
			this.chkDataBrut.Size = new System.Drawing.Size(15, 14);
			this.chkDataBrut.TabIndex = 22;
			this.chkDataBrut.UseVisualStyleBackColor = true;
			this.chkDataBrut.CheckedChanged += new System.EventHandler(this.ChkDataBrut_CheckedChanged);
			// 
			// comboMethode
			// 
			this.comboMethode.FormattingEnabled = true;
			this.comboMethode.Items.AddRange(new object[] {
            "Standard",
            "DirectMemory",
            "Blocks"});
			this.comboMethode.Location = new System.Drawing.Point(25, 78);
			this.comboMethode.Name = "comboMethode";
			this.comboMethode.Size = new System.Drawing.Size(121, 21);
			this.comboMethode.TabIndex = 23;
			this.comboMethode.SelectedIndexChanged += new System.EventHandler(this.ComboMethode_SelectedIndexChanged);
			// 
			// SaveAnim
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(487, 235);
			this.Controls.Add(this.comboMethode);
			this.Controls.Add(this.chkDataBrut);
			this.Controls.Add(this.grpAscii);
			this.Controls.Add(this.grpGenereLigne);
			this.Controls.Add(this.chkCol);
			this.Controls.Add(this.chkZoneVert);
			this.Controls.Add(this.chk2Zone);
			this.Controls.Add(this.chkDelai);
			this.Controls.Add(this.tbxAdrMax);
			this.Controls.Add(this.chkMaxMem);
			this.Controls.Add(this.txbAdrDeb);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.chkBoucle);
			this.Controls.Add(this.chk128Ko);
			this.Controls.Add(this.bpSave);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "SaveAnim";
			this.Text = "SaveAnim";
			this.grpGenereLigne.ResumeLayout(false);
			this.grpGenereLigne.PerformLayout();
			this.grpAscii.ResumeLayout(false);
			this.grpAscii.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button bpSave;
		private System.Windows.Forms.CheckBox chk128Ko;
		private System.Windows.Forms.CheckBox chkBoucle;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox txbAdrDeb;
		private System.Windows.Forms.CheckBox chkMaxMem;
		private System.Windows.Forms.TextBox tbxAdrMax;
		private System.Windows.Forms.CheckBox chkDelai;
		private System.Windows.Forms.RadioButton rb1L;
		private System.Windows.Forms.RadioButton rb2L;
		private System.Windows.Forms.RadioButton rb4L;
		private System.Windows.Forms.RadioButton rb8L;
		private System.Windows.Forms.CheckBox chk2Zone;
		private System.Windows.Forms.CheckBox chkZoneVert;
		private System.Windows.Forms.CheckBox chkCol;
		private System.Windows.Forms.GroupBox grpGenereLigne;
		private System.Windows.Forms.GroupBox grpAscii;
		private System.Windows.Forms.RadioButton rbFrameFull;
		private System.Windows.Forms.RadioButton rbFrameD;
		private System.Windows.Forms.RadioButton rbFrameO;
		private System.Windows.Forms.CheckBox chkDataBrut;
		private System.Windows.Forms.ComboBox comboMethode;
	}
}