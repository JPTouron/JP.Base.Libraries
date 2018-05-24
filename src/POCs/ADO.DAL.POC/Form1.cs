using System;
using System.Data;
using System.Windows.Forms;

namespace ADO.DAL.POC
{
    public partial class Form1 : Form
    {
        private IDbConnection conn;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            var s = new SqlInterface();

            s.GetData();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            var x = new XlInterface();

            x.DeleteDataWithOleDb();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            var x = new XlInterface();

            x.SetData();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            var x = new XlInterface();

            x.GetData();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var s = new SqlInterface();

            s.SetData();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            var a = new AccessInterface();

            a.GetData();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            var a = new AccessInterface();

            a.SetData();
        }
    }
}