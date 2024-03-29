﻿using AutoMapper;
using BjBygg.Application.Application.Common.Dto;
using BjBygg.Core.Entities;

namespace BjBygg.Application.Application.Commands.MissionCommands.Documents.Upload
{
    class UploadMissionDocumentProfile : Profile
    {
        public UploadMissionDocumentProfile()
        {
            CreateMap<UploadMissionDocumentCommand, MissionDocument>()
                .ForMember(dest => dest.FileName, opt => opt.Ignore())
                .ForSourceMember(src => src.File, dest => dest.DoNotValidate());
        }
    }
}
