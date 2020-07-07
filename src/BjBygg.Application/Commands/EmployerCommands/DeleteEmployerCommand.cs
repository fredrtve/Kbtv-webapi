using BjBygg.Application.Commands.BaseEntityCommands.Delete;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Infrastructure.Data;

namespace BjBygg.Application.Commands.EmployerCommands
{
    public class DeleteEmployerCommand : DeleteCommand
    {
    }
    public class DeleteEmployerCommandHandler : DeleteCommandHandler<Employer, DeleteEmployerCommand>
    {
        public DeleteEmployerCommandHandler(AppDbContext dbContext) :
            base(dbContext)
        { }
    }
}
