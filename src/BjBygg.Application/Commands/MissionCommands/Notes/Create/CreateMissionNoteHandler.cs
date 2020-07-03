using AutoMapper;
using BjBygg.Application.Commands.BaseEntityCommands.Create;
using BjBygg.Application.Shared;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Infrastructure.Data;

namespace BjBygg.Application.Commands.MissionCommands.Notes.Create
{
    public class CreateMissionNoteHandler : CreateCommandHandler<MissionNote, CreateMissionNoteCommand, MissionNoteDto>
    {
        public CreateMissionNoteHandler(AppDbContext dbContext, IMapper mapper) :
            base(dbContext, mapper) {}
    }
}
