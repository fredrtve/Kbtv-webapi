using FluentValidation;

namespace BjBygg.Application.Application.Queries.TimesheetQueries
{
    public class TimesheetQueryValidator : AbstractValidator<TimesheetQuery>
    {
        public TimesheetQueryValidator()
        {
            RuleFor(x => x.StartDate)
                .NotEmpty().When(m => m.EndDate == null)
                .LessThanOrEqualTo(x => x.EndDate).When(m => m.StartDate != null && m.EndDate != null)
                .WithName("Startdato");

            RuleFor(x => x.EndDate)
                .NotEmpty().When(m => m.StartDate == null)
                .GreaterThanOrEqualTo(x => x.StartDate).When(m => m.StartDate != null && m.EndDate != null)
                .WithName("Sluttdato");
        }
    }
}
