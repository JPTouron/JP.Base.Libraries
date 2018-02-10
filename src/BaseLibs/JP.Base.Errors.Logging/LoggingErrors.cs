namespace JP.Base.Errors.Logging
{
    /// <summary>
    /// This class contains the errors occurred while logging errors normally
    /// </summary>
    public class LoggingErrors
    {
        private string _DataBaseLoggingErrors = "";
        private string _EventLogLoggingErrors = "";
        private string _FileLoggingErrors = "";

        public LoggingErrors()
        {
        }

        public string DataBaseLoggingErrors
        {
            get { return _DataBaseLoggingErrors; }
            set { _DataBaseLoggingErrors = value; }
        }

        public string EventLogLoggingErrors
        {
            get { return _EventLogLoggingErrors; }
            set { _EventLogLoggingErrors = value; }
        }

        public string FileLoggingErrors
        {
            get { return _FileLoggingErrors; }
            set { _FileLoggingErrors = value; }
        }
    }
}