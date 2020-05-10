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
			this.SuspendLayout();
			// 
			// bpSave
			// 
			this.bpSave.Location = new System.Drawing.Point(409, 121);
			this.bpSave.Name = "bpSave";
			this.bpSave.Size = new System.Drawing.Size(75, 23);
			this.bpSave.TabIndex = 1;
			this.bpSave.Text = "Enregistrer";
			this.bpSave.UseVisualStyleBackColor = true;
			this.bpSave.Click += new System.EventHandler(this.bpSave_Click);
			// 
			// chk128Ko
			// 
			this.chk128Ko.AutoSize = true;
			this.chk128Ko.Location = new System.Drawing.Point(25, 35);
			this.chk128Ko.Name = "chk128Ko";
			this.chk128Ko.Size = new System.Drawing.Size(143, 17);
			this.chk128Ko.TabIndex = 2;
			this.chk128Ko.Text = "Gérer 128Ko de mémoire";
			this.chk128Ko.UseVisualStyleBackColor = true;
			this.chk128Ko.CheckedChanged += new System.EventHandler(this.chk128Ko_CheckedChanged);
			// 
			// chkBoucle
			// 
			this.chkBoucle.AutoSize = true;
			this.chkBoucle.Location = new System.Drawing.Point(25, 12);
			this.chkBoucle.Name = "chkBoucle";
			this.chkBoucle.Size = new System.Drawing.Size(186, 17);
			this.chkBoucle.TabIndex = 3;
			this.chkBoucle.Text = "Rebouclage sur la première image";
			this.chkBoucle.UseVisualStyleBackColor = true;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(329, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(96, 13);
			this.label1.TabIndex = 4;
			this.label1.Text = "Adresse de début :";
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
			this.chkMaxMem.Size = new System.Drawing.Size(196, 17);
			this.chkMaxMem.TabIndex = 6;
			this.chkMaxMem.Text = "Adresse mémoire à ne pas dépasser";
			this.chkMaxMem.UseVisualStyleBackColor = true;
			this.chkMaxMem.Visible = false;
			this.chkMaxMem.CheckedChanged += new System.EventHandler(this.chkMaxMem_CheckedChanged);
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
			// SaveAnim
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(495, 156);
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
			this.Text = "Sauvegarde animation";
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
	}
}