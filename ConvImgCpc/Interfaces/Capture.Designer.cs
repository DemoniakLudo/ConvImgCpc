namespace ConvImgCpc {
	partial class Capture {
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
			this.pictCapture = new System.Windows.Forms.PictureBox();
			this.rbCapt1 = new System.Windows.Forms.RadioButton();
			this.rbCapt2 = new System.Windows.Forms.RadioButton();
			this.rbCapt4 = new System.Windows.Forms.RadioButton();
			this.comboBanque = new System.Windows.Forms.ComboBox();
			this.numSprite = new System.Windows.Forms.NumericUpDown();
			this.label1 = new System.Windows.Forms.Label();
			this.bpCapture = new System.Windows.Forms.Button();
			this.rbCaptUser = new System.Windows.Forms.RadioButton();
			this.lblNbX = new System.Windows.Forms.Label();
			this.lblNbY = new System.Windows.Forms.Label();
			this.txbNbX = new System.Windows.Forms.TextBox();
			this.txbNbY = new System.Windows.Forms.TextBox();
			((System.ComponentModel.ISupportInitialize)(this.pictCapture)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numSprite)).BeginInit();
			this.SuspendLayout();
			// 
			// pictCapture
			// 
			this.pictCapture.Location = new System.Drawing.Point(276, 12);
			this.pictCapture.Name = "pictCapture";
			this.pictCapture.Size = new System.Drawing.Size(512, 512);
			this.pictCapture.TabIndex = 9;
			this.pictCapture.TabStop = false;
			// 
			// rbCapt1
			// 
			this.rbCapt1.AutoSize = true;
			this.rbCapt1.Checked = true;
			this.rbCapt1.Location = new System.Drawing.Point(12, 12);
			this.rbCapt1.Name = "rbCapt1";
			this.rbCapt1.Size = new System.Drawing.Size(14, 13);
			this.rbCapt1.TabIndex = 10;
			this.rbCapt1.TabStop = true;
			this.rbCapt1.UseVisualStyleBackColor = true;
			this.rbCapt1.CheckedChanged += new System.EventHandler(this.rbCapt1_CheckedChanged);
			// 
			// rbCapt2
			// 
			this.rbCapt2.AutoSize = true;
			this.rbCapt2.Location = new System.Drawing.Point(12, 36);
			this.rbCapt2.Name = "rbCapt2";
			this.rbCapt2.Size = new System.Drawing.Size(14, 13);
			this.rbCapt2.TabIndex = 10;
			this.rbCapt2.UseVisualStyleBackColor = true;
			this.rbCapt2.CheckedChanged += new System.EventHandler(this.rbCapt2_CheckedChanged);
			// 
			// rbCapt4
			// 
			this.rbCapt4.AutoSize = true;
			this.rbCapt4.Location = new System.Drawing.Point(12, 60);
			this.rbCapt4.Name = "rbCapt4";
			this.rbCapt4.Size = new System.Drawing.Size(14, 13);
			this.rbCapt4.TabIndex = 10;
			this.rbCapt4.UseVisualStyleBackColor = true;
			this.rbCapt4.CheckedChanged += new System.EventHandler(this.rbCapt4_CheckedChanged);
			// 
			// comboBanque
			// 
			this.comboBanque.FormattingEnabled = true;
			this.comboBanque.Items.AddRange(new object[] {
            "Bank 1",
            "Bank 2",
            "Bank 3",
            "Bank 4",
            "Bank 5",
            "Bank 6",
            "Bank 7",
            "Bank 8"});
			this.comboBanque.Location = new System.Drawing.Point(12, 332);
			this.comboBanque.Name = "comboBanque";
			this.comboBanque.Size = new System.Drawing.Size(89, 21);
			this.comboBanque.TabIndex = 14;
			// 
			// numSprite
			// 
			this.numSprite.Location = new System.Drawing.Point(65, 309);
			this.numSprite.Maximum = new decimal(new int[] {
            15,
            0,
            0,
            0});
			this.numSprite.Name = "numSprite";
			this.numSprite.Size = new System.Drawing.Size(36, 20);
			this.numSprite.TabIndex = 15;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 311);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(47, 13);
			this.label1.TabIndex = 16;
			this.label1.Text = "Sprite n°";
			// 
			// bpCapture
			// 
			this.bpCapture.Location = new System.Drawing.Point(128, 321);
			this.bpCapture.Name = "bpCapture";
			this.bpCapture.Size = new System.Drawing.Size(75, 23);
			this.bpCapture.TabIndex = 17;
			this.bpCapture.UseVisualStyleBackColor = true;
			this.bpCapture.Click += new System.EventHandler(this.bpCapture_Click);
			// 
			// rbCaptUser
			// 
			this.rbCaptUser.AutoSize = true;
			this.rbCaptUser.Location = new System.Drawing.Point(12, 84);
			this.rbCaptUser.Name = "rbCaptUser";
			this.rbCaptUser.Size = new System.Drawing.Size(14, 13);
			this.rbCaptUser.TabIndex = 18;
			this.rbCaptUser.UseVisualStyleBackColor = true;
			this.rbCaptUser.CheckedChanged += new System.EventHandler(this.rbCaptUser_CheckedChanged);
			// 
			// lblNbX
			// 
			this.lblNbX.AutoSize = true;
			this.lblNbX.Location = new System.Drawing.Point(142, 87);
			this.lblNbX.Name = "lblNbX";
			this.lblNbX.Size = new System.Drawing.Size(17, 13);
			this.lblNbX.TabIndex = 19;
			this.lblNbX.Text = "X:";
			this.lblNbX.Visible = false;
			// 
			// lblNbY
			// 
			this.lblNbY.AutoSize = true;
			this.lblNbY.Location = new System.Drawing.Point(142, 109);
			this.lblNbY.Name = "lblNbY";
			this.lblNbY.Size = new System.Drawing.Size(17, 13);
			this.lblNbY.TabIndex = 19;
			this.lblNbY.Text = "Y:";
			this.lblNbY.Visible = false;
			// 
			// txbNbX
			// 
			this.txbNbX.Location = new System.Drawing.Point(165, 86);
			this.txbNbX.Name = "txbNbX";
			this.txbNbX.Size = new System.Drawing.Size(18, 20);
			this.txbNbX.TabIndex = 20;
			this.txbNbX.Text = "1";
			this.txbNbX.Visible = false;
			this.txbNbX.TextChanged += new System.EventHandler(this.txbNbX_TextChanged);
			// 
			// txbNbY
			// 
			this.txbNbY.Location = new System.Drawing.Point(165, 106);
			this.txbNbY.Name = "txbNbY";
			this.txbNbY.Size = new System.Drawing.Size(18, 20);
			this.txbNbY.TabIndex = 20;
			this.txbNbY.Text = "1";
			this.txbNbY.Visible = false;
			this.txbNbY.TextChanged += new System.EventHandler(this.txbNbY_TextChanged);
			// 
			// Capture
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(800, 535);
			this.Controls.Add(this.txbNbY);
			this.Controls.Add(this.txbNbX);
			this.Controls.Add(this.lblNbY);
			this.Controls.Add(this.lblNbX);
			this.Controls.Add(this.rbCaptUser);
			this.Controls.Add(this.bpCapture);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.numSprite);
			this.Controls.Add(this.comboBanque);
			this.Controls.Add(this.rbCapt4);
			this.Controls.Add(this.rbCapt2);
			this.Controls.Add(this.rbCapt1);
			this.Controls.Add(this.pictCapture);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "Capture";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Capture sprites hard";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Capture_FormClosed);
			((System.ComponentModel.ISupportInitialize)(this.pictCapture)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numSprite)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.PictureBox pictCapture;
		private System.Windows.Forms.RadioButton rbCapt1;
		private System.Windows.Forms.RadioButton rbCapt2;
		private System.Windows.Forms.RadioButton rbCapt4;
		private System.Windows.Forms.ComboBox comboBanque;
		private System.Windows.Forms.NumericUpDown numSprite;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button bpCapture;
		private System.Windows.Forms.RadioButton rbCaptUser;
		private System.Windows.Forms.Label lblNbX;
		private System.Windows.Forms.Label lblNbY;
		private System.Windows.Forms.TextBox txbNbX;
		private System.Windows.Forms.TextBox txbNbY;
	}
}