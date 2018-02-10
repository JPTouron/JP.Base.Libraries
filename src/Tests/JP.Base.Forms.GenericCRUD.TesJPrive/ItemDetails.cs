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
	public partial class ItemDetails : UserControl
	{

		
		public string Item
		{
			get
			{
				return textBox1.Text;
			}
			set
			{
				textBox1.Text = value;
			}
		}

		public ItemDetails()
		{
			InitializeComponent();
		}

	}
}
