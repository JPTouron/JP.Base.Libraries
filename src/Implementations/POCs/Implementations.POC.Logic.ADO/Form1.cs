using JP.Base.DAL.ADO.Implementations.Factories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Implementations.POC.Logic.ADO
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var uf = new UoWFac();

            var l = new ClientLogic(uf, null);
            var data = l.GetList();

            this.dataGridView1.DataSource = data;
            
        }
    }
}
