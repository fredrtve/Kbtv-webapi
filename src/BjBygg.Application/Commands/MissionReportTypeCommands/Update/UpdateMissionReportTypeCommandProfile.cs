using AutoMapper;
using CleanArchitecture.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BjBygg.Application.Commands.ReportTypeCommands.Update
{
    class UpdateReportTypeCommandProfile : Profile
    {
        public UpdateReportTypeCommandProfile()
        {
            CreateMap<UpdateReportTypeCommand, ReportType>();
        }
    }
}
