using AutoMapper;
using BjBygg.Application.Application.Common.Interfaces;
using BjBygg.Application.Common.BaseEntityCommands.Create;
using BjBygg.Core.Entities;
using BjBygg.SharedKernel;

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
