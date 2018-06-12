using System.Diagnostics;
namespace JP.Base.Implementations.MVP.Winforms.Implementation
{
    partial class BaseListControl<TModel, TIView>
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
            //Debug.WriteLine("<< DESTROYED BaseListControl<" + typeof(TModel).ToString() + "> instance: " + j.ToString());
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.worker = new System.ComponentModel.BackgroundWorker();
            this.SuspendLayout();
            // 
            // worker
            // 
            this.worker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.OnWorkerDoWork);
            this.worker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.OnWorkerProgressChanged);
            this.worker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.OnWorkerRunWorkerCompleted);
            // 
            // BaseListControl
            // 
            this.Name = "BaseListControl";
            this.ResumeLayout(false);

        }

        #endregion

        protected System.ComponentModel.BackgroundWorker worker;
    }
}
