using System;
using System.Windows.Forms;

namespace JP.Base.Errors.Logging.Support
{
    internal static class MyAppContext
    {
        public static string ApplicationPath
        {
            get { return AppDomain.CurrentDomain.BaseDirectory; }
        }

        public static string ApplicationStartupPath
        {
            get { return Application.StartupPath; }
        }
    }
}