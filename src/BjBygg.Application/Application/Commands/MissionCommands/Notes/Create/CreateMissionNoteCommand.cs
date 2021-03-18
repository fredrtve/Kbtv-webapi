using AutoMapper;
using BjBygg.Application.Application.Common.Interfaces;
using BjBygg.Application.Common.BaseEntityCommands.Create;
using BjBygg.Core.Entities;

namespace BjBygg.Application.Application.Commands.MissionCommands.Notes.Create
{
    public class CreateMissionNoteCommand : CreateCommand
    {
        public string MissionId { get; set; }
        public string? Title { get; set; }
        public string Content { get; set; }
    }

    public class CreateMissionNoteHandler : CreateCommandHandler<MissionNote, CreateMissionNoteCommand>
    {
        public CreateMissionNoteHandler(IAppDbContext dbContext, IMapper mapper) :
            base(dbContext, mapper)
        { }
    }
}
