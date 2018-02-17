namespace JP.Base.Errors.Managing.Support
{
    using System.Configuration;

    internal static class ConfigReader
    {
        public static string BodyPreface
        {
            get
            {
                return ConfigurationManager.AppSettings["BodyPreface"];
            }
        }

        public static string FromAddress
        {
            get
            {
                return ConfigurationManager.AppSettings["FromAddress"];
            }
        }

        public static string SubjectPreface
        {
            get
            {
                return ConfigurationManager.AppSettings["SubjectPreface"];
            }
        }

        public static string ToAddress
        {
            get
            {
                return ConfigurationManager.AppSettings["ToAddress"];
            }
        }
    }
}