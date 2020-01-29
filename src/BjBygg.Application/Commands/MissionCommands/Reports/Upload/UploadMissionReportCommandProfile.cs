using AutoMapper;
using BjBygg.Application.Shared;
using CleanArchitecture.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BjBygg.Application.Commands.MissionCommands.Reports.Upload
{
    class UploadMissionReportCommandProfile : Profile
    {
        public UploadMissionReportCommandProfile()
        {
            CreateMap<UploadMissionReportCommand, MissionReport>()
                .ForMember(dest => dest.MissionReportType, opt => opt.MapFrom(src => src.MissionReportType))
                .ForMember(dest => dest.FileURL, opt => opt.Ignore())
                .ForSourceMember(src => src.File, dest => dest.DoNotValidate());

            CreateMap<MissionReport, MissionReportDto>()
                .ForMember(dest => dest.MissionReportType, opt => opt.MapFrom(src => src.MissionReportType));

            CreateMap<MissionReportTypeDto, MissionReportType>();
            CreateMap<MissionReportType, MissionReportTypeDto>();
        }
    }
}
