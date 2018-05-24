using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TD.Base.Logging.ErrorLogging.Tests.Support
{
    public static class FileHelper
    {
        public static List<string> FindFiles(string pattern, string findInPath)
        {
            return Directory.GetFiles(findInPath, pattern).ToList<string>();
        }

        public static string GetPathToFile(this string fullPath)
        {
            string v;

            var filename = Path.GetFileName(fullPath);
            v = fullPath.Substring(0, fullPath.IndexOf(filename) - 1);

            return v;
        }
    }
}