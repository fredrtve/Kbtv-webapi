using BjBygg.Application.Application.Common.Interfaces;
using BjBygg.Application.Common.BaseEntityCommands.DeleteRange;
using CleanArchitecture.Core.Entities;

namespace BjBygg.Application.Application.Commands.EmployerCommands
{
    public class DeleteRangeEmployerCommand : DeleteRangeCommand { }
    public class DeleteRangeEmployerCommandValidator : DeleteRangeCommandValidator<DeleteRangeEmployerCommand> { }
    public class DeleteRangeEmployerCommandHandler : DeleteRangeCommandHandler<Employer, DeleteRangeEmployerCommand>
    {
        public DeleteRangeEmployerCommandHandler(IAppDbContext dbContext) :
            base(dbContext)
        { }
    }
}
