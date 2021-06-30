using AutoMapper;
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

namespace BjBygg.Application.Application.Commands.MissionCommands.Documents.Upload
{
    public class UploadMissionDocumentCommandHandler : IRequestHandler<UploadMissionDocumentCommand>
    {
        private readonly IAppDbContext _dbContext;
        private readonly IBlobStorageService _storageService;
        private readonly IMapper _mapper;
        private readonly ResourceFolders _resourceFolders;

        public UploadMissionDocumentCommandHandler(IAppDbContext dbContext, IBlobStorageService storageService, IMapper mapper, IOptions<ResourceFolders> resourceFolders)
        {
            _dbContext = dbContext;
            _storageService = storageService;
            _mapper = mapper;
            _resourceFolders = resourceFolders.Value;
        }

        public async Task<Unit> Handle(UploadMissionDocumentCommand request, CancellationToken cancellationToken)
        {
            var document = _mapper.Map<MissionDocument>(request);

            var fileName = Guid.NewGuid() + request.FileExtension;

            var fileURL = await _storageService.UploadFileAsync(request.File, fileName, _resourceFolders.Document);

            if (fileURL == null) throw new Exception("Opplasting av fil mislyktes");

            document.FileName = fileName;

            await _dbContext.Set<MissionDocument>().AddAsync(document);

            await _dbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
