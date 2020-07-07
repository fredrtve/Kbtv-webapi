using BjBygg.Application.Commands.BaseEntityCommands.DeleteRange;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Infrastructure.Data;

namespace BjBygg.Application.Commands.MissionCommands.Images
{
    public class DeleteRangeMissionImageCommand : DeleteRangeCommand
    {
    }

    public class DeleteRangeMissionImageCommandHandler : DeleteRangeHandler<MissionImage, DeleteRangeMissionImageCommand>
    {
        public DeleteRangeMissionImageCommandHandler(AppDbContext dbContext) :
            base(dbContext)
        { }
    }
}
