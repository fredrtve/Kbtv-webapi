using AutoMapper;
using CleanArchitecture.Core.Entities;

namespace BjBygg.Application.Commands.MissionTypeCommands.Update
{
    class UpdateMissionTypeCommandProfile : Profile
    {
        public UpdateMissionTypeCommandProfile()
        {
            CreateMap<UpdateMissionTypeCommand, MissionType>();
        }
    }
}
