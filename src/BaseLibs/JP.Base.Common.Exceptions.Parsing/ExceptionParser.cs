using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Globalization;
using System.Net;
using System.Reflection;
using System.Text;
using JP.Base.Common.Exceptions.Parsing.Support;

namespace JP.Base.Common.Exceptions.Parsing
{
    internal class ExceptionParser
    {
        private const string ROOT_HTTP_EXCEPTION = "System.Web.HttpUnhandledException";

        private const string ROOT_WS_EXCEPTION = "System.Web.Services.Protocols.SoapException";

        /// <summary>
        /// create a new instance of this class
        /// </summary>
        /// <param name="ex">the exception you want to parse, you should set it, unless you plan to set it on every call to the public methods of this class</param>
        /// <param name="assembly">the assembly in which the exception happened, this is not required, it is the executing assembly by default</param>
        public ExceptionParser(Exception ex, Assembly assembly = null)
        {
            CurrentException = ex;
            CurrentAssembly = assembly ?? Assembly.GetExecutingAssembly();
        }

        public Assembly CurrentAssembly
        {
            get; private set;
        }

        public Exception CurrentException
        {
            get; private set;
        }

        /// <summary>
        /// translate exception object to string, with additional system info
        /// </summary>
        /// <param name="ex">Exception to be converted</param>
        public string ExceptionToString(Exception ex = null)
        {
            ex = ex ?? CurrentException;

            StringBuilder builder = new StringBuilder();

            // Inner exceptions are handled recursively
            if (!(ex.InnerException == null))
            {
                // sometimes the original exception is wrapped in a more relevant outer exception
                // the detail exception is the "inner" exception
                // see http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dnbda/html/exceptdotnet.asp

                // don't return the outer root ASP exception; it is redundant.
                if (ex.GetType().ToString() == ROOT_HTTP_EXCEPTION || ex.GetType().ToString() == ROOT_WS_EXCEPTION)
                    return ExceptionToString(ex.InnerException);
                else
                {
                    builder.Append("(Inner Exception)");
                    builder.Append(Environment.NewLine);
                    builder.Append(ExceptionToString(ex.InnerException));
                    builder.Append(Environment.NewLine);
                    builder.Append("(Outer Exception)");
                    builder.Append(Environment.NewLine);
                }
            }

            builder.Append("[Exception Information]");
            builder.Append(Environment.NewLine);

            // get exception-specific information
            builder.Append("Exception Source:        ");
            builder.Append(ex.Source);

            builder.Append(Environment.NewLine);

            //get exception type
            builder.Append(GetExceptionType(ex));

            builder.Append(Environment.NewLine);
            builder.Append("Exception Message:      ");

            builder.Append(ex.Message);

            builder.Append(Environment.NewLine);
            builder.Append("Exception Target Site:   ");

            builder.Append(ex.TargetSite.Name);

            // Check for specific exception types (and output specific details if known)
            if (ex is SqlException)
            {
                builder.Append(Environment.NewLine);
                SqlExceptionToString(builder, ex);
            }

            builder.Append(Environment.NewLine);
            builder.Append(Environment.NewLine);

            return builder.ToString();
        }

        public string GetAssemblyInfo()
        {
            StringBuilder builder = new StringBuilder();

            builder.Append("[Assembly information]");
            builder.Append(Environment.NewLine);

            builder.Append("Application path:      ");
            builder.Append(MyAppContext.ApplicationPath);

            builder.Append(Environment.NewLine);
            builder.Append("Application Domain:    ");

            builder.Append(System.AppDomain.CurrentDomain.FriendlyName);

            builder.Append(Environment.NewLine);
            builder.Append("Assembly Codebase:     ");
            builder.Append(CurrentAssembly.CodeBase);
            builder.Append(Environment.NewLine);

            builder.Append("Assembly Full Name:    ");

            builder.Append(CurrentAssembly.FullName);
            builder.Append(Environment.NewLine);

            builder.Append("Assembly Version:      ");
            builder.Append(CurrentAssembly.GetName().Version.ToString());
            builder.Append(Environment.NewLine);

            builder.Append(Environment.NewLine);
            builder.Append(Environment.NewLine);

            return builder.ToString();
        }

