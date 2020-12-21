using BjBygg.Application.Application.Common.Interfaces;
using BjBygg.Application.Common.BaseEntityCommands.Update;
using CleanArchitecture.Core.Entities;

namespace BjBygg.Application.Application.Commands.MissionCommands.Notes.Update
{
    public class UpdateMissionNoteCommand : UpdateCommand
    {
        public string? Title { get; set; }
        public string Content { get; set; }

    }
    public class UpdateMissionNoteCommandHandler : UpdateCommandHandler<MissionNote, UpdateMissionNoteCommand>
    {
        public UpdateMissionNoteCommandHandler(IAppDbContext dbContext) :
            base(dbContext)
        { }

    }
}
