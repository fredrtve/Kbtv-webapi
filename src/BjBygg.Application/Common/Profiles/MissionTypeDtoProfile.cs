using AutoMapper;
using CleanArchitecture.Core.Entities;

namespace BjBygg.Application.Common
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
