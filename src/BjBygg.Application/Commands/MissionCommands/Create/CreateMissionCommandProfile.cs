using AutoMapper;
using BjBygg.Application.Shared;
using CleanArchitecture.Core.Entities;

namespace BjBygg.Application.Commands.MissionCommands.Create
{
    class CreateMissionCommandProfile : Profile
    {
        public CreateMissionCommandProfile()
        {
            CreateMap<CreateMissionCommand, Mission>()
                .ForMember(dest => dest.MissionType, opt => opt.MapFrom(src => src.MissionType))
                .ForMember(dest => dest.Employer, opt => opt.MapFrom(src => src.Employer));

            CreateMap<MissionTypeDto, MissionType>();
            CreateMap<EmployerDto, Employer>();
        }
    }
}
