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
			this.bpLoad = new System.Windows.Forms.Button();
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
			this.trackModeX = new System.Windows.Forms.TrackBar();
			this.bpOverscan = new System.Windows.Forms.Button();
			this.modePlus = new System.Windows.Forms.CheckBox();
			this.tramage = new System.Windows.Forms.GroupBox();
			this.chkLissage = new System.Windows.Forms.CheckBox();
			this.chkPalCpc = new System.Windows.Forms.CheckBox();
			this.chkMotif2 = new System.Windows.Forms.CheckBox();
			this.chkMotif = new System.Windows.Forms.CheckBox();
			this.lblPct = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.lumi = new System.Windows.Forms.TrackBar();
			this.sat = new System.Windows.Forms.TrackBar();
			this.label8 = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.bpRazLumi = new System.Windows.Forms.Button();
			this.bpRazSat = new System.Windows.Forms.Button();
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
			this.radioOrigin = new System.Windows.Forms.RadioButton();
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
			this.bpCtrstMoins = new System.Windows.Forms.Button();
			this.bpCtrstPlus = new System.Windows.Forms.Button();
			this.bpSatMoins = new System.Windows.Forms.Button();
			this.bpSatPlus = new System.Windows.Forms.Button();
			this.bpLumMoins = new System.Windows.Forms.Button();
			this.bpLumPlus = new System.Windows.Forms.Button();
			this.bpSave = new System.Windows.Forms.Button();
			this.lblInfoVersion = new System.Windows.Forms.Label();
			this.withCode = new System.Windows.Forms.CheckBox();
			this.withPalette = new System.Windows.Forms.CheckBox();
			this.lblNumImage = new System.Windows.Forms.Label();
			this.numImage = new System.Windows.Forms.NumericUpDown();
			this.lblMaxImage = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.nbCols)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.nbLignes)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pctTrame)).BeginInit();
			this.resoCPC.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.trackModeX)).BeginInit();
			this.tramage.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.lumi)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.sat)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.contrast)).BeginInit();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numImage)).BeginInit();
			this.SuspendLayout();
			// 
			// checkImageSource
			// 
			this.checkImageSource.AutoSize = true;
			this.checkImageSource.Location = new System.Drawing.Point(3, 41);
			this.checkImageSource.Name = "checkImageSource";
			this.checkImageSource.Size = new System.Drawing.Size(93, 30);
			this.checkImageSource.TabIndex = 0;
			this.checkImageSource.Text = "Afficher image\nsource";
			this.checkImageSource.UseVisualStyleBackColor = true;
			this.checkImageSource.CheckedChanged += new System.EventHandler(this.checkImageSource_CheckedChanged);
			// 
			// bpLoad
			// 
			this.bpLoad.Location = new System.Drawing.Point(3, 12);
			this.bpLoad.Name = "bpLoad";
			this.bpLoad.Size = new System.Drawing.Size(108, 23);
			this.bpLoad.TabIndex = 2;
			this.bpLoad.Text = "Lecture source";
			this.bpLoad.UseVisualStyleBackColor = true;
			this.bpLoad.Click += new System.EventHandler(this.bpLoad_Click);
			// 
			// bpConvert
			// 
			this.bpConvert.Location = new System.Drawing.Point(3, 140);
			this.bpConvert.Name = "bpConvert";
			this.bpConvert.Size = new System.Drawing.Size(108, 23);
			this.bpConvert.TabIndex = 3;
			this.bpConvert.Text = "Conversion";
			this.bpConvert.UseVisualStyleBackColor = true;
			this.bpConvert.Click += new System.EventHandler(this.bpConvert_Click);
			// 
			// nbCols
			// 
			this.nbCols.Location = new System.Drawing.Point(103, 21);
			this.nbCols.Maximum = new decimal(new int[] {
            96,
            0,
            0,
            0});
			this.nbCols.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.nbCols.Name = "nbCols";
			this.nbCols.Size = new System.Drawing.Size(44, 20);
			this.nbCols.TabIndex = 4;
			this.nbCols.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.nbCols.ValueChanged += new System.EventHandler(this.nbCols_ValueChanged);
			// 
			// nbLignes
			// 
			this.nbLignes.Location = new System.Drawing.Point(103, 47);
			this.nbLignes.Maximum = new decimal(new int[] {
            272,
            0,
            0,
            0});
			this.nbLignes.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.nbLignes.Name = "nbLignes";
			this.nbLignes.Size = new System.Drawing.Size(44, 20);
			this.nbLignes.TabIndex = 5;
			this.nbLignes.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.nbLignes.ValueChanged += new System.EventHandler(this.nbLignes_ValueChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(5, 23);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(68, 13);
			this.label1.TabIndex = 6;
			this.label1.Text = "Nb Colonnes";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(5, 49);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(55, 13);
			this.label2.TabIndex = 6;
			this.label2.Text = "Nb Lignes";
			// 
			// mode
			// 
			this.mode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.mode.FormattingEnabled = true;
			this.mode.Location = new System.Drawing.Point(65, 100);
			this.mode.Name = "mode";
			this.mode.Size = new System.Drawing.Size(82, 21);
			this.mode.TabIndex = 7;
			this.mode.SelectedIndexChanged += new System.EventHandler(this.mode_SelectedIndexChanged);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(3, 103);
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
			this.pctTrame.Visible = false;
			this.pctTrame.ValueChanged += new System.EventHandler(this.pctTrame_ValueChanged);
			// 
			// resoCPC
			// 
			this.resoCPC.Controls.Add(this.trackModeX);
			this.resoCPC.Controls.Add(this.bpOverscan);
			this.resoCPC.Controls.Add(this.label1);
			this.resoCPC.Controls.Add(this.nbCols);
			this.resoCPC.Controls.Add(this.nbLignes);
			this.resoCPC.Controls.Add(this.label2);
			this.resoCPC.Controls.Add(this.label3);
			this.resoCPC.Controls.Add(this.mode);
			this.resoCPC.Location = new System.Drawing.Point(117, 12);
			this.resoCPC.Name = "resoCPC";
			this.resoCPC.Size = new System.Drawing.Size(152, 172);
			this.resoCPC.TabIndex = 11;
			this.resoCPC.TabStop = false;
			this.resoCPC.Text = "Résolution CPC";
			// 
			// trackModeX
			// 
			this.trackModeX.Location = new System.Drawing.Point(3, 127);
			this.trackModeX.Maximum = 32;
			this.trackModeX.Minimum = 1;
			this.trackModeX.Name = "trackModeX";
			this.trackModeX.Size = new System.Drawing.Size(145, 42);
			this.trackModeX.TabIndex = 11;
			this.trackModeX.Value = 1;
			this.trackModeX.Visible = false;
			this.trackModeX.Scroll += new System.EventHandler(this.InterfaceChange);
			// 
			// bpOverscan
			// 
			this.bpOverscan.Location = new System.Drawing.Point(64, 71);
			this.bpOverscan.Name = "bpOverscan";
			this.bpOverscan.Size = new System.Drawing.Size(83, 22);
			this.bpOverscan.TabIndex = 10;
			this.bpOverscan.Text = "Overscan";
			this.bpOverscan.UseVisualStyleBackColor = true;
			this.bpOverscan.Click += new System.EventHandler(this.bpOverscan_Click);
			// 
			// modePlus
			// 
			this.modePlus.AutoSize = true;
			this.modePlus.Location = new System.Drawing.Point(6, 42);
			this.modePlus.Name = "modePlus";
			this.modePlus.Size = new System.Drawing.Size(53, 17);
			this.modePlus.TabIndex = 8;
			this.modePlus.Text = "CPC+";
			this.modePlus.UseVisualStyleBackColor = true;
			this.modePlus.CheckedChanged += new System.EventHandler(this.modePlus_CheckedChanged);
			// 
			// tramage
			// 
			this.tramage.Controls.Add(this.chkLissage);
			this.tramage.Controls.Add(this.chkPalCpc);
			this.tramage.Controls.Add(this.chkMotif2);
			this.tramage.Controls.Add(this.chkMotif);
			this.tramage.Controls.Add(this.lblPct);
			this.tramage.Controls.Add(this.methode);
			this.tramage.Controls.Add(this.pctTrame);
			this.tramage.Controls.Add(this.label4);
			this.tramage.Location = new System.Drawing.Point(421, 12);
			this.tramage.Name = "tramage";
			this.tramage.Size = new System.Drawing.Size(172, 172);
			this.tramage.TabIndex = 11;
			this.tramage.TabStop = false;
			this.tramage.Text = "Tramage et rendu";
			// 
			// chkLissage
			// 
			this.chkLissage.AutoSize = true;
			this.chkLissage.Location = new System.Drawing.Point(48, 105);
			this.chkLissage.Name = "chkLissage";
			this.chkLissage.Size = new System.Drawing.Size(62, 17);
			this.chkLissage.TabIndex = 15;
			this.chkLissage.Text = "Lissage";
			this.chkLissage.UseVisualStyleBackColor = true;
			this.chkLissage.CheckedChanged += new System.EventHandler(this.InterfaceChange);
			// 
			// chkPalCpc
			// 
			this.chkPalCpc.AutoSize = true;
			this.chkPalCpc.Location = new System.Drawing.Point(48, 70);
			this.chkPalCpc.Name = "chkPalCpc";
			this.chkPalCpc.Size = new System.Drawing.Size(110, 30);
			this.chkPalCpc.TabIndex = 14;
			this.chkPalCpc.Text = "Réduction palette\nimage source";
			this.chkPalCpc.UseVisualStyleBackColor = true;
			this.chkPalCpc.CheckedChanged += new System.EventHandler(this.InterfaceChange);
			// 
			// chkMotif2
			// 
			this.chkMotif2.AutoSize = true;
			this.chkMotif2.Location = new System.Drawing.Point(48, 149);
			this.chkMotif2.Name = "chkMotif2";
			this.chkMotif2.Size = new System.Drawing.Size(102, 17);
			this.chkMotif2.TabIndex = 13;
			this.chkMotif2.Text = "Trames \"motif2\"";
			this.chkMotif2.UseVisualStyleBackColor = true;
			this.chkMotif2.CheckedChanged += new System.EventHandler(this.chkMotif2_CheckedChanged);
			// 
			// chkMotif
			// 
			this.chkMotif.AutoSize = true;
			this.chkMotif.Location = new System.Drawing.Point(48, 127);
			this.chkMotif.Name = "chkMotif";
			this.chkMotif.Size = new System.Drawing.Size(96, 17);
			this.chkMotif.TabIndex = 13;
			this.chkMotif.Text = "Trames \"motif\"";
			this.chkMotif.UseVisualStyleBackColor = true;
			this.chkMotif.CheckedChanged += new System.EventHandler(this.chkMotif_CheckedChanged);
			// 
			// lblPct
			// 
			this.lblPct.AutoSize = true;
			this.lblPct.Location = new System.Drawing.Point(124, 48);
			this.lblPct.Name = "lblPct";
			this.lblPct.Size = new System.Drawing.Size(15, 13);
			this.lblPct.TabIndex = 12;
			this.lblPct.Text = "%";
			this.lblPct.Visible = false;
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
			this.lumi.Location = new System.Drawing.Point(53, 91);
			this.lumi.Maximum = 200;
			this.lumi.Name = "lumi";
			this.lumi.Size = new System.Drawing.Size(362, 42);
			this.lumi.TabIndex = 12;
			this.lumi.Value = 100;
			this.lumi.ValueChanged += new System.EventHandler(this.lumi_ValueChanged);
			// 
			// sat
			// 
			this.sat.Location = new System.Drawing.Point(53, 141);
			this.sat.Maximum = 200;
			this.sat.Name = "sat";
			this.sat.Size = new System.Drawing.Size(362, 42);
			this.sat.TabIndex = 12;
			this.sat.Value = 100;
			this.sat.ValueChanged += new System.EventHandler(this.sat_ValueChanged);
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(5, 101);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(30, 13);
			this.label8.TabIndex = 13;
			this.label8.Text = "Lum.";
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Location = new System.Drawing.Point(5, 151);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(26, 13);
			this.label9.TabIndex = 13;
			this.label9.Text = "Sat.";
			// 
			// bpRazLumi
			// 
			this.bpRazLumi.Location = new System.Drawing.Point(437, 96);
			this.bpRazLumi.Name = "bpRazLumi";
			this.bpRazLumi.Size = new System.Drawing.Size(35, 23);
			this.bpRazLumi.TabIndex = 14;
			this.bpRazLumi.Text = "Raz";
			this.bpRazLumi.UseVisualStyleBackColor = true;
			this.bpRazLumi.Click += new System.EventHandler(this.bpRazLumi_Click);
			// 
			// bpRazSat
			// 
			this.bpRazSat.Location = new System.Drawing.Point(437, 146);
			this.bpRazSat.Name = "bpRazSat";
			this.bpRazSat.Size = new System.Drawing.Size(35, 23);
			this.bpRazSat.TabIndex = 14;
			this.bpRazSat.Text = "Raz";
			this.bpRazSat.UseVisualStyleBackColor = true;
			this.bpRazSat.Click += new System.EventHandler(this.bpRazSat_Click);
			// 
			// reducPal2
			// 
			this.reducPal2.AutoSize = true;
			this.reducPal2.Location = new System.Drawing.Point(357, 42);
			this.reducPal2.Name = "reducPal2";
			this.reducPal2.Size = new System.Drawing.Size(84, 17);
			this.reducPal2.TabIndex = 47;
			this.reducPal2.Text = "Réduction 2";
			this.reducPal2.UseVisualStyleBackColor = true;
			this.reducPal2.Visible = false;
			this.reducPal2.CheckedChanged += new System.EventHandler(this.reducPal2_CheckedChanged);
			// 
			// reducPal1
			// 
			this.reducPal1.AutoSize = true;
			this.reducPal1.Location = new System.Drawing.Point(174, 42);
			this.reducPal1.Name = "reducPal1";
			this.reducPal1.Size = new System.Drawing.Size(84, 17);
			this.reducPal1.TabIndex = 46;
			this.reducPal1.Text = "Réduction 1";
			this.reducPal1.UseVisualStyleBackColor = true;
			this.reducPal1.Visible = false;
			this.reducPal1.CheckedChanged += new System.EventHandler(this.reducPal1_CheckedChanged);
			// 
			// newMethode
			// 
			this.newMethode.AutoSize = true;
			this.newMethode.Location = new System.Drawing.Point(358, 19);
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
			this.autoRecalc.Location = new System.Drawing.Point(3, 163);
			this.autoRecalc.Name = "autoRecalc";
			this.autoRecalc.Size = new System.Drawing.Size(108, 30);
			this.autoRecalc.TabIndex = 44;
			this.autoRecalc.Text = "Recalculer\r\nAutomatiquement";
			this.autoRecalc.UseVisualStyleBackColor = true;
			this.autoRecalc.CheckedChanged += new System.EventHandler(this.InterfaceChange);
			// 
			// bpRazContrast
			// 
			this.bpRazContrast.Location = new System.Drawing.Point(437, 196);
			this.bpRazContrast.Name = "bpRazContrast";
			this.bpRazContrast.Size = new System.Drawing.Size(35, 23);
			this.bpRazContrast.TabIndex = 43;
			this.bpRazContrast.Text = "Raz";
			this.bpRazContrast.UseVisualStyleBackColor = true;
			this.bpRazContrast.Click += new System.EventHandler(this.bpRazContrast_Click);
			// 
			// contrast
			// 
			this.contrast.Location = new System.Drawing.Point(53, 191);
			this.contrast.Maximum = 200;
			this.contrast.Name = "contrast";
			this.contrast.Size = new System.Drawing.Size(362, 42);
			this.contrast.TabIndex = 42;
			this.contrast.Value = 100;
			this.contrast.ValueChanged += new System.EventHandler(this.contrast_ValueChanged);
			// 
			// radioKeepLarger
			// 
			this.radioKeepLarger.AutoSize = true;
			this.radioKeepLarger.Location = new System.Drawing.Point(6, 59);
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
			this.radioKeepSmaller.Location = new System.Drawing.Point(6, 39);
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
			this.groupBox1.Controls.Add(this.radioOrigin);
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
			// radioOrigin
			// 
			this.radioOrigin.AutoSize = true;
			this.radioOrigin.Enabled = false;
			this.radioOrigin.Location = new System.Drawing.Point(6, 99);
			this.radioOrigin.Name = "radioOrigin";
			this.radioOrigin.Size = new System.Drawing.Size(92, 17);
			this.radioOrigin.TabIndex = 46;
			this.radioOrigin.TabStop = true;
			this.radioOrigin.Text = "Taille d\'origine";
			this.radioOrigin.UseVisualStyleBackColor = true;
			this.radioOrigin.CheckedChanged += new System.EventHandler(this.radioUserSize_CheckedChanged);
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(3, 149);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(50, 13);
			this.label7.TabIndex = 45;
			this.label7.Text = "Position :";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(3, 125);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(38, 13);
			this.label5.TabIndex = 45;
			this.label5.Text = "Taille :";
			// 
			// tbxPosY
			// 
			this.tbxPosY.Location = new System.Drawing.Point(100, 146);
			this.tbxPosY.Name = "tbxPosY";
			this.tbxPosY.Size = new System.Drawing.Size(34, 20);
			this.tbxPosY.TabIndex = 44;
			this.tbxPosY.Text = "0";
			this.tbxPosY.Validating += new System.ComponentModel.CancelEventHandler(this.InterfaceChange);
			// 
			// tbxSizeY
			// 
			this.tbxSizeY.Location = new System.Drawing.Point(100, 122);
			this.tbxSizeY.Name = "tbxSizeY";
			this.tbxSizeY.Size = new System.Drawing.Size(34, 20);
			this.tbxSizeY.TabIndex = 44;
			this.tbxSizeY.Validating += new System.ComponentModel.CancelEventHandler(this.InterfaceChange);
			// 
			// tbxPosX
			// 
			this.tbxPosX.Location = new System.Drawing.Point(58, 146);
			this.tbxPosX.Name = "tbxPosX";
			this.tbxPosX.Size = new System.Drawing.Size(36, 20);
			this.tbxPosX.TabIndex = 43;
			this.tbxPosX.Text = "0";
			this.tbxPosX.Validating += new System.ComponentModel.CancelEventHandler(this.InterfaceChange);
			// 
			// tbxSizeX
			// 
			this.tbxSizeX.Location = new System.Drawing.Point(58, 122);
			this.tbxSizeX.Name = "tbxSizeX";
			this.tbxSizeX.Size = new System.Drawing.Size(36, 20);
			this.tbxSizeX.TabIndex = 43;
			this.tbxSizeX.Validating += new System.ComponentModel.CancelEventHandler(this.InterfaceChange);
			// 
			// radioUserSize
			// 
			this.radioUserSize.AutoSize = true;
			this.radioUserSize.Enabled = false;
			this.radioUserSize.Location = new System.Drawing.Point(6, 79);
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
			this.label10.Size = new System.Drawing.Size(31, 13);
			this.label10.TabIndex = 13;
			this.label10.Text = "Ctrst.";
			// 
			// nb
			// 
			this.nb.AutoSize = true;
			this.nb.Location = new System.Drawing.Point(175, 19);
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
			this.sortPal.Location = new System.Drawing.Point(6, 19);
			this.sortPal.Name = "sortPal";
			this.sortPal.Size = new System.Drawing.Size(47, 17);
			this.sortPal.TabIndex = 50;
			this.sortPal.Text = "Trier";
			this.sortPal.UseVisualStyleBackColor = true;
			this.sortPal.CheckedChanged += new System.EventHandler(this.sortPal_CheckedChanged);
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.bpCtrstMoins);
			this.groupBox2.Controls.Add(this.bpCtrstPlus);
			this.groupBox2.Controls.Add(this.bpSatMoins);
			this.groupBox2.Controls.Add(this.bpSatPlus);
			this.groupBox2.Controls.Add(this.bpLumMoins);
			this.groupBox2.Controls.Add(this.bpLumPlus);
			this.groupBox2.Controls.Add(this.modePlus);
			this.groupBox2.Controls.Add(this.nb);
			this.groupBox2.Controls.Add(this.sortPal);
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
			this.groupBox2.Location = new System.Drawing.Point(117, 190);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(476, 238);
			this.groupBox2.TabIndex = 52;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Effets de palette";
			// 
			// bpCtrstMoins
			// 
			this.bpCtrstMoins.Location = new System.Drawing.Point(37, 196);
			this.bpCtrstMoins.Name = "bpCtrstMoins";
			this.bpCtrstMoins.Size = new System.Drawing.Size(23, 23);
			this.bpCtrstMoins.TabIndex = 55;
			this.bpCtrstMoins.Text = "-";
			this.bpCtrstMoins.UseVisualStyleBackColor = true;
			this.bpCtrstMoins.Click += new System.EventHandler(this.bpCtrstMoins_Click);
			// 
			// bpCtrstPlus
			// 
			this.bpCtrstPlus.Location = new System.Drawing.Point(409, 196);
			this.bpCtrstPlus.Name = "bpCtrstPlus";
			this.bpCtrstPlus.Size = new System.Drawing.Size(23, 23);
			this.bpCtrstPlus.TabIndex = 56;
			this.bpCtrstPlus.Text = "+";
			this.bpCtrstPlus.UseVisualStyleBackColor = true;
			this.bpCtrstPlus.Click += new System.EventHandler(this.bpCtrstPlus_Click);
			// 
			// bpSatMoins
			// 
			this.bpSatMoins.Location = new System.Drawing.Point(37, 146);
			this.bpSatMoins.Name = "bpSatMoins";
			this.bpSatMoins.Size = new System.Drawing.Size(23, 23);
			this.bpSatMoins.TabIndex = 53;
			this.bpSatMoins.Text = "-";
			this.bpSatMoins.UseVisualStyleBackColor = true;
			this.bpSatMoins.Click += new System.EventHandler(this.bpSatMoins_Click);
			// 
			// bpSatPlus
			// 
			this.bpSatPlus.Location = new System.Drawing.Point(409, 146);
			this.bpSatPlus.Name = "bpSatPlus";
			this.bpSatPlus.Size = new System.Drawing.Size(23, 23);
			this.bpSatPlus.TabIndex = 54;
			this.bpSatPlus.Text = "+";
			this.bpSatPlus.UseVisualStyleBackColor = true;
			this.bpSatPlus.Click += new System.EventHandler(this.bpSatPlus_Click);
			// 
			// bpLumMoins
			// 
			this.bpLumMoins.Location = new System.Drawing.Point(37, 96);
			this.bpLumMoins.Name = "bpLumMoins";
			this.bpLumMoins.Size = new System.Drawing.Size(23, 23);
			this.bpLumMoins.TabIndex = 52;
			this.bpLumMoins.Text = "-";
			this.bpLumMoins.UseVisualStyleBackColor = true;
			this.bpLumMoins.Click += new System.EventHandler(this.bpLumMoins_Click);
			// 
			// bpLumPlus
			// 
			this.bpLumPlus.Location = new System.Drawing.Point(408, 96);
			this.bpLumPlus.Name = "bpLumPlus";
			this.bpLumPlus.Size = new System.Drawing.Size(23, 23);
			this.bpLumPlus.TabIndex = 52;
			this.bpLumPlus.Text = "+";
			this.bpLumPlus.UseVisualStyleBackColor = true;
			this.bpLumPlus.Click += new System.EventHandler(this.bpLumPlus_Click);
			// 
			// bpSave
			// 
			this.bpSave.Location = new System.Drawing.Point(3, 240);
			this.bpSave.Name = "bpSave";
			this.bpSave.Size = new System.Drawing.Size(108, 23);
			this.bpSave.TabIndex = 52;
			this.bpSave.Text = "Enregistrement";
			this.bpSave.UseVisualStyleBackColor = true;
			this.bpSave.Click += new System.EventHandler(this.bpSave_Click);
			// 
			// lblInfoVersion
			// 
			this.lblInfoVersion.Location = new System.Drawing.Point(0, 381);
			this.lblInfoVersion.Name = "lblInfoVersion";
			this.lblInfoVersion.Size = new System.Drawing.Size(116, 47);
			this.lblInfoVersion.TabIndex = 52;
			// 
			// withCode
			// 
			this.withCode.AutoSize = true;
			this.withCode.Checked = true;
			this.withCode.CheckState = System.Windows.Forms.CheckState.Checked;
			this.withCode.Location = new System.Drawing.Point(3, 265);
			this.withCode.Name = "withCode";
			this.withCode.Size = new System.Drawing.Size(104, 43);
			this.withCode.TabIndex = 52;
			this.withCode.Text = "Inclure le code\r\nd\'affichage dans\r\nl\'image";
			this.withCode.UseVisualStyleBackColor = true;
			this.withCode.CheckedChanged += new System.EventHandler(this.withCode_CheckedChanged);
			// 
			// withPalette
			// 
			this.withPalette.AutoSize = true;
			this.withPalette.Checked = true;
			this.withPalette.CheckState = System.Windows.Forms.CheckState.Checked;
			this.withPalette.Location = new System.Drawing.Point(3, 314);
			this.withPalette.Name = "withPalette";
			this.withPalette.Size = new System.Drawing.Size(104, 30);
			this.withPalette.TabIndex = 52;
			this.withPalette.Text = "Inclure la palette\r\ndans l\'image";
			this.withPalette.UseVisualStyleBackColor = true;
			this.withPalette.CheckedChanged += new System.EventHandler(this.withPalette_CheckedChanged);
			// 
			// lblNumImage
			// 
			this.lblNumImage.AutoSize = true;
			this.lblNumImage.Location = new System.Drawing.Point(0, 80);
			this.lblNumImage.Name = "lblNumImage";
			this.lblNumImage.Size = new System.Drawing.Size(51, 13);
			this.lblNumImage.TabIndex = 53;
			this.lblNumImage.Text = "N° Image";
			this.lblNumImage.Visible = false;
			// 
			// numImage
			// 
			this.numImage.Location = new System.Drawing.Point(57, 76);
			this.numImage.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.numImage.Name = "numImage";
			this.numImage.Size = new System.Drawing.Size(59, 20);
			this.numImage.TabIndex = 54;
			this.numImage.Visible = false;
			this.numImage.ValueChanged += new System.EventHandler(this.numImage_ValueChanged);
			// 
			// lblMaxImage
			// 
			this.lblMaxImage.AutoSize = true;
			this.lblMaxImage.Location = new System.Drawing.Point(0, 111);
			this.lblMaxImage.Name = "lblMaxImage";
			this.lblMaxImage.Size = new System.Drawing.Size(0, 13);
			this.lblMaxImage.TabIndex = 53;
			this.lblMaxImage.Visible = false;
			// 
			// Main
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(597, 431);
			this.Controls.Add(this.numImage);
			this.Controls.Add(this.lblMaxImage);
			this.Controls.Add(this.lblNumImage);
			this.Controls.Add(this.withPalette);
			this.Controls.Add(this.withCode);
			this.Controls.Add(this.lblInfoVersion);
			this.Controls.Add(this.bpSave);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.autoRecalc);
			this.Controls.Add(this.tramage);
			this.Controls.Add(this.resoCPC);
			this.Controls.Add(this.bpConvert);
			this.Controls.Add(this.bpLoad);
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
			((System.ComponentModel.ISupportInitialize)(this.trackModeX)).EndInit();
			this.tramage.ResumeLayout(false);
			this.tramage.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.lumi)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.sat)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.contrast)).EndInit();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.numImage)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.CheckBox checkImageSource;
		private System.Windows.Forms.Button bpLoad;
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
		private System.Windows.Forms.Label lblPct;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.CheckBox modePlus;
		private System.Windows.Forms.TrackBar lumi;
		private System.Windows.Forms.TrackBar sat;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Button bpRazLumi;
		private System.Windows.Forms.Button bpRazSat;
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
		private System.Windows.Forms.Button bpSave;
		private System.Windows.Forms.Label lblInfoVersion;
		private System.Windows.Forms.RadioButton radioUserSize;
		private System.Windows.Forms.TextBox tbxSizeY;
		private System.Windows.Forms.TextBox tbxPosX;
		private System.Windows.Forms.TextBox tbxSizeX;
		private System.Windows.Forms.TextBox tbxPosY;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox withCode;
		private System.Windows.Forms.Button bpOverscan;
		private System.Windows.Forms.CheckBox withPalette;
		private System.Windows.Forms.CheckBox chkMotif;
		private System.Windows.Forms.CheckBox chkPalCpc;
		private System.Windows.Forms.CheckBox chkLissage;
		private System.Windows.Forms.CheckBox chkMotif2;
		private System.Windows.Forms.RadioButton radioOrigin;
		private System.Windows.Forms.TrackBar trackModeX;
		private System.Windows.Forms.Button bpCtrstMoins;
		private System.Windows.Forms.Button bpCtrstPlus;
		private System.Windows.Forms.Button bpSatMoins;
		private System.Windows.Forms.Button bpSatPlus;
		private System.Windows.Forms.Button bpLumMoins;
		private System.Windows.Forms.Button bpLumPlus;
		private System.Windows.Forms.Label lblNumImage;
		private System.Windows.Forms.NumericUpDown numImage;
		private System.Windows.Forms.Label lblMaxImage;
	}
}

