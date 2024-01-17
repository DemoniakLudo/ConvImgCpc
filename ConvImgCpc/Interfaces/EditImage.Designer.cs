namespace ConvImgCpc {
	partial class EditImage {
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
			this.zoom = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.tailleCrayon = new System.Windows.Forms.ComboBox();
			this.label2 = new System.Windows.Forms.Label();
			this.crayonColor = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// zoom
			// 
			this.zoom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.zoom.FormattingEnabled = true;
			this.zoom.Items.AddRange(new object[] {
            "1",
            "2",
            "4",
            "8"});
			this.zoom.Location = new System.Drawing.Point(57, 6);
			this.zoom.Name = "zoom";
			this.zoom.Size = new System.Drawing.Size(63, 21);
			this.zoom.TabIndex = 0;
			this.zoom.SelectedIndexChanged += new System.EventHandler(this.zoom_SelectedIndexChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(40, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "Zoom :";
			// 
			// tailleCrayon
			// 
			this.tailleCrayon.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.tailleCrayon.FormattingEnabled = true;
			this.tailleCrayon.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "8"});
			this.tailleCrayon.Location = new System.Drawing.Point(82, 142);
			this.tailleCrayon.Name = "tailleCrayon";
			this.tailleCrayon.Size = new System.Drawing.Size(74, 21);
			this.tailleCrayon.TabIndex = 0;
			this.tailleCrayon.SelectedIndexChanged += new System.EventHandler(this.crayon_SelectedIndexChanged);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(3, 145);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(73, 13);
			this.label2.TabIndex = 1;
			this.label2.Text = "Taille crayon :";
			// 
			// crayonColor
			// 
			this.crayonColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.crayonColor.Location = new System.Drawing.Point(29, 55);
			this.crayonColor.Name = "crayonColor";
			this.crayonColor.Size = new System.Drawing.Size(100, 75);
			this.crayonColor.TabIndex = 2;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(36, 33);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(84, 13);
			this.label3.TabIndex = 1;
			this.label3.Text = "Couleur crayon :";
			// 
			// EditImage
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(159, 168);
			this.Controls.Add(this.crayonColor);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.tailleCrayon);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.zoom);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "EditImage";
			this.Text = "Edition image";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.EditImage_FormClosed);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ComboBox zoom;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox tailleCrayon;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label crayonColor;
		private System.Windows.Forms.Label label3;
	}
}