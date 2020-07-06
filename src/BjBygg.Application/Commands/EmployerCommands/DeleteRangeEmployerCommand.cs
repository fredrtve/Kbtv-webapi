using BjBygg.Application.Commands.BaseEntityCommands.DeleteRange;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Infrastructure.Data;

namespace BjBygg.Application.Commands.EmployerCommands
{
    public class DeleteRangeEmployerCommand : DeleteRangeCommand 
    {
    }
    public class DeleteRangeEmployerCommandHandler : DeleteRangeHandler<Employer, DeleteRangeEmployerCommand>
    {
        public DeleteRangeEmployerCommandHandler(AppDbContext dbContext) :
            base(dbContext) {}
    }
}
