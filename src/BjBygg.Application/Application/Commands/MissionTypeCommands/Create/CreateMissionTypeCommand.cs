using AutoMapper;
using BjBygg.Application.Application.Common.Dto;
using BjBygg.Application.Application.Common.Interfaces;
using BjBygg.Application.Common.BaseEntityCommands.Create;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.SharedKernel;

namespace BjBygg.Application.Application.Commands.MissionTypeCommands.Create
{
    public class CreateMissionTypeCommand : CreateCommand, IName
    {
        public string Name { get; set; }
    }
    public class CreateMissionTypeCommandHandler : CreateCommandHandler<MissionType, CreateMissionTypeCommand>
    {
        public CreateMissionTypeCommandHandler(IAppDbContext dbContext, IMapper mapper) :
            base(dbContext, mapper)
        { }
    }
}
