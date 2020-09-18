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
			this.bpCalcSprite = new System.Windows.Forms.Button();
			this.bpEditTrame = new System.Windows.Forms.Button();
			this.trackModeX = new System.Windows.Forms.TrackBar();
			this.bpStandard = new System.Windows.Forms.Button();
			this.bpOverscan = new System.Windows.Forms.Button();
			this.tramage = new System.Windows.Forms.GroupBox();
			this.chkTrameTC = new System.Windows.Forms.CheckBox();
			this.chkLissage = new System.Windows.Forms.CheckBox();
			this.chkPalCpc = new System.Windows.Forms.CheckBox();
			this.lblPct = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.autoRecalc = new System.Windows.Forms.CheckBox();
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
			this.bpSave = new System.Windows.Forms.Button();
			this.lblInfoVersion = new System.Windows.Forms.Label();
			this.withCode = new System.Windows.Forms.CheckBox();
			this.withPalette = new System.Windows.Forms.CheckBox();
			this.chkAllPics = new System.Windows.Forms.CheckBox();
			this.bpImport = new System.Windows.Forms.Button();
			this.bpCreate = new System.Windows.Forms.Button();
			this.chkInfo = new System.Windows.Forms.CheckBox();
			this.chkCouleur = new System.Windows.Forms.CheckBox();
			this.chkParamInterne = new System.Windows.Forms.CheckBox();
			((System.ComponentModel.ISupportInitialize)(this.nbCols)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.nbLignes)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pctTrame)).BeginInit();
			this.resoCPC.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.trackModeX)).BeginInit();
			this.tramage.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// bpLoad
			// 
			this.bpLoad.Location = new System.Drawing.Point(2, 2);
			this.bpLoad.Name = "bpLoad";
			this.bpLoad.Size = new System.Drawing.Size(108, 23);
			this.bpLoad.TabIndex = 2;
			this.bpLoad.Text = "Lecture";
			this.bpLoad.UseVisualStyleBackColor = true;
			this.bpLoad.Click += new System.EventHandler(this.bpLoad_Click);
			// 
			// bpConvert
			// 
			this.bpConvert.Location = new System.Drawing.Point(3, 59);
			this.bpConvert.Name = "bpConvert";
			this.bpConvert.Size = new System.Drawing.Size(108, 23);
			this.bpConvert.TabIndex = 3;
			this.bpConvert.Text = "Conversion";
			this.bpConvert.UseVisualStyleBackColor = true;
			this.bpConvert.Click += new System.EventHandler(this.bpConvert_Click);
			// 
			// nbCols
			// 
			this.nbCols.Location = new System.Drawing.Point(103, 16);
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
			this.nbLignes.Location = new System.Drawing.Point(103, 42);
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
			this.label1.Location = new System.Drawing.Point(5, 18);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(68, 13);
			this.label1.TabIndex = 6;
			this.label1.Text = "Nb Colonnes";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(5, 44);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(55, 13);
			this.label2.TabIndex = 6;
			this.label2.Text = "Nb Lignes";
			// 
			// mode
			// 
			this.mode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.mode.FormattingEnabled = true;
			this.mode.Location = new System.Drawing.Point(65, 121);
			this.mode.Name = "mode";
			this.mode.Size = new System.Drawing.Size(82, 21);
			this.mode.TabIndex = 7;
			this.mode.SelectedIndexChanged += new System.EventHandler(this.mode_SelectedIndexChanged);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(3, 124);
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
            "Ordered 3 (4x4)",
            "Ordered 4 (8x8)",
            "ZigZag1 (3x3)",
            "ZigZag2 (4x3)",
            "ZigZag3 (5x4)",
            "Test0",
            "Test1",
            "Test2",
            "Test3",
            "Test4",
            "Test5",
            "Test6",
            "Test7",
            "Test8",
            "Test9"});
			this.methode.Location = new System.Drawing.Point(48, 14);
			this.methode.Name = "methode";
			this.methode.Size = new System.Drawing.Size(117, 21);
			this.methode.TabIndex = 8;
			this.methode.SelectedIndexChanged += new System.EventHandler(this.InterfaceChange);
			// 
			// pctTrame
			// 
			this.pctTrame.Location = new System.Drawing.Point(48, 41);
			this.pctTrame.Maximum = new decimal(new int[] {
            400,
            0,
            0,
            0});
			this.pctTrame.Name = "pctTrame";
			this.pctTrame.Size = new System.Drawing.Size(70, 20);
			this.pctTrame.TabIndex = 9;
			this.pctTrame.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
			this.pctTrame.Visible = false;
			this.pctTrame.ValueChanged += new System.EventHandler(this.pctTrame_ValueChanged);
			// 
			// resoCPC
			// 
			this.resoCPC.Controls.Add(this.bpCalcSprite);
			this.resoCPC.Controls.Add(this.bpEditTrame);
			this.resoCPC.Controls.Add(this.trackModeX);
			this.resoCPC.Controls.Add(this.bpStandard);
			this.resoCPC.Controls.Add(this.bpOverscan);
			this.resoCPC.Controls.Add(this.label1);
			this.resoCPC.Controls.Add(this.nbCols);
			this.resoCPC.Controls.Add(this.nbLignes);
			this.resoCPC.Controls.Add(this.label2);
			this.resoCPC.Controls.Add(this.label3);
			this.resoCPC.Controls.Add(this.mode);
			this.resoCPC.Location = new System.Drawing.Point(237, 2);
			this.resoCPC.Name = "resoCPC";
			this.resoCPC.Size = new System.Drawing.Size(152, 203);
			this.resoCPC.TabIndex = 11;
			this.resoCPC.TabStop = false;
			this.resoCPC.Text = "Résolution CPC";
			// 
			// bpCalcSprite
			// 
			this.bpCalcSprite.Location = new System.Drawing.Point(8, 92);
			this.bpCalcSprite.Name = "bpCalcSprite";
			this.bpCalcSprite.Size = new System.Drawing.Size(138, 23);
			this.bpCalcSprite.TabIndex = 13;
			this.bpCalcSprite.Text = "Calcul auto. sprite";
			this.bpCalcSprite.UseVisualStyleBackColor = true;
			this.bpCalcSprite.Visible = false;
			this.bpCalcSprite.Click += new System.EventHandler(this.bpCalcSprite_Click);
			// 
			// bpEditTrame
			// 
			this.bpEditTrame.Location = new System.Drawing.Point(53, 148);
			this.bpEditTrame.Name = "bpEditTrame";
			this.bpEditTrame.Size = new System.Drawing.Size(93, 23);
			this.bpEditTrame.TabIndex = 12;
			this.bpEditTrame.Text = "Edition trames";
			this.bpEditTrame.UseVisualStyleBackColor = true;
			this.bpEditTrame.Visible = false;
			this.bpEditTrame.Click += new System.EventHandler(this.bpEditTrame_Click);
			// 
			// trackModeX
			// 
			this.trackModeX.Location = new System.Drawing.Point(3, 148);
			this.trackModeX.Maximum = 32;
			this.trackModeX.Minimum = 1;
			this.trackModeX.Name = "trackModeX";
			this.trackModeX.Size = new System.Drawing.Size(145, 45);
			this.trackModeX.TabIndex = 11;
			this.trackModeX.Value = 1;
			this.trackModeX.Visible = false;
			this.trackModeX.Scroll += new System.EventHandler(this.InterfaceChange);
			// 
			// bpStandard
			// 
			this.bpStandard.Location = new System.Drawing.Point(8, 66);
			this.bpStandard.Name = "bpStandard";
			this.bpStandard.Size = new System.Drawing.Size(62, 22);
			this.bpStandard.TabIndex = 10;
			this.bpStandard.Text = "Standard";
			this.bpStandard.UseVisualStyleBackColor = true;
			this.bpStandard.Click += new System.EventHandler(this.bpStandard_Click);
			// 
			// bpOverscan
			// 
			this.bpOverscan.Location = new System.Drawing.Point(85, 66);
			this.bpOverscan.Name = "bpOverscan";
			this.bpOverscan.Size = new System.Drawing.Size(62, 22);
			this.bpOverscan.TabIndex = 10;
			this.bpOverscan.Text = "Overscan";
			this.bpOverscan.UseVisualStyleBackColor = true;
			this.bpOverscan.Click += new System.EventHandler(this.bpOverscan_Click);
			// 
			// tramage
			// 
			this.tramage.Controls.Add(this.chkTrameTC);
			this.tramage.Controls.Add(this.chkLissage);
			this.tramage.Controls.Add(this.chkPalCpc);
			this.tramage.Controls.Add(this.lblPct);
			this.tramage.Controls.Add(this.methode);
			this.tramage.Controls.Add(this.pctTrame);
			this.tramage.Controls.Add(this.label4);
			this.tramage.Location = new System.Drawing.Point(541, 2);
			this.tramage.Name = "tramage";
			this.tramage.Size = new System.Drawing.Size(172, 203);
			this.tramage.TabIndex = 11;
			this.tramage.TabStop = false;
			this.tramage.Text = "Tramage et rendu";
			// 
			// chkTrameTC
			// 
			this.chkTrameTC.AutoSize = true;
			this.chkTrameTC.Location = new System.Drawing.Point(48, 125);
			this.chkTrameTC.Name = "chkTrameTC";
			this.chkTrameTC.Size = new System.Drawing.Size(88, 17);
			this.chkTrameTC.TabIndex = 16;
			this.chkTrameTC.Text = "Trames \"TC\"";
			this.chkTrameTC.UseVisualStyleBackColor = true;
			this.chkTrameTC.CheckedChanged += new System.EventHandler(this.InterfaceChange);
			// 
			// chkLissage
			// 
			this.chkLissage.AutoSize = true;
			this.chkLissage.Location = new System.Drawing.Point(48, 100);
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
			this.chkPalCpc.Location = new System.Drawing.Point(48, 65);
			this.chkPalCpc.Name = "chkPalCpc";
			this.chkPalCpc.Size = new System.Drawing.Size(110, 30);
			this.chkPalCpc.TabIndex = 14;
			this.chkPalCpc.Text = "Réduction palette\nimage source";
			this.chkPalCpc.UseVisualStyleBackColor = true;
			this.chkPalCpc.CheckedChanged += new System.EventHandler(this.InterfaceChange);
			// 
			// lblPct
			// 
			this.lblPct.AutoSize = true;
			this.lblPct.Location = new System.Drawing.Point(124, 43);
			this.lblPct.Name = "lblPct";
			this.lblPct.Size = new System.Drawing.Size(15, 13);
			this.lblPct.TabIndex = 12;
			this.lblPct.Text = "%";
			this.lblPct.Visible = false;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(6, 17);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(31, 13);
			this.label4.TabIndex = 12;
			this.label4.Text = "Type";
			// 
			// autoRecalc
			// 
			this.autoRecalc.AutoSize = true;
			this.autoRecalc.Location = new System.Drawing.Point(3, 88);
			this.autoRecalc.Name = "autoRecalc";
			this.autoRecalc.Size = new System.Drawing.Size(162, 17);
			this.autoRecalc.TabIndex = 44;
			this.autoRecalc.Text = "Recalculer Automatiquement";
			this.autoRecalc.UseVisualStyleBackColor = true;
			this.autoRecalc.CheckedChanged += new System.EventHandler(this.InterfaceChange);
			// 
			// radioKeepLarger
			// 
			this.radioKeepLarger.AutoSize = true;
			this.radioKeepLarger.Location = new System.Drawing.Point(6, 54);
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
			this.radioKeepSmaller.Location = new System.Drawing.Point(6, 34);
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
			this.radioFit.Location = new System.Drawing.Point(6, 14);
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
			this.groupBox1.Location = new System.Drawing.Point(395, 2);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(140, 203);
			this.groupBox1.TabIndex = 49;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Taille image source";
			// 
			// radioOrigin
			// 
			this.radioOrigin.AutoSize = true;
			this.radioOrigin.Enabled = false;
			this.radioOrigin.Location = new System.Drawing.Point(6, 94);
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
			this.label7.Location = new System.Drawing.Point(3, 144);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(50, 13);
			this.label7.TabIndex = 45;
			this.label7.Text = "Position :";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(3, 120);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(38, 13);
			this.label5.TabIndex = 45;
			this.label5.Text = "Taille :";
			// 
			// tbxPosY
			// 
			this.tbxPosY.Location = new System.Drawing.Point(100, 141);
			this.tbxPosY.Name = "tbxPosY";
			this.tbxPosY.Size = new System.Drawing.Size(34, 20);
			this.tbxPosY.TabIndex = 46;
			this.tbxPosY.Text = "0";
			this.tbxPosY.Validating += new System.ComponentModel.CancelEventHandler(this.InterfaceChange);
			// 
			// tbxSizeY
			// 
			this.tbxSizeY.Location = new System.Drawing.Point(100, 117);
			this.tbxSizeY.Name = "tbxSizeY";
			this.tbxSizeY.Size = new System.Drawing.Size(34, 20);
			this.tbxSizeY.TabIndex = 44;
			this.tbxSizeY.Validating += new System.ComponentModel.CancelEventHandler(this.InterfaceChange);
			// 
			// tbxPosX
			// 
			this.tbxPosX.Location = new System.Drawing.Point(58, 141);
			this.tbxPosX.Name = "tbxPosX";
			this.tbxPosX.Size = new System.Drawing.Size(36, 20);
			this.tbxPosX.TabIndex = 45;
			this.tbxPosX.Text = "0";
			this.tbxPosX.Validating += new System.ComponentModel.CancelEventHandler(this.InterfaceChange);
			// 
			// tbxSizeX
			// 
			this.tbxSizeX.Location = new System.Drawing.Point(58, 117);
			this.tbxSizeX.Name = "tbxSizeX";
			this.tbxSizeX.Size = new System.Drawing.Size(36, 20);
			this.tbxSizeX.TabIndex = 43;
			this.tbxSizeX.Validating += new System.ComponentModel.CancelEventHandler(this.InterfaceChange);
			// 
			// radioUserSize
			// 
			this.radioUserSize.AutoSize = true;
			this.radioUserSize.Enabled = false;
			this.radioUserSize.Location = new System.Drawing.Point(6, 74);
			this.radioUserSize.Name = "radioUserSize";
			this.radioUserSize.Size = new System.Drawing.Size(97, 17);
			this.radioUserSize.TabIndex = 42;
			this.radioUserSize.TabStop = true;
			this.radioUserSize.Text = "Taille utilisateur";
			this.radioUserSize.UseVisualStyleBackColor = true;
			this.radioUserSize.CheckedChanged += new System.EventHandler(this.radioUserSize_CheckedChanged);
			// 
			// bpSave
			// 
			this.bpSave.Location = new System.Drawing.Point(2, 111);
			this.bpSave.Name = "bpSave";
			this.bpSave.Size = new System.Drawing.Size(108, 23);
			this.bpSave.TabIndex = 52;
			this.bpSave.Text = "Enregistrement";
			this.bpSave.UseVisualStyleBackColor = true;
			this.bpSave.Click += new System.EventHandler(this.bpSave_Click);
			// 
			// lblInfoVersion
			// 
			this.lblInfoVersion.AutoSize = true;
			this.lblInfoVersion.Location = new System.Drawing.Point(0, 211);
			this.lblInfoVersion.Name = "lblInfoVersion";
			this.lblInfoVersion.Size = new System.Drawing.Size(42, 13);
			this.lblInfoVersion.TabIndex = 52;
			this.lblInfoVersion.Text = "Version";
			// 
			// withCode
			// 
			this.withCode.AutoSize = true;
			this.withCode.Checked = true;
			this.withCode.CheckState = System.Windows.Forms.CheckState.Checked;
			this.withCode.Location = new System.Drawing.Point(2, 136);
			this.withCode.Name = "withCode";
			this.withCode.Size = new System.Drawing.Size(212, 17);
			this.withCode.TabIndex = 52;
			this.withCode.Text = "Inclure le code d\'affichage dans l\'image";
			this.withCode.UseVisualStyleBackColor = true;
			this.withCode.CheckedChanged += new System.EventHandler(this.withCode_CheckedChanged);
			// 
			// withPalette
			// 
			this.withPalette.AutoSize = true;
			this.withPalette.Checked = true;
			this.withPalette.CheckState = System.Windows.Forms.CheckState.Checked;
			this.withPalette.Location = new System.Drawing.Point(2, 159);
			this.withPalette.Name = "withPalette";
			this.withPalette.Size = new System.Drawing.Size(165, 17);
			this.withPalette.TabIndex = 52;
			this.withPalette.Text = "Inclure la palette dans l\'image";
			this.withPalette.UseVisualStyleBackColor = true;
			this.withPalette.CheckedChanged += new System.EventHandler(this.withPalette_CheckedChanged);
			// 
			// chkAllPics
			// 
			this.chkAllPics.AutoSize = true;
			this.chkAllPics.Location = new System.Drawing.Point(117, 63);
			this.chkAllPics.Name = "chkAllPics";
			this.chkAllPics.Size = new System.Drawing.Size(111, 17);
			this.chkAllPics.TabIndex = 57;
			this.chkAllPics.Text = "Toutes les images";
			this.chkAllPics.UseVisualStyleBackColor = true;
			this.chkAllPics.Visible = false;
			this.chkAllPics.CheckedChanged += new System.EventHandler(this.chkAllPics_CheckedChanged);
			// 
			// bpImport
			// 
			this.bpImport.Location = new System.Drawing.Point(3, 30);
			this.bpImport.Name = "bpImport";
			this.bpImport.Size = new System.Drawing.Size(108, 23);
			this.bpImport.TabIndex = 58;
			this.bpImport.Text = "Import";
			this.bpImport.UseVisualStyleBackColor = true;
			this.bpImport.Click += new System.EventHandler(this.bpImport_Click);
			// 
			// bpCreate
			// 
			this.bpCreate.Location = new System.Drawing.Point(125, 2);
			this.bpCreate.Name = "bpCreate";
			this.bpCreate.Size = new System.Drawing.Size(108, 23);
			this.bpCreate.TabIndex = 59;
			this.bpCreate.Text = "Création";
			this.bpCreate.UseVisualStyleBackColor = true;
			this.bpCreate.Click += new System.EventHandler(this.bpCreate_Click);
			// 
			// chkInfo
			// 
			this.chkInfo.AutoSize = true;
			this.chkInfo.Location = new System.Drawing.Point(237, 210);
			this.chkInfo.Name = "chkInfo";
			this.chkInfo.Size = new System.Drawing.Size(83, 17);
			this.chkInfo.TabIndex = 61;
			this.chkInfo.Text = "Informations";
			this.chkInfo.UseVisualStyleBackColor = true;
			this.chkInfo.CheckedChanged += new System.EventHandler(this.chkInfo_CheckedChanged);
			// 
			// chkCouleur
			// 
			this.chkCouleur.AutoSize = true;
			this.chkCouleur.Location = new System.Drawing.Point(541, 210);
			this.chkCouleur.Name = "chkCouleur";
			this.chkCouleur.Size = new System.Drawing.Size(125, 17);
			this.chkCouleur.TabIndex = 62;
			this.chkCouleur.Text = "Gestion des couleurs";
			this.chkCouleur.UseVisualStyleBackColor = true;
			this.chkCouleur.CheckedChanged += new System.EventHandler(this.chkCouleur_CheckedChanged);
			// 
			// chkParamInterne
			// 
			this.chkParamInterne.AutoSize = true;
			this.chkParamInterne.Location = new System.Drawing.Point(368, 210);
			this.chkParamInterne.Name = "chkParamInterne";
			this.chkParamInterne.Size = new System.Drawing.Size(119, 17);
			this.chkParamInterne.TabIndex = 63;
			this.chkParamInterne.Text = "Paramètres internes";
			this.chkParamInterne.UseVisualStyleBackColor = true;
			this.chkParamInterne.CheckedChanged += new System.EventHandler(this.chkParamInterne_CheckedChanged);
			// 
			// Main
			// 
			this.AllowDrop = true;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(714, 243);
			this.Controls.Add(this.chkParamInterne);
			this.Controls.Add(this.chkCouleur);
			this.Controls.Add(this.chkInfo);
			this.Controls.Add(this.bpCreate);
			this.Controls.Add(this.bpImport);
			this.Controls.Add(this.chkAllPics);
			this.Controls.Add(this.withPalette);
			this.Controls.Add(this.withCode);
			this.Controls.Add(this.lblInfoVersion);
			this.Controls.Add(this.bpSave);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.autoRecalc);
			this.Controls.Add(this.tramage);
			this.Controls.Add(this.resoCPC);
			this.Controls.Add(this.bpConvert);
			this.Controls.Add(this.bpLoad);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.Name = "Main";
			this.Text = "ConvImgCPC";
			this.DragDrop += new System.Windows.Forms.DragEventHandler(this.Main_DragDrop);
			this.DragEnter += new System.Windows.Forms.DragEventHandler(this.Main_DragEnter);
			((System.ComponentModel.ISupportInitialize)(this.nbCols)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.nbLignes)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pctTrame)).EndInit();
			this.resoCPC.ResumeLayout(false);
			this.resoCPC.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.trackModeX)).EndInit();
			this.tramage.ResumeLayout(false);
			this.tramage.PerformLayout();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

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
		private System.Windows.Forms.CheckBox autoRecalc;
		private System.Windows.Forms.RadioButton radioKeepLarger;
		private System.Windows.Forms.RadioButton radioKeepSmaller;
		private System.Windows.Forms.RadioButton radioFit;
		private System.Windows.Forms.GroupBox groupBox1;
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
		private System.Windows.Forms.CheckBox chkPalCpc;
		private System.Windows.Forms.CheckBox chkLissage;
		private System.Windows.Forms.RadioButton radioOrigin;
		private System.Windows.Forms.TrackBar trackModeX;
		private System.Windows.Forms.Button bpStandard;
		private System.Windows.Forms.Button bpEditTrame;
		private System.Windows.Forms.CheckBox chkAllPics;
		private System.Windows.Forms.Button bpImport;
		private System.Windows.Forms.Button bpCreate;
		private System.Windows.Forms.CheckBox chkInfo;
		private System.Windows.Forms.Button bpCalcSprite;
		private System.Windows.Forms.CheckBox chkCouleur;
		private System.Windows.Forms.CheckBox chkTrameTC;
		private System.Windows.Forms.CheckBox chkParamInterne;
	}
}

