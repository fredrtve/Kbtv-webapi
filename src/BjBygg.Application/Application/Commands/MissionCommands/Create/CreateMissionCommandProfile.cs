using AutoMapper;
using BjBygg.Core.Entities;

namespace BjBygg.Application.Application.Commands.MissionCommands.Create
{
    class CreateMissionCommandProfile : Profile
    {
        public CreateMissionCommandProfile()
        {
            CreateMap<CreateMissionCommand, Mission>()
                .ForMember(dest => dest.MissionType, opt => opt.MapFrom(src => src.MissionType))
                .ForMember(dest => dest.Employer, opt => opt.MapFrom(src => src.Employer))
                .ForMember(dest => dest.MissionActivities, opt => opt.MapFrom(src => src.MissionActivities))
                .ForMember(dest => dest.FileName, opt => opt.Ignore());

            CreateMap<MissionTypeDto, MissionType>();
            CreateMap<EmployerDto, Employer>();
            CreateMap<ActivityDto, Activity>();
            CreateMap<MissionActivityDto, MissionActivity>();
        }
    }
}
