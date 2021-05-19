using BjBygg.Application.Application.Common.Interfaces;
using BjBygg.Application.Common.BaseEntityCommands.Delete;
using BjBygg.Core.Entities;

namespace BjBygg.Application.Application.Commands.MissionTypeCommands
{
    public class DeleteMissionTypeCommand : DeleteCommand { }

    public class DeleteMissionTypeCommandValidator : DeleteCommandValidator<DeleteMissionTypeCommand> { }

    public class DeleteMissionTypeCommandHandler : DeleteCommandHandler<MissionType, DeleteMissionTypeCommand>
    {
        public DeleteMissionTypeCommandHandler(IAppDbContext dbContext) :
            base(dbContext)
        { }
    }
}