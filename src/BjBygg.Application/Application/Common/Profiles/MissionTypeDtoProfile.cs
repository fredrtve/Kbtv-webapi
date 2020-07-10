using AutoMapper;
using CleanArchitecture.Core.Entities;

namespace BjBygg.Application.Application.Common.Profiles
{
    public class MissionTypeDtoProfile : Profile
    {
        public MissionTypeDtoProfile()
        {
            CreateMap<MissionType, MissionTypeDtoProfile>();
            CreateMap<MissionTypeDtoProfile, MissionType>();
        }
    }
}
