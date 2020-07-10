using AutoMapper;
using BjBygg.Application.Application.Common.Dto;
using BjBygg.Application.Application.Common.Interfaces;
using BjBygg.Application.Common.BaseEntityCommands.Update;
using CleanArchitecture.Core.Entities;

namespace BjBygg.Application.Application.Commands.MissionCommands.Notes.Update
{
    public class UpdateMissionNoteCommand : UpdateCommand<MissionNoteDto>
    {
        public string? Title { get; set; }
        public string Content { get; set; }
        public bool? Pinned { get; set; }

    }
    public class UpdateMissionNoteCommandHandler : UpdateCommandHandler<MissionNote, UpdateMissionNoteCommand, MissionNoteDto>
    {
        public UpdateMissionNoteCommandHandler(IAppDbContext dbContext, IMapper mapper) :
            base(dbContext, mapper)
        { }

    }
}
