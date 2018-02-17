using System;
using JP.Base.Common.Exceptions.Parsing;
using JP.Base.Errors.Logging;

namespace JP.Base.Exceptions.Winforms.Support
{
    public static class ExceptionExtensions
    {
        public static void LogException(this Exception ex)
        {
            var errObject = new ExceptionData(ex);
            var errLogger = new ErrorLogger(errObject);
            errLogger.LogException();
        }
    }
}