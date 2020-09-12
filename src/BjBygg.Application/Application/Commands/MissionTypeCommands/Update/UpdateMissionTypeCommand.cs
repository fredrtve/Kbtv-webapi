using AutoMapper;
using BjBygg.Application.Application.Common.Dto;
using BjBygg.Application.Application.Common.Interfaces;
using BjBygg.Application.Common.BaseEntityCommands.Update;
using CleanArchitecture.Core.Entities;

namespace BjBygg.Application.Application.Commands.MissionTypeCommands.Update
{
    public class UpdateMissionTypeCommand : UpdateCommand
    {
        public string Name { get; set; }
    }
    public class UpdateMissionTypeCommandHandler : UpdateCommandHandler<MissionType, UpdateMissionTypeCommand>
    {
        public UpdateMissionTypeCommandHandler(IAppDbContext dbContext, IMapper mapper) :
            base(dbContext, mapper)
        { }
    }
}
