using AutoMapper;
using CleanArchitecture.Core.Entities;

namespace BjBygg.Application.Common
{
    public class TimesheetDtoProfile : Profile
    {
        public TimesheetDtoProfile()
        {
            CreateMap<Timesheet, TimesheetDto>();

            CreateMap<TimesheetDto, Timesheet>();
        }
    }
}
