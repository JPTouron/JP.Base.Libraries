namespace ImplementingUserControls
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
            this.listControl1 = new ImplementingUserControls.ListControl();
            this.SuspendLayout();
            // 
            // listControl1
            // 
            this.listControl1.CurrentPage = 1;
            this.listControl1.CurrentPageSize = 10;
            this.listControl1.ListControl = null;
            this.listControl1.Location = new System.Drawing.Point(12, 19);
            this.listControl1.Name = "listControl1";
            this.listControl1.Size = new System.Drawing.Size(269, 212);
            this.listControl1.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(408, 304);
            this.Controls.Add(this.listControl1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private ListControl listControl1;
    }
}

