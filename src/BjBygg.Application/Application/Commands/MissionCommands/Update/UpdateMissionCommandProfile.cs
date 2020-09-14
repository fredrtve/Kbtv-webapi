using AutoMapper;
using BjBygg.Application.Application.Common.Dto;
using CleanArchitecture.Core.Entities;

namespace BjBygg.Application.Application.Commands.MissionCommands.Update
{
    class UpdateMissionCommandProfile : Profile
    {
        public UpdateMissionCommandProfile()
        {
            CreateMap<UpdateMissionCommand, Mission>()
                .ForMember(dest => dest.MissionType, opt => opt.MapFrom(src => src.MissionType))
                .ForMember(dest => dest.Employer, opt => opt.MapFrom(src => src.Employer))
                .ForMember(dest => dest.FileName, opt => opt.Ignore());

            CreateMap<MissionTypeDto, MissionType>();
            CreateMap<EmployerDto, Employer>();
        }
    }
}
