using AutoMapper;
using BjBygg.Application.Application.Common;
using BjBygg.Application.Application.Common.Interfaces;
using BjBygg.Application.Common;
using BjBygg.Core;
using BjBygg.Core.Entities;
using MediatR;
using Microsoft.Extensions.Options;
using System;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;

namespace BjBygg.Application.Application.Commands.MissionCommands.Images.Upload
{
    public class UploadMissionImageCommandHandler : IRequestHandler<UploadMissionImageCommand>
    {
        private readonly IAppDbContext _dbContext;
        private readonly IBlobStorageService _storageService;
        private readonly IMapper _mapper;
        private readonly ResourceFolders _resourceFolders;

        public UploadMissionImageCommandHandler(
            IAppDbContext dbContext,
            IBlobStorageService storageService,
            IMapper mapper,
            IOptions<ResourceFolders> resourceFolders)
        {
            _dbContext = dbContext;
            _storageService = storageService;
            _mapper = mapper;
            _resourceFolders = resourceFolders.Value;
        }
       
        public async Task<Unit> Handle(UploadMissionImageCommand request, CancellationToken cancellationToken)
        {
            var image = _mapper.Map<MissionImage>(request);

            var fileName = new AppImageFileName(request.File, request.FileExtension).ToString();

            var fileURL = await _storageService.UploadFileAsync(request.File, fileName, _resourceFolders.OriginalMissionImage);

            if (fileURL == null) throw new Exception("Opplasting av bilde mislyktes");

            image.FileName = fileName;

            await _dbContext.Set<MissionImage>().AddAsync(image);

            await _dbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
