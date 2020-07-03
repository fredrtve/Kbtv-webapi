using AutoMapper;
using BjBygg.Application.Commands.BaseEntityCommands.Create;
using BjBygg.Application.Shared;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Infrastructure.Data;

namespace BjBygg.Application.Commands.MissionTypeCommands.Create
{
    public class CreateMissionTypeHandler : CreateHandler<MissionType, CreateMissionTypeCommand, MissionTypeDto>
    {
        public CreateMissionTypeHandler(AppDbContext dbContext, IMapper mapper) :
            base(dbContext, mapper) { }
    }
}
