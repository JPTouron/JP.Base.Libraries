using System;

namespace JP.Base.Common.Exceptions
{
    public class CriticalException : Exception
    {
        public CriticalException()
        {
        }

        public CriticalException(string message)
            : base(message)
        {
        }

        public CriticalException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}