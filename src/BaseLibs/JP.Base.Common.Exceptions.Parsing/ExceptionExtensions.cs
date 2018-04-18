using System;

namespace JP.Base.Common.Exceptions.Parsing
{
    public static class ExceptionExtensions
    {
        /// <summary>
        /// extracts all the data from an extension ni a formatted string
        /// </summary>
        public static string ExceptionToString(this Exception ex)
        {
            return new ExceptionParser(ex).ExceptionToString(ex);
        }
    }
}