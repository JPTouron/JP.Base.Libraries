using System;
using System.Collections.Generic;

namespace JP.Base.Common.Errors
{
    public class ExceptionData
    {
        public ExceptionData(Exception ex)
            : this(ex, false)
        {
        }

        public ExceptionData(Exception ex, bool isUnhandledException)
        {
            IsUnhandledException = isUnhandledException;
            ParseExceptionData(ex);
        }

        public string AssemblyInfo
        {
            get; private set;
        }

        public Exception CurrenException
        {
            get; private set;
        }

        public string EnvironmentInfo
        {
            get; private set;
        }

        public List<ExceptionLocationData> ErrorLocations
        {
            get; private set;
        }

        public string ExceptionText
        {
            get; private set;
        }

        public string ExceptionType
        {
            get; private set;
        }

        public bool IsUnhandledException
        {
            get; set;
        }

        public string StackTrace
        {
            get; private set;
        }

        private void ParseExceptionData(Exception ex)
        {
            var errParser = new ExceptionParser(ex);
            ErrorLocations = errParser.GetErrorLocations();
            EnvironmentInfo = errParser.SysInfoToString();
            AssemblyInfo = errParser.GetAssemblyInfo();
            ExceptionText = errParser.ExceptionToString();
            ExceptionType = errParser.GetExceptionType();
            StackTrace = errParser.GetEnhancedStackTrace();
        }
    }
}