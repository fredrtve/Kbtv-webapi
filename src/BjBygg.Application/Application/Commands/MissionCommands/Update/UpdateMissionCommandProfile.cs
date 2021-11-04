using AutoMapper;
using BjBygg.Application.Application.Common.Dto;
using BjBygg.Core.Entities;

namespace BjBygg.Application.Application.Commands.MissionCommands.Update
{
    public class UpdateMissionCommandProfile : Profile
    {
        public UpdateMissionCommandProfile()
        {
            CreateMap<UpdateMissionCommand, Mission>()
                .ForMember(dest => dest.MissionType, opt => opt.MapFrom(src => src.MissionType))
                .ForMember(dest => dest.Employer, opt => opt.MapFrom(src => src.Employer));

            CreateMap<MissionTypeDto, MissionType>();
            CreateMap<EmployerDto, Employer>(); 
            CreateMap<ActivityDto, Activity>();
            CreateMap<MissionActivityDto, MissionActivity>();
        }
    }
}
