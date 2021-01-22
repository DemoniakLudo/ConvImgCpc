namespace ConvImgCpc {
	partial class EditTrameAscii {
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditTrameAscii));
			this.pictEditMatrice = new System.Windows.Forms.PictureBox();
			this.pictAllMatrice = new System.Windows.Forms.PictureBox();
			this.bpPrev = new System.Windows.Forms.Button();
			this.bpSuiv = new System.Windows.Forms.Button();
			this.lblPen0 = new System.Windows.Forms.Label();
			this.lblPen1 = new System.Windows.Forms.Label();
			this.lblPen2 = new System.Windows.Forms.Label();
			this.lblPen3 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.lblPenLeft = new System.Windows.Forms.Label();
			this.lblPenRight = new System.Windows.Forms.Label();
			this.bpRead = new System.Windows.Forms.Button();
			this.bpSave = new System.Windows.Forms.Button();
			this.bpAutoGene = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.pictEditMatrice)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictAllMatrice)).BeginInit();
			this.SuspendLayout();
			// 
			// pictEditMatrice
			// 
			resources.ApplyResources(this.pictEditMatrice, "pictEditMatrice");
			this.pictEditMatrice.Name = "pictEditMatrice";
			this.pictEditMatrice.TabStop = false;
			this.pictEditMatrice.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictEditMatrice_MouseMove);
			this.pictEditMatrice.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictEditMatrice_MouseMove);
			// 
			// pictAllMatrice
			// 
			resources.ApplyResources(this.pictAllMatrice, "pictAllMatrice");
			this.pictAllMatrice.Name = "pictAllMatrice";
			this.pictAllMatrice.TabStop = false;
			this.pictAllMatrice.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictAllMatrice_MouseDown);
			// 
			// bpPrev
			// 
			resources.ApplyResources(this.bpPrev, "bpPrev");
			this.bpPrev.Name = "bpPrev";
			this.bpPrev.UseVisualStyleBackColor = true;
			this.bpPrev.Click += new System.EventHandler(this.bpPrev_Click);
			// 
			// bpSuiv
			// 
			resources.ApplyResources(this.bpSuiv, "bpSuiv");
			this.bpSuiv.Name = "bpSuiv";
			this.bpSuiv.UseVisualStyleBackColor = true;
			this.bpSuiv.Click += new System.EventHandler(this.bpSuiv_Click);
			// 
			// lblPen0
			// 
			this.lblPen0.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			resources.ApplyResources(this.lblPen0, "lblPen0");
			this.lblPen0.Name = "lblPen0";
			this.lblPen0.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lblPen0_Click);
			// 
			// lblPen1
			// 
			this.lblPen1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			resources.ApplyResources(this.lblPen1, "lblPen1");
			this.lblPen1.Name = "lblPen1";
			this.lblPen1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lblPen1_Click);
			// 
			// lblPen2
			// 
			this.lblPen2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			resources.ApplyResources(this.lblPen2, "lblPen2");
			this.lblPen2.Name = "lblPen2";
			this.lblPen2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lblPen2_Click);
			// 
			// lblPen3
			// 
			this.lblPen3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			resources.ApplyResources(this.lblPen3, "lblPen3");
			this.lblPen3.Name = "lblPen3";
			this.lblPen3.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lblPen3_Click);
			// 
			// label1
			// 
			resources.ApplyResources(this.label1, "label1");
			this.label1.Name = "label1";
			// 
			// label2
			// 
			resources.ApplyResources(this.label2, "label2");
			this.label2.Name = "label2";
			// 
			// lblPenLeft
			// 
			this.lblPenLeft.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			resources.ApplyResources(this.lblPenLeft, "lblPenLeft");
			this.lblPenLeft.Name = "lblPenLeft";
			// 
			// lblPenRight
			// 
			this.lblPenRight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			resources.ApplyResources(this.lblPenRight, "lblPenRight");
			this.lblPenRight.Name = "lblPenRight";
			// 
			// bpRead
			// 
			resources.ApplyResources(this.bpRead, "bpRead");
			this.bpRead.Name = "bpRead";
			this.bpRead.UseVisualStyleBackColor = true;
			this.bpRead.Click += new System.EventHandler(this.bpRead_Click);
			// 
			// bpSave
			// 
			resources.ApplyResources(this.bpSave, "bpSave");
			this.bpSave.Name = "bpSave";
			this.bpSave.UseVisualStyleBackColor = true;
			this.bpSave.Click += new System.EventHandler(this.bpSave_Click);
			// 
			// bpAutoGene
			// 
			resources.ApplyResources(this.bpAutoGene, "bpAutoGene");
			this.bpAutoGene.Name = "bpAutoGene";
			this.bpAutoGene.UseVisualStyleBackColor = true;
			this.bpAutoGene.Click += new System.EventHandler(this.bpAutoGene_Click);
			// 
			// EditTrameAscii
			// 
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.bpAutoGene);
			this.Controls.Add(this.bpSave);
			this.Controls.Add(this.bpRead);
			this.Controls.Add(this.lblPenRight);
			this.Controls.Add(this.lblPenLeft);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.lblPen3);
			this.Controls.Add(this.lblPen2);
			this.Controls.Add(this.lblPen1);
			this.Controls.Add(this.lblPen0);
			this.Controls.Add(this.bpSuiv);
			this.Controls.Add(this.bpPrev);
			this.Controls.Add(this.pictAllMatrice);
			this.Controls.Add(this.pictEditMatrice);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "EditTrameAscii";
			this.ShowIcon = false;
			((System.ComponentModel.ISupportInitialize)(this.pictEditMatrice)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictAllMatrice)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.PictureBox pictEditMatrice;
		private System.Windows.Forms.PictureBox pictAllMatrice;
		private System.Windows.Forms.Button bpPrev;
		private System.Windows.Forms.Button bpSuiv;
		private System.Windows.Forms.Label lblPen0;
		private System.Windows.Forms.Label lblPen1;
		private System.Windows.Forms.Label lblPen2;
		private System.Windows.Forms.Label lblPen3;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label lblPenLeft;
		private System.Windows.Forms.Label lblPenRight;
		private System.Windows.Forms.Button bpRead;
		private System.Windows.Forms.Button bpSave;
		private System.Windows.Forms.Button bpAutoGene;
	}
}