using AutoMapper;
using BjBygg.Application.Application.Common.Interfaces;
using BjBygg.Application.Common.BaseEntityCommands.Create;
using CleanArchitecture.Core.Entities;

namespace BjBygg.Application.Application.Commands.MissionCommands.Create
{
    public class CreateMissionCommandHandler : CreateCommandHandler<Mission, CreateMissionCommand>
    {
        public CreateMissionCommandHandler(IAppDbContext dbContext, IMapper mapper) :
            base(dbContext, mapper)
        { }
    }
}
