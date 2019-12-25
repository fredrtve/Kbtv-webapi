using AutoMapper;
using BjBygg.Application.Shared;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Interfaces;
using CleanArchitecture.Infrastructure.Data;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BjBygg.Application.Commands.MissionCommands.Images.Upload
{
    public class UploadMissionImageHandler : IRequestHandler<UploadMissionImageCommand, IEnumerable<MissionImageDto>>
    {
        private readonly IMissionImageUploader _imageUploader;
        private readonly IMapper _mapper;

        public UploadMissionImageHandler(IMissionImageUploader imageUploader, IMapper mapper)
        {
            _imageUploader = imageUploader;
            _mapper = mapper;
        }

        public async Task<IEnumerable<MissionImageDto>> Handle(UploadMissionImageCommand request, CancellationToken cancellationToken)
        {
            IEnumerable<MissionImage> images = null;

            if(request.Files != null && request.Files.Count != 0)
                images = await _imageUploader.UploadCollection(request.Files, request.MissionId);

            return images.Select(x => _mapper.Map<MissionImageDto>(x));
        }
    }
}
