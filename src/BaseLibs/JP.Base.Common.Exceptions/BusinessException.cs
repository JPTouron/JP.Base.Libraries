using System;

namespace JP.Base.Common.Exceptions
{
    public class BusinessException : Exception
    {
        public BusinessException()
        {
        }

        public BusinessException(string msj)
            : base(msj)
        {
        }

        public BusinessException(string msj, Exception inner)
            : base(msj, inner)
        {
        }
    }
}