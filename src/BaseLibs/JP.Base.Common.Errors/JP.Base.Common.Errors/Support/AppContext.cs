using System;

namespace JP.Base.Common.Errors.Support
{
    internal static class AppContext
    {
        public static string ApplicationPath
        {
            get { return AppDomain.CurrentDomain.BaseDirectory; }
        }
    }
}