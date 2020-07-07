using AutoMapper;
using BjBygg.Application.Commands.BaseEntityCommands.Update;
using BjBygg.Application.Common;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Infrastructure.Data;

namespace BjBygg.Application.Commands.MissionTypeCommands.Update
{
    public class UpdateMissionTypeCommand : UpdateCommand<MissionTypeDto>
    {
        public string Name { get; set; }
    }
    public class UpdateMissionTypeCommandHandler : UpdateCommandHandler<MissionType, UpdateMissionTypeCommand, MissionTypeDto>
    {
        public UpdateMissionTypeCommandHandler(AppDbContext dbContext, IMapper mapper) :
            base(dbContext, mapper)
        { }
    }
}
