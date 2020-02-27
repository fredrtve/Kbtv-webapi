using BjBygg.Application.Commands.Shared.DeleteRange;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Infrastructure.Data;


namespace BjBygg.Application.Commands.MissionCommands.DeleteRange
{
    public class DeleteRangeMissionHandler : DeleteRangeHandler<Mission, DeleteRangeMissionCommand>
    {
        public DeleteRangeMissionHandler(AppDbContext dbContext) : base(dbContext){}
    }
}
