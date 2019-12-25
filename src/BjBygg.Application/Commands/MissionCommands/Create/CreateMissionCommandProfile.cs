using AutoMapper;
using CleanArchitecture.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BjBygg.Application.Commands.MissionCommands.Create
{
    class CreateMissionCommandProfile : Profile
    {
        public CreateMissionCommandProfile()
        {
            CreateMap<CreateMissionCommand, Mission>()
                .ForMember(dest => dest.EmployerId, opt => opt.Condition(src => (src.EmployerId > 0)))
                .ForMember(dest => dest.MissionTypeId, opt => opt.Condition(src => (src.MissionTypeId > 0)));
        }
    }
}
