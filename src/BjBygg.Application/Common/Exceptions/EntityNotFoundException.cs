using System;
using System.Collections.Generic;
using System.Linq;

namespace BjBygg.Application.Common.Exceptions
{
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException() : base() { }

        public EntityNotFoundException(string message) : base(message) { }

        public EntityNotFoundException(string message, Exception ex) : base(message, ex) { }

        public EntityNotFoundException(string name, object key)
        : base($"Entity \"{name}\" ({key}) was not found.") { }
    }
}
