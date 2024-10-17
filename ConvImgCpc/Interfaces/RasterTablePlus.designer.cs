namespace ConvImgCpc {
	partial class RasterTablePlus {
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
			this.pictureBox = new System.Windows.Forms.PictureBox();
			this.bpAddLine = new System.Windows.Forms.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.bpImportKit = new System.Windows.Forms.Button();
			this.bpCopyToClipboard = new System.Windows.Forms.Button();
			this.bpGenFade = new System.Windows.Forms.Button();
			this.bpGenerate = new System.Windows.Forms.Button();
			this.txbLineEnd = new System.Windows.Forms.TextBox();
			this.txbLineStart = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.txbStartB = new System.Windows.Forms.TextBox();
			this.txbStartV = new System.Windows.Forms.TextBox();
			this.txbStartR = new System.Windows.Forms.TextBox();
			this.trkStartV = new System.Windows.Forms.TrackBar();
			this.trkStartB = new System.Windows.Forms.TrackBar();
			this.trkStartR = new System.Windows.Forms.TrackBar();
			this.lblStartColor = new System.Windows.Forms.Label();
			this.bpAddConstant = new System.Windows.Forms.Button();
			this.txbConstant = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.bpClearAll = new System.Windows.Forms.Button();
			this.bpUndo = new System.Windows.Forms.Button();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.rbMoins = new System.Windows.Forms.RadioButton();
			this.rbPlus = new System.Windows.Forms.RadioButton();
			this.txbStart = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.bpBPlus = new System.Windows.Forms.Button();
			this.bpVPlus = new System.Windows.Forms.Button();
			this.bpRPlus = new System.Windows.Forms.Button();
			this.bpBMoins = new System.Windows.Forms.Button();
			this.bpVMoins = new System.Windows.Forms.Button();
			this.bpRMoins = new System.Windows.Forms.Button();
			this.bpImportImage = new System.Windows.Forms.Button();
			this.lblLine = new System.Windows.Forms.Label();
			this.bpLoad = new System.Windows.Forms.Button();
			this.bpSave = new System.Windows.Forms.Button();
			this.bpScrollUp = new System.Windows.Forms.Button();
			this.bpScrollDown = new System.Windows.Forms.Button();
			this.txbScroll = new System.Windows.Forms.TextBox();
			this.label8 = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
			this.groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.trkStartV)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.trkStartB)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.trkStartR)).BeginInit();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// pictureBox
			// 
			this.pictureBox.Location = new System.Drawing.Point(247, 7);
			this.pictureBox.Name = "pictureBox";
			this.pictureBox.Size = new System.Drawing.Size(768, 544);
			this.pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.pictureBox.TabIndex = 0;
			this.pictureBox.TabStop = false;
			this.pictureBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PictureBox_MouseMove);
			// 
			// bpAddLine
			// 
			this.bpAddLine.Location = new System.Drawing.Point(108, 36);
			this.bpAddLine.Name = "bpAddLine";
			this.bpAddLine.Size = new System.Drawing.Size(97, 23);
			this.bpAddLine.TabIndex = 1;
			this.bpAddLine.Text = "Add Lines color";
			this.bpAddLine.UseVisualStyleBackColor = true;
			this.bpAddLine.Click += new System.EventHandler(this.BpAddLine_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.bpImportKit);
			this.groupBox1.Controls.Add(this.bpCopyToClipboard);
			this.groupBox1.Controls.Add(this.bpGenFade);
			this.groupBox1.Controls.Add(this.bpGenerate);
			this.groupBox1.Controls.Add(this.txbLineEnd);
			this.groupBox1.Controls.Add(this.txbLineStart);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Location = new System.Drawing.Point(3, 437);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(238, 114);
			this.groupBox1.TabIndex = 2;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Output";
			// 
			// bpImportKit
			// 
			this.bpImportKit.Location = new System.Drawing.Point(122, 92);
			this.bpImportKit.Name = "bpImportKit";
			this.bpImportKit.Size = new System.Drawing.Size(116, 23);
			this.bpImportKit.TabIndex = 30;
			this.bpImportKit.Text = "Import KIT";
			this.bpImportKit.UseVisualStyleBackColor = true;
			this.bpImportKit.Click += new System.EventHandler(this.BpImportKit_Click);
			// 
			// bpCopyToClipboard
			// 
			this.bpCopyToClipboard.Location = new System.Drawing.Point(0, 92);
			this.bpCopyToClipboard.Name = "bpCopyToClipboard";
			this.bpCopyToClipboard.Size = new System.Drawing.Size(116, 23);
			this.bpCopyToClipboard.TabIndex = 29;
			this.bpCopyToClipboard.Text = "Copy to clipboard";
			this.bpCopyToClipboard.UseVisualStyleBackColor = true;
			this.bpCopyToClipboard.Click += new System.EventHandler(this.BpCopyToClipboard_Click);
			// 
			// bpGenFade
			// 
			this.bpGenFade.Location = new System.Drawing.Point(122, 63);
			this.bpGenFade.Name = "bpGenFade";
			this.bpGenFade.Size = new System.Drawing.Size(116, 23);
			this.bpGenFade.TabIndex = 2;
			this.bpGenFade.Text = "Generate asm fade";
			this.bpGenFade.UseVisualStyleBackColor = true;
			this.bpGenFade.Click += new System.EventHandler(this.BpGenFade_Click);
			// 
			// bpGenerate
			// 
			this.bpGenerate.Location = new System.Drawing.Point(0, 63);
			this.bpGenerate.Name = "bpGenerate";
			this.bpGenerate.Size = new System.Drawing.Size(116, 23);
			this.bpGenerate.TabIndex = 2;
			this.bpGenerate.Text = "Generate asm output";
			this.bpGenerate.UseVisualStyleBackColor = true;
			this.bpGenerate.Click += new System.EventHandler(this.BpGenerate_Click);
			// 
			// txbLineEnd
			// 
			this.txbLineEnd.Location = new System.Drawing.Point(67, 40);
			this.txbLineEnd.MaxLength = 3;
			this.txbLineEnd.Name = "txbLineEnd";
			this.txbLineEnd.Size = new System.Drawing.Size(32, 20);
			this.txbLineEnd.TabIndex = 1;
			this.txbLineEnd.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// txbLineStart
			// 
			this.txbLineStart.Location = new System.Drawing.Point(67, 17);
			this.txbLineStart.MaxLength = 3;
			this.txbLineStart.Name = "txbLineStart";
			this.txbLineStart.Size = new System.Drawing.Size(32, 20);
			this.txbLineStart.TabIndex = 1;
			this.txbLineStart.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(6, 43);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(45, 13);
			this.label2.TabIndex = 0;
			this.label2.Text = "To line :";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(6, 20);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(55, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "From line :";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(3, 169);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(14, 13);
			this.label5.TabIndex = 24;
			this.label5.Text = "B";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(4, 77);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(14, 13);
			this.label3.TabIndex = 23;
			this.label3.Text = "V";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(3, 120);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(15, 13);
			this.label4.TabIndex = 22;
			this.label4.Text = "R";
			// 
			// txbStartB
			// 
			this.txbStartB.Location = new System.Drawing.Point(202, 169);
			this.txbStartB.MaxLength = 2;
			this.txbStartB.Name = "txbStartB";
			this.txbStartB.Size = new System.Drawing.Size(30, 20);
			this.txbStartB.TabIndex = 19;
			this.txbStartB.Text = "0";
			this.txbStartB.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.txbStartB.TextChanged += new System.EventHandler(this.TxbStartB_TextChanged);
			// 
			// txbStartV
			// 
			this.txbStartV.Location = new System.Drawing.Point(202, 74);
			this.txbStartV.MaxLength = 2;
			this.txbStartV.Name = "txbStartV";
			this.txbStartV.Size = new System.Drawing.Size(30, 20);
			this.txbStartV.TabIndex = 20;
			this.txbStartV.Text = "0";
			this.txbStartV.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.txbStartV.TextChanged += new System.EventHandler(this.TxbStartV_TextChanged);
			// 
			// txbStartR
			// 
			this.txbStartR.Location = new System.Drawing.Point(202, 120);
			this.txbStartR.MaxLength = 2;
			this.txbStartR.Name = "txbStartR";
			this.txbStartR.Size = new System.Drawing.Size(30, 20);
			this.txbStartR.TabIndex = 21;
			this.txbStartR.Text = "0";
			this.txbStartR.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.txbStartR.TextChanged += new System.EventHandler(this.TxbStartR_TextChanged);
			// 
			// trkStartV
			// 
			this.trkStartV.Location = new System.Drawing.Point(52, 74);
			this.trkStartV.Maximum = 15;
			this.trkStartV.Name = "trkStartV";
			this.trkStartV.Size = new System.Drawing.Size(104, 45);
			this.trkStartV.TabIndex = 18;
			this.trkStartV.Scroll += new System.EventHandler(this.TrkStartV_Scroll);
			this.trkStartV.ValueChanged += new System.EventHandler(this.TrkStartV_Scroll);
			// 
			// trkStartB
			// 
			this.trkStartB.Location = new System.Drawing.Point(52, 169);
			this.trkStartB.Maximum = 15;
			this.trkStartB.Name = "trkStartB";
			this.trkStartB.Size = new System.Drawing.Size(104, 45);
			this.trkStartB.TabIndex = 17;
			this.trkStartB.Scroll += new System.EventHandler(this.TrkStartB_Scroll);
			this.trkStartB.ValueChanged += new System.EventHandler(this.TrkStartB_Scroll);
			// 
			// trkStartR
			// 
			this.trkStartR.Location = new System.Drawing.Point(52, 120);
			this.trkStartR.Maximum = 15;
			this.trkStartR.Name = "trkStartR";
			this.trkStartR.Size = new System.Drawing.Size(104, 45);
			this.trkStartR.TabIndex = 16;
			this.trkStartR.Scroll += new System.EventHandler(this.TrkStartR_Scroll);
			this.trkStartR.ValueChanged += new System.EventHandler(this.TrkStartR_Scroll);
			// 
			// lblStartColor
			// 
			this.lblStartColor.BackColor = System.Drawing.Color.Black;
			this.lblStartColor.Location = new System.Drawing.Point(86, 208);
			this.lblStartColor.Name = "lblStartColor";
			this.lblStartColor.Size = new System.Drawing.Size(64, 64);
			this.lblStartColor.TabIndex = 15;
			// 
			// bpAddConstant
			// 
			this.bpAddConstant.Location = new System.Drawing.Point(7, 19);
			this.bpAddConstant.Name = "bpAddConstant";
			this.bpAddConstant.Size = new System.Drawing.Size(54, 23);
			this.bpAddConstant.TabIndex = 25;
			this.bpAddConstant.Text = "Add for";
			this.bpAddConstant.UseVisualStyleBackColor = true;
			this.bpAddConstant.Click += new System.EventHandler(this.BpAddConstant_Click);
			// 
			// txbConstant
			// 
			this.txbConstant.Location = new System.Drawing.Point(67, 19);
			this.txbConstant.MaxLength = 3;
			this.txbConstant.Name = "txbConstant";
			this.txbConstant.Size = new System.Drawing.Size(31, 20);
			this.txbConstant.TabIndex = 26;
			this.txbConstant.Text = "1";
			this.txbConstant.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(100, 24);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(28, 13);
			this.label6.TabIndex = 27;
			this.label6.Text = "lines";
			// 
			// bpClearAll
			// 
			this.bpClearAll.Location = new System.Drawing.Point(5, 36);
			this.bpClearAll.Name = "bpClearAll";
			this.bpClearAll.Size = new System.Drawing.Size(97, 23);
			this.bpClearAll.TabIndex = 28;
			this.bpClearAll.Text = "Clear all lines";
			this.bpClearAll.UseVisualStyleBackColor = true;
			this.bpClearAll.Click += new System.EventHandler(this.BpClearAll_Click);
			// 
			// bpUndo
			// 
			this.bpUndo.Enabled = false;
			this.bpUndo.Location = new System.Drawing.Point(7, 45);
			this.bpUndo.Name = "bpUndo";
			this.bpUndo.Size = new System.Drawing.Size(54, 23);
			this.bpUndo.TabIndex = 29;
			this.bpUndo.Text = "Undo";
			this.bpUndo.UseVisualStyleBackColor = true;
			this.bpUndo.Click += new System.EventHandler(this.BpUndo_Click);
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.rbMoins);
			this.groupBox2.Controls.Add(this.rbPlus);
			this.groupBox2.Controls.Add(this.txbStart);
			this.groupBox2.Controls.Add(this.label7);
			this.groupBox2.Controls.Add(this.bpBPlus);
			this.groupBox2.Controls.Add(this.bpVPlus);
			this.groupBox2.Controls.Add(this.bpRPlus);
			this.groupBox2.Controls.Add(this.bpBMoins);
			this.groupBox2.Controls.Add(this.bpRMoins);
			this.groupBox2.Controls.Add(this.bpUndo);
			this.groupBox2.Controls.Add(this.bpVMoins);
			this.groupBox2.Controls.Add(this.label6);
			this.groupBox2.Controls.Add(this.txbConstant);
			this.groupBox2.Controls.Add(this.bpAddConstant);
			this.groupBox2.Controls.Add(this.label5);
			this.groupBox2.Controls.Add(this.label4);
			this.groupBox2.Controls.Add(this.trkStartV);
			this.groupBox2.Controls.Add(this.txbStartB);
			this.groupBox2.Controls.Add(this.txbStartV);
			this.groupBox2.Controls.Add(this.lblStartColor);
			this.groupBox2.Controls.Add(this.label3);
			this.groupBox2.Controls.Add(this.txbStartR);
			this.groupBox2.Controls.Add(this.trkStartR);
			this.groupBox2.Controls.Add(this.trkStartB);
			this.groupBox2.Location = new System.Drawing.Point(3, 100);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(238, 273);
			this.groupBox2.TabIndex = 30;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Constant color";
			// 
			// rbMoins
			// 
			this.rbMoins.AutoSize = true;
			this.rbMoins.Location = new System.Drawing.Point(134, 34);
			this.rbMoins.Name = "rbMoins";
			this.rbMoins.Size = new System.Drawing.Size(28, 17);
			this.rbMoins.TabIndex = 36;
			this.rbMoins.Text = "-";
			this.rbMoins.UseVisualStyleBackColor = true;
			// 
			// rbPlus
			// 
			this.rbPlus.AutoSize = true;
			this.rbPlus.Checked = true;
			this.rbPlus.Location = new System.Drawing.Point(134, 12);
			this.rbPlus.Name = "rbPlus";
			this.rbPlus.Size = new System.Drawing.Size(31, 17);
			this.rbPlus.TabIndex = 36;
			this.rbPlus.TabStop = true;
			this.rbPlus.Text = "+";
			this.rbPlus.UseVisualStyleBackColor = true;
			// 
			// txbStart
			// 
			this.txbStart.Location = new System.Drawing.Point(201, 22);
			this.txbStart.MaxLength = 3;
			this.txbStart.Name = "txbStart";
			this.txbStart.Size = new System.Drawing.Size(31, 20);
			this.txbStart.TabIndex = 35;
			this.txbStart.Text = "1";
			this.txbStart.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(163, 25);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(32, 13);
			this.label7.TabIndex = 34;
			this.label7.Text = "Start:";
			// 
			// bpBPlus
			// 
			this.bpBPlus.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.bpBPlus.Location = new System.Drawing.Point(159, 167);
			this.bpBPlus.Name = "bpBPlus";
			this.bpBPlus.Size = new System.Drawing.Size(30, 23);
			this.bpBPlus.TabIndex = 30;
			this.bpBPlus.Text = "+";
			this.bpBPlus.UseVisualStyleBackColor = true;
			this.bpBPlus.Click += new System.EventHandler(this.BpBPlus_Click);
			// 
			// bpVPlus
			// 
			this.bpVPlus.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.bpVPlus.Location = new System.Drawing.Point(159, 72);
			this.bpVPlus.Name = "bpVPlus";
			this.bpVPlus.Size = new System.Drawing.Size(30, 23);
			this.bpVPlus.TabIndex = 30;
			this.bpVPlus.Text = "+";
			this.bpVPlus.UseVisualStyleBackColor = true;
			this.bpVPlus.Click += new System.EventHandler(this.BpVPlus_Click);
			// 
			// bpRPlus
			// 
			this.bpRPlus.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.bpRPlus.Location = new System.Drawing.Point(159, 120);
			this.bpRPlus.Name = "bpRPlus";
			this.bpRPlus.Size = new System.Drawing.Size(30, 23);
			this.bpRPlus.TabIndex = 30;
			this.bpRPlus.Text = "+";
			this.bpRPlus.UseVisualStyleBackColor = true;
			this.bpRPlus.Click += new System.EventHandler(this.BpRPlus_Click);
			// 
			// bpBMoins
			// 
			this.bpBMoins.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.bpBMoins.Location = new System.Drawing.Point(21, 167);
			this.bpBMoins.Name = "bpBMoins";
			this.bpBMoins.Size = new System.Drawing.Size(30, 23);
			this.bpBMoins.TabIndex = 30;
			this.bpBMoins.Text = "-";
			this.bpBMoins.UseVisualStyleBackColor = true;
			this.bpBMoins.Click += new System.EventHandler(this.BpBMoins_Click);
			// 
			// bpVMoins
			// 
			this.bpVMoins.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.bpVMoins.Location = new System.Drawing.Point(21, 74);
			this.bpVMoins.Name = "bpVMoins";
			this.bpVMoins.Size = new System.Drawing.Size(30, 23);
			this.bpVMoins.TabIndex = 30;
			this.bpVMoins.Text = "-";
			this.bpVMoins.UseVisualStyleBackColor = true;
			this.bpVMoins.Click += new System.EventHandler(this.BpVMoins_Click);
			// 
			// bpRMoins
			// 
			this.bpRMoins.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.bpRMoins.Location = new System.Drawing.Point(21, 120);
			this.bpRMoins.Name = "bpRMoins";
			this.bpRMoins.Size = new System.Drawing.Size(30, 23);
			this.bpRMoins.TabIndex = 30;
			this.bpRMoins.Text = "-";
			this.bpRMoins.UseVisualStyleBackColor = true;
			this.bpRMoins.Click += new System.EventHandler(this.BpRMoins_Click);
			// 
			// bpImportImage
			// 
			this.bpImportImage.Location = new System.Drawing.Point(5, 65);
			this.bpImportImage.Name = "bpImportImage";
			this.bpImportImage.Size = new System.Drawing.Size(154, 23);
			this.bpImportImage.TabIndex = 31;
			this.bpImportImage.Text = "Import background image";
			this.bpImportImage.UseVisualStyleBackColor = true;
			this.bpImportImage.Click += new System.EventHandler(this.BpImportImage_Click);
			// 
			// lblLine
			// 
			this.lblLine.AutoSize = true;
			this.lblLine.Location = new System.Drawing.Point(197, 70);
			this.lblLine.Name = "lblLine";
			this.lblLine.Size = new System.Drawing.Size(44, 13);
			this.lblLine.TabIndex = 32;
			this.lblLine.Text = "line 271";
			// 
			// bpLoad
			// 
			this.bpLoad.Location = new System.Drawing.Point(3, 7);
			this.bpLoad.Name = "bpLoad";
			this.bpLoad.Size = new System.Drawing.Size(99, 23);
			this.bpLoad.TabIndex = 33;
			this.bpLoad.Text = "Load rasters";
			this.bpLoad.UseVisualStyleBackColor = true;
			this.bpLoad.Click += new System.EventHandler(this.BpLoad_Click);
			// 
			// bpSave
			// 
			this.bpSave.Location = new System.Drawing.Point(106, 7);
			this.bpSave.Name = "bpSave";
			this.bpSave.Size = new System.Drawing.Size(99, 23);
			this.bpSave.TabIndex = 33;
			this.bpSave.Text = "Save rasters";
			this.bpSave.UseVisualStyleBackColor = true;
			this.bpSave.Click += new System.EventHandler(this.BpSave_Click);
			// 
			// bpScrollUp
			// 
			this.bpScrollUp.Location = new System.Drawing.Point(100, 379);
			this.bpScrollUp.Name = "bpScrollUp";
			this.bpScrollUp.Size = new System.Drawing.Size(79, 23);
			this.bpScrollUp.TabIndex = 37;
			this.bpScrollUp.Text = "Scroll Up";
			this.bpScrollUp.UseVisualStyleBackColor = true;
			this.bpScrollUp.Click += new System.EventHandler(this.BpScrollUp_Click);
			// 
			// bpScrollDown
			// 
			this.bpScrollDown.Location = new System.Drawing.Point(100, 408);
			this.bpScrollDown.Name = "bpScrollDown";
			this.bpScrollDown.Size = new System.Drawing.Size(79, 23);
			this.bpScrollDown.TabIndex = 37;
			this.bpScrollDown.Text = "Scroll Down";
			this.bpScrollDown.UseVisualStyleBackColor = true;
			this.bpScrollDown.Click += new System.EventHandler(this.BpScrollDown_Click);
			// 
			// txbScroll
			// 
			this.txbScroll.Location = new System.Drawing.Point(183, 391);
			this.txbScroll.MaxLength = 3;
			this.txbScroll.Name = "txbScroll";
			this.txbScroll.Size = new System.Drawing.Size(31, 20);
			this.txbScroll.TabIndex = 38;
			this.txbScroll.Text = "1";
			this.txbScroll.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(215, 394);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(28, 13);
			this.label8.TabIndex = 27;
			this.label8.Text = "lines";
			// 
			// RasterTablePlus
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1019, 555);
			this.Controls.Add(this.txbScroll);
			this.Controls.Add(this.bpScrollDown);
			this.Controls.Add(this.bpScrollUp);
			this.Controls.Add(this.bpSave);
			this.Controls.Add(this.bpLoad);
			this.Controls.Add(this.lblLine);
			this.Controls.Add(this.bpImportImage);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.bpClearAll);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.bpAddLine);
			this.Controls.Add(this.label8);
			this.Controls.Add(this.pictureBox);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "RasterTablePlus";
			this.ShowIcon = false;
			this.Text = "RasterTablePlus";
			((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.trkStartV)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.trkStartB)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.trkStartR)).EndInit();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.PictureBox pictureBox;
		private System.Windows.Forms.Button bpAddLine;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Button bpGenerate;
		private System.Windows.Forms.TextBox txbLineEnd;
		private System.Windows.Forms.TextBox txbLineStart;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox txbStartB;
		private System.Windows.Forms.TextBox txbStartV;
		private System.Windows.Forms.TextBox txbStartR;
		private System.Windows.Forms.TrackBar trkStartV;
		private System.Windows.Forms.TrackBar trkStartB;
		private System.Windows.Forms.TrackBar trkStartR;
		private System.Windows.Forms.Label lblStartColor;
		private System.Windows.Forms.Button bpAddConstant;
		private System.Windows.Forms.TextBox txbConstant;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Button bpClearAll;
		private System.Windows.Forms.Button bpCopyToClipboard;
		private System.Windows.Forms.Button bpUndo;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Button bpRPlus;
		private System.Windows.Forms.Button bpRMoins;
		private System.Windows.Forms.Button bpBMoins;
		private System.Windows.Forms.Button bpVMoins;
		private System.Windows.Forms.Button bpBPlus;
		private System.Windows.Forms.Button bpVPlus;
		private System.Windows.Forms.Button bpImportImage;
		private System.Windows.Forms.Label lblLine;
		private System.Windows.Forms.Button bpLoad;
		private System.Windows.Forms.Button bpSave;
		private System.Windows.Forms.Button bpGenFade;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.TextBox txbStart;
		private System.Windows.Forms.RadioButton rbMoins;
		private System.Windows.Forms.RadioButton rbPlus;
		private System.Windows.Forms.Button bpScrollUp;
		private System.Windows.Forms.Button bpScrollDown;
		private System.Windows.Forms.TextBox txbScroll;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Button bpImportKit;
	}
}