using AutoMapper;
using CleanArchitecture.Core.Entities;

namespace BjBygg.Application.Application.Commands.TimesheetCommands.Update
{
    class UpdateTimesheetCommandProfile : Profile
    {
        public UpdateTimesheetCommandProfile()
        {
            CreateMap<UpdateTimesheetCommand, Timesheet>();
        }
    }
}
