using AutoMapper;
using BjBygg.Application.Application.Common.Dto;
using BjBygg.Core.Entities;

namespace BjBygg.Application.Application.Common.Profiles
{
    public class MissionTypeDtoProfile : Profile
    {
        public MissionTypeDtoProfile()
        {
            CreateMap<MissionType, MissionTypeDto>();
            CreateMap<MissionTypeDto, MissionType>();
        }
    }
}
