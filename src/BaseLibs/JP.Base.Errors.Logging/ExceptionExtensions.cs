using System;
using JP.Base.Common.Errors;

namespace JP.Base.Errors.Logging
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