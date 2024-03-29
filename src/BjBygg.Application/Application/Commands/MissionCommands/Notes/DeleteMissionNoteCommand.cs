using BjBygg.Application.Application.Common.Interfaces;
using BjBygg.Application.Common.BaseEntityCommands.Delete;
using BjBygg.Core.Entities;

namespace BjBygg.Application.Application.Commands.MissionCommands.Notes
{
    public class DeleteMissionNoteCommand : DeleteCommand { }
    public class DeleteMissionNoteCommandValidator : DeleteCommandValidator<DeleteMissionNoteCommand> { }
    public class DeleteMissionNoteCommandHandler : DeleteCommandHandler<MissionNote, DeleteMissionNoteCommand>
    {
        public DeleteMissionNoteCommandHandler(IAppDbContext dbContext) :
            base(dbContext)
        { }
    }
}