        public string GetEnhancedStackTrace(Exception ex = null, bool SkipErrorHandlingClasses = true)
        {
            ex = ex ?? CurrentException;

            var objStackTrace = new StackTrace(ex);
            var strSkipClassName = SkipErrorHandlingClasses ? "ASPUnhandledException" : string.Empty;

            var sb = new StringBuilder();

            sb.Append(Environment.NewLine);
            sb.Append("[Stack Trace Information]");
            sb.Append(Environment.NewLine);

            for (int intFrame = objStackTrace.FrameCount - 1; intFrame >= 0; intFrame--)
            {
                StackFrame sf = objStackTrace.GetFrame(intFrame);
                MemberInfo mi = sf.GetMethod();

                if (!(!string.IsNullOrEmpty(strSkipClassName) && (mi.DeclaringType.Name.IndexOf(strSkipClassName) > -1)))
                    sb.Append(StackFrameToString(sf));
            }
            sb.Append(Environment.NewLine);

            return sb.ToString();
        }

        public List<ExceptionLocationData> GetErrorLocations()
        {
            StackTrace stackTrace = new StackTrace(CurrentException, true);
            List<ExceptionLocationData> result = null;

            if (stackTrace != null)
            {
                if (stackTrace.FrameCount > 0)
                {
                    result = new List<ExceptionLocationData>();

                    foreach (StackFrame sf in stackTrace.GetFrames())
                    {
                        var lineNumber = sf.GetFileLineNumber();
                        var fileName = sf.GetFileName();
                        var methodInfo = GetExceptionMethodInfo(sf);

                        var location = new ExceptionLocationData(lineNumber, methodInfo, fileName);

                        result.Add(location);
                    }
                }
            }

            return result;
        }

        public string GetExceptionType(Exception ex = null)
        {
            ex = ex ?? CurrentException;

            StringBuilder builder = new StringBuilder();
            builder.Append("Exception Type:           ");
            try
            {
                builder.Append(ex.GetType().FullName);
            }
            catch (Exception e)
            {
                builder.Append("* Exception Type Error: " + e.ToString());
            }

            return builder.ToString();
        }

        /// <summary>
        /// gather some system information that is helpful to diagnosing exception
        /// </summary>
        /// <param name="blnIncludeStackTrace"></param>
        /// <returns></returns>
        public string SysInfoToString()
        {
            StringBuilder builder = new StringBuilder();

            builder.Append("[System information]");
            builder.Append(Environment.NewLine);

            builder.Append("Date and Time:         ");
            builder.Append(DateTime.Now.ToString(CultureInfo.CurrentCulture.DateTimeFormat.FullDateTimePattern));
            builder.Append(Environment.NewLine);

            builder.Append("Machine Name:          ");
            try
            {
                builder.Append(Environment.MachineName);
            }
            catch (Exception e)
            {
                builder.Append(e.Message);
            }
            builder.Append(Environment.NewLine);

            builder.Append("IP Addresses:            ");
            builder.Append(GetCurrentIP());
            builder.Append(Environment.NewLine);

            builder.Append("Current User:          ");
            builder.Append(ProcessIdentity());
            builder.Append(Environment.NewLine);
            builder.Append(Environment.NewLine);

            return builder.ToString();
        }

