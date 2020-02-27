using CleanArchitecture.Core.Entities;
using CleanArchitecture.Infrastructure.Data;
using BjBygg.Application.Commands.Shared.DeleteRange;

namespace BjBygg.Application.Commands.MissionTypeCommands.DeleteRange
{
    public class DeleteRangeMissionTypeHandler : DeleteRangeHandler<MissionType, DeleteRangeMissionTypeCommand>
    {
        public DeleteRangeMissionTypeHandler(AppDbContext dbContext) : base(dbContext){}
    }
}
