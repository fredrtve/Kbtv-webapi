using AutoMapper;
using BjBygg.Application.Shared;
using CleanArchitecture.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BjBygg.Application.Commands.MissionCommands.Images.Upload
{
    class UploadMissionImageCommandProfile : Profile
    {
        public UploadMissionImageCommandProfile()
        {
            CreateMap<MissionImage, MissionImageDto>();
        }
    }
}
