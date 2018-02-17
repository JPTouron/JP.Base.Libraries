using System.Configuration;

namespace JP.Base.Errors.Logging.Support
{
    internal static class ConfigReader
    {
        public static string LogFileName
        {
            get { return ConfigurationManager.AppSettings["LogFileName"]; }
        }

        public static string LogFilePath
        {
            get { return ConfigurationManager.AppSettings["LogFilePath"]; }
        }

        public static string MiniDumpPath
        {
            get { return ConfigurationManager.AppSettings["MiniDumpPath"]; }
        }

        public static string ScreenshotFileName
        {
            get { return ConfigurationManager.AppSettings["ScreenShotFileName"]; }
        }
    }
}