using System;
using System.Linq;
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

        private void button3_Click(object sender, EventArgs e)
        {
            var l = new EmployerLogic(new UoWFac(), null);
            var list = l.GetList();

            var logic = new EmployeeLogic(new UoWFac(), null);

            var rnd = new Random();

            logic.CreateEmployee(new EmployeeVM
            {
                Age = rnd.Next(),
                EmployerArea = $"Area{rnd.Next()}",
                EmployerId = list.Count() == 0 ? 0 : list.First().Id, //rnd.Next(list.Min(x => x.Id), list.Max(x => x.Id)),
                Function = $"Function{rnd.Next()}",
                Name = $"Name{rnd.Next()}",
                EmployerName = $"Employer_Name{rnd.Next()}",
            });

            var employeeList = logic.GetList();

            dataGridView2.DataSource = employeeList;
        }
    }
}