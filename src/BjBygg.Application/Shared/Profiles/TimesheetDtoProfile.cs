using AutoMapper;
using CleanArchitecture.Core.Entities;

namespace BjBygg.Application.Shared
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
