using AutoMapper;
using CleanArchitecture.Core.Entities;

namespace BjBygg.Application.Shared
{
    public class MissionReportTypeDtoProfile : Profile
    {
        public MissionReportTypeDtoProfile()
        {
            CreateMap<MissionReportType, MissionReportTypeDtoProfile>();
            CreateMap<MissionReportTypeDtoProfile, MissionReportType>();
        }
    }
}
