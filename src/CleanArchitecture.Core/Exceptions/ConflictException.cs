using System;

namespace CleanArchitecture.Core.Exceptions
{
    public class ConflictException : Exception
    {
        public ConflictException() : base() { }

        public ConflictException(string message) : base(message) { }

        public ConflictException(string message, Exception ex) : base(message, ex) { }
    }
}
