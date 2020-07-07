using AutoMapper;
using BjBygg.Application.Commands.BaseEntityCommands.Update;
using BjBygg.Application.Common;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Infrastructure.Data;

namespace BjBygg.Application.Commands.MissionCommands.Notes.Update
{
    public class UpdateMissionNoteCommand : UpdateCommand<MissionNoteDto>
    {
        public string? Title { get; set; }
        public string Content { get; set; }
        public bool? Pinned { get; set; }

    }
    public class UpdateMissionNoteCommandHandler : UpdateCommandHandler<MissionNote, UpdateMissionNoteCommand, MissionNoteDto>
    {
        public UpdateMissionNoteCommandHandler(AppDbContext dbContext, IMapper mapper) :
            base(dbContext, mapper)
        { }

    }
}
