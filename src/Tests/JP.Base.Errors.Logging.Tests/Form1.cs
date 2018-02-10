using JP.Base.Common.Errors;
using System;
using System.Windows.Forms;

namespace TD.Base.Logging.ErrorLogging.TestDrive
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var r = new ExceptionData(new Exception("test"));
        }
    }
}