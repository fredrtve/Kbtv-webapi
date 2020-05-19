using AutoMapper;
using CleanArchitecture.Core.Entities;

namespace BjBygg.Application.Shared
{
    public class ReportTypeDtoProfile : Profile
    {
        public ReportTypeDtoProfile()
        {
            CreateMap<ReportType, ReportTypeDtoProfile>();
            CreateMap<ReportTypeDtoProfile, ReportType>();
        }
    }
}
