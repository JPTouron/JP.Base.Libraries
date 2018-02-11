using System;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class ListControl : SliceControl
    {
        public ListControl()
        {
            InitializeComponent();
            ListControl = listBox1;

            this.FinishedLoadingItems += ListControl_FinishedLoadingItems;
            this.ItemHasBeenSelected += ListControl_ItemHasBeenSelected;

            bindingSource1.DataSource = typeof(Model);

            listBox1.DataSource = bindingSource1;
            listBox1.DisplayMember = "Display";
            listBox1.SelectedIndexChanged += ListBox1_SelectedIndexChanged;

            BindingSource = bindingSource1;
            View = new ListView();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.LoadAllItems(new JP.Base.Logic.Search.SortAndFilterData
            {
                SearchString = textBox1.Text
            });
        }

        private void ListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetSelectedItem(listBox1.SelectedIndex);
        }

        private void ListControl_FinishedLoadingItems(object sender, EventArgs e)
        {
            MessageBox.Show("all loaded");
        }

        private void ListControl_ItemHasBeenSelected(object sender, EventArgs e)
        {
            MessageBox.Show($"item selected {View.Model.name}");
        }
    }
}