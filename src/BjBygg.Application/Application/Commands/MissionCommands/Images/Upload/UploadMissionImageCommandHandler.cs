using AutoMapper;
using BjBygg.Application.Application.Common.Dto;
using BjBygg.Application.Application.Common.Interfaces;
using CleanArchitecture.Core;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.SharedKernel;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BjBygg.Application.Application.Commands.MissionCommands.Images.Upload
{
    public class UploadMissionImageCommandHandler : IRequestHandler<UploadMissionImageCommand>
    {
        private readonly IAppDbContext _dbContext;
        private readonly IBlobStorageService _storageService;

        public UploadMissionImageCommandHandler(
            IAppDbContext dbContext, 
            IBlobStorageService storageService)
        {
            _dbContext = dbContext;
            _storageService = storageService;
        }

        public async Task<Unit> Handle(UploadMissionImageCommand request, CancellationToken cancellationToken)
        {
            var imageUrls = await _storageService.UploadFilesAsync(request.Files, ResourceFolderConstants.Image);
            if (imageUrls == null || imageUrls.Count() != request.Files.Count())
                throw new Exception("Something went wrong trying to upload images");

            List<MissionImage> images = new List<MissionImage>();

            images.AddRange(
                request.Files.Select(file => new MissionImage() { 
                    Id = file.FileNameNoExtension, 
                    MissionId = request.MissionId, 
                    FileName = file.FileName,
                })
            );

            _dbContext.Set<MissionImage>().AddRange(images);

            await _dbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
