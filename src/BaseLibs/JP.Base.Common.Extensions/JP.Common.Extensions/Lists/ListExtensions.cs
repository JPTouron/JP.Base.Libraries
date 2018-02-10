using System.Collections.Generic;
using System.Text;

namespace JP.Base.Common.Extensions.Lists
{
    public static class ListExtensions
    {
        public static string ToSeparatedValues<T>(this IList<T> list, string separator)
        {
            StringBuilder buffer = new StringBuilder();

            foreach (T item in list)
            {
                buffer.Append(string.Format("{0}{1}", item.ToString(), separator));
            }
            string result = buffer.ToString();

            if (result.Length > 0)
                result = result.Substring(0, result.Length - 1);

            return result;
        }
    }
}