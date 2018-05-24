using System;

namespace JP.Base.Common.Exceptions.Parsing.Support
{
    internal static class MyAppContext
    {
        public static string ApplicationPath
        {
            get { return AppDomain.CurrentDomain.BaseDirectory; }
        }
    }
}