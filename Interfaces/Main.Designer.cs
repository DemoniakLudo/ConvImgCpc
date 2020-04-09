namespace ConvImgCpc {
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
			this.bpReadSrc = new System.Windows.Forms.Button();
			this.bpConvert = new System.Windows.Forms.Button();
			this.nbCols = new System.Windows.Forms.NumericUpDown();
			this.nbLignes = new System.Windows.Forms.NumericUpDown();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.mode = new System.Windows.Forms.ComboBox();
			this.label3 = new System.Windows.Forms.Label();
			this.methode = new System.Windows.Forms.ComboBox();
			this.pctTrame = new System.Windows.Forms.NumericUpDown();
			this.resoCPC = new System.Windows.Forms.GroupBox();
			this.chkOverscan = new System.Windows.Forms.CheckBox();
			this.modePlus = new System.Windows.Forms.CheckBox();
			this.tramage = new System.Windows.Forms.GroupBox();
			this.label6 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.lumi = new System.Windows.Forms.TrackBar();
			this.sat = new System.Windows.Forms.TrackBar();
			this.label8 = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.bpRazLumi = new System.Windows.Forms.Button();
			this.bpRazSat = new System.Windows.Forms.Button();
			this.newReduc = new System.Windows.Forms.CheckBox();
			this.reducPal2 = new System.Windows.Forms.CheckBox();
			this.reducPal1 = new System.Windows.Forms.CheckBox();
			this.newMethode = new System.Windows.Forms.CheckBox();
			this.autoRecalc = new System.Windows.Forms.CheckBox();
			this.bpRazContrast = new System.Windows.Forms.Button();
			this.contrast = new System.Windows.Forms.TrackBar();
			this.radioKeepLarger = new System.Windows.Forms.RadioButton();
			this.radioKeepSmaller = new System.Windows.Forms.RadioButton();
			this.radioFit = new System.Windows.Forms.RadioButton();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.label7 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.tbxPosY = new System.Windows.Forms.TextBox();
			this.tbxSizeY = new System.Windows.Forms.TextBox();
			this.tbxPosX = new System.Windows.Forms.TextBox();
			this.tbxSizeX = new System.Windows.Forms.TextBox();
			this.radioUserSize = new System.Windows.Forms.RadioButton();
			this.label10 = new System.Windows.Forms.Label();
			this.nb = new System.Windows.Forms.CheckBox();
			this.sortPal = new System.Windows.Forms.CheckBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.bpLoadParam = new System.Windows.Forms.Button();
			this.bpSaveParam = new System.Windows.Forms.Button();
			this.bpSaveImage = new System.Windows.Forms.Button();
			this.lblInfoVersion = new System.Windows.Forms.Label();
			this.trackModeX = new System.Windows.Forms.TrackBar();
			((System.ComponentModel.ISupportInitialize)(this.nbCols)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.nbLignes)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pctTrame)).BeginInit();
			this.resoCPC.SuspendLayout();
			this.tramage.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.lumi)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.sat)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.contrast)).BeginInit();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.trackModeX)).BeginInit();
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
			// bpReadSrc
			// 
			this.bpReadSrc.Location = new System.Drawing.Point(3, 12);
			this.bpReadSrc.Name = "bpReadSrc";
			this.bpReadSrc.Size = new System.Drawing.Size(108, 23);
			this.bpReadSrc.TabIndex = 2;
			this.bpReadSrc.Text = "Lecture source";
			this.bpReadSrc.UseVisualStyleBackColor = true;
			this.bpReadSrc.Click += new System.EventHandler(this.bpReadSrc_Click);
			// 
			// bpConvert
			// 
			this.bpConvert.Location = new System.Drawing.Point(3, 107);
			this.bpConvert.Name = "bpConvert";
			this.bpConvert.Size = new System.Drawing.Size(108, 23);
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
			this.label1.Location = new System.Drawing.Point(5, 28);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(68, 13);
			this.label1.TabIndex = 6;
			this.label1.Text = "Nb Colonnes";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(5, 54);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(55, 13);
			this.label2.TabIndex = 6;
			this.label2.Text = "Nb Lignes";
			// 
			// mode
			// 
			this.mode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.mode.FormattingEnabled = true;
			this.mode.Location = new System.Drawing.Point(49, 100);
			this.mode.Name = "mode";
			this.mode.Size = new System.Drawing.Size(78, 21);
			this.mode.TabIndex = 7;
			this.mode.SelectedIndexChanged += new System.EventHandler(this.mode_SelectedIndexChanged);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(6, 103);
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
            "Aucun",
            "Floyd-Steinberg (2x2)",
            "Bayer 1 (2X2)",
            "Bayer 2 (4x4)",
            "Bayer 3 (4X4)",
            "Ordered 1 (2x2)",
            "Ordered 2 (3x3)",
            "Ordered 3 (3x3)",
            "Ordered 4 (4x4)",
            "ZigZag1 (3x3)",
            "ZigZag2 (4x3)",
            "ZigZag3 (5x4)",
            "Test"});
			this.methode.Location = new System.Drawing.Point(48, 19);
			this.methode.Name = "methode";
			this.methode.Size = new System.Drawing.Size(117, 21);
			this.methode.TabIndex = 8;
			this.methode.SelectedIndexChanged += new System.EventHandler(this.InterfaceChange);
			// 
			// pctTrame
			// 
			this.pctTrame.Location = new System.Drawing.Point(48, 46);
			this.pctTrame.Maximum = new decimal(new int[] {
            400,
            0,
            0,
            0});
			this.pctTrame.Name = "pctTrame";
			this.pctTrame.Size = new System.Drawing.Size(70, 20);
			this.pctTrame.TabIndex = 9;
			this.pctTrame.ValueChanged += new System.EventHandler(this.pctTrame_ValueChanged);
			// 
			// resoCPC
			// 
			this.resoCPC.Controls.Add(this.trackModeX);
			this.resoCPC.Controls.Add(this.chkOverscan);
			this.resoCPC.Controls.Add(this.label1);
			this.resoCPC.Controls.Add(this.nbCols);
			this.resoCPC.Controls.Add(this.nbLignes);
			this.resoCPC.Controls.Add(this.label2);
			this.resoCPC.Controls.Add(this.label3);
			this.resoCPC.Controls.Add(this.mode);
			this.resoCPC.Location = new System.Drawing.Point(137, 12);
			this.resoCPC.Name = "resoCPC";
			this.resoCPC.Size = new System.Drawing.Size(132, 172);
			this.resoCPC.TabIndex = 11;
			this.resoCPC.TabStop = false;
			this.resoCPC.Text = "Résolution CPC";
			// 
			// chkOverscan
			// 
			this.chkOverscan.AutoSize = true;
			this.chkOverscan.Location = new System.Drawing.Point(49, 78);
			this.chkOverscan.Name = "chkOverscan";
			this.chkOverscan.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.chkOverscan.Size = new System.Drawing.Size(72, 17);
			this.chkOverscan.TabIndex = 8;
			this.chkOverscan.Text = "Overscan";
			this.chkOverscan.UseVisualStyleBackColor = true;
			this.chkOverscan.CheckedChanged += new System.EventHandler(this.chkOverscan_CheckedChanged);
			// 
			// modePlus
			// 
			this.modePlus.AutoSize = true;
			this.modePlus.Location = new System.Drawing.Point(6, 19);
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
			this.tramage.Controls.Add(this.pctTrame);
			this.tramage.Controls.Add(this.label4);
			this.tramage.Location = new System.Drawing.Point(421, 12);
			this.tramage.Name = "tramage";
			this.tramage.Size = new System.Drawing.Size(172, 172);
			this.tramage.TabIndex = 11;
			this.tramage.TabStop = false;
			this.tramage.Text = "Tramage";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(129, 48);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(15, 13);
			this.label6.TabIndex = 12;
			this.label6.Text = "%";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(6, 22);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(31, 13);
			this.label4.TabIndex = 12;
			this.label4.Text = "Type";
			// 
			// lumi
			// 
			this.lumi.Location = new System.Drawing.Point(59, 91);
			this.lumi.Maximum = 200;
			this.lumi.Name = "lumi";
			this.lumi.Size = new System.Drawing.Size(360, 45);
			this.lumi.TabIndex = 12;
			this.lumi.Value = 100;
			this.lumi.ValueChanged += new System.EventHandler(this.lumi_ValueChanged);
			// 
			// sat
			// 
			this.sat.Location = new System.Drawing.Point(59, 141);
			this.sat.Maximum = 200;
			this.sat.Name = "sat";
			this.sat.Size = new System.Drawing.Size(360, 45);
			this.sat.TabIndex = 12;
			this.sat.Value = 100;
			this.sat.ValueChanged += new System.EventHandler(this.sat_ValueChanged);
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(5, 101);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(57, 13);
			this.label8.TabIndex = 13;
			this.label8.Text = "Luminosité";
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Location = new System.Drawing.Point(5, 151);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(55, 13);
			this.label9.TabIndex = 13;
			this.label9.Text = "Saturation";
			// 
			// bpRazLumi
			// 
			this.bpRazLumi.Location = new System.Drawing.Point(415, 96);
			this.bpRazLumi.Name = "bpRazLumi";
			this.bpRazLumi.Size = new System.Drawing.Size(35, 23);
			this.bpRazLumi.TabIndex = 14;
			this.bpRazLumi.Text = "Raz";
			this.bpRazLumi.UseVisualStyleBackColor = true;
			this.bpRazLumi.Click += new System.EventHandler(this.bpRazLumi_Click);
			// 
			// bpRazSat
			// 
			this.bpRazSat.Location = new System.Drawing.Point(415, 146);
			this.bpRazSat.Name = "bpRazSat";
			this.bpRazSat.Size = new System.Drawing.Size(35, 23);
			this.bpRazSat.TabIndex = 14;
			this.bpRazSat.Text = "Raz";
			this.bpRazSat.UseVisualStyleBackColor = true;
			this.bpRazSat.Click += new System.EventHandler(this.bpRazSat_Click);
			// 
			// newReduc
			// 
			this.newReduc.AutoSize = true;
			this.newReduc.Enabled = false;
			this.newReduc.Location = new System.Drawing.Point(357, 67);
			this.newReduc.Name = "newReduc";
			this.newReduc.Size = new System.Drawing.Size(84, 17);
			this.newReduc.TabIndex = 48;
			this.newReduc.Text = "Réduction 3";
			this.newReduc.UseVisualStyleBackColor = true;
			this.newReduc.CheckedChanged += new System.EventHandler(this.newReduc_CheckedChanged);
			// 
			// reducPal2
			// 
			this.reducPal2.AutoSize = true;
			this.reducPal2.Enabled = false;
			this.reducPal2.Location = new System.Drawing.Point(174, 67);
			this.reducPal2.Name = "reducPal2";
			this.reducPal2.Size = new System.Drawing.Size(84, 17);
			this.reducPal2.TabIndex = 47;
			this.reducPal2.Text = "Réduction 2";
			this.reducPal2.UseVisualStyleBackColor = true;
			this.reducPal2.CheckedChanged += new System.EventHandler(this.reducPal2_CheckedChanged);
			// 
			// reducPal1
			// 
			this.reducPal1.AutoSize = true;
			this.reducPal1.Enabled = false;
			this.reducPal1.Location = new System.Drawing.Point(6, 67);
			this.reducPal1.Name = "reducPal1";
			this.reducPal1.Size = new System.Drawing.Size(84, 17);
			this.reducPal1.TabIndex = 46;
			this.reducPal1.Text = "Réduction 1";
			this.reducPal1.UseVisualStyleBackColor = true;
			this.reducPal1.CheckedChanged += new System.EventHandler(this.reducPal1_CheckedChanged);
			// 
			// newMethode
			// 
			this.newMethode.AutoSize = true;
			this.newMethode.Location = new System.Drawing.Point(175, 44);
			this.newMethode.Name = "newMethode";
			this.newMethode.Size = new System.Drawing.Size(83, 17);
			this.newMethode.TabIndex = 45;
			this.newMethode.Text = "Plus précise";
			this.newMethode.UseVisualStyleBackColor = true;
			this.newMethode.CheckedChanged += new System.EventHandler(this.newMethode_CheckedChanged);
			// 
			// autoRecalc
			// 
			this.autoRecalc.AutoSize = true;
			this.autoRecalc.Location = new System.Drawing.Point(3, 136);
			this.autoRecalc.Name = "autoRecalc";
			this.autoRecalc.Size = new System.Drawing.Size(108, 30);
			this.autoRecalc.TabIndex = 44;
			this.autoRecalc.Text = "Recalculer\r\nAutomatiquement";
			this.autoRecalc.UseVisualStyleBackColor = true;
			this.autoRecalc.CheckedChanged += new System.EventHandler(this.InterfaceChange);
			// 
			// bpRazContrast
			// 
			this.bpRazContrast.Location = new System.Drawing.Point(415, 196);
			this.bpRazContrast.Name = "bpRazContrast";
			this.bpRazContrast.Size = new System.Drawing.Size(35, 23);
			this.bpRazContrast.TabIndex = 43;
			this.bpRazContrast.Text = "Raz";
			this.bpRazContrast.UseVisualStyleBackColor = true;
			this.bpRazContrast.Click += new System.EventHandler(this.bpRazContrast_Click);
			// 
			// contrast
			// 
			this.contrast.Location = new System.Drawing.Point(59, 191);
			this.contrast.Maximum = 200;
			this.contrast.Name = "contrast";
			this.contrast.Size = new System.Drawing.Size(360, 45);
			this.contrast.TabIndex = 42;
			this.contrast.Value = 100;
			this.contrast.ValueChanged += new System.EventHandler(this.contrast_ValueChanged);
			// 
			// radioKeepLarger
			// 
			this.radioKeepLarger.AutoSize = true;
			this.radioKeepLarger.Location = new System.Drawing.Point(6, 65);
			this.radioKeepLarger.Name = "radioKeepLarger";
			this.radioKeepLarger.Size = new System.Drawing.Size(80, 17);
			this.radioKeepLarger.TabIndex = 41;
			this.radioKeepLarger.Text = "KeepLarger";
			this.radioKeepLarger.UseVisualStyleBackColor = true;
			this.radioKeepLarger.CheckedChanged += new System.EventHandler(this.InterfaceChange);
			// 
			// radioKeepSmaller
			// 
			this.radioKeepSmaller.AutoSize = true;
			this.radioKeepSmaller.Location = new System.Drawing.Point(6, 42);
			this.radioKeepSmaller.Name = "radioKeepSmaller";
			this.radioKeepSmaller.Size = new System.Drawing.Size(84, 17);
			this.radioKeepSmaller.TabIndex = 40;
			this.radioKeepSmaller.Text = "KeepSmaller";
			this.radioKeepSmaller.UseVisualStyleBackColor = true;
			this.radioKeepSmaller.CheckedChanged += new System.EventHandler(this.InterfaceChange);
			// 
			// radioFit
			// 
			this.radioFit.AutoSize = true;
			this.radioFit.Checked = true;
			this.radioFit.Location = new System.Drawing.Point(6, 19);
			this.radioFit.Name = "radioFit";
			this.radioFit.Size = new System.Drawing.Size(36, 17);
			this.radioFit.TabIndex = 39;
			this.radioFit.TabStop = true;
			this.radioFit.Text = "Fit";
			this.radioFit.UseVisualStyleBackColor = true;
			this.radioFit.CheckedChanged += new System.EventHandler(this.InterfaceChange);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.label7);
			this.groupBox1.Controls.Add(this.label5);
			this.groupBox1.Controls.Add(this.tbxPosY);
			this.groupBox1.Controls.Add(this.tbxSizeY);
			this.groupBox1.Controls.Add(this.tbxPosX);
			this.groupBox1.Controls.Add(this.tbxSizeX);
			this.groupBox1.Controls.Add(this.radioUserSize);
			this.groupBox1.Controls.Add(this.radioFit);
			this.groupBox1.Controls.Add(this.radioKeepSmaller);
			this.groupBox1.Controls.Add(this.radioKeepLarger);
			this.groupBox1.Location = new System.Drawing.Point(275, 12);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(140, 172);
			this.groupBox1.TabIndex = 49;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Taille image source";
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(0, 140);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(50, 13);
			this.label7.TabIndex = 45;
			this.label7.Text = "Position :";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(0, 116);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(38, 13);
			this.label5.TabIndex = 45;
			this.label5.Text = "Taille :";
			// 
			// tbxPosY
			// 
			this.tbxPosY.Location = new System.Drawing.Point(100, 137);
			this.tbxPosY.Name = "tbxPosY";
			this.tbxPosY.Size = new System.Drawing.Size(34, 20);
			this.tbxPosY.TabIndex = 44;
			this.tbxPosY.Text = "0";
			// 
			// tbxSizeY
			// 
			this.tbxSizeY.Location = new System.Drawing.Point(100, 113);
			this.tbxSizeY.Name = "tbxSizeY";
			this.tbxSizeY.Size = new System.Drawing.Size(34, 20);
			this.tbxSizeY.TabIndex = 44;
			// 
			// tbxPosX
			// 
			this.tbxPosX.Location = new System.Drawing.Point(58, 137);
			this.tbxPosX.Name = "tbxPosX";
			this.tbxPosX.Size = new System.Drawing.Size(36, 20);
			this.tbxPosX.TabIndex = 43;
			this.tbxPosX.Text = "0";
			// 
			// tbxSizeX
			// 
			this.tbxSizeX.Location = new System.Drawing.Point(58, 113);
			this.tbxSizeX.Name = "tbxSizeX";
			this.tbxSizeX.Size = new System.Drawing.Size(36, 20);
			this.tbxSizeX.TabIndex = 43;
			// 
			// radioUserSize
			// 
			this.radioUserSize.AutoSize = true;
			this.radioUserSize.Location = new System.Drawing.Point(6, 88);
			this.radioUserSize.Name = "radioUserSize";
			this.radioUserSize.Size = new System.Drawing.Size(97, 17);
			this.radioUserSize.TabIndex = 42;
			this.radioUserSize.TabStop = true;
			this.radioUserSize.Text = "Taille utilisateur";
			this.radioUserSize.UseVisualStyleBackColor = true;
			this.radioUserSize.CheckedChanged += new System.EventHandler(this.radioUserSize_CheckedChanged);
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.Location = new System.Drawing.Point(5, 201);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(52, 13);
			this.label10.TabIndex = 13;
			this.label10.Text = "Contraste";
			// 
			// nb
			// 
			this.nb.AutoSize = true;
			this.nb.Location = new System.Drawing.Point(6, 44);
			this.nb.Name = "nb";
			this.nb.Size = new System.Drawing.Size(83, 17);
			this.nb.TabIndex = 51;
			this.nb.Text = "Noir && blanc";
			this.nb.UseVisualStyleBackColor = true;
			this.nb.CheckedChanged += new System.EventHandler(this.nb_CheckedChanged);
			// 
			// sortPal
			// 
			this.sortPal.AutoSize = true;
			this.sortPal.Location = new System.Drawing.Point(358, 44);
			this.sortPal.Name = "sortPal";
			this.sortPal.Size = new System.Drawing.Size(47, 17);
			this.sortPal.TabIndex = 50;
			this.sortPal.Text = "Trier";
			this.sortPal.UseVisualStyleBackColor = true;
			this.sortPal.CheckedChanged += new System.EventHandler(this.sortPal_CheckedChanged);
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.modePlus);
			this.groupBox2.Controls.Add(this.nb);
			this.groupBox2.Controls.Add(this.sortPal);
			this.groupBox2.Controls.Add(this.newReduc);
			this.groupBox2.Controls.Add(this.bpRazContrast);
			this.groupBox2.Controls.Add(this.reducPal2);
			this.groupBox2.Controls.Add(this.contrast);
			this.groupBox2.Controls.Add(this.reducPal1);
			this.groupBox2.Controls.Add(this.bpRazSat);
			this.groupBox2.Controls.Add(this.newMethode);
			this.groupBox2.Controls.Add(this.bpRazLumi);
			this.groupBox2.Controls.Add(this.label10);
			this.groupBox2.Controls.Add(this.lumi);
			this.groupBox2.Controls.Add(this.label9);
			this.groupBox2.Controls.Add(this.sat);
			this.groupBox2.Controls.Add(this.label8);
			this.groupBox2.Location = new System.Drawing.Point(137, 190);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(456, 238);
			this.groupBox2.TabIndex = 52;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Effets de palette";
			// 
			// bpLoadParam
			// 
			this.bpLoadParam.Location = new System.Drawing.Point(3, 248);
			this.bpLoadParam.Name = "bpLoadParam";
			this.bpLoadParam.Size = new System.Drawing.Size(108, 23);
			this.bpLoadParam.TabIndex = 53;
			this.bpLoadParam.Text = "Lire paramètres";
			this.bpLoadParam.UseVisualStyleBackColor = true;
			this.bpLoadParam.Click += new System.EventHandler(this.bpLoadParam_Click);
			// 
			// bpSaveParam
			// 
			this.bpSaveParam.Location = new System.Drawing.Point(3, 277);
			this.bpSaveParam.Name = "bpSaveParam";
			this.bpSaveParam.Size = new System.Drawing.Size(108, 23);
			this.bpSaveParam.TabIndex = 53;
			this.bpSaveParam.Text = "Sauver paramètres";
			this.bpSaveParam.UseVisualStyleBackColor = true;
			this.bpSaveParam.Click += new System.EventHandler(this.bpSaveParam_Click);
			// 
			// bpSaveImage
			// 
			this.bpSaveImage.Enabled = false;
			this.bpSaveImage.Location = new System.Drawing.Point(3, 177);
			this.bpSaveImage.Name = "bpSaveImage";
			this.bpSaveImage.Size = new System.Drawing.Size(108, 23);
			this.bpSaveImage.TabIndex = 52;
			this.bpSaveImage.Text = "Sauver image";
			this.bpSaveImage.UseVisualStyleBackColor = true;
			this.bpSaveImage.Click += new System.EventHandler(this.bpSaveImage_Click);
			// 
			// lblInfoVersion
			// 
			this.lblInfoVersion.AutoSize = true;
			this.lblInfoVersion.Location = new System.Drawing.Point(0, 364);
			this.lblInfoVersion.Name = "lblInfoVersion";
			this.lblInfoVersion.Size = new System.Drawing.Size(0, 13);
			this.lblInfoVersion.TabIndex = 52;
			// 
			// trackModeX
			// 
			this.trackModeX.Location = new System.Drawing.Point(0, 127);
			this.trackModeX.Maximum = 200;
			this.trackModeX.Name = "trackModeX";
			this.trackModeX.Size = new System.Drawing.Size(132, 45);
			this.trackModeX.TabIndex = 9;
			this.trackModeX.Visible = false;
			this.trackModeX.Scroll += new System.EventHandler(this.trackModeX_Scroll);
			// 
			// Main
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(597, 431);
			this.Controls.Add(this.lblInfoVersion);
			this.Controls.Add(this.bpSaveImage);
			this.Controls.Add(this.bpSaveParam);
			this.Controls.Add(this.bpLoadParam);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.autoRecalc);
			this.Controls.Add(this.tramage);
			this.Controls.Add(this.resoCPC);
			this.Controls.Add(this.bpConvert);
			this.Controls.Add(this.bpReadSrc);
			this.Controls.Add(this.checkImageSource);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
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
			((System.ComponentModel.ISupportInitialize)(this.contrast)).EndInit();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.trackModeX)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.CheckBox checkImageSource;
		private System.Windows.Forms.Button bpReadSrc;
		private System.Windows.Forms.Button bpConvert;
		private System.Windows.Forms.NumericUpDown nbCols;
		private System.Windows.Forms.NumericUpDown nbLignes;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ComboBox mode;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.ComboBox methode;
		private System.Windows.Forms.NumericUpDown pctTrame;
		private System.Windows.Forms.GroupBox resoCPC;
		private System.Windows.Forms.GroupBox tramage;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.CheckBox modePlus;
		private System.Windows.Forms.TrackBar lumi;
		private System.Windows.Forms.TrackBar sat;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Button bpRazLumi;
		private System.Windows.Forms.Button bpRazSat;
		private System.Windows.Forms.CheckBox newReduc;
		private System.Windows.Forms.CheckBox reducPal2;
		private System.Windows.Forms.CheckBox reducPal1;
		private System.Windows.Forms.CheckBox newMethode;
		private System.Windows.Forms.CheckBox autoRecalc;
		private System.Windows.Forms.Button bpRazContrast;
		private System.Windows.Forms.TrackBar contrast;
		private System.Windows.Forms.RadioButton radioKeepLarger;
		private System.Windows.Forms.RadioButton radioKeepSmaller;
		private System.Windows.Forms.RadioButton radioFit;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.CheckBox nb;
		private System.Windows.Forms.CheckBox sortPal;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Button bpLoadParam;
		private System.Windows.Forms.Button bpSaveParam;
		private System.Windows.Forms.Button bpSaveImage;
		private System.Windows.Forms.CheckBox chkOverscan;
		private System.Windows.Forms.Label lblInfoVersion;
		private System.Windows.Forms.RadioButton radioUserSize;
		private System.Windows.Forms.TextBox tbxSizeY;
		private System.Windows.Forms.TextBox tbxPosX;
		private System.Windows.Forms.TextBox tbxSizeX;
		private System.Windows.Forms.TextBox tbxPosY;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TrackBar trackModeX;
	}
}

