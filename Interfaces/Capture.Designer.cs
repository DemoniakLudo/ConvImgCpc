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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Capture));
			this.pictCapture = new System.Windows.Forms.PictureBox();
			this.rbCapt1 = new System.Windows.Forms.RadioButton();
			this.rbCapt2 = new System.Windows.Forms.RadioButton();
			this.rbCapt4 = new System.Windows.Forms.RadioButton();
			this.comboBanque = new System.Windows.Forms.ComboBox();
			this.numSprite = new System.Windows.Forms.NumericUpDown();
			this.label1 = new System.Windows.Forms.Label();
			this.bpCapture = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.pictCapture)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numSprite)).BeginInit();
			this.SuspendLayout();
			// 
			// pictCapture
			// 
			resources.ApplyResources(this.pictCapture, "pictCapture");
			this.pictCapture.Name = "pictCapture";
			this.pictCapture.TabStop = false;
			// 
			// rbCapt1
			// 
			resources.ApplyResources(this.rbCapt1, "rbCapt1");
			this.rbCapt1.Checked = true;
			this.rbCapt1.Name = "rbCapt1";
			this.rbCapt1.TabStop = true;
			this.rbCapt1.UseVisualStyleBackColor = true;
			this.rbCapt1.CheckedChanged += new System.EventHandler(this.rbCapt1_CheckedChanged);
			// 
			// rbCapt2
			// 
			resources.ApplyResources(this.rbCapt2, "rbCapt2");
			this.rbCapt2.Name = "rbCapt2";
			this.rbCapt2.UseVisualStyleBackColor = true;
			this.rbCapt2.CheckedChanged += new System.EventHandler(this.rbCapt2_CheckedChanged);
			// 
			// rbCapt4
			// 
			resources.ApplyResources(this.rbCapt4, "rbCapt4");
			this.rbCapt4.Name = "rbCapt4";
			this.rbCapt4.UseVisualStyleBackColor = true;
			this.rbCapt4.CheckedChanged += new System.EventHandler(this.rbCapt4_CheckedChanged);
			// 
			// comboBanque
			// 
			this.comboBanque.FormattingEnabled = true;
			this.comboBanque.Items.AddRange(new object[] {
            resources.GetString("comboBanque.Items"),
            resources.GetString("comboBanque.Items1"),
            resources.GetString("comboBanque.Items2"),
            resources.GetString("comboBanque.Items3")});
			resources.ApplyResources(this.comboBanque, "comboBanque");
			this.comboBanque.Name = "comboBanque";
			// 
			// numSprite
			// 
			resources.ApplyResources(this.numSprite, "numSprite");
			this.numSprite.Maximum = new decimal(new int[] {
            15,
            0,
            0,
            0});
			this.numSprite.Name = "numSprite";
			// 
			// label1
			// 
			resources.ApplyResources(this.label1, "label1");
			this.label1.Name = "label1";
			// 
			// bpCapture
			// 
			resources.ApplyResources(this.bpCapture, "bpCapture");
			this.bpCapture.Name = "bpCapture";
			this.bpCapture.UseVisualStyleBackColor = true;
			this.bpCapture.Click += new System.EventHandler(this.bpCapture_Click);
			// 
			// Capture
			// 
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ControlBox = false;
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
	}
}