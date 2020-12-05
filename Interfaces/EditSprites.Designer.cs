namespace ConvImgCpc {
	partial class EditSprites {
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
			this.bpPrev = new System.Windows.Forms.Button();
			this.bpSuiv = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.lblPenLeft = new System.Windows.Forms.Label();
			this.lblPenRight = new System.Windows.Forms.Label();
			this.bpRead = new System.Windows.Forms.Button();
			this.bpSave = new System.Windows.Forms.Button();
			this.pictAllSprites = new System.Windows.Forms.PictureBox();
			this.lblSelSprite = new System.Windows.Forms.Label();
			this.pictTest = new System.Windows.Forms.PictureBox();
			this.rb1Sprite = new System.Windows.Forms.RadioButton();
			this.rb2Sprite = new System.Windows.Forms.RadioButton();
			this.rb4Sprite = new System.Windows.Forms.RadioButton();
			this.bpTest = new System.Windows.Forms.Button();
			this.zoomX = new System.Windows.Forms.NumericUpDown();
			this.zoomY = new System.Windows.Forms.NumericUpDown();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.pictEditMatrice)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictAllSprites)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictTest)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.zoomX)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.zoomY)).BeginInit();
			this.SuspendLayout();
			// 
			// pictEditMatrice
			// 
			this.pictEditMatrice.Location = new System.Drawing.Point(160, 71);
			this.pictEditMatrice.Name = "pictEditMatrice";
			this.pictEditMatrice.Size = new System.Drawing.Size(640, 640);
			this.pictEditMatrice.TabIndex = 0;
			this.pictEditMatrice.TabStop = false;
			this.pictEditMatrice.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictEditMatrice_MouseMove);
			this.pictEditMatrice.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictEditMatrice_MouseMove);
			// 
			// bpPrev
			// 
			this.bpPrev.Location = new System.Drawing.Point(3, 71);
			this.bpPrev.Name = "bpPrev";
			this.bpPrev.Size = new System.Drawing.Size(75, 23);
			this.bpPrev.TabIndex = 2;
			this.bpPrev.Text = "Précédent";
			this.bpPrev.UseVisualStyleBackColor = true;
			this.bpPrev.Visible = false;
			this.bpPrev.Click += new System.EventHandler(this.bpPrev_Click);
			// 
			// bpSuiv
			// 
			this.bpSuiv.Location = new System.Drawing.Point(952, 71);
			this.bpSuiv.Name = "bpSuiv";
			this.bpSuiv.Size = new System.Drawing.Size(75, 23);
			this.bpSuiv.TabIndex = 2;
			this.bpSuiv.Text = "Suivant";
			this.bpSuiv.UseVisualStyleBackColor = true;
			this.bpSuiv.Click += new System.EventHandler(this.bpSuiv_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 174);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(62, 13);
			this.label1.TabIndex = 4;
			this.label1.Text = "Bp gauche:";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(12, 239);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(52, 13);
			this.label2.TabIndex = 4;
			this.label2.Text = "Bp droite:";
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
			this.bpRead.Location = new System.Drawing.Point(15, 656);
			this.bpRead.Name = "bpRead";
			this.bpRead.Size = new System.Drawing.Size(85, 23);
			this.bpRead.TabIndex = 6;
			this.bpRead.Text = "Lire sprites";
			this.bpRead.UseVisualStyleBackColor = true;
			this.bpRead.Visible = false;
			this.bpRead.Click += new System.EventHandler(this.bpRead_Click);
			// 
			// bpSave
			// 
			this.bpSave.Location = new System.Drawing.Point(15, 685);
			this.bpSave.Name = "bpSave";
			this.bpSave.Size = new System.Drawing.Size(85, 23);
			this.bpSave.TabIndex = 6;
			this.bpSave.Text = "Sauver sprites";
			this.bpSave.UseVisualStyleBackColor = true;
			this.bpSave.Visible = false;
			this.bpSave.Click += new System.EventHandler(this.bpSave_Click);
			// 
			// pictAllSprites
			// 
			this.pictAllSprites.Location = new System.Drawing.Point(3, 1);
			this.pictAllSprites.Name = "pictAllSprites";
			this.pictAllSprites.Size = new System.Drawing.Size(1024, 64);
			this.pictAllSprites.TabIndex = 1;
			this.pictAllSprites.TabStop = false;
			this.pictAllSprites.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictAllMatrice_MouseDown);
			// 
			// lblSelSprite
			// 
			this.lblSelSprite.AutoSize = true;
			this.lblSelSprite.Location = new System.Drawing.Point(0, 111);
			this.lblSelSprite.Name = "lblSelSprite";
			this.lblSelSprite.Size = new System.Drawing.Size(59, 13);
			this.lblSelSprite.TabIndex = 7;
			this.lblSelSprite.Text = "lblSelSprite";
			// 
			// pictTest
			// 
			this.pictTest.Location = new System.Drawing.Point(899, 199);
			this.pictTest.Name = "pictTest";
			this.pictTest.Size = new System.Drawing.Size(512, 512);
			this.pictTest.TabIndex = 8;
			this.pictTest.TabStop = false;
			// 
			// rb1Sprite
			// 
			this.rb1Sprite.AutoSize = true;
			this.rb1Sprite.Checked = true;
			this.rb1Sprite.Location = new System.Drawing.Point(910, 170);
			this.rb1Sprite.Name = "rb1Sprite";
			this.rb1Sprite.Size = new System.Drawing.Size(61, 17);
			this.rb1Sprite.TabIndex = 9;
			this.rb1Sprite.TabStop = true;
			this.rb1Sprite.Text = "1 Sprite";
			this.rb1Sprite.UseVisualStyleBackColor = true;
			// 
			// rb2Sprite
			// 
			this.rb2Sprite.AutoSize = true;
			this.rb2Sprite.Location = new System.Drawing.Point(1001, 170);
			this.rb2Sprite.Name = "rb2Sprite";
			this.rb2Sprite.Size = new System.Drawing.Size(75, 17);
			this.rb2Sprite.TabIndex = 9;
			this.rb2Sprite.Text = "2x2 sprites";
			this.rb2Sprite.UseVisualStyleBackColor = true;
			// 
			// rb4Sprite
			// 
			this.rb4Sprite.AutoSize = true;
			this.rb4Sprite.Location = new System.Drawing.Point(1092, 172);
			this.rb4Sprite.Name = "rb4Sprite";
			this.rb4Sprite.Size = new System.Drawing.Size(75, 17);
			this.rb4Sprite.TabIndex = 9;
			this.rb4Sprite.Text = "4x4 sprites";
			this.rb4Sprite.UseVisualStyleBackColor = true;
			// 
			// bpTest
			// 
			this.bpTest.Location = new System.Drawing.Point(1255, 164);
			this.bpTest.Name = "bpTest";
			this.bpTest.Size = new System.Drawing.Size(75, 23);
			this.bpTest.TabIndex = 10;
			this.bpTest.Text = "Test !";
			this.bpTest.UseVisualStyleBackColor = true;
			this.bpTest.Click += new System.EventHandler(this.bpTest_Click);
			// 
			// zoomX
			// 
			this.zoomX.Location = new System.Drawing.Point(952, 141);
			this.zoomX.Maximum = new decimal(new int[] {
            3,
            0,
            0,
            0});
			this.zoomX.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.zoomX.Name = "zoomX";
			this.zoomX.Size = new System.Drawing.Size(38, 20);
			this.zoomX.TabIndex = 11;
			this.zoomX.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// zoomY
			// 
			this.zoomY.Location = new System.Drawing.Point(1082, 141);
			this.zoomY.Maximum = new decimal(new int[] {
            3,
            0,
            0,
            0});
			this.zoomY.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.zoomY.Name = "zoomY";
			this.zoomY.Size = new System.Drawing.Size(38, 20);
			this.zoomY.TabIndex = 11;
			this.zoomY.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(896, 145);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(44, 13);
			this.label3.TabIndex = 12;
			this.label3.Text = "Zoom X";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(1032, 145);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(44, 13);
			this.label4.TabIndex = 12;
			this.label4.Text = "Zoom Y";
			// 
			// EditSprites
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1414, 715);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.zoomY);
			this.Controls.Add(this.zoomX);
			this.Controls.Add(this.bpTest);
			this.Controls.Add(this.rb4Sprite);
			this.Controls.Add(this.rb2Sprite);
			this.Controls.Add(this.rb1Sprite);
			this.Controls.Add(this.pictTest);
			this.Controls.Add(this.lblSelSprite);
			this.Controls.Add(this.bpSave);
			this.Controls.Add(this.bpRead);
			this.Controls.Add(this.lblPenRight);
			this.Controls.Add(this.lblPenLeft);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.bpSuiv);
			this.Controls.Add(this.bpPrev);
			this.Controls.Add(this.pictAllSprites);
			this.Controls.Add(this.pictEditMatrice);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "EditSprites";
			this.ShowIcon = false;
			this.Text = "Edition Sprites Hard";
			((System.ComponentModel.ISupportInitialize)(this.pictEditMatrice)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictAllSprites)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictTest)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.zoomX)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.zoomY)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.PictureBox pictEditMatrice;
		private System.Windows.Forms.Button bpPrev;
		private System.Windows.Forms.Button bpSuiv;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label lblPenLeft;
		private System.Windows.Forms.Label lblPenRight;
		private System.Windows.Forms.Button bpRead;
		private System.Windows.Forms.Button bpSave;
		private System.Windows.Forms.PictureBox pictAllSprites;
		private System.Windows.Forms.Label lblSelSprite;
		private System.Windows.Forms.PictureBox pictTest;
		private System.Windows.Forms.RadioButton rb1Sprite;
		private System.Windows.Forms.RadioButton rb2Sprite;
		private System.Windows.Forms.RadioButton rb4Sprite;
		private System.Windows.Forms.Button bpTest;
		private System.Windows.Forms.NumericUpDown zoomX;
		private System.Windows.Forms.NumericUpDown zoomY;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
	}
}