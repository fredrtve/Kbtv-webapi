using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BjBygg.Application.Common.Exceptions
{
    public class ValidationException : Exception
    {
        public ValidationException()
            : base("En eller flere valideringsfeil har oppstått.")
        {
            Errors = new Dictionary<string, string[]>();
        }
        public ValidationException(string key, string message)
            : base("En eller flere valideringsfeil har oppstått.")
        {
            Errors = new Dictionary<string, string[]>() {
                { key, new string[1] { message } }
            };
        }

        public ValidationException(IEnumerable<ValidationFailure> failures)
            : this()
        {
            var failureGroups = failures
                .GroupBy(e => e.PropertyName, e => e.ErrorMessage);

            foreach (var failureGroup in failureGroups)
            {
                var propertyName = failureGroup.Key;
                var propertyFailures = failureGroup.ToArray();

                Errors.Add(propertyName, propertyFailures);
            }
        }

        public IDictionary<string, string[]> Errors { get; }
    }
}