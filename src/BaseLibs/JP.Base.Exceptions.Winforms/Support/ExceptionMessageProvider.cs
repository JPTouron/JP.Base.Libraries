using System;
using System.Collections.Generic;

namespace JP.Base.Exceptions.Winforms.Support
{
    /// <summary>
    /// call this within your app initialization to setup the default message and any known error types with fixed messages
    /// </summary>
    public static class ErrorMessageProvider
    {
        static ErrorMessageProvider()
        {
            KnownErrorTypes = new Dictionary<Type, string>();
        }

        public static string DefaultErrorMessage { get; set; }

        public static Func<Exception, bool> IsExceptionOfBaseType { get; set; }
        public static IDictionary<Type, string> KnownErrorTypes { get; private set; }

        /// <summary>
        /// returns either a custom message from a defined BusinessException or a default message for an Exception type
        /// </summary>
        public static string GetMessageForError(this Exception ex)
        {
            var result = string.IsNullOrEmpty(DefaultErrorMessage) ? "An error has occurred" : DefaultErrorMessage;

            if (KnownErrorTypes.ContainsKey(ex.GetType()) || (IsExceptionOfBaseType != null && IsExceptionOfBaseType(ex)))
                result = string.IsNullOrEmpty(ex.Message) ? result : ex.Message;

            return result;
        }
    }
}