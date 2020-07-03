using BjBygg.Application.Commands.BaseEntityCommands.Delete;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Infrastructure.Data;

namespace BjBygg.Application.Commands.EmployerCommands.Delete
{
    public class DeleteEmployerHandler : DeleteCommandHandler<Employer, DeleteEmployerCommand>
    {
        public DeleteEmployerHandler(AppDbContext dbContext) :
            base(dbContext) {}
    }
}
