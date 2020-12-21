using BjBygg.Application.Application.Common.Interfaces;
using BjBygg.Application.Common.BaseEntityCommands.DeleteRange;
using CleanArchitecture.Core.Entities;

namespace BjBygg.Application.Application.Commands.MissionCommands.Images
{
    public class DeleteRangeMissionImageCommand : DeleteRangeCommand { }
    public class DeleteRangeMissionImageCommandValidator : DeleteRangeCommandValidator<DeleteRangeMissionImageCommand> { }
    public class DeleteRangeMissionImageCommandHandler : DeleteRangeCommandHandler<MissionImage, DeleteRangeMissionImageCommand>
    {
        public DeleteRangeMissionImageCommandHandler(IAppDbContext dbContext) :
            base(dbContext)
        { }
    }
}
