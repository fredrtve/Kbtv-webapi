using AutoMapper;
using BjBygg.Application.Commands.MissionTypeCommands.Create;
using CleanArchitecture.Core.Entities;

namespace BjBygg.Application.Commands.TimesheetCommands.Create
{
    public class CreateMissionTypeCommandProfile : Profile
    {
        public CreateMissionTypeCommandProfile()
        {
            CreateMap<CreateMissionTypeCommand, MissionType>();
            CreateMap<MissionType, CreateMissionTypeCommand>();
        }
    }
}
