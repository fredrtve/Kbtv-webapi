using CleanArchitecture.Core.Entities;
using CleanArchitecture.Infrastructure.Data;
using BjBygg.Application.Commands.BaseEntityCommands.DeleteRange;

namespace BjBygg.Application.Commands.MissionTypeCommands.DeleteRange
{
    public class DeleteRangeMissionTypeHandler : DeleteRangeCommandHandler<MissionType, DeleteRangeMissionTypeCommand>
    {
        public DeleteRangeMissionTypeHandler(AppDbContext dbContext) : base(dbContext){}
    }
}
