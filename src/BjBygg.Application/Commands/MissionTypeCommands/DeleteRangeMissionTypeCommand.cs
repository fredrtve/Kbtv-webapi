using BjBygg.Application.Commands.BaseEntityCommands.DeleteRange;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Infrastructure.Data;

namespace BjBygg.Application.Commands.MissionTypeCommands
{
    public class DeleteRangeMissionTypeCommand : DeleteRangeCommand
    {
    }
    public class DeleteRangeMissionTypeCommandHandler : DeleteRangeHandler<MissionType, DeleteRangeMissionTypeCommand>
    {
        public DeleteRangeMissionTypeCommandHandler(AppDbContext dbContext) :
            base(dbContext)
        { }
    }
}
