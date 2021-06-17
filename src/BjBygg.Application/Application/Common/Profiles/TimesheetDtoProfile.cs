using AutoMapper;
using BjBygg.Application.Application.Common.Dto;
using BjBygg.Core.Entities;

namespace BjBygg.Application.Application.Common.Profiles
{
    public class TimesheetDtoProfile : Profile
    {
        public TimesheetDtoProfile()
        {
            CreateMap<Timesheet, TimesheetDto>();

            CreateMap<TimesheetDto, Timesheet>(); 
            
            CreateMap<Timesheet, UserTimesheetDto>();

            CreateMap<UserTimesheetDto, Timesheet>();
        }
    }
}
