using AutoMapper;
using BjBygg.Application.Shared;
using CleanArchitecture.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BjBygg.Application.Commands.MissionCommands.Update
{
    class UpdateMissionProfile : Profile
    {
        public UpdateMissionProfile()
        {
            CreateMap<UpdateMissionCommand, Mission>()
                .ForMember(dest => dest.MissionType, opt => opt.MapFrom(src => src.MissionType))
                .ForMember(dest => dest.Employer, opt => opt.MapFrom(src => src.Employer))
                .ForMember(dest => dest.ImageURL, opt => opt.Ignore())
                .ForSourceMember(src => src.Image, dest => dest.DoNotValidate());

            CreateMap<MissionTypeDto, MissionType>();
            CreateMap<EmployerDto, Employer>();
        }
    }
}
