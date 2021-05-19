using AutoMapper;
using BjBygg.Core.Entities;

namespace BjBygg.Application.Application.Commands.TimesheetCommands.CreateTimesheet
{
    public class CreateTimesheetCommandProfile : Profile
    {
        public CreateTimesheetCommandProfile()
        {
            CreateMap<CreateTimesheetCommand, Timesheet>();
        }
    }
}
