using CleanArchitecture.SharedKernel;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace BjBygg.Application.Application.Common
{
    public class BasicFileStreamValidator : AbstractValidator<BasicFileStream>
    {
        public BasicFileStreamValidator(HashSet<string> allowedExtensions)
        {
            RuleFor(x => x.FileExtension)
                .Must(x => allowedExtensions.Contains(x))
                .WithName("Filtype");
        }
    }
}
