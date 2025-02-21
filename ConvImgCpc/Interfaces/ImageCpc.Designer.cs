namespace ConvImgCpc {
	partial class ImageCpc {
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
			this.lockAllPal = new System.Windows.Forms.CheckBox();
			this.modeEdition = new System.Windows.Forms.CheckBox();
			this.hScrollBar = new System.Windows.Forms.HScrollBar();
			this.vScrollBar = new System.Windows.Forms.VScrollBar();
			this.drawColor = new System.Windows.Forms.Label();
			this.lblPenColor = new System.Windows.Forms.Label();
			this.lblPenSize = new System.Windows.Forms.Label();
			this.tailleCrayon = new System.Windows.Forms.ComboBox();
			this.grpEdition = new System.Windows.Forms.GroupBox();
			this.bpSaveWin = new System.Windows.Forms.Button();
			this.rbFill = new System.Windows.Forms.RadioButton();
			this.bpLoadWin = new System.Windows.Forms.Button();
			this.rbPickColor = new System.Windows.Forms.RadioButton();
			this.bpVerFlip = new System.Windows.Forms.Button();
			this.bpHorFlip = new System.Windows.Forms.Button();
			this.lblInfoPos = new System.Windows.Forms.Label();
			this.rbCopy = new System.Windows.Forms.RadioButton();
			this.undrawColor = new System.Windows.Forms.Label();
			this.lblZoom = new System.Windows.Forms.Label();
			this.rbZoom = new System.Windows.Forms.RadioButton();
			this.rbDraw = new System.Windows.Forms.RadioButton();
			this.chkDoRedo = new System.Windows.Forms.CheckBox();
			this.bpRedo = new System.Windows.Forms.Button();
			this.bpUndo = new System.Windows.Forms.Button();
			this.chkRendu = new System.Windows.Forms.CheckBox();
			this.label4 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.txbTailleNop = new System.Windows.Forms.TextBox();
			this.txbStartNop = new System.Windows.Forms.TextBox();
			this.chkGrille = new System.Windows.Forms.CheckBox();
			this.modeCaptureSprites = new System.Windows.Forms.CheckBox();
			this.bpCopyPal = new System.Windows.Forms.Button();
			this.chkX2 = new System.Windows.Forms.CheckBox();
			this.pictImpDraw = new System.Windows.Forms.PictureBox();
			this.pictureBox = new System.Windows.Forms.PictureBox();
			this.bpGenPal = new System.Windows.Forms.Button();
			this.chkGrilleSprite = new System.Windows.Forms.CheckBox();
			this.bpCopyImage = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.groupBoxPosSprite = new System.Windows.Forms.GroupBox();
			this.chkCopyZoom = new System.Windows.Forms.CheckBox();
			this.bpSaveCoord = new System.Windows.Forms.Button();
			this.bpReadCoord = new System.Windows.Forms.Button();
			this.bpCopiePosSprites = new System.Windows.Forms.Button();
			this.chkAfficheSH = new System.Windows.Forms.CheckBox();
			this.numPosY = new System.Windows.Forms.NumericUpDown();
			this.numPosX = new System.Windows.Forms.NumericUpDown();
			this.label8 = new System.Windows.Forms.Label();
			this.numZoomY = new System.Windows.Forms.NumericUpDown();
			this.label7 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.numZoomX = new System.Windows.Forms.NumericUpDown();
			this.label5 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.numSprite = new System.Windows.Forms.NumericUpDown();
			this.chkAutoCopy = new System.Windows.Forms.CheckBox();
			this.tilesSizeX = new System.Windows.Forms.NumericUpDown();
			this.tilesSizeY = new System.Windows.Forms.NumericUpDown();
			this.label9 = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.bpSaveTiles = new System.Windows.Forms.Button();
			this.chkTilesGrid = new System.Windows.Forms.CheckBox();
			this.grpEdition.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictImpDraw)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
			this.groupBoxPosSprite.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numPosY)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numPosX)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numZoomY)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numZoomX)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numSprite)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.tilesSizeX)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.tilesSizeY)).BeginInit();
			this.SuspendLayout();
			// 
			// lockAllPal
			// 
			this.lockAllPal.AutoSize = true;
			this.lockAllPal.Location = new System.Drawing.Point(12, 20);
			this.lockAllPal.Name = "lockAllPal";
			this.lockAllPal.Size = new System.Drawing.Size(15, 14);
			this.lockAllPal.TabIndex = 1;
			this.lockAllPal.UseVisualStyleBackColor = true;
			this.lockAllPal.CheckedChanged += new System.EventHandler(this.LockAllPal_CheckedChanged);
			// 
			// modeEdition
			// 
			this.modeEdition.AutoSize = true;
			this.modeEdition.Location = new System.Drawing.Point(19, 101);
			this.modeEdition.Name = "modeEdition";
			this.modeEdition.Size = new System.Drawing.Size(15, 14);
			this.modeEdition.TabIndex = 2;
			this.modeEdition.UseVisualStyleBackColor = true;
			this.modeEdition.CheckedChanged += new System.EventHandler(this.ModeEdition_CheckedChanged);
			// 
			// hScrollBar
			// 
			this.hScrollBar.LargeChange = 32;
			this.hScrollBar.Location = new System.Drawing.Point(168, 608);
			this.hScrollBar.Name = "hScrollBar";
			this.hScrollBar.Size = new System.Drawing.Size(1024, 16);
			this.hScrollBar.SmallChange = 8;
			this.hScrollBar.TabIndex = 1;
			this.hScrollBar.Visible = false;
			this.hScrollBar.Scroll += new System.Windows.Forms.ScrollEventHandler(this.HScrollBar_Scroll);
			// 
			// vScrollBar
			// 
			this.vScrollBar.LargeChange = 32;
			this.vScrollBar.Location = new System.Drawing.Point(1195, 61);
			this.vScrollBar.Name = "vScrollBar";
			this.vScrollBar.Size = new System.Drawing.Size(16, 544);
			this.vScrollBar.SmallChange = 8;
			this.vScrollBar.TabIndex = 0;
			this.vScrollBar.Visible = false;
			this.vScrollBar.Scroll += new System.Windows.Forms.ScrollEventHandler(this.VScrollBar_Scroll);
			// 
			// drawColor
			// 
			this.drawColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.drawColor.Location = new System.Drawing.Point(6, 253);
			this.drawColor.Name = "drawColor";
			this.drawColor.Size = new System.Drawing.Size(70, 70);
			this.drawColor.TabIndex = 7;
			this.drawColor.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// lblPenColor
			// 
			this.lblPenColor.AutoSize = true;
			this.lblPenColor.Location = new System.Drawing.Point(7, 237);
			this.lblPenColor.Name = "lblPenColor";
			this.lblPenColor.Size = new System.Drawing.Size(60, 13);
			this.lblPenColor.TabIndex = 4;
			this.lblPenColor.Text = "lblPenColor";
			// 
			// lblPenSize
			// 
			this.lblPenSize.AutoSize = true;
			this.lblPenSize.Location = new System.Drawing.Point(7, 219);
			this.lblPenSize.Name = "lblPenSize";
			this.lblPenSize.Size = new System.Drawing.Size(56, 13);
			this.lblPenSize.TabIndex = 5;
			this.lblPenSize.Text = "lblPenSize";
			// 
			// tailleCrayon
			// 
			this.tailleCrayon.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.tailleCrayon.Enabled = false;
			this.tailleCrayon.FormattingEnabled = true;
			this.tailleCrayon.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "8"});
			this.tailleCrayon.Location = new System.Drawing.Point(96, 215);
			this.tailleCrayon.Name = "tailleCrayon";
			this.tailleCrayon.Size = new System.Drawing.Size(31, 21);
			this.tailleCrayon.TabIndex = 3;
			this.tailleCrayon.SelectedIndexChanged += new System.EventHandler(this.TailleCrayon_SelectedIndexChanged);
			// 
			// grpEdition
			// 
			this.grpEdition.Controls.Add(this.bpSaveWin);
			this.grpEdition.Controls.Add(this.rbFill);
			this.grpEdition.Controls.Add(this.bpLoadWin);
			this.grpEdition.Controls.Add(this.rbPickColor);
			this.grpEdition.Controls.Add(this.bpVerFlip);
			this.grpEdition.Controls.Add(this.bpHorFlip);
			this.grpEdition.Controls.Add(this.lblInfoPos);
			this.grpEdition.Controls.Add(this.rbCopy);
			this.grpEdition.Controls.Add(this.undrawColor);
			this.grpEdition.Controls.Add(this.lblZoom);
			this.grpEdition.Controls.Add(this.rbZoom);
			this.grpEdition.Controls.Add(this.rbDraw);
			this.grpEdition.Controls.Add(this.chkDoRedo);
			this.grpEdition.Controls.Add(this.bpRedo);
			this.grpEdition.Controls.Add(this.bpUndo);
			this.grpEdition.Controls.Add(this.chkRendu);
			this.grpEdition.Controls.Add(this.drawColor);
			this.grpEdition.Controls.Add(this.lblPenColor);
			this.grpEdition.Controls.Add(this.lblPenSize);
			this.grpEdition.Controls.Add(this.tailleCrayon);
			this.grpEdition.Location = new System.Drawing.Point(13, 145);
			this.grpEdition.Name = "grpEdition";
			this.grpEdition.Size = new System.Drawing.Size(149, 467);
			this.grpEdition.TabIndex = 9;
			this.grpEdition.TabStop = false;
			this.grpEdition.Visible = false;
			// 
			// bpSaveWin
			// 
			this.bpSaveWin.Enabled = false;
			this.bpSaveWin.Location = new System.Drawing.Point(68, 113);
			this.bpSaveWin.Name = "bpSaveWin";
			this.bpSaveWin.Size = new System.Drawing.Size(75, 23);
			this.bpSaveWin.TabIndex = 22;
			this.bpSaveWin.Text = "Save Win";
			this.bpSaveWin.UseVisualStyleBackColor = true;
			this.bpSaveWin.Click += new System.EventHandler(this.BpSaveWin_Click);
			// 
			// rbFill
			// 
			this.rbFill.Image = global::ConvImgCpc.Properties.Resources.Fill;
			this.rbFill.Location = new System.Drawing.Point(10, 160);
			this.rbFill.Name = "rbFill";
			this.rbFill.Size = new System.Drawing.Size(48, 32);
			this.rbFill.TabIndex = 21;
			this.rbFill.TabStop = true;
			this.rbFill.UseVisualStyleBackColor = true;
			this.rbFill.CheckedChanged += new System.EventHandler(this.RbFill_CheckedChanged);
			// 
			// bpLoadWin
			// 
			this.bpLoadWin.Enabled = false;
			this.bpLoadWin.Location = new System.Drawing.Point(68, 91);
			this.bpLoadWin.Name = "bpLoadWin";
			this.bpLoadWin.Size = new System.Drawing.Size(75, 23);
			this.bpLoadWin.TabIndex = 20;
			this.bpLoadWin.Text = "Load Win";
			this.bpLoadWin.UseVisualStyleBackColor = true;
			this.bpLoadWin.Click += new System.EventHandler(this.BpLoadWin_Click);
			// 
			// rbPickColor
			// 
			this.rbPickColor.Image = global::ConvImgCpc.Properties.Resources.PickColor;
			this.rbPickColor.Location = new System.Drawing.Point(10, 127);
			this.rbPickColor.Name = "rbPickColor";
			this.rbPickColor.Size = new System.Drawing.Size(48, 32);
			this.rbPickColor.TabIndex = 14;
			this.rbPickColor.TabStop = true;
			this.rbPickColor.UseVisualStyleBackColor = true;
			this.rbPickColor.CheckedChanged += new System.EventHandler(this.RbPickColor_CheckedChanged);
			// 
			// bpVerFlip
			// 
			this.bpVerFlip.Location = new System.Drawing.Point(83, 400);
			this.bpVerFlip.Name = "bpVerFlip";
			this.bpVerFlip.Size = new System.Drawing.Size(60, 23);
			this.bpVerFlip.TabIndex = 19;
			this.bpVerFlip.Text = "Ver. Flip";
			this.bpVerFlip.UseVisualStyleBackColor = true;
			this.bpVerFlip.Click += new System.EventHandler(this.BpVerFlip_Click);
			// 
			// bpHorFlip
			// 
			this.bpHorFlip.Location = new System.Drawing.Point(6, 398);
			this.bpHorFlip.Name = "bpHorFlip";
			this.bpHorFlip.Size = new System.Drawing.Size(60, 23);
			this.bpHorFlip.TabIndex = 18;
			this.bpHorFlip.Text = "Hor. Flip";
			this.bpHorFlip.UseVisualStyleBackColor = true;
			this.bpHorFlip.Click += new System.EventHandler(this.BpHorFlip_Click);
			// 
			// lblInfoPos
			// 
			this.lblInfoPos.AutoSize = true;
			this.lblInfoPos.Location = new System.Drawing.Point(7, 200);
			this.lblInfoPos.Name = "lblInfoPos";
			this.lblInfoPos.Size = new System.Drawing.Size(43, 13);
			this.lblInfoPos.TabIndex = 17;
			this.lblInfoPos.Text = "position";
			// 
			// rbCopy
			// 
			this.rbCopy.Image = global::ConvImgCpc.Properties.Resources.Copy;
			this.rbCopy.Location = new System.Drawing.Point(10, 94);
			this.rbCopy.Name = "rbCopy";
			this.rbCopy.Size = new System.Drawing.Size(48, 32);
			this.rbCopy.TabIndex = 16;
			this.rbCopy.UseVisualStyleBackColor = true;
			this.rbCopy.CheckedChanged += new System.EventHandler(this.RbCopy_CheckedChanged);
			// 
			// undrawColor
			// 
			this.undrawColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.undrawColor.Location = new System.Drawing.Point(6, 326);
			this.undrawColor.Name = "undrawColor";
			this.undrawColor.Size = new System.Drawing.Size(70, 70);
			this.undrawColor.TabIndex = 15;
			this.undrawColor.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// lblZoom
			// 
			this.lblZoom.AutoSize = true;
			this.lblZoom.Location = new System.Drawing.Point(65, 74);
			this.lblZoom.Name = "lblZoom";
			this.lblZoom.Size = new System.Drawing.Size(22, 13);
			this.lblZoom.TabIndex = 14;
			this.lblZoom.Text = "1:1";
			// 
			// rbZoom
			// 
			this.rbZoom.Image = global::ConvImgCpc.Properties.Resources.Zoom;
			this.rbZoom.Location = new System.Drawing.Point(10, 61);
			this.rbZoom.Name = "rbZoom";
			this.rbZoom.Size = new System.Drawing.Size(48, 32);
			this.rbZoom.TabIndex = 13;
			this.rbZoom.UseVisualStyleBackColor = true;
			this.rbZoom.CheckedChanged += new System.EventHandler(this.RbZoom_CheckedChanged);
			// 
			// rbDraw
			// 
			this.rbDraw.Checked = true;
			this.rbDraw.Image = global::ConvImgCpc.Properties.Resources.Draw;
			this.rbDraw.Location = new System.Drawing.Point(10, 28);
			this.rbDraw.Name = "rbDraw";
			this.rbDraw.Size = new System.Drawing.Size(48, 32);
			this.rbDraw.TabIndex = 12;
			this.rbDraw.TabStop = true;
			this.rbDraw.UseVisualStyleBackColor = true;
			this.rbDraw.CheckedChanged += new System.EventHandler(this.RbDraw_CheckedChanged);
			// 
			// chkDoRedo
			// 
			this.chkDoRedo.AutoSize = true;
			this.chkDoRedo.Location = new System.Drawing.Point(14, 452);
			this.chkDoRedo.Name = "chkDoRedo";
			this.chkDoRedo.Size = new System.Drawing.Size(15, 14);
			this.chkDoRedo.TabIndex = 11;
			this.chkDoRedo.UseVisualStyleBackColor = true;
			// 
			// bpRedo
			// 
			this.bpRedo.Enabled = false;
			this.bpRedo.Location = new System.Drawing.Point(83, 425);
			this.bpRedo.Name = "bpRedo";
			this.bpRedo.Size = new System.Drawing.Size(60, 23);
			this.bpRedo.TabIndex = 10;
			this.bpRedo.Text = "Redo";
			this.bpRedo.UseVisualStyleBackColor = true;
			this.bpRedo.Click += new System.EventHandler(this.BpRedo_Click);
			// 
			// bpUndo
			// 
			this.bpUndo.Enabled = false;
			this.bpUndo.Location = new System.Drawing.Point(6, 425);
			this.bpUndo.Name = "bpUndo";
			this.bpUndo.Size = new System.Drawing.Size(60, 23);
			this.bpUndo.TabIndex = 10;
			this.bpUndo.Text = "Undo";
			this.bpUndo.UseVisualStyleBackColor = true;
			this.bpUndo.Click += new System.EventHandler(this.BpUndo_Click);
			// 
			// chkRendu
			// 
			this.chkRendu.AutoSize = true;
			this.chkRendu.Location = new System.Drawing.Point(6, 10);
			this.chkRendu.Name = "chkRendu";
			this.chkRendu.Size = new System.Drawing.Size(15, 14);
			this.chkRendu.TabIndex = 9;
			this.chkRendu.UseVisualStyleBackColor = true;
			this.chkRendu.CheckedChanged += new System.EventHandler(this.ChkRendu_CheckedChanged);
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(30, 658);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(50, 13);
			this.label4.TabIndex = 17;
			this.label4.Text = "nb NOPs";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(30, 637);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(53, 13);
			this.label1.TabIndex = 16;
			this.label1.Text = "start NOP";
			// 
			// txbTailleNop
			// 
			this.txbTailleNop.Location = new System.Drawing.Point(90, 655);
			this.txbTailleNop.Name = "txbTailleNop";
			this.txbTailleNop.Size = new System.Drawing.Size(34, 20);
			this.txbTailleNop.TabIndex = 15;
			this.txbTailleNop.Text = "1";
			// 
			// txbStartNop
			// 
			this.txbStartNop.Location = new System.Drawing.Point(89, 633);
			this.txbStartNop.Name = "txbStartNop";
			this.txbStartNop.Size = new System.Drawing.Size(34, 20);
			this.txbStartNop.TabIndex = 15;
			this.txbStartNop.Text = "0";
			// 
			// chkGrille
			// 
			this.chkGrille.AutoSize = true;
			this.chkGrille.Location = new System.Drawing.Point(27, 617);
			this.chkGrille.Name = "chkGrille";
			this.chkGrille.Size = new System.Drawing.Size(129, 17);
			this.chkGrille.TabIndex = 14;
			this.chkGrille.Text = "Afficher grille verticale";
			this.chkGrille.UseVisualStyleBackColor = true;
			this.chkGrille.CheckedChanged += new System.EventHandler(this.ChkGrille_CheckedChanged);
			// 
			// modeCaptureSprites
			// 
			this.modeCaptureSprites.AutoSize = true;
			this.modeCaptureSprites.Location = new System.Drawing.Point(19, 116);
			this.modeCaptureSprites.Name = "modeCaptureSprites";
			this.modeCaptureSprites.Size = new System.Drawing.Size(15, 14);
			this.modeCaptureSprites.TabIndex = 10;
			this.modeCaptureSprites.UseVisualStyleBackColor = true;
			this.modeCaptureSprites.Visible = false;
			this.modeCaptureSprites.CheckedChanged += new System.EventHandler(this.ModeCaptureSprites_CheckedChanged);
			// 
			// bpCopyPal
			// 
			this.bpCopyPal.Location = new System.Drawing.Point(4, 34);
			this.bpCopyPal.Name = "bpCopyPal";
			this.bpCopyPal.Size = new System.Drawing.Size(107, 48);
			this.bpCopyPal.TabIndex = 11;
			this.bpCopyPal.UseVisualStyleBackColor = true;
			this.bpCopyPal.Click += new System.EventHandler(this.BpCopyPal_Click);
			// 
			// chkX2
			// 
			this.chkX2.AutoSize = true;
			this.chkX2.Location = new System.Drawing.Point(19, 83);
			this.chkX2.Name = "chkX2";
			this.chkX2.Size = new System.Drawing.Size(39, 17);
			this.chkX2.TabIndex = 13;
			this.chkX2.Text = "X2";
			this.chkX2.UseVisualStyleBackColor = true;
			this.chkX2.CheckedChanged += new System.EventHandler(this.ChkX2_CheckedChanged);
			// 
			// pictImpDraw
			// 
			this.pictImpDraw.Location = new System.Drawing.Point(168, 61);
			this.pictImpDraw.Name = "pictImpDraw";
			this.pictImpDraw.Size = new System.Drawing.Size(768, 2);
			this.pictImpDraw.TabIndex = 12;
			this.pictImpDraw.TabStop = false;
			// 
			// pictureBox
			// 
			this.pictureBox.Location = new System.Drawing.Point(168, 61);
			this.pictureBox.Name = "pictureBox";
			this.pictureBox.Size = new System.Drawing.Size(1024, 544);
			this.pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.pictureBox.TabIndex = 0;
			this.pictureBox.TabStop = false;
			this.pictureBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TrtMouseMove);
			this.pictureBox.MouseLeave += new System.EventHandler(this.PictureBox_MouseLeave);
			this.pictureBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.TrtMouseMove);
			// 
			// bpGenPal
			// 
			this.bpGenPal.Image = global::ConvImgCpc.Properties.Resources.GenPalette;
			this.bpGenPal.Location = new System.Drawing.Point(117, 34);
			this.bpGenPal.Name = "bpGenPal";
			this.bpGenPal.Size = new System.Drawing.Size(48, 48);
			this.bpGenPal.TabIndex = 27;
			this.bpGenPal.UseVisualStyleBackColor = true;
			this.bpGenPal.Click += new System.EventHandler(this.BpGenPal_Click);
			// 
			// chkGrilleSprite
			// 
			this.chkGrilleSprite.AutoSize = true;
			this.chkGrilleSprite.Location = new System.Drawing.Point(19, 131);
			this.chkGrilleSprite.Name = "chkGrilleSprite";
			this.chkGrilleSprite.Size = new System.Drawing.Size(15, 14);
			this.chkGrilleSprite.TabIndex = 28;
			this.chkGrilleSprite.UseVisualStyleBackColor = true;
			this.chkGrilleSprite.Visible = false;
			this.chkGrilleSprite.CheckedChanged += new System.EventHandler(this.ChkGrilleSprite_CheckedChanged);
			// 
			// bpCopyImage
			// 
			this.bpCopyImage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.bpCopyImage.Location = new System.Drawing.Point(168, 631);
			this.bpCopyImage.Name = "bpCopyImage";
			this.bpCopyImage.Size = new System.Drawing.Size(106, 44);
			this.bpCopyImage.TabIndex = 29;
			this.bpCopyImage.UseVisualStyleBackColor = true;
			this.bpCopyImage.Click += new System.EventHandler(this.BpCopyImage_Click);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(122, 2);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(42, 13);
			this.label2.TabIndex = 30;
			this.label2.Text = "Disable";
			// 
			// groupBoxPosSprite
			// 
			this.groupBoxPosSprite.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.groupBoxPosSprite.Controls.Add(this.chkCopyZoom);
			this.groupBoxPosSprite.Controls.Add(this.bpSaveCoord);
			this.groupBoxPosSprite.Controls.Add(this.bpReadCoord);
			this.groupBoxPosSprite.Controls.Add(this.bpCopiePosSprites);
			this.groupBoxPosSprite.Controls.Add(this.chkAfficheSH);
			this.groupBoxPosSprite.Controls.Add(this.numPosY);
			this.groupBoxPosSprite.Controls.Add(this.numPosX);
			this.groupBoxPosSprite.Controls.Add(this.label8);
			this.groupBoxPosSprite.Controls.Add(this.numZoomY);
			this.groupBoxPosSprite.Controls.Add(this.label7);
			this.groupBoxPosSprite.Controls.Add(this.label6);
			this.groupBoxPosSprite.Controls.Add(this.numZoomX);
			this.groupBoxPosSprite.Controls.Add(this.label5);
			this.groupBoxPosSprite.Controls.Add(this.label3);
			this.groupBoxPosSprite.Controls.Add(this.numSprite);
			this.groupBoxPosSprite.Location = new System.Drawing.Point(280, 633);
			this.groupBoxPosSprite.Name = "groupBoxPosSprite";
			this.groupBoxPosSprite.Size = new System.Drawing.Size(889, 64);
			this.groupBoxPosSprite.TabIndex = 31;
			this.groupBoxPosSprite.TabStop = false;
			this.groupBoxPosSprite.Text = "Affichage sprites hard sur image";
			// 
			// chkCopyZoom
			// 
			this.chkCopyZoom.Location = new System.Drawing.Point(309, 25);
			this.chkCopyZoom.Name = "chkCopyZoom";
			this.chkCopyZoom.Size = new System.Drawing.Size(125, 35);
			this.chkCopyZoom.TabIndex = 8;
			this.chkCopyZoom.Text = "Recopier zoom aux autres sprites";
			this.chkCopyZoom.UseVisualStyleBackColor = true;
			// 
			// bpSaveCoord
			// 
			this.bpSaveCoord.Location = new System.Drawing.Point(611, 12);
			this.bpSaveCoord.Name = "bpSaveCoord";
			this.bpSaveCoord.Size = new System.Drawing.Size(60, 23);
			this.bpSaveCoord.TabIndex = 7;
			this.bpSaveCoord.Text = "Save";
			this.bpSaveCoord.UseVisualStyleBackColor = true;
			this.bpSaveCoord.Click += new System.EventHandler(this.BpSaveCoord_Click);
			// 
			// bpReadCoord
			// 
			this.bpReadCoord.Location = new System.Drawing.Point(611, 41);
			this.bpReadCoord.Name = "bpReadCoord";
			this.bpReadCoord.Size = new System.Drawing.Size(60, 23);
			this.bpReadCoord.TabIndex = 7;
			this.bpReadCoord.Text = "Read";
			this.bpReadCoord.UseVisualStyleBackColor = true;
			this.bpReadCoord.Click += new System.EventHandler(this.BpReadCoord_Click);
			// 
			// bpCopiePosSprites
			// 
			this.bpCopiePosSprites.Location = new System.Drawing.Point(696, 18);
			this.bpCopiePosSprites.Name = "bpCopiePosSprites";
			this.bpCopiePosSprites.Size = new System.Drawing.Size(187, 23);
			this.bpCopiePosSprites.TabIndex = 6;
			this.bpCopiePosSprites.Text = "Copier positions dans presse-papier";
			this.bpCopiePosSprites.UseVisualStyleBackColor = true;
			this.bpCopiePosSprites.Click += new System.EventHandler(this.BpCopiePosSprites_Click);
			// 
			// chkAfficheSH
			// 
			this.chkAfficheSH.AutoSize = true;
			this.chkAfficheSH.Location = new System.Drawing.Point(6, 25);
			this.chkAfficheSH.Name = "chkAfficheSH";
			this.chkAfficheSH.Size = new System.Drawing.Size(59, 17);
			this.chkAfficheSH.TabIndex = 5;
			this.chkAfficheSH.Text = "Activer";
			this.chkAfficheSH.UseVisualStyleBackColor = true;
			this.chkAfficheSH.CheckedChanged += new System.EventHandler(this.ChkAfficheSH_CheckedChanged);
			// 
			// numPosY
			// 
			this.numPosY.Location = new System.Drawing.Point(501, 38);
			this.numPosY.Maximum = new decimal(new int[] {
            272,
            0,
            0,
            0});
			this.numPosY.Name = "numPosY";
			this.numPosY.Size = new System.Drawing.Size(39, 20);
			this.numPosY.TabIndex = 4;
			this.numPosY.ValueChanged += new System.EventHandler(this.NumPosY_ValueChanged);
			// 
			// numPosX
			// 
			this.numPosX.Location = new System.Drawing.Point(501, 18);
			this.numPosX.Maximum = new decimal(new int[] {
            1024,
            0,
            0,
            0});
			this.numPosX.Name = "numPosX";
			this.numPosX.Size = new System.Drawing.Size(39, 20);
			this.numPosX.TabIndex = 4;
			this.numPosX.ValueChanged += new System.EventHandler(this.NumPosX_ValueChanged);
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(460, 42);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(35, 13);
			this.label8.TabIndex = 2;
			this.label8.Text = "Pos Y";
			// 
			// numZoomY
			// 
			this.numZoomY.Location = new System.Drawing.Point(262, 40);
			this.numZoomY.Maximum = new decimal(new int[] {
            3,
            0,
            0,
            0});
			this.numZoomY.Name = "numZoomY";
			this.numZoomY.Size = new System.Drawing.Size(26, 20);
			this.numZoomY.TabIndex = 3;
			this.numZoomY.ValueChanged += new System.EventHandler(this.NumZoomY_ValueChanged);
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(460, 22);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(35, 13);
			this.label7.TabIndex = 2;
			this.label7.Text = "Pos X";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(216, 44);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(44, 13);
			this.label6.TabIndex = 2;
			this.label6.Text = "Zoom Y";
			// 
			// numZoomX
			// 
			this.numZoomX.Location = new System.Drawing.Point(262, 20);
			this.numZoomX.Maximum = new decimal(new int[] {
            3,
            0,
            0,
            0});
			this.numZoomX.Name = "numZoomX";
			this.numZoomX.Size = new System.Drawing.Size(26, 20);
			this.numZoomX.TabIndex = 3;
			this.numZoomX.ValueChanged += new System.EventHandler(this.NumZoomX_ValueChanged);
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(215, 24);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(44, 13);
			this.label5.TabIndex = 2;
			this.label5.Text = "Zoom X";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(114, 22);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(46, 13);
			this.label3.TabIndex = 1;
			this.label3.Text = "N°Sprite";
			// 
			// numSprite
			// 
			this.numSprite.Location = new System.Drawing.Point(166, 20);
			this.numSprite.Maximum = new decimal(new int[] {
            15,
            0,
            0,
            0});
			this.numSprite.Name = "numSprite";
			this.numSprite.Size = new System.Drawing.Size(37, 20);
			this.numSprite.TabIndex = 0;
			this.numSprite.ValueChanged += new System.EventHandler(this.NumSprite_ValueChanged);
			// 
			// chkAutoCopy
			// 
			this.chkAutoCopy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.chkAutoCopy.AutoSize = true;
			this.chkAutoCopy.Location = new System.Drawing.Point(168, 681);
			this.chkAutoCopy.Name = "chkAutoCopy";
			this.chkAutoCopy.Size = new System.Drawing.Size(61, 17);
			this.chkAutoCopy.TabIndex = 32;
			this.chkAutoCopy.Text = "AllCopy";
			this.chkAutoCopy.UseVisualStyleBackColor = true;
			// 
			// tilesSizeX
			// 
			this.tilesSizeX.Location = new System.Drawing.Point(1022, 4);
			this.tilesSizeX.Maximum = new decimal(new int[] {
            64,
            0,
            0,
            0});
			this.tilesSizeX.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.tilesSizeX.Name = "tilesSizeX";
			this.tilesSizeX.Size = new System.Drawing.Size(43, 20);
			this.tilesSizeX.TabIndex = 33;
			this.tilesSizeX.Value = new decimal(new int[] {
            16,
            0,
            0,
            0});
			// 
			// tilesSizeY
			// 
			this.tilesSizeY.Location = new System.Drawing.Point(1022, 30);
			this.tilesSizeY.Maximum = new decimal(new int[] {
            64,
            0,
            0,
            0});
			this.tilesSizeY.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.tilesSizeY.Name = "tilesSizeY";
			this.tilesSizeY.Size = new System.Drawing.Size(43, 20);
			this.tilesSizeY.TabIndex = 33;
			this.tilesSizeY.Value = new decimal(new int[] {
            16,
            0,
            0,
            0});
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Location = new System.Drawing.Point(959, 8);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(56, 13);
			this.label9.TabIndex = 34;
			this.label9.Text = "TilesSizeX";
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.Location = new System.Drawing.Point(959, 33);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(56, 13);
			this.label10.TabIndex = 34;
			this.label10.Text = "TilesSizeY";
			// 
			// bpSaveTiles
			// 
			this.bpSaveTiles.Location = new System.Drawing.Point(1096, 4);
			this.bpSaveTiles.Name = "bpSaveTiles";
			this.bpSaveTiles.Size = new System.Drawing.Size(96, 23);
			this.bpSaveTiles.TabIndex = 35;
			this.bpSaveTiles.Text = "Save as Tileset";
			this.bpSaveTiles.UseVisualStyleBackColor = true;
			this.bpSaveTiles.Click += new System.EventHandler(this.BpSaveFnt_Click);
			// 
			// chkTilesGrid
			// 
			this.chkTilesGrid.AutoSize = true;
			this.chkTilesGrid.Location = new System.Drawing.Point(1096, 29);
			this.chkTilesGrid.Name = "chkTilesGrid";
			this.chkTilesGrid.Size = new System.Drawing.Size(94, 17);
			this.chkTilesGrid.TabIndex = 36;
			this.chkTilesGrid.Text = "Show tiles grid";
			this.chkTilesGrid.UseVisualStyleBackColor = true;
			this.chkTilesGrid.CheckedChanged += new System.EventHandler(this.ChkTilesGrid_CheckedChanged);
			// 
			// ImageCpc
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1216, 699);
			this.ControlBox = false;
			this.Controls.Add(this.chkTilesGrid);
			this.Controls.Add(this.bpSaveTiles);
			this.Controls.Add(this.label10);
			this.Controls.Add(this.label9);
			this.Controls.Add(this.tilesSizeY);
			this.Controls.Add(this.tilesSizeX);
			this.Controls.Add(this.chkAutoCopy);
			this.Controls.Add(this.groupBoxPosSprite);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.bpCopyImage);
			this.Controls.Add(this.chkGrilleSprite);
			this.Controls.Add(this.bpGenPal);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.chkX2);
			this.Controls.Add(this.pictImpDraw);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.bpCopyPal);
			this.Controls.Add(this.modeCaptureSprites);
			this.Controls.Add(this.txbTailleNop);
			this.Controls.Add(this.grpEdition);
			this.Controls.Add(this.vScrollBar);
			this.Controls.Add(this.txbStartNop);
			this.Controls.Add(this.hScrollBar);
			this.Controls.Add(this.modeEdition);
			this.Controls.Add(this.chkGrille);
			this.Controls.Add(this.lockAllPal);
			this.Controls.Add(this.pictureBox);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ImageCpc";
			this.ShowIcon = false;
			this.Text = "Image CPC";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ImageCpc_FormClosing);
			this.grpEdition.ResumeLayout(false);
			this.grpEdition.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictImpDraw)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
			this.groupBoxPosSprite.ResumeLayout(false);
			this.groupBoxPosSprite.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.numPosY)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numPosX)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numZoomY)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numZoomX)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numSprite)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.tilesSizeX)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.tilesSizeY)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.PictureBox pictureBox;
		public System.Windows.Forms.CheckBox lockAllPal;
		private System.Windows.Forms.CheckBox modeEdition;
		private System.Windows.Forms.HScrollBar hScrollBar;
		private System.Windows.Forms.VScrollBar vScrollBar;
		private System.Windows.Forms.Label drawColor;
		private System.Windows.Forms.Label lblPenColor;
		private System.Windows.Forms.Label lblPenSize;
		private System.Windows.Forms.ComboBox tailleCrayon;
		private System.Windows.Forms.GroupBox grpEdition;
		private System.Windows.Forms.CheckBox chkRendu;
		private System.Windows.Forms.Button bpRedo;
		private System.Windows.Forms.Button bpUndo;
		private System.Windows.Forms.CheckBox chkDoRedo;
		private System.Windows.Forms.RadioButton rbDraw;
		private System.Windows.Forms.RadioButton rbZoom;
		private System.Windows.Forms.Label lblZoom;
		private System.Windows.Forms.Label undrawColor;
		private System.Windows.Forms.RadioButton rbCopy;
		private System.Windows.Forms.Label lblInfoPos;
		public System.Windows.Forms.CheckBox modeCaptureSprites;
		private System.Windows.Forms.Button bpCopyPal;
		private System.Windows.Forms.PictureBox pictImpDraw;
		private System.Windows.Forms.Button bpHorFlip;
		private System.Windows.Forms.Button bpVerFlip;
		private System.Windows.Forms.CheckBox chkX2;
		private System.Windows.Forms.RadioButton rbPickColor;
		private System.Windows.Forms.CheckBox chkGrille;
		private System.Windows.Forms.TextBox txbStartNop;
		private System.Windows.Forms.TextBox txbTailleNop;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Button bpGenPal;
		private System.Windows.Forms.Button bpLoadWin;
		private System.Windows.Forms.CheckBox chkGrilleSprite;
		private System.Windows.Forms.Button bpCopyImage;
		private System.Windows.Forms.RadioButton rbFill;
		private System.Windows.Forms.Button bpSaveWin;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.GroupBox groupBoxPosSprite;
		private System.Windows.Forms.NumericUpDown numZoomX;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.NumericUpDown numSprite;
		private System.Windows.Forms.NumericUpDown numPosY;
		private System.Windows.Forms.NumericUpDown numPosX;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.NumericUpDown numZoomY;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label6;
		public System.Windows.Forms.CheckBox chkAfficheSH;
		private System.Windows.Forms.Button bpCopiePosSprites;
		private System.Windows.Forms.Button bpSaveCoord;
		private System.Windows.Forms.Button bpReadCoord;
		private System.Windows.Forms.CheckBox chkCopyZoom;
		private System.Windows.Forms.CheckBox chkAutoCopy;
		private System.Windows.Forms.NumericUpDown tilesSizeX;
		private System.Windows.Forms.NumericUpDown tilesSizeY;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Button bpSaveTiles;
		private System.Windows.Forms.CheckBox chkTilesGrid;
	}
}