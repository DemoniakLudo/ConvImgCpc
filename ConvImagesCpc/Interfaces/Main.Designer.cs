namespace CpcConvImg {
	partial class Main {
		/// <summary>
		/// Variable nécessaire au concepteur.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Nettoyage des ressources utilisées.
		/// </summary>
		/// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
		protected override void Dispose(bool disposing) {
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Code généré par le Concepteur Windows Form

		/// <summary>
		/// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
		/// le contenu de cette méthode avec l'éditeur de code.
		/// </summary>
		private void InitializeComponent() {
			this.checkImageSource = new System.Windows.Forms.CheckBox();
			this.checkImageCPC = new System.Windows.Forms.CheckBox();
			this.bpReadSrc = new System.Windows.Forms.Button();
			this.bpConvert = new System.Windows.Forms.Button();
			this.nbCols = new System.Windows.Forms.NumericUpDown();
			this.nbLignes = new System.Windows.Forms.NumericUpDown();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.mode = new System.Windows.Forms.ComboBox();
			this.label3 = new System.Windows.Forms.Label();
			this.methode = new System.Windows.Forms.ComboBox();
			this.matrice = new System.Windows.Forms.ComboBox();
			this.pctTrame = new System.Windows.Forms.NumericUpDown();
			this.renderMode = new System.Windows.Forms.ComboBox();
			this.resoCPC = new System.Windows.Forms.GroupBox();
			this.modePlus = new System.Windows.Forms.CheckBox();
			this.tramage = new System.Windows.Forms.GroupBox();
			this.label6 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.lumi = new System.Windows.Forms.TrackBar();
			this.sat = new System.Windows.Forms.TrackBar();
			this.label8 = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.bpRazLumi = new System.Windows.Forms.Button();
			this.bpRazSat = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.nbCols)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.nbLignes)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pctTrame)).BeginInit();
			this.resoCPC.SuspendLayout();
			this.tramage.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.lumi)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.sat)).BeginInit();
			this.SuspendLayout();
			// 
			// checkImageSource
			// 
			this.checkImageSource.AutoSize = true;
			this.checkImageSource.Location = new System.Drawing.Point(3, 51);
			this.checkImageSource.Name = "checkImageSource";
			this.checkImageSource.Size = new System.Drawing.Size(128, 17);
			this.checkImageSource.TabIndex = 0;
			this.checkImageSource.Text = "Afficher image source";
			this.checkImageSource.UseVisualStyleBackColor = true;
			this.checkImageSource.CheckedChanged += new System.EventHandler(this.checkImageSource_CheckedChanged);
			// 
			// checkImageCPC
			// 
			this.checkImageCPC.AutoSize = true;
			this.checkImageCPC.Location = new System.Drawing.Point(3, 74);
			this.checkImageCPC.Name = "checkImageCPC";
			this.checkImageCPC.Size = new System.Drawing.Size(117, 17);
			this.checkImageCPC.TabIndex = 1;
			this.checkImageCPC.Text = "Afficher image CPC";
			this.checkImageCPC.UseVisualStyleBackColor = true;
			this.checkImageCPC.CheckedChanged += new System.EventHandler(this.checkImageCPC_CheckedChanged);
			// 
			// bpReadSrc
			// 
			this.bpReadSrc.Location = new System.Drawing.Point(3, 12);
			this.bpReadSrc.Name = "bpReadSrc";
			this.bpReadSrc.Size = new System.Drawing.Size(97, 23);
			this.bpReadSrc.TabIndex = 2;
			this.bpReadSrc.Text = "Lecture source";
			this.bpReadSrc.UseVisualStyleBackColor = true;
			this.bpReadSrc.Click += new System.EventHandler(this.bpReadSrc_Click);
			// 
			// bpConvert
			// 
			this.bpConvert.Location = new System.Drawing.Point(3, 107);
			this.bpConvert.Name = "bpConvert";
			this.bpConvert.Size = new System.Drawing.Size(97, 23);
			this.bpConvert.TabIndex = 3;
			this.bpConvert.Text = "Conversion";
			this.bpConvert.UseVisualStyleBackColor = true;
			this.bpConvert.Click += new System.EventHandler(this.bpConvert_Click);
			// 
			// nbCols
			// 
			this.nbCols.Location = new System.Drawing.Point(80, 26);
			this.nbCols.Maximum = new decimal(new int[] {
            96,
            0,
            0,
            0});
			this.nbCols.Name = "nbCols";
			this.nbCols.Size = new System.Drawing.Size(44, 20);
			this.nbCols.TabIndex = 4;
			this.nbCols.ValueChanged += new System.EventHandler(this.nbCols_ValueChanged);
			// 
			// nbLignes
			// 
			this.nbLignes.Location = new System.Drawing.Point(80, 52);
			this.nbLignes.Maximum = new decimal(new int[] {
            272,
            0,
            0,
            0});
			this.nbLignes.Name = "nbLignes";
			this.nbLignes.Size = new System.Drawing.Size(44, 20);
			this.nbLignes.TabIndex = 5;
			this.nbLignes.ValueChanged += new System.EventHandler(this.nbLignes_ValueChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(6, 28);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(68, 13);
			this.label1.TabIndex = 6;
			this.label1.Text = "Nb Colonnes";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(19, 54);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(55, 13);
			this.label2.TabIndex = 6;
			this.label2.Text = "Nb Lignes";
			// 
			// mode
			// 
			this.mode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.mode.FormattingEnabled = true;
			this.mode.Items.AddRange(new object[] {
            "0",
            "1",
            "2"});
			this.mode.Location = new System.Drawing.Point(80, 78);
			this.mode.Name = "mode";
			this.mode.Size = new System.Drawing.Size(44, 21);
			this.mode.TabIndex = 7;
			this.mode.SelectedIndexChanged += new System.EventHandler(this.mode_SelectedIndexChanged);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(40, 81);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(34, 13);
			this.label3.TabIndex = 6;
			this.label3.Text = "Mode";
			// 
			// methode
			// 
			this.methode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.methode.FormattingEnabled = true;
			this.methode.Items.AddRange(new object[] {
            "Pas de tramage",
            "Méthode 1",
            "Méthode 2",
            "Méthode 3"});
			this.methode.Location = new System.Drawing.Point(92, 19);
			this.methode.Name = "methode";
			this.methode.Size = new System.Drawing.Size(97, 21);
			this.methode.TabIndex = 8;
			// 
			// matrice
			// 
			this.matrice.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.matrice.FormattingEnabled = true;
			this.matrice.Items.AddRange(new object[] {
            "Matrice 2x2",
            "Matrice 3x3"});
			this.matrice.Location = new System.Drawing.Point(92, 72);
			this.matrice.Name = "matrice";
			this.matrice.Size = new System.Drawing.Size(97, 21);
			this.matrice.TabIndex = 8;
			// 
			// pctTrame
			// 
			this.pctTrame.Location = new System.Drawing.Point(92, 46);
			this.pctTrame.Maximum = new decimal(new int[] {
            400,
            0,
            0,
            0});
			this.pctTrame.Name = "pctTrame";
			this.pctTrame.Size = new System.Drawing.Size(70, 20);
			this.pctTrame.TabIndex = 9;
			// 
			// renderMode
			// 
			this.renderMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.renderMode.FormattingEnabled = true;
			this.renderMode.Items.AddRange(new object[] {
            "Aucun",
            "Niveau 1",
            "Niveau 2",
            "Niveau 3"});
			this.renderMode.Location = new System.Drawing.Point(92, 101);
			this.renderMode.Name = "renderMode";
			this.renderMode.Size = new System.Drawing.Size(97, 21);
			this.renderMode.TabIndex = 10;
			// 
			// resoCPC
			// 
			this.resoCPC.Controls.Add(this.modePlus);
			this.resoCPC.Controls.Add(this.label1);
			this.resoCPC.Controls.Add(this.nbCols);
			this.resoCPC.Controls.Add(this.nbLignes);
			this.resoCPC.Controls.Add(this.label2);
			this.resoCPC.Controls.Add(this.label3);
			this.resoCPC.Controls.Add(this.mode);
			this.resoCPC.Location = new System.Drawing.Point(137, 12);
			this.resoCPC.Name = "resoCPC";
			this.resoCPC.Size = new System.Drawing.Size(132, 129);
			this.resoCPC.TabIndex = 11;
			this.resoCPC.TabStop = false;
			this.resoCPC.Text = "Résolution CPC";
			// 
			// modePlus
			// 
			this.modePlus.AutoSize = true;
			this.modePlus.Location = new System.Drawing.Point(71, 106);
			this.modePlus.Name = "modePlus";
			this.modePlus.Size = new System.Drawing.Size(53, 17);
			this.modePlus.TabIndex = 8;
			this.modePlus.Text = "CPC+";
			this.modePlus.UseVisualStyleBackColor = true;
			this.modePlus.CheckedChanged += new System.EventHandler(this.modePlus_CheckedChanged);
			// 
			// tramage
			// 
			this.tramage.Controls.Add(this.label6);
			this.tramage.Controls.Add(this.methode);
			this.tramage.Controls.Add(this.label7);
			this.tramage.Controls.Add(this.matrice);
			this.tramage.Controls.Add(this.label5);
			this.tramage.Controls.Add(this.pctTrame);
			this.tramage.Controls.Add(this.label4);
			this.tramage.Controls.Add(this.renderMode);
			this.tramage.Location = new System.Drawing.Point(275, 12);
			this.tramage.Name = "tramage";
			this.tramage.Size = new System.Drawing.Size(203, 129);
			this.tramage.TabIndex = 11;
			this.tramage.TabStop = false;
			this.tramage.Text = "Tramage";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(173, 48);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(15, 13);
			this.label6.TabIndex = 12;
			this.label6.Text = "%";
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(9, 104);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(83, 13);
			this.label7.TabIndex = 12;
			this.label7.Text = "Niveau Forçage";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(50, 75);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(42, 13);
			this.label5.TabIndex = 12;
			this.label5.Text = "Matrice";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(50, 22);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(31, 13);
			this.label4.TabIndex = 12;
			this.label4.Text = "Type";
			// 
			// lumi
			// 
			this.lumi.Location = new System.Drawing.Point(137, 147);
			this.lumi.Maximum = 200;
			this.lumi.Name = "lumi";
			this.lumi.Size = new System.Drawing.Size(341, 42);
			this.lumi.TabIndex = 12;
			this.lumi.Value = 100;
			// 
			// sat
			// 
			this.sat.Location = new System.Drawing.Point(137, 185);
			this.sat.Maximum = 200;
			this.sat.Name = "sat";
			this.sat.Size = new System.Drawing.Size(341, 42);
			this.sat.TabIndex = 12;
			this.sat.Value = 100;
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(85, 164);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(57, 13);
			this.label8.TabIndex = 13;
			this.label8.Text = "Luminosité";
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Location = new System.Drawing.Point(85, 200);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(55, 13);
			this.label9.TabIndex = 13;
			this.label9.Text = "Saturation";
			// 
			// bpRazLumi
			// 
			this.bpRazLumi.Location = new System.Drawing.Point(479, 156);
			this.bpRazLumi.Name = "bpRazLumi";
			this.bpRazLumi.Size = new System.Drawing.Size(35, 23);
			this.bpRazLumi.TabIndex = 14;
			this.bpRazLumi.Text = "Raz";
			this.bpRazLumi.UseVisualStyleBackColor = true;
			this.bpRazLumi.Click += new System.EventHandler(this.bpRazLumi_Click);
			// 
			// bpRazSat
			// 
			this.bpRazSat.Location = new System.Drawing.Point(479, 190);
			this.bpRazSat.Name = "bpRazSat";
			this.bpRazSat.Size = new System.Drawing.Size(35, 23);
			this.bpRazSat.TabIndex = 14;
			this.bpRazSat.Text = "Raz";
			this.bpRazSat.UseVisualStyleBackColor = true;
			this.bpRazSat.Click += new System.EventHandler(this.bpRazSat_Click);
			// 
			// Main
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(516, 239);
			this.Controls.Add(this.bpRazSat);
			this.Controls.Add(this.bpRazLumi);
			this.Controls.Add(this.label9);
			this.Controls.Add(this.label8);
			this.Controls.Add(this.sat);
			this.Controls.Add(this.lumi);
			this.Controls.Add(this.tramage);
			this.Controls.Add(this.resoCPC);
			this.Controls.Add(this.bpConvert);
			this.Controls.Add(this.bpReadSrc);
			this.Controls.Add(this.checkImageCPC);
			this.Controls.Add(this.checkImageSource);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.Name = "Main";
			this.Text = "ConvImgCPC";
			((System.ComponentModel.ISupportInitialize)(this.nbCols)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.nbLignes)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pctTrame)).EndInit();
			this.resoCPC.ResumeLayout(false);
			this.resoCPC.PerformLayout();
			this.tramage.ResumeLayout(false);
			this.tramage.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.lumi)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.sat)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.CheckBox checkImageSource;
		private System.Windows.Forms.CheckBox checkImageCPC;
		private System.Windows.Forms.Button bpReadSrc;
		private System.Windows.Forms.Button bpConvert;
		private System.Windows.Forms.NumericUpDown nbCols;
		private System.Windows.Forms.NumericUpDown nbLignes;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ComboBox mode;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.ComboBox methode;
		private System.Windows.Forms.ComboBox matrice;
		private System.Windows.Forms.NumericUpDown pctTrame;
		private System.Windows.Forms.ComboBox renderMode;
		private System.Windows.Forms.GroupBox resoCPC;
		private System.Windows.Forms.GroupBox tramage;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.CheckBox modePlus;
		private System.Windows.Forms.TrackBar lumi;
		private System.Windows.Forms.TrackBar sat;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Button bpRazLumi;
		private System.Windows.Forms.Button bpRazSat;
	}
}

