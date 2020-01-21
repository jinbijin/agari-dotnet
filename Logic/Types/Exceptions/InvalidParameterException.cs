using System;
using System.Runtime.Serialization;

namespace Logic.Types.Exceptions
{
    public class InvalidParameterException : Exception
    {
        public InvalidParameterException() : base()
        {
        }

        public InvalidParameterException(string? message) : base(message)
        {
        }

        public InvalidParameterException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected InvalidParameterException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

    }
}
