using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TD.Forms.GenericABM.FormTestDrive
{
	public partial class ItemsList : UserControl
	{

		public void AddItem(string item)
		{
			listBox1.Items.Add(item);
		}

		public ItemsList()
		{
			InitializeComponent();
		}
		public void LoadAllItems()
		{

			listBox1.Items.Clear();
			for (int i = 0; i < 5; i++)
			{

				listBox1.Items.Add("Item" + i.ToString());

			}

		}
		string selectedItem;

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
		public event EventHandler ItemSelected;

		private void ItemsList_Load(object sender, EventArgs e)
		{
			LoadAllItems();
		}

		public void DeleteSelectedItem()
		{
			if (listBox1.SelectedIndex >= 0)
			{
				listBox1.Items.RemoveAt(listBox1.SelectedIndex);
			}
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
