namespace Aviant.DDD.Application.Exceptions
{
    using System;
    using Core.Exceptions;

    public class ApplicationException : DomainException
    {
        public ApplicationException()
        { }

        public ApplicationException(string message)
            : base(message)
        { }

        public ApplicationException(string message, Exception inner)
            : base(message, inner)
        { }

        public ApplicationException(
            string     message,
            int        errorCode,
            int?       familyCode = null,
            Exception? inner      = null)
            : base(
                message,
                errorCode,
                familyCode,
                inner)
        { }
    }
}