using AutoMapper;
using BjBygg.Application.Application.Common;
using BjBygg.Application.Application.Common.Interfaces;
using BjBygg.Application.Commands.MissionCommands.UpdateHeaderImage;
using BjBygg.Application.Common;
using BjBygg.Application.Common.Exceptions;
using BjBygg.Core;
using BjBygg.Core.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace BjBygg.Application.Application.Commands.MissionCommands.UpdateHeaderImage
{
    public class UpdateMissionHeaderImageCommandHandler : IRequestHandler<UpdateMissionHeaderImageCommand>
    {
        private readonly IAppDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IBlobStorageService _storageService;
        private readonly IImageResizer _imageResizer;
        private readonly ResourceFolders _resourceFolders;

        public UpdateMissionHeaderImageCommandHandler(
            IAppDbContext dbContext, 
            IMapper mapper, 
            IBlobStorageService storageService, 
            IImageResizer imageResizer,
            IOptions<ResourceFolders> resourceFolders)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _storageService = storageService;
            _imageResizer = imageResizer;
            _resourceFolders = resourceFolders.Value;
        }

        public async Task<Unit> Handle(UpdateMissionHeaderImageCommand request, CancellationToken cancellationToken)
        {
            var dbMission = await _dbContext.Missions.FirstOrDefaultAsync(x => x.Id == request.Id);

            if (dbMission == null)
                throw new EntityNotFoundException(nameof(Mission), request.Id);

            using (var resized = new MemoryStream())
            {
                _imageResizer.ResizeImage(request.Image, resized, request.FileExtension, 700);
                var fileName = new AppImageFileName(resized, request.FileExtension).ToString();
                dbMission.FileName = fileName;
                var fileURL = await _storageService.UploadFileAsync(resized, fileName, _resourceFolders.MissionHeader);

            }

            await _dbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
