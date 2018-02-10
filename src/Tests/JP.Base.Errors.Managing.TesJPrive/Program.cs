using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace TD.Base.Errors.Managing.TestDrive
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			UnHandledExcListener.StartListening(true, true,true);
			UnHandledExcListener.UnhandledExceptionOcurred += new EventHandler(UnHandledExcListener_UnhandledExceptionOcurred);
			

			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new Form1());
		}

		static void UnHandledExcListener_UnhandledExceptionOcurred(object sender, EventArgs e)
		{
			
		}
	}
}
