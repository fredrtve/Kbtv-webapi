using AutoMapper;
using BjBygg.Core.Entities;

namespace BjBygg.Application.Application.Queries.TimesheetQueries
{
    public class TimesheetDtoProfile : Profile
    {
        public TimesheetDtoProfile()
        {
            CreateMap<Timesheet, TimesheetQueryDto>();
            CreateMap<TimesheetQueryDto, TimesheetDto>();
            CreateMap<MissionActivity, MissionActivityDto>();
        }
    }
}
