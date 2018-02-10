using System.Globalization;

namespace JP.Base.Common.Extensions.Strings
{
    public static class StringExtensions
    {
        /// <summary>
        /// Formats the string using the CultureInfo.CurrentCulture format provider
        /// </summary>
        public static string ToCurrentCulture(this string value)
        {
            return string.Format(CultureInfo.CurrentCulture, value);
        }

        /// <summary>
        /// Returns the passed int parameter with its first letter capitalized
        /// </summary>
        public static string ToProperCase(this string value)
        {
            string buffer = string.Concat(value.Substring(0, 1).ToUpper(), value.Substring(1));
            return buffer;
        }
    }
}