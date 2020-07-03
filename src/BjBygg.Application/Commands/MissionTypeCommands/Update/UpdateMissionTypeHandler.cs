using AutoMapper;
using BjBygg.Application.Commands.BaseEntityCommands.Update;
using BjBygg.Application.Shared;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Infrastructure.Data;

namespace BjBygg.Application.Commands.MissionTypeCommands.Update
{
    public class UpdateMissionTypeHandler : UpdateHandler<MissionType, UpdateMissionTypeCommand, MissionTypeDto>
    {
        public UpdateMissionTypeHandler(AppDbContext dbContext, IMapper mapper) :
            base(dbContext, mapper) {}
    }
}
