namespace TD.Base.Errors.Managing.TestDrive
{
	partial class Form1
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.cmdunhandled = new System.Windows.Forms.Button();
			this.cmdhandled = new System.Windows.Forms.Button();
			this.cmdhibrid = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// cmdunhandled
			// 
			this.cmdunhandled.Location = new System.Drawing.Point(23, 98);
			this.cmdunhandled.Name = "cmdunhandled";
			this.cmdunhandled.Size = new System.Drawing.Size(75, 23);
			this.cmdunhandled.TabIndex = 0;
			this.cmdunhandled.Text = "unhandled";
			this.cmdunhandled.UseVisualStyleBackColor = true;
			this.cmdunhandled.Click += new System.EventHandler(this.cmdunhandled_Click);
			// 
			// cmdhandled
			// 
			this.cmdhandled.Location = new System.Drawing.Point(154, 98);
			this.cmdhandled.Name = "cmdhandled";
			this.cmdhandled.Size = new System.Drawing.Size(75, 23);
			this.cmdhandled.TabIndex = 1;
			this.cmdhandled.Text = "handled";
			this.cmdhandled.UseVisualStyleBackColor = true;
			this.cmdhandled.Click += new System.EventHandler(this.cmdhandled_Click);
			// 
			// cmdhibrid
			// 
			this.cmdhibrid.Location = new System.Drawing.Point(103, 161);
			this.cmdhibrid.Name = "cmdhibrid";
			this.cmdhibrid.Size = new System.Drawing.Size(75, 23);
			this.cmdhibrid.TabIndex = 2;
			this.cmdhibrid.Text = "hibrid";
			this.cmdhibrid.UseVisualStyleBackColor = true;
			this.cmdhibrid.Click += new System.EventHandler(this.cmdhibrid_Click);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(284, 262);
			this.Controls.Add(this.cmdhibrid);
			this.Controls.Add(this.cmdhandled);
			this.Controls.Add(this.cmdunhandled);
			this.Name = "Form1";
			this.Text = "Form1";
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button cmdunhandled;
		private System.Windows.Forms.Button cmdhandled;
		private System.Windows.Forms.Button cmdhibrid;
	}
}

