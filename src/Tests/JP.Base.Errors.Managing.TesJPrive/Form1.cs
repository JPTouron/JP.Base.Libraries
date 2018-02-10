using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TD.Base.Errors.Managing.TestDrive
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		private void cmdunhandled_Click(object sender, EventArgs e)
		{
			NewMethod();
		}

		private static void NewMethod()
		{
			int a = 10;
			int b = 0;
			int c = a / b;
		}

		private void cmdhandled_Click(object sender, EventArgs e)
		{
			try
			{
				NewMethod();
			}
			catch (Exception ex)
			{

				MessageBox.Show("handled error: " + ex.Message);
			}
		}

		private void cmdhibrid_Click(object sender, EventArgs e)
		{
			try
			{
				NewMethod();
			}
			catch
			{

				throw;
			}
		}
	}
}
