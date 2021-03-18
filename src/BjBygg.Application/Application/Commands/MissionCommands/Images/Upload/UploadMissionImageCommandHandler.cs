using AutoMapper;
using BjBygg.Application.Application.Common.Interfaces;
using BjBygg.Core;
using BjBygg.Core.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BjBygg.Application.Application.Commands.MissionCommands.Images.Upload
{
    public class UploadMissionImageCommandHandler : IRequestHandler<UploadMissionImageCommand>
    {
        private readonly IAppDbContext _dbContext;
        private readonly IBlobStorageService _storageService;
        private readonly IMapper _mapper;

        public UploadMissionImageCommandHandler(
            IAppDbContext dbContext,
            IBlobStorageService storageService,
            IMapper mapper)
        {
            _dbContext = dbContext;
            _storageService = storageService;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UploadMissionImageCommand request, CancellationToken cancellationToken)
        {
            var image = _mapper.Map<MissionImage>(request);

            var fileURL = await _storageService.UploadFileAsync(request.File, ResourceFolderConstants.OriginalMissionImage);

            if (fileURL == null) throw new Exception("Opplasting av bilde mislyktes");

            image.FileName = request.File.FileName;

            await _dbContext.Set<MissionImage>().AddAsync(image);

            await _dbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
