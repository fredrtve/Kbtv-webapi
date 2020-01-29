using AutoMapper;
using BjBygg.Application.Shared;
using CleanArchitecture.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BjBygg.Application.Shared
{
    class MissionImageDtoProfile : Profile
    {
        public MissionImageDtoProfile()
        {
            CreateMap<MissionImage, MissionImageDto>();
        }
    }
}
