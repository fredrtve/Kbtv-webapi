using BjBygg.Application.Application.Common.Interfaces;
using BjBygg.Application.Common.BaseEntityCommands.Delete;
using CleanArchitecture.Core.Entities;

namespace BjBygg.Application.Application.Commands.MissionCommands.Notes
{
    public class DeleteMissionNoteCommand : DeleteCommand
    {
    }
    public class DeleteMissionNoteCommandHandler : DeleteCommandHandler<MissionNote, DeleteMissionNoteCommand>
    {
        public DeleteMissionNoteCommandHandler(IAppDbContext dbContext) :
            base(dbContext)
        { }
    }
}
