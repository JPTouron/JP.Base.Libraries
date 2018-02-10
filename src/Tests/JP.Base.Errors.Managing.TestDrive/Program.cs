using JP.Base.Errors.Managing;
using System;
using System.Windows.Forms;

namespace TD.Base.Errors.Managing.TestDrive
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            UnHandledExcListener.StartListening(true, true, true);
            UnHandledExcListener.UnhandledExceptionOcurred += new EventHandler(UnHandledExcListener_UnhandledExceptionOcurred);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }

        private static void UnHandledExcListener_UnhandledExceptionOcurred(object sender, EventArgs e)
        {
        }
    }
}