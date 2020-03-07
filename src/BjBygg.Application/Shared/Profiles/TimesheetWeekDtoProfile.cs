using AutoMapper;
using CleanArchitecture.Core.Entities;

namespace BjBygg.Application.Shared
{
    public class TimesheetWeekDtoProfile : Profile
    {
        public TimesheetWeekDtoProfile()
        {
            CreateMap<TimesheetWeek, TimesheetWeekDto>();
            CreateMap<TimesheetWeekDto, TimesheetWeek>();
        }
    }
}
