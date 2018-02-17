namespace JP.Base.Exceptions.Winforms
{
	internal partial class ErrorReporter : System.Windows.Forms.Form
	{

		//Form overrides dispose to clean up the component list.

		protected override void Dispose(bool disposing)
		{
			try
			{
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		//NOTE: The following procedure is required by the Windows Form Designer
		//It can be modified using the Windows Form Designer.  
		//Do not modify it using the code editor.
		[System.Diagnostics.DebuggerStepThrough()]
		private void InitializeComponent()
		{
			this.cmdSendEmail = new System.Windows.Forms.Button();
			this.cmdClose = new System.Windows.Forms.Button();
			this.cmdCopyClipboard = new System.Windows.Forms.Button();
			this.statusBar = new System.Windows.Forms.StatusStrip();
			this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
			this.lblInformation = new System.Windows.Forms.ToolStripStatusLabel();
			this.richtextMessage = new System.Windows.Forms.RichTextBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
			this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
			this.statusBar.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.tableLayoutPanel3.SuspendLayout();
			this.tableLayoutPanel4.SuspendLayout();
			this.SuspendLayout();
			// 
			// cmdSendEmail
			// 
			this.cmdSendEmail.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.cmdSendEmail.AutoSize = true;
			this.cmdSendEmail.Location = new System.Drawing.Point(188, 3);
			this.cmdSendEmail.Name = "cmdSendEmail";
			this.cmdSendEmail.Size = new System.Drawing.Size(82, 23);
			this.cmdSendEmail.TabIndex = 7;
			this.cmdSendEmail.Text = "&Reportar error";
			this.cmdSendEmail.UseVisualStyleBackColor = true;
			this.cmdSendEmail.Click += new System.EventHandler(this.btnBug_Click);
			// 
			// cmdClose
			// 
			this.cmdClose.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.cmdClose.AutoSize = true;
			this.cmdClose.Location = new System.Drawing.Point(276, 3);
			this.cmdClose.Name = "cmdClose";
			this.cmdClose.Size = new System.Drawing.Size(110, 23);
			this.cmdClose.TabIndex = 9;
			this.cmdClose.Text = "&Cerrar esta ventana";
			this.cmdClose.UseVisualStyleBackColor = true;
			this.cmdClose.Click += new System.EventHandler(this.btnClose_Click);
			// 
			// cmdCopyClipboard
			// 
			this.cmdCopyClipboard.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.cmdCopyClipboard.AutoSize = true;
			this.cmdCopyClipboard.Location = new System.Drawing.Point(3, 3);
			this.cmdCopyClipboard.Name = "cmdCopyClipboard";
			this.cmdCopyClipboard.Size = new System.Drawing.Size(179, 23);
			this.cmdCopyClipboard.TabIndex = 10;
			this.cmdCopyClipboard.Text = "Co&piar informacion al portapapeles";
			this.cmdCopyClipboard.UseVisualStyleBackColor = true;
			this.cmdCopyClipboard.Click += new System.EventHandler(this.btnAdditionalInfo_Click);
			// 
			// statusBar
			// 
			this.statusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatus,
            this.lblInformation});
			this.statusBar.Location = new System.Drawing.Point(0, 372);
			this.statusBar.Name = "statusBar";
			this.statusBar.Size = new System.Drawing.Size(739, 22);
			this.statusBar.TabIndex = 13;
			// 
			// lblStatus
			// 
			this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
			this.lblStatus.Name = "lblStatus";
			this.lblStatus.Size = new System.Drawing.Size(55, 17);
			this.lblStatus.Text = "lblStatus";
			// 
			// lblInformation
			// 
			this.lblInformation.Name = "lblInformation";
			this.lblInformation.Size = new System.Drawing.Size(669, 17);
			this.lblInformation.Spring = true;
			this.lblInformation.Text = "lblInformation";
			this.lblInformation.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.lblInformation.Click += new System.EventHandler(this.lblInformation_Click);
			// 
			// richtextMessage
			// 
			this.richtextMessage.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.richtextMessage.Dock = System.Windows.Forms.DockStyle.Fill;
			this.richtextMessage.Location = new System.Drawing.Point(3, 16);
			this.richtextMessage.Name = "richtextMessage";
			this.richtextMessage.ReadOnly = true;
			this.richtextMessage.Size = new System.Drawing.Size(727, 312);
			this.richtextMessage.TabIndex = 4;
			this.richtextMessage.Text = "";
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.richtextMessage);
			this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupBox1.Location = new System.Drawing.Point(3, 3);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(733, 331);
			this.groupBox1.TabIndex = 14;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Detalles del Error";
			// 
			// tableLayoutPanel3
			// 
			this.tableLayoutPanel3.ColumnCount = 1;
			this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel4, 0, 1);
			this.tableLayoutPanel3.Controls.Add(this.groupBox1, 0, 0);
			this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel3.Name = "tableLayoutPanel3";
			this.tableLayoutPanel3.RowCount = 2;
			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel3.Size = new System.Drawing.Size(739, 372);
			this.tableLayoutPanel3.TabIndex = 15;
			// 
			// tableLayoutPanel4
			// 
			this.tableLayoutPanel4.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.tableLayoutPanel4.AutoSize = true;
			this.tableLayoutPanel4.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.tableLayoutPanel4.ColumnCount = 3;
			this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel4.Controls.Add(this.cmdClose, 2, 0);
			this.tableLayoutPanel4.Controls.Add(this.cmdSendEmail, 1, 0);
			this.tableLayoutPanel4.Controls.Add(this.cmdCopyClipboard, 0, 0);
			this.tableLayoutPanel4.Location = new System.Drawing.Point(347, 340);
			this.tableLayoutPanel4.Name = "tableLayoutPanel4";
			this.tableLayoutPanel4.RowCount = 1;
			this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel4.Size = new System.Drawing.Size(389, 29);
			this.tableLayoutPanel4.TabIndex = 16;
			// 
			// ErrorReporter
			// 
			this.ClientSize = new System.Drawing.Size(739, 394);
			this.Controls.Add(this.tableLayoutPanel3);
			this.Controls.Add(this.statusBar);
			this.Name = "ErrorReporter";
			this.ShowIcon = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Se ha encontrado un error";
			this.TopMost = true;
			this.Load += new System.EventHandler(this.ErrorReporter_Load);
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ErrorReporter_FormClosed);
			this.statusBar.ResumeLayout(false);
			this.statusBar.PerformLayout();
			this.groupBox1.ResumeLayout(false);
			this.tableLayoutPanel3.ResumeLayout(false);
			this.tableLayoutPanel3.PerformLayout();
			this.tableLayoutPanel4.ResumeLayout(false);
			this.tableLayoutPanel4.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		internal System.Windows.Forms.RichTextBox richtextMessage;
		//internal System.Windows.Forms.Button btnBug;
		//internal System.Windows.Forms.Button btnClose;
		//internal System.Windows.Forms.TableLayoutPanel TableLayoutPanel1;
		//internal System.Windows.Forms.TableLayoutPanel TableLayoutPanel2;
		//internal System.Windows.Forms.Label lblErrorMessage;
		internal System.Windows.Forms.StatusStrip statusBar;
		//internal System.Windows.Forms.ToolStripStatusLabel statuslblStatus;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
	}
}
