using AutoMapper;
using CleanArchitecture.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BjBygg.Application.Commands.MissionCommands.Update
{
    class UpdateMissionCommandProfile : Profile
    {
        public UpdateMissionCommandProfile()
        {
            CreateMap<UpdateMissionCommand, Mission>();
        }
    }
}
