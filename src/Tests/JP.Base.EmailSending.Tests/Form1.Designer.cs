namespace TD.Base.EmailSending.TestDrive
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
			this.button1 = new System.Windows.Forms.Button();
			this.txtfrom = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.txtto = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.txtsubject = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.txtmsg = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.txtbcc = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(352, 229);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(75, 23);
			this.button1.TabIndex = 6;
			this.button1.Text = "enviar";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// txtfrom
			// 
			this.txtfrom.Location = new System.Drawing.Point(53, 6);
			this.txtfrom.Name = "txtfrom";
			this.txtfrom.Size = new System.Drawing.Size(269, 20);
			this.txtfrom.TabIndex = 0;
			this.txtfrom.Text = "juan.touron@tddesarrollos.com";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(27, 13);
			this.label1.TabIndex = 2;
			this.label1.Text = "from";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(12, 35);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(16, 13);
			this.label2.TabIndex = 4;
			this.label2.Text = "to";
			// 
			// txtto
			// 
			this.txtto.Location = new System.Drawing.Point(53, 32);
			this.txtto.Name = "txtto";
			this.txtto.Size = new System.Drawing.Size(269, 20);
			this.txtto.TabIndex = 1;
			this.txtto.Text = "silverlight@silver.light.com";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(12, 61);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(41, 13);
			this.label3.TabIndex = 6;
			this.label3.Text = "subject";
			// 
			// txtsubject
			// 
			this.txtsubject.Location = new System.Drawing.Point(53, 58);
			this.txtsubject.Name = "txtsubject";
			this.txtsubject.Size = new System.Drawing.Size(269, 20);
			this.txtsubject.TabIndex = 2;
			this.txtsubject.Text = "this is a testing devices";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(12, 113);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(26, 13);
			this.label4.TabIndex = 8;
			this.label4.Text = "msg";
			// 
			// txtmsg
			// 
			this.txtmsg.Location = new System.Drawing.Point(53, 110);
			this.txtmsg.Multiline = true;
			this.txtmsg.Name = "txtmsg";
			this.txtmsg.Size = new System.Drawing.Size(374, 113);
			this.txtmsg.TabIndex = 4;
			this.txtmsg.Text = "this is a sample simple message...";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(12, 87);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(25, 13);
			this.label5.TabIndex = 10;
			this.label5.Text = "bcc";
			// 
			// txtbcc
			// 
			this.txtbcc.Location = new System.Drawing.Point(53, 84);
			this.txtbcc.Name = "txtbcc";
			this.txtbcc.Size = new System.Drawing.Size(269, 20);
			this.txtbcc.TabIndex = 3;
			this.txtbcc.Text = "theunsuspectedlistener@spy.com";
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(440, 260);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.txtbcc);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.txtmsg);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.txtsubject);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.txtto);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.txtfrom);
			this.Controls.Add(this.button1);
			this.Name = "Form1";
			this.Text = "Form1";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.TextBox txtfrom;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox txtto;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox txtsubject;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox txtmsg;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox txtbcc;
	}
}

