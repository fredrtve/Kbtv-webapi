using BjBygg.Application.Commands.BaseEntityCommands.Delete;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Infrastructure.Data;

namespace BjBygg.Application.Commands.MissionCommands.Notes
{
    public class DeleteMissionNoteCommand : DeleteCommand
    {
    }
    public class DeleteMissionNoteCommandHandler : DeleteCommandHandler<MissionNote, DeleteMissionNoteCommand>
    {
        public DeleteMissionNoteCommandHandler(AppDbContext dbContext) :
            base(dbContext)
        { }
    }
}
