using JP.Base.Common.Exceptions.Parsing;
using JP.Base.Errors.Logging.Support;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Text;





namespace JP.Base.Errors.Logging
{
    /// <summary>
    /// Administra el logueo de errores
    /// </summary>
    public class ErrorLogger : IDisposable
    {
        internal const string LOG_TO_DATABASE = "LogToDataBase";
        internal const string LOG_TO_EVENTLOG = "LotToEventLog";
        internal const string LOG_TO_FILE = "LogToFile";

        private ExceptionData currentError;
        private bool disposed;
        private string screenShotPath;

        //private ErrorParser errorParser;

        /// <summary>
        /// Inicializa la clase con Logueo en la base de datos
        /// </summary>
        /// <param name="exception">Excepcion capturada</param>
        public ErrorLogger(ExceptionData exception)
        : this(exception, false, true, true, string.Empty, null)
        {
        }

        /// <summary>
        /// Inicializa la clase con Logueo en archivo
        /// </summary>
        /// <param name="pLogFilePath">Ruta de archivo de logueo</param>
        /// <param name="exception">Excepcion capturada</param>
        public ErrorLogger(ExceptionData exception, string logFilePath = null)
        : this(exception, false, true, false, logFilePath, null)
        {
        }

        /// <summary>
        /// Inicializa la clase con Logueo en Eventlog
        /// </summary>
        /// <param name="EvntLog">Objeto Eventlog con los datos para loguear el error</param>
        /// <param name="exception">Excepcion capturada</param>
        public ErrorLogger(ExceptionData exception, EventLog eventLog)
            : this(exception, true, false, false, string.Empty, eventLog)
        {
        }

        /// <param name="LogToEvenLog">Loguear en Event Log</param>
        /// <param name="LogToLogFile">Loguear en archivo</param>
        /// <param name="LogToDatabase">Loguear en base de datos</param>
        /// <param name="pLogFilePath">Ruta del archivo de logueo</param>
        /// <param name="EvntLog">Objeto Eventlog con los datos para loguear el error</param>
        /// <param name="exception">Excepcion capturada</param>
        public ErrorLogger(ExceptionData exception, bool logToEvenLog, bool logToLogFile, bool logToDatabase, string logFilePath, EventLog eventLog)
        {
            LogToDatabase = logToDatabase;
            LogToEventLog = logToEvenLog;
            LogToLogFile = logToLogFile;
            LogFilePath = logFilePath;
            EventLog = eventLog;

            LogFileName = ConfigReader.LogFileName;
            currentError = exception;
            disposed = false;

            //errorParser = new ErrorParser(currentError.CurrentException);
        }

        ~ErrorLogger()
        {
            Dispose(false);
        }

        public delegate void DatabaseLogEventHandler(string logInformation);

        public static event DatabaseLogEventHandler OnWrittenToDatabase;

        public EventLog EventLog
        {
            get; private set;
        }

        public string LogFileName
        {
            get; set;
        }

        public string LogFilePath
        {
            get; set;
        }

        public LoggingErrors LoggingErrors
        {
            get; private set;
        }

        public bool LogToDatabase
        {
            get; private set;
        }

        public bool LogToEventLog
        {
            get; private set;
        }

        public bool LogToLogFile
        {
            get; private set;
        }

        public string MiniDumpPath
        {
            get; set;
        }

        public string SavedLogFileName
        {
            get; private set;
        }

        public bool SaveMiniDump
        {
            get; set;
        }

        public bool TakeScreenShot
        {
            get; set;
        }

