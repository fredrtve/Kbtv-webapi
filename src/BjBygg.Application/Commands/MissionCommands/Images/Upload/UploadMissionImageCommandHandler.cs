using AutoMapper;
using BjBygg.Application.Common;
using CleanArchitecture.Core;
using CleanArchitecture.Core.Entities;
using BjBygg.Application.Common.Exceptions;
using CleanArchitecture.Core.Interfaces.Services;
using CleanArchitecture.Infrastructure.Data;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BjBygg.Application.Commands.MissionCommands.Images.Upload
{
    public class UploadMissionImageCommandHandler : IRequestHandler<UploadMissionImageCommand, IEnumerable<MissionImageDto>>
    {
        private readonly AppDbContext _dbContext;
        private readonly IBlobStorageService _storageService;
        private readonly IMapper _mapper;

        public UploadMissionImageCommandHandler(AppDbContext dbContext, IBlobStorageService storageService, IMapper mapper)
        {
            _dbContext = dbContext;
            _storageService = storageService;
            _mapper = mapper;
        }

        public async Task<IEnumerable<MissionImageDto>> Handle(UploadMissionImageCommand request, CancellationToken cancellationToken)
        {
            List<MissionImage> images = new List<MissionImage>();

            var imageUrls = await _storageService.UploadFilesAsync(request.Files, ResourceFolderConstants.Image);

            images.AddRange(
                imageUrls.Select(url => new MissionImage() { MissionId = request.MissionId, FileURL = url }));

            _dbContext.Set<MissionImage>().AddRange(images);

            await _dbContext.SaveChangesAsync();
            
            return images.Select(x => _mapper.Map<MissionImageDto>(x));
        }
    }
}
