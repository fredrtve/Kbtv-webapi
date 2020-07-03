using AutoMapper;
using CleanArchitecture.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BjBygg.Application.Commands.TimesheetCommands.Create
{
    public class CreateTimesheetProfile : Profile
    {
        public CreateTimesheetProfile()
        {
            CreateMap<CreateTimesheetCommand, Timesheet>();
        }
    }
}
