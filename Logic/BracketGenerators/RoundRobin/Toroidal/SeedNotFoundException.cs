using System;
using System.Runtime.Serialization;

namespace Logic.BracketGenerators.RoundRobin.Toroidal
{
    public class SeedNotFoundException : Exception
    {
        public SeedNotFoundException() : base()
        {
        }

        public SeedNotFoundException(string? message) : base(message)
        {
        }

        public SeedNotFoundException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected SeedNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
