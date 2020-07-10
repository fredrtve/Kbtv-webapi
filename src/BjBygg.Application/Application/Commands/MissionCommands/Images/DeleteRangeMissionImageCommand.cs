using BjBygg.Application.Application.Common.Interfaces;
using BjBygg.Application.Common.BaseEntityCommands.DeleteRange;
using CleanArchitecture.Core.Entities;

namespace BjBygg.Application.Application.Commands.MissionCommands.Images
{
    public class DeleteRangeMissionImageCommand : DeleteRangeCommand
    {
    }

    public class DeleteRangeMissionImageCommandHandler : DeleteRangeHandler<MissionImage, DeleteRangeMissionImageCommand>
    {
        public DeleteRangeMissionImageCommandHandler(IAppDbContext dbContext) :
            base(dbContext)
        { }
    }
}
