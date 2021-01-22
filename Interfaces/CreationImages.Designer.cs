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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CreationImages));
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
			resources.ApplyResources(this.rbSingle, "rbSingle");
			this.rbSingle.Name = "rbSingle";
			this.rbSingle.TabStop = true;
			this.rbSingle.UseVisualStyleBackColor = true;
			// 
			// rbAnim
			// 
			resources.ApplyResources(this.rbAnim, "rbAnim");
			this.rbAnim.Name = "rbAnim";
			this.rbAnim.TabStop = true;
			this.rbAnim.UseVisualStyleBackColor = true;
			// 
			// bpCreer
			// 
			resources.ApplyResources(this.bpCreer, "bpCreer");
			this.bpCreer.Name = "bpCreer";
			this.bpCreer.UseVisualStyleBackColor = true;
			this.bpCreer.Click += new System.EventHandler(this.bpCreer_Click);
			// 
			// label1
			// 
			resources.ApplyResources(this.label1, "label1");
			this.label1.Name = "label1";
			// 
			// nbImages
			// 
			resources.ApplyResources(this.nbImages, "nbImages");
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
			this.nbImages.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// lblNbImages
			// 
			resources.ApplyResources(this.lblNbImages, "lblNbImages");
			this.lblNbImages.Name = "lblNbImages";
			// 
			// CreationImages
			// 
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
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