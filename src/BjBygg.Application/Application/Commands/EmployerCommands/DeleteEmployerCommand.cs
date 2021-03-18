using BjBygg.Application.Application.Common.Interfaces;
using BjBygg.Application.Common.BaseEntityCommands.Delete;
using BjBygg.Core.Entities;

namespace BjBygg.Application.Application.Commands.EmployerCommands
{
    public class DeleteEmployerCommand : DeleteCommand { }
    public class DeleteEmployerCommandValidator : DeleteCommandValidator<DeleteEmployerCommand> { }
    public class DeleteEmployerCommandHandler : DeleteCommandHandler<Employer, DeleteEmployerCommand>
    {
        public DeleteEmployerCommandHandler(IAppDbContext dbContext) :
            base(dbContext)
        { }
    }
}
