namespace TD.Forms.GenericABM.FormTestDrive
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
			TD.Forms.GenericABM.FormTestDrive.User user2 = new TD.Forms.GenericABM.FormTestDrive.User();
			this.userDetails1 = new TD.Forms.GenericABM.FormTestDrive.UserDetails();
			this.itemDetails1 = new TD.Forms.GenericABM.FormTestDrive.ItemDetails();
			this.usersList1 = new TD.Forms.GenericABM.FormTestDrive.UsersList();
			this.itemsList1 = new TD.Forms.GenericABM.FormTestDrive.ItemsList();
			this.GrpDatosCargados.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.PixBox)).BeginInit();
			this.GrpCategorias.SuspendLayout();
			this.GrpCargaDatos.SuspendLayout();
			this.PNLCategorias.SuspendLayout();
			this.SPCDatosCargados.Panel1.SuspendLayout();
			this.SPCDatosCargados.Panel2.SuspendLayout();
			this.SPCDatosCargados.SuspendLayout();
			this.PNLDatosCargados.SuspendLayout();
			this.PNLCargarDatos.SuspendLayout();
			this.PNLToolBar.SuspendLayout();
			this.SuspendLayout();
			// 
			// GrpDatosCargados
			// 
			this.GrpDatosCargados.Size = new System.Drawing.Size(679, 225);
			// 
			// TViewCategorias
			// 
			this.TViewCategorias.LineColor = System.Drawing.Color.Black;
			// 
			// GrpCargaDatos
			// 
			this.GrpCargaDatos.Controls.Add(this.userDetails1);
			this.GrpCargaDatos.Controls.Add(this.itemDetails1);
			this.GrpCargaDatos.Size = new System.Drawing.Size(679, 232);
			// 
			// SPCDatosCargados
			// 
			// 
			// SPCDatosCargados.Panel2
			// 
			this.SPCDatosCargados.Panel2.Controls.Add(this.usersList1);
			this.SPCDatosCargados.Panel2.Controls.Add(this.itemsList1);
			this.SPCDatosCargados.Size = new System.Drawing.Size(669, 201);
			// 
			// PNLDatosCargados
			// 
			this.PNLDatosCargados.Size = new System.Drawing.Size(689, 235);
			// 
			// PNLCargarDatos
			// 
			this.PNLCargarDatos.Size = new System.Drawing.Size(689, 242);
			// 
			// VerticalSplitter
			// 
			this.VerticalSplitter.Location = new System.Drawing.Point(0, 235);
			// 
			// userDetails1
			// 
			this.userDetails1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.userDetails1.Location = new System.Drawing.Point(5, 19);
			this.userDetails1.Login = "";
			this.userDetails1.Name = "userDetails1";
			this.userDetails1.Password = "";
			this.userDetails1.Size = new System.Drawing.Size(669, 208);
			this.userDetails1.TabIndex = 1;
			user2.Login = "";
			user2.Password = "";
			this.userDetails1.User = user2;
			// 
			// itemDetails1
			// 
			this.itemDetails1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.itemDetails1.Item = "";
			this.itemDetails1.Location = new System.Drawing.Point(5, 19);
			this.itemDetails1.Name = "itemDetails1";
			this.itemDetails1.Size = new System.Drawing.Size(669, 208);
			this.itemDetails1.TabIndex = 0;
			// 
			// usersList1
			// 
			this.usersList1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.usersList1.Location = new System.Drawing.Point(5, 5);
			this.usersList1.Margin = new System.Windows.Forms.Padding(12, 3, 12, 3);
			this.usersList1.Name = "usersList1";
			this.usersList1.Size = new System.Drawing.Size(599, 191);
			this.usersList1.TabIndex = 1;
			// 
			// itemsList1
			// 
			this.itemsList1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.itemsList1.Location = new System.Drawing.Point(5, 5);
			this.itemsList1.Margin = new System.Windows.Forms.Padding(28, 3, 28, 3);
			this.itemsList1.Name = "itemsList1";
			this.itemsList1.SelectedItem = null;
			this.itemsList1.Size = new System.Drawing.Size(599, 191);
			this.itemsList1.TabIndex = 0;
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(876, 550);
			this.Name = "Form1";
			this.Text = "Form1";
			this.Load += new System.EventHandler(this.Form1_Load);
			this.GrpDatosCargados.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.PixBox)).EndInit();
			this.GrpCategorias.ResumeLayout(false);
			this.GrpCargaDatos.ResumeLayout(false);
			this.PNLCategorias.ResumeLayout(false);
			this.SPCDatosCargados.Panel1.ResumeLayout(false);
			this.SPCDatosCargados.Panel1.PerformLayout();
			this.SPCDatosCargados.Panel2.ResumeLayout(false);
			this.SPCDatosCargados.ResumeLayout(false);
			this.PNLDatosCargados.ResumeLayout(false);
			this.PNLCargarDatos.ResumeLayout(false);
			this.PNLToolBar.ResumeLayout(false);
			this.PNLToolBar.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private ItemDetails itemDetails1;
		private ItemsList itemsList1;
		private UserDetails userDetails1;
		private UsersList usersList1;
	}
}