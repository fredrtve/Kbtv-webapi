using AutoMapper;
using BjBygg.Core.Entities;

namespace BjBygg.Application.Application.Commands.TimesheetCommands.UpdateTimesheet
{
    class UpdateTimesheetCommandProfile : Profile
    {
        public UpdateTimesheetCommandProfile()
        {
            CreateMap<UpdateTimesheetCommand, Timesheet>();
        }
    }
}
