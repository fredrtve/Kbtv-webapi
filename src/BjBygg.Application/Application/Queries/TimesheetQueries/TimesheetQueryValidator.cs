using FluentValidation;

namespace BjBygg.Application.Application.Queries.TimesheetQueries
{
    public class TimesheetQueryValidator : AbstractValidator<TimesheetQuery>
    {
        public TimesheetQueryValidator()
        {
            RuleFor(x => x.StartDate)
                .LessThanOrEqualTo(x => x.EndDate)
                .WithName("Startdato");

            RuleFor(x => x.EndDate)
                .GreaterThanOrEqualTo(x => x.StartDate)
                .WithName("Sluttdato");
        }
    }
}
