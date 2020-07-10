using AutoMapper;
using BjBygg.Application.Application.Common.Dto;
using BjBygg.Application.Application.Common.Interfaces;
using BjBygg.Application.Common.BaseEntityCommands.Create;
using CleanArchitecture.Core.Entities;

namespace BjBygg.Application.Application.Commands.MissionCommands.Notes.Create
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
        public CreateMissionNoteHandler(IAppDbContext dbContext, IMapper mapper) :
            base(dbContext, mapper)
        { }
    }
}
