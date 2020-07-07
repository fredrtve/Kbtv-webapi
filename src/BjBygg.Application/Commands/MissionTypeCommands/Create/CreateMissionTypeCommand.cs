using AutoMapper;
using BjBygg.Application.Commands.BaseEntityCommands.Create;
using BjBygg.Application.Common;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Infrastructure.Data;

namespace BjBygg.Application.Commands.MissionTypeCommands.Create
{
    public class CreateMissionTypeCommand : CreateCommand<MissionTypeDto>
    {
        public string Name { get; set; }
    }
    public class CreateMissionTypeCommandHandler : CreateCommandHandler<MissionType, CreateMissionTypeCommand, MissionTypeDto>
    {
        public CreateMissionTypeCommandHandler(AppDbContext dbContext, IMapper mapper) :
            base(dbContext, mapper)
        { }
    }
}