        protected NameValueCollection LogResults
        {
            get; private set;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public NameValueCollection LogException()
        {
            NameValueCollection results = null;
            LogFilePath = GetFilePath();
            results = LogInformation();
            return results;
        }

        internal NameValueCollection LogInformation()
        {
            LogResults = new NameValueCollection();
            LoggingErrors = new LoggingErrors();

            if (SaveMiniDump)
            {
                MiniDumpPath = SaveMiniDumpFile();
            }
            if (TakeScreenShot)
            {
                screenShotPath = SaveScreenShotFile();
            }

            if (LogToDatabase)
            {
                LogResults.Add(LOG_TO_DATABASE, DoLogToDatabase().ToString());
            }
            if (LogToEventLog)
            {
                LogResults.Add(LOG_TO_EVENTLOG, DoLogToEventLog().ToString()); ;
            }
            if (LogToLogFile)
            {
                LogResults.Add(LOG_TO_FILE, DoLogToFile().ToString());
            }
            return LogResults;
        }

        protected void Dispose(bool Disposing)
        {
            if (!disposed && Disposing && EventLog != null)
                EventLog.Dispose();

            disposed = true;
        }

        /// <summary>
        ///DE ACA SE SACAN LOS DATOS DEFINIENDO UN EVENTO QUE MANDE POR PARAMS EL STRING A LOGUEAR, LUEGO AL VOLVER DE LA EJECUCION
        ///DEL EVENTO SE SIGUE CON ESTA CLASE, VER CODIGO ORIGINAL.
        ///EL PROBLEMA ES *EN QUE CLASE* COLOCAR LA SUBSCRIPCION AL EVENTO
        /// </summary>
        protected bool DoLogToDatabase()
        {
            bool logToDatabaseOK = false;

            if (OnWrittenToDatabase != null)
            {
                try
                {
                    OnWrittenToDatabase(CreateLogContent());
                    logToDatabaseOK = true;
                }
                catch (Exception ex)
                {
                    LoggingErrors.DataBaseLoggingErrors = ex.ToString();
                }
            }
            else
            {
                LoggingErrors.DataBaseLoggingErrors = "No subscriptions to OnWrittenToDatabase event";
            }

            return logToDatabaseOK;
        }

        protected bool DoLogToEventLog()
        {
            bool LogToEventLogOK = false;
            try
            {
                EventLog.WriteEntry(EventLog.Source, CreateLogContent(), EventLogEntryType.Error);

                LogToEventLogOK = true;
            }
            catch (Exception ex)
            {
                LoggingErrors.EventLogLoggingErrors = ex.Message;
            }
            return LogToEventLogOK;
        }

        /// <summary>
        /// Log current exception info to a text file;
        /// requires Log permissions for the target folder
        /// </summary>
        protected bool DoLogToFile()
        {
            bool LogToFileOK = false;
            StreamWriter sw = null;

            try
            {
                if (!Directory.Exists(LogFilePath))
                    CreateFilePath();

                string sFullPath = Path.Combine(LogFilePath, CreateFileName());

                using (sw = new StreamWriter(sFullPath, true))
                {
                    sw.Write(CreateLogContent());
                    sw.Close();
                }

                LogToFileOK = true;
            }
            catch (Exception ex)
            {
                LoggingErrors.FileLoggingErrors = ex.Message;
            }
            finally
            {
                if (sw != null)
                    sw.Close();
            }
            return LogToFileOK;
        }

        /// <summary>
        /// send current exception info via email
        /// </summary>
        protected bool LogToEmail(string subjectPrefix, string[] attachmentFiles, Dictionary<string, Stream> attachmentStreams)
        {
            //string to = Config.GetString("EmailTo", string.Empty);

            //if (string.IsNullOrEmpty(to))
            //{
            //    // don't bother mailing if we don't have anyone to mail to..
            //    //_blnLogToEmail = false;
            //    return true;
            //}

            //try
            //{
            //    string host = Config.GetString("SmtpServer", string.Empty);
            //    int port = Config.GetInteger("SmtpPort", 21);
            //    string userName = Config.GetString("SmtpUser", string.Empty);
            //    string password = Config.GetString("SmtpPwd", string.Empty);
            //    string domain = Config.GetString("SmtpDefaultDomain", string.Empty);
            //    bool enableSsl = Config.GetBoolean("SmtpUseSsl", true);

            //    string from = Config.GetString("SmtpFromAddress", string.Empty);

            //    using (MailMessage msg = new MailMessage())
            //    {
            //        msg.From = new MailAddress(from);
            //        msg.Subject = subjectPrefix + _GetExceptionType(); // "ERROR: " + _GetExceptionType();
            //        msg.Body = _exceptionText;
            //        msg.BodyEncoding = System.Text.Encoding.UTF8;
            //        msg.IsBodyHtml = false;

            //        string[] recipients = to.Split('|', ';', ',');
            //        foreach (string recip in recipients)
            //        {
            //            if (!string.IsNullOrEmpty(recip.Trim()))
            //            {
            //                msg.To.Add(recip.Trim());
            //            }
            //        }

            //        NetworkCredential auth = CredentialCache.DefaultNetworkCredentials;

            //        if (!string.IsNullOrEmpty(userName))
            //        {
            //            if (string.IsNullOrEmpty(domain))
            //                auth = new NetworkCredential(userName, password);
            //            else
            //                auth = new NetworkCredential(userName, password, domain);
            //        }

            //        SmtpClient smtpClient = new SmtpClient(host, port);

            //        smtpClient.EnableSsl = enableSsl;
            //        smtpClient.Credentials = auth;
            //        smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;

            //        foreach (string file in attachmentFiles)
            //        {
            //            msg.Attachments.Add(new Attachment(file));
            //        }

            //        foreach (string name in attachmentStreams.Keys)
            //        {
            //            msg.Attachments.Add(new Attachment(attachmentStreams[name], name));
            //        }

            //        smtpClient.Send(msg);

            //        return true;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    _results.Add("LogToEmail", ex.Message);
            //    // we're in an unhandled exception handler
            //}

            return false;
        }

        private string CreateFileName()
        {
            string result;
            string ticks = DateTime.Now.Ticks.ToString();

            if (LogFileName.Contains("."))
            {
                string extension = LogFileName.Substring(LogFileName.LastIndexOf("."));
                string fileName = LogFileName.Substring(0, LogFileName.LastIndexOf("."));
                fileName = string.Format("{0}[{1}]{2}", fileName, ticks, extension);
                result = fileName;
            }
            else
            {
                string fileName = string.Format("{0}[{1}]", LogFileName, ticks);
                result = fileName;
            }

            SavedLogFileName = result;
            return result;
        }

        private void CreateFilePath()
        {
            Directory.CreateDirectory(LogFilePath);
        }

        private string CreateLogContent()
        {
            StringBuilder sb = new StringBuilder();
            List<ExceptionLocationData> _GetErrorLocations = null;

            sb.AppendLine(">>>> INICIO DE LOG >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>");
            sb.AppendLine(string.Empty);
            sb.AppendLine(DateTime.Now.ToString(CultureInfo.CurrentCulture.DateTimeFormat.FullDateTimePattern));
            sb.Append("Se ha capturado una excepcion ");
            sb.AppendLine((currentError.IsUnhandledException ? "NO MANEJADA" : "MANEJADA"));
            sb.AppendLine(string.Empty);
            sb.AppendLine("[Mini Dump File]");

            if (SaveMiniDump)
            {
                sb.Append("Se ha intentado crear un archivo mini dump en la ubicacion:");
                sb.AppendLine(string.Empty);

                sb.Append(MiniDumpPath);
                sb.AppendLine(string.Empty);
                sb.AppendLine(string.Empty);
            }
            else
            {
                sb.AppendLine("No se ha guardado un archivo mini dump");
                sb.AppendLine(string.Empty);
            }

            sb.AppendLine("[Screenshot File]");

            if (TakeScreenShot)
            {
                sb.Append("Se ha intentado crear una impresion de pantalla del error en:");
                sb.AppendLine(string.Empty);
                sb.Append(screenShotPath);
                sb.AppendLine(string.Empty);
                sb.AppendLine(string.Empty);
            }
            else
            {
                sb.AppendLine("No se ha tomado una impresion de pantalla");
                sb.AppendLine(string.Empty);
            }

            sb.AppendLine(new String('_', 50));
            sb.AppendLine("A continuacion se muestra la informacion recopilada por el sistema de errores:");
            sb.AppendLine(new String('_', 50));
            sb.AppendLine(string.Empty);
            sb.Append(currentError.ExceptionToString());

            sb.AppendLine(currentError.SysInfoToString());
            sb.AppendLine(currentError.GetAssemblyInfo());

            _GetErrorLocations = currentError.GetErrorLocations();

            if (_GetErrorLocations != null)
            {
                if (_GetErrorLocations.Count > 0)
                {
                    sb.AppendLine("[Ubicaciones de los errores]");

                    foreach (ExceptionLocationData ErrLocation in _GetErrorLocations)
                    {
                        sb.Append("Archivo: ");
                        sb.Append(ErrLocation.FileName);
                        sb.Append(Environment.NewLine);
                        sb.Append("Metodo: ");
                        sb.Append(ErrLocation.Method);
                        sb.Append(Environment.NewLine);
                        sb.Append("Linea: ");
                        sb.Append(ErrLocation.Line);
                        sb.AppendLine(string.Empty);
                        sb.AppendLine(new String('_', 30));
                    }
                }
            }

            sb.AppendLine(string.Empty);
            sb.AppendLine("[Stack Trace Information]");
            sb.AppendLine(currentError.GetEnhancedStackTrace());
            sb.AppendLine(string.Empty);

            sb.AppendLine("<<<< FIN DE LOG <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<");
            return sb.ToString(); ;
        }

        private string GetFilePath()
        {
            string result = (string.IsNullOrEmpty(LogFilePath) ? ConfigReader.LogFilePath : LogFilePath);

            result = string.IsNullOrEmpty(result) ? MyAppContext.ApplicationStartupPath : result;

            return result;
        }

        private void GetLoggingErrors(ref StringBuilder sb, string LogType)
        {
            sb.Append(Environment.NewLine);
            sb.Append("La causa del error fue:");
            sb.Append(Environment.NewLine);
            switch (LogType)
            {
                case LOG_TO_DATABASE:
                    sb.Append(LoggingErrors.DataBaseLoggingErrors);
                    break;

                case LOG_TO_FILE:
                    sb.Append(LoggingErrors.FileLoggingErrors);
                    break;

                case LOG_TO_EVENTLOG:
                    sb.Append(LoggingErrors.EventLogLoggingErrors);
                    break;
            }
        }

        private string SaveMiniDumpFile()
        {
            string sReturn = string.Empty;

            try
            {
                sReturn = new MiniDumpManager().SaveMiniDump();
            }
            catch (Exception ex)
            {

                sReturn = "No se pudo guardar el Mini Dump: " + Environment.NewLine + Common.Exceptions.Parsing.ExceptionExtensions.ExceptionToString(ex);

                MiniDumpPath = "Dump File not created";
            }

            return sReturn;
        }

        private string SaveScreenShotFile()
        {
            string result = string.Empty;

            try
            {
                var screenShotManager = new ScreenShotManager();
                screenShotManager.TakeScreenshot();
                result = screenShotManager.ScreenshotFullName;
            }
            catch (Exception ex)
            {

                result = "Could not take screen shot, the following error occurred: " + Environment.NewLine + Common.Exceptions.Parsing.ExceptionExtensions.ExceptionToString(ex);

                screenShotPath = "Screen shot not taken";
            }
            return result;
        }
    }
}