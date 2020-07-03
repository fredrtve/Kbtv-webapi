using AutoMapper;
using BjBygg.Application.Shared;
using CleanArchitecture.Core.Entities;

namespace BjBygg.Application.Commands.MissionCommands.Create
{
    class CreateMissionProfile : Profile
    {
        public CreateMissionProfile()
        {
            CreateMap<CreateMissionCommand, Mission>()
                .ForMember(dest => dest.MissionType, opt => opt.MapFrom(src => src.MissionType))
                .ForMember(dest => dest.Employer, opt => opt.MapFrom(src => src.Employer))
                .ForMember(dest => dest.ImageURL, opt => opt.Ignore())
                .ForSourceMember(src => src.Image, dest => dest.DoNotValidate());

            CreateMap<MissionTypeDto, MissionType>();
            CreateMap<EmployerDto, Employer>();
        }
    }
}
