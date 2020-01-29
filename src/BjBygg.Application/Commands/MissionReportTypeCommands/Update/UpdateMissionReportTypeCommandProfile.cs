using AutoMapper;
using CleanArchitecture.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BjBygg.Application.Commands.MissionReportTypeCommands.Update
{
    class UpdateMissionReportTypeCommandProfile : Profile
    {
        public UpdateMissionReportTypeCommandProfile()
        {
            CreateMap<UpdateMissionReportTypeCommand, MissionReportType>();
        }
    }
}
