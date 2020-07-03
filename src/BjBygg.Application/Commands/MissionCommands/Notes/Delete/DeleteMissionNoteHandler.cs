using BjBygg.Application.Commands.BaseEntityCommands.Delete;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Infrastructure.Data;

namespace BjBygg.Application.Commands.MissionCommands.Notes.Delete
{
    public class DeleteMissionNoteHandler : DeleteHandler<MissionNote, DeleteMissionNoteCommand>
    {
        public DeleteMissionNoteHandler(AppDbContext dbContext) :
            base(dbContext) { }
    }
}
