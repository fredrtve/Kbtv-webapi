using BjBygg.Application.Application.Common.Interfaces;
using BjBygg.Application.Common.BaseEntityCommands.DeleteRange;
using CleanArchitecture.Core.Entities;

namespace BjBygg.Application.Application.Commands.MissionTypeCommands
{
    public class DeleteRangeMissionTypeCommand : DeleteRangeCommand
    {
    }
    public class DeleteRangeMissionTypeCommandHandler : DeleteRangeHandler<MissionType, DeleteRangeMissionTypeCommand>
    {
        public DeleteRangeMissionTypeCommandHandler(IAppDbContext dbContext) :
            base(dbContext)
        { }
    }
}
