using AutoMapper;
using BjBygg.Application.Application.Common.Dto;
using CleanArchitecture.Core.Entities;

namespace BjBygg.Application.Application.Commands.MissionCommands.Images.Upload
{
    class UploadMissionImageProfile : Profile
    {
        public UploadMissionImageProfile()
        {
            CreateMap<UploadMissionImageCommand, MissionImage>()
                .ForMember(dest => dest.FileName, opt => opt.Ignore())
                .ForSourceMember(src => src.File, dest => dest.DoNotValidate());

            CreateMap<MissionImage, MissionImageDto>();
        }
    }
}
