namespace ConvImgCpc {
	partial class CreationImages {
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
			this.rbSingle = new System.Windows.Forms.RadioButton();
			this.rbAnim = new System.Windows.Forms.RadioButton();
			this.bpCreer = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.nbImages = new System.Windows.Forms.NumericUpDown();
			this.lblNbImages = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.nbImages)).BeginInit();
			this.SuspendLayout();
			// 
			// rbSingle
			// 
			this.rbSingle.AutoSize = true;
			this.rbSingle.Location = new System.Drawing.Point(12, 37);
			this.rbSingle.Name = "rbSingle";
			this.rbSingle.Size = new System.Drawing.Size(89, 17);
			this.rbSingle.TabIndex = 0;
			this.rbSingle.TabStop = true;
			this.rbSingle.Text = "Image unique";
			this.rbSingle.UseVisualStyleBackColor = true;
			// 
			// rbAnim
			// 
			this.rbAnim.AutoSize = true;
			this.rbAnim.Location = new System.Drawing.Point(12, 77);
			this.rbAnim.Name = "rbAnim";
			this.rbAnim.Size = new System.Drawing.Size(71, 17);
			this.rbAnim.TabIndex = 0;
			this.rbAnim.TabStop = true;
			this.rbAnim.Text = "Animation";
			this.rbAnim.UseVisualStyleBackColor = true;
			// 
			// bpCreer
			// 
			this.bpCreer.Location = new System.Drawing.Point(47, 120);
			this.bpCreer.Name = "bpCreer";
			this.bpCreer.Size = new System.Drawing.Size(116, 23);
			this.bpCreer.TabIndex = 1;
			this.bpCreer.Text = "Créer";
			this.bpCreer.UseVisualStyleBackColor = true;
			this.bpCreer.Click += new System.EventHandler(this.bpCreer_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(44, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(119, 13);
			this.label1.TabIndex = 2;
			this.label1.Text = "Type de média à créer :";
			// 
			// nbImages
			// 
			this.nbImages.Location = new System.Drawing.Point(101, 77);
			this.nbImages.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
			this.nbImages.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.nbImages.Name = "nbImages";
			this.nbImages.Size = new System.Drawing.Size(48, 20);
			this.nbImages.TabIndex = 3;
			this.nbImages.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// lblNbImages
			// 
			this.lblNbImages.AutoSize = true;
			this.lblNbImages.Location = new System.Drawing.Point(155, 79);
			this.lblNbImages.Name = "lblNbImages";
			this.lblNbImages.Size = new System.Drawing.Size(46, 13);
			this.lblNbImages.TabIndex = 4;
			this.lblNbImages.Text = "image(s)";
			// 
			// CreationImages
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(216, 155);
			this.Controls.Add(this.lblNbImages);
			this.Controls.Add(this.nbImages);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.bpCreer);
			this.Controls.Add(this.rbAnim);
			this.Controls.Add(this.rbSingle);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "CreationImages";
			this.Text = "CreationImages";
			((System.ComponentModel.ISupportInitialize)(this.nbImages)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.RadioButton rbSingle;
		private System.Windows.Forms.RadioButton rbAnim;
		private System.Windows.Forms.Button bpCreer;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.NumericUpDown nbImages;
		private System.Windows.Forms.Label lblNbImages;
	}
}