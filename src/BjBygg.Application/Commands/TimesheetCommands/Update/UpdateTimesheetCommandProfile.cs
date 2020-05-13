using AutoMapper;
using BjBygg.Application.Shared;
using CleanArchitecture.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BjBygg.Application.Commands.TimesheetCommands.Update
{
    class UpdateTimesheetCommandProfile : Profile
    {
        public UpdateTimesheetCommandProfile()
        {
            CreateMap<UpdateTimesheetCommand, Timesheet>();
        }
    }
}
