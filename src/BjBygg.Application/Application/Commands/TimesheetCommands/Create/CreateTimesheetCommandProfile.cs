using AutoMapper;
using CleanArchitecture.Core.Entities;

namespace BjBygg.Application.Application.Commands.TimesheetCommands.Create
{
    public class CreateTimesheetCommandProfile : Profile
    {
        public CreateTimesheetCommandProfile()
        {
            CreateMap<CreateTimesheetCommand, Timesheet>()
                .ForMember(x => x.StartTime, opt => opt.Ignore())
                .ForMember(x => x.EndTime, opt => opt.Ignore());
        }
    }
}
