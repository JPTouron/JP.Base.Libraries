using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Implementations.POC.Logic.EF6
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var logic = new EmployerLogic(new UoWFac(), null);

            var list = logic.GetList();

            dataGridView1.DataSource = list;


        }

        private void button2_Click(object sender, EventArgs e)
        {
            var logic = new EmployerLogic(new UoWFac(), null);

            var rnd = new Random();


            logic.Create(new EmployerVM
            {
                Area = $"area {rnd.Next()}",
                Name = $"Name {rnd.Next()}",
            });

            var list = logic.GetList();

            dataGridView1.DataSource = list;

        }
    }
}
