using AutoMapper;
using CleanArchitecture.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BjBygg.Application.Commands.MissionTypeCommands.Update
{
    class UpdateMissionTypeProfile : Profile
    {
        public UpdateMissionTypeProfile()
        {
            CreateMap<UpdateMissionTypeCommand, MissionType>();
        }
    }
}
