namespace JP.Base.Exceptions.Winforms.Support
{
    using System.Configuration;

    internal static class ConfigReader
    {
        public static string BodyPreface => ConfigurationManager.AppSettings["BodyPreface"];

        public static string FromAddress => ConfigurationManager.AppSettings["FromAddress"];

        public static string SubjectPreface => ConfigurationManager.AppSettings["SubjectPreface"];

        public static string ToAddress => ConfigurationManager.AppSettings["ToAddress"];
    }
}