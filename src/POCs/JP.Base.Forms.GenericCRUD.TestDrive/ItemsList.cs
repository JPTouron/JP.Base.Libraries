using System;
using System.Windows.Forms;

namespace TD.Forms.GenericABM.FormTestDrive
{
    public partial class ItemsList : UserControl
    {
        private string selectedItem;

        public ItemsList()
        {
            InitializeComponent();
        }

        public event EventHandler ItemSelected;

        public string SelectedItem
        {
            get { return selectedItem; }
            set
            {
                if (value != null)
                {
                    selectedItem = value;
                    if (listBox1.SelectedIndex >= 0)
                        listBox1.Items[listBox1.SelectedIndex] = selectedItem;
                }
            }
        }

        public void AddItem(string item)
        {
            listBox1.Items.Add(item);
        }

        public void DeleteSelectedItem()
        {
            if (listBox1.SelectedIndex >= 0)
            {
                listBox1.Items.RemoveAt(listBox1.SelectedIndex);
            }
        }

        public void LoadAllItems()
        {
            listBox1.Items.Clear();
            for (int i = 0; i < 5; i++)
            {
                listBox1.Items.Add("Item" + i.ToString());
            }
        }

        private void ItemsList_Load(object sender, EventArgs e)
        {
            LoadAllItems();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                selectedItem = listBox1.SelectedItem.ToString();
                if (ItemSelected != null)
                {
                    ItemSelected(sender, e);
                }
            }
        }
    }
}