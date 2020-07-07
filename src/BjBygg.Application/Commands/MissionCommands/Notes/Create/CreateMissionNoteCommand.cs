using AutoMapper;
using BjBygg.Application.Commands.BaseEntityCommands.Create;
using BjBygg.Application.Common;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Infrastructure.Data;

namespace BjBygg.Application.Commands.MissionCommands.Notes.Create
{
    public class CreateMissionNoteCommand : CreateCommand<MissionNoteDto>
    {
        public int MissionId { get; set; }
        public string? Title { get; set; }
        public string Content { get; set; }
        public bool? Pinned { get; set; }
    }

    public class CreateMissionNoteHandler : CreateCommandHandler<MissionNote, CreateMissionNoteCommand, MissionNoteDto>
    {
        public CreateMissionNoteHandler(AppDbContext dbContext, IMapper mapper) :
            base(dbContext, mapper)
        { }
    }
}
