using AutoMapper;
using CleanArchitecture.Core.Entities;

namespace BjBygg.Application.Shared
{
    public class TimesheetDtoProfile : Profile
    {
        public TimesheetDtoProfile()
        {
            CreateMap<Timesheet, TimesheetDto>()
                .ForMember(dest => dest.TimesheetWeek, opt => opt.MapFrom(src => src.TimesheetWeek));

            CreateMap<TimesheetDto, Timesheet>();

            CreateMap<TimesheetWeek, TimesheetWeekDto>();
        }
    }
}
