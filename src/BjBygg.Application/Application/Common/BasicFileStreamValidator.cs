using BjBygg.SharedKernel;
using FluentValidation;
using System.Collections.Generic;

namespace BjBygg.Application.Application.Common
{
    public class BasicFileStreamValidator : AbstractValidator<BasicFileStream>
    {
        public BasicFileStreamValidator(HashSet<string> allowedExtensions)
        {
            RuleFor(x => x.FileExtension)
                .Must(x => allowedExtensions.Contains(x.ToLower()))
                .WithName("Filtype");
        }
    }
}
