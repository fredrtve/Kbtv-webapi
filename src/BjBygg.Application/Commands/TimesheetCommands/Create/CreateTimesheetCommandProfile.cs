using AutoMapper;
using CleanArchitecture.Core.Entities;

namespace BjBygg.Application.Commands.TimesheetCommands.Create
{
    public class CreateTimesheetCommandProfile : Profile
    {
        public CreateTimesheetCommandProfile()
        {
            CreateMap<CreateTimesheetCommand, Timesheet>();
        }
    }
}
