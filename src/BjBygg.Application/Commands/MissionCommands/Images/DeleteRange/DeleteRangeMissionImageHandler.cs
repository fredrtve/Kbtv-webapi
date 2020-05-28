using BjBygg.Application.Commands.Shared.DeleteRange;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Infrastructure.Data;


namespace BjBygg.Application.Commands.MissionCommands.Images.DeleteRange
{
    public class DeleteRangeMissionImageHandler : DeleteRangeHandler<MissionImage, DeleteRangeMissionImageCommand>
    {
        public DeleteRangeMissionImageHandler(AppDbContext dbContext) : base(dbContext){}
    }
}
