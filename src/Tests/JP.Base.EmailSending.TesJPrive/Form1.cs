using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TD.Base.EmailSending.TestDrive
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs e)
		{

			var mailsender = new EMailSender(txtfrom.Text, txtto.Text);

			mailsender.Subject = txtsubject.Text;
			mailsender.BCCAddresses = new List<string>() { txtbcc.Text };
			mailsender.CCAddresses = new List<string>() { "sarlanga@ssassy.com"};
			mailsender.BodyContent = txtmsg.Text;
			if (mailsender.Send())
			{
				MessageBox.Show("Mesage sent successfully! :)");
			}

		}
	}
}