        /// <summary>
        /// exception-safe System.Environment "domain\username" retrieval
        /// </summary>
        private string CurrentEnvironmentIdentity()
        {
            try
            {
                return System.Environment.UserDomainName + "\\" + System.Environment.UserName;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// exception-safe WindowsIdentity.GetCurrent retrieval; returns "domain\username"
        /// </summary>
        /// <remarks>
        /// per MS, this can sometimes randomly fail with "Access Denied" on NT4
        /// </remarks>
        private string CurrentWindowsIdentity()
        {
            try
            {
                return System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// get IP address of this machine
        /// </summary>
        /// <remarks>Not an ideal method for a number of reasons (guess why!) but the alternatives are very ugly</remarks>
        /// <returns></returns>
        private string GetCurrentIP()
        {
            try
            {
                StringBuilder ipAddressList = new StringBuilder();
                foreach (IPAddress ipAddr in Dns.GetHostEntry(Dns.GetHostName()).AddressList)
                {
                    if (ipAddr.IsIPv6LinkLocal)
                    {
                        ipAddressList.AppendLine(string.Empty);
                        ipAddressList.Append("IPv6:	");
                    }
                    else
                    {
                        ipAddressList.AppendLine(string.Empty);
                        ipAddressList.Append("IPv4:	");
                    }
                    ipAddressList.Append(ipAddr.ToString());
                }

                return ipAddressList.ToString();
            }
            catch (Exception)
            {
                return "127.0.0.1";
            }
        }

        private string GetExceptionMethodInfo(StackFrame sf)
        {
            StringBuilder sb = new StringBuilder();
            int intParam;
            MemberInfo mi = sf.GetMethod();

            // build method name
            sb.Append("	");
            sb.Append(mi.DeclaringType.Namespace);
            sb.Append(".");
            sb.Append(mi.DeclaringType.Name);
            sb.Append(".");
            sb.Append(mi.Name);

            // build method params
            ParameterInfo[] objParameters = sf.GetMethod().GetParameters();

            sb.Append("( ");
            intParam = 0;
            foreach (ParameterInfo objParameter in objParameters)
            {
                intParam += 1;
                if (intParam > 1) sb.Append(", ");

                sb.Append(objParameter.ParameterType.Name);
                sb.Append(" ");
                sb.Append(objParameter.Name);
            }
            sb.Append(" )");

            return sb.ToString();
        }

        /// <summary>
        /// retrieve Process identity with fallback on error to safer method
        /// </summary>
        private string ProcessIdentity()
        {
            string strTemp = CurrentWindowsIdentity();

            if (string.IsNullOrEmpty(strTemp))
                strTemp = CurrentEnvironmentIdentity();

            return strTemp;
        }

        private void SqlExceptionToString(StringBuilder builder, Exception ex)
        {
            var se = ex as SqlException;

            builder.Append(Environment.NewLine);
            builder.Append("SQL Procedure:         ");

            builder.Append(se.Procedure);
            builder.Append(Environment.NewLine);

            builder.Append("SQL Server:            ");

            builder.Append(se.Server);
            builder.Append(Environment.NewLine);

            builder.Append("SQL State:             ");
            builder.Append(se.State);

            builder.Append(Environment.NewLine);

            builder.Append("SQL Source:            ");
            builder.Append(se.Source);

            builder.Append(Environment.NewLine);

            builder.Append("SQL Error Number:      ");
            builder.Append(se.Number);

            builder.Append(Environment.NewLine);

            builder.Append("SQL Line Number:       ");
            builder.Append(se.LineNumber);

            builder.Append(Environment.NewLine);

            foreach (SqlError error in se.Errors)
            {
                builder.Append("SQL Error Class:       ");
                builder.Append(error.Class);
                builder.Append(Environment.NewLine);

                builder.Append("SQL Error Line Number: ");
                builder.Append(error.LineNumber);
                builder.Append(Environment.NewLine);

                builder.Append("SQL Error Message:     ");
                builder.Append(error.Message);
                builder.Append(Environment.NewLine);

                builder.Append("SQL Error Number:      ");
                builder.Append(error.Number);
                builder.Append(Environment.NewLine);

                builder.Append("SQL Error Procedure:   ");
                builder.Append(error.Procedure);
                builder.Append(Environment.NewLine);

                builder.Append("SQL Error Server:      ");
                builder.Append(error.Server);
                builder.Append(Environment.NewLine);

                builder.Append("SQL Error Source:      ");
                builder.Append(error.Source);
                builder.Append(Environment.NewLine);

                builder.Append("SQL Error State:       ");
                builder.Append(error.State);

                builder.Append(Environment.NewLine);
                builder.Append(Environment.NewLine);
            }
        }

        /// <summary>
        /// turns a single stack frame object into an informative string
        /// </summary>
        /// <param name="sf"></param>
        /// <returns></returns>
        private string StackFrameToString(StackFrame sf)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(GetExceptionMethodInfo(sf));

            if (string.IsNullOrEmpty(sf.GetFileName()))
            {
                sb.Append(Environment.NewLine);
                sb.Append("		");

                if (CurrentAssembly != null)
                    sb.Append(System.IO.Path.GetFileName(CurrentAssembly.CodeBase));
                else
                    sb.Append("(unknown file)");

                // native code offset is always available
                sb.Append(": N ");
                sb.Append(String.Format("{0:#00000}", sf.GetNativeOffset()));
            }
            else
            {
                var exceptionFileName = sf.GetFileName();
                var exceptionFileLine = sf.GetFileLineNumber();
                var fileColumnNumber = sf.GetFileColumnNumber();

                sb.Append(Environment.NewLine);
                sb.Append("		");

                sb.Append(System.IO.Path.GetFileName(exceptionFileName));
                sb.Append(": line ");
                sb.Append(string.Format("{0:#0000}", exceptionFileLine));
                sb.Append(", col ");

                sb.Append(string.Format("{0:#00}", fileColumnNumber));
                // if IL is available, append IL location info
                if (sf.GetILOffset() != System.Diagnostics.StackFrame.OFFSET_UNKNOWN)
                {
                    sb.Append(", IL ");
                    sb.Append(String.Format("{0:#0000}", sf.GetILOffset()));
                }
            }
            sb.Append(Environment.NewLine);

            return sb.ToString();
        }
    }
}