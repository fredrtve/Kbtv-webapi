using AutoMapper;
using BjBygg.Application.Commands.BaseEntityCommands.Update;
using BjBygg.Application.Common;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Infrastructure.Data;

namespace BjBygg.Application.Commands.MissionCommands.Notes.Update
{
    public class UpdateMissionNoteHandler : UpdateHandler<MissionNote, UpdateMissionNoteCommand, MissionNoteDto>
    {
        public UpdateMissionNoteHandler(AppDbContext dbContext, IMapper mapper) :
            base(dbContext, mapper) {}

    }
}
