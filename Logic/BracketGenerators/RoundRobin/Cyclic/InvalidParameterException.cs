using System;
using System.Runtime.Serialization;

namespace Logic.BracketGenerators.RoundRobin.Cyclic
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
