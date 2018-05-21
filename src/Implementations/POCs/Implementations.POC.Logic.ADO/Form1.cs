using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace Implementations.POC.Logic.ADO
{
    public partial class Form1 : Form
    {
        private ClientLogic l;
        private UoWFac uf;

        public Form1()
        {
            InitializeComponent();

            uf = new UoWFac();
            l = new ClientLogic(uf, null);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var data = l.GetList();

            this.dataGridView1.DataSource = data;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var rnd = new Random();

            l.Create(new ClientVM
            {
                Code = $"code: {rnd.Next()}",
                Name = $"Name: {rnd.Next()}",
            });

            var list = l.GetList().OrderByDescending(x => x.Id).ToList();// in-memory sorting...
            this.dataGridView1.DataSource = list;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var rnd = new Random();

            var id = rnd.Next(1, 100);
            var item = new CustomLogic(uf, null).GetJoinedData(id);

            dataGridView2.DataSource = new List<OperatorWithClientVM> { item };


        }

        private void button4_Click(object sender, EventArgs e)
        {
            var rnd = new Random();

            var id = rnd.Next(1, 100);
            var item = l.GetById(id);

            dataGridView1.DataSource = new List<ClientVM> { item };
        }

        private void button5_Click(object sender, EventArgs e)
        {
            var rnd = new Random();

            var id = rnd.Next(1, 100);
            var item = l.GetById(id);

            item.Name = $"the new name {System.DateTime.Now.ToLongTimeString()}";

            var itemNew = l.Update(item);

            dataGridView1.DataSource = new List<ClientVM> { itemNew };
        }
    }
}