using BjBygg.Application.Application.Common.Interfaces;
using BjBygg.Application.Common.BaseEntityCommands.DeleteRange;
using BjBygg.Core.Entities;

namespace BjBygg.Application.Application.Commands.MissionTypeCommands
{
    public class DeleteRangeMissionTypeCommand : DeleteRangeCommand { }
    public class DeleteRangeMissionTypeCommandValidator : DeleteRangeCommandValidator<DeleteRangeMissionTypeCommand> { }
    public class DeleteRangeMissionTypeCommandHandler : DeleteRangeCommandHandler<MissionType, DeleteRangeMissionTypeCommand>
    {
        public DeleteRangeMissionTypeCommandHandler(IAppDbContext dbContext) :
            base(dbContext)
        { }
    }
}
