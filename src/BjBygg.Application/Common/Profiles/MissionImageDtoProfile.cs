using AutoMapper;
using BjBygg.Application.Common;
using CleanArchitecture.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BjBygg.Application.Common
{
    class MissionImageDtoProfile : Profile
    {
        public MissionImageDtoProfile()
        {
            CreateMap<MissionImage, MissionImageDto>();
        }
    }
}
