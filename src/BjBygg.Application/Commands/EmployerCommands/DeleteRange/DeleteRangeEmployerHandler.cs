using BjBygg.Application.Commands.BaseEntityCommands.DeleteRange;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Infrastructure.Data;


namespace BjBygg.Application.Commands.EmployerCommands.DeleteRange
{
    public class DeleteRangeEmployerHandler : DeleteRangeHandler<Employer, DeleteRangeEmployerCommand>
    {
        public DeleteRangeEmployerHandler(AppDbContext dbContext) : 
            base(dbContext) {}
    }
}
