using AutoMapper;
using BjBygg.Application.Application.Common.Dto;
using BjBygg.Application.Application.Common.Interfaces;
using CleanArchitecture.Core;
using CleanArchitecture.Core.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BjBygg.Application.Application.Commands.MissionCommands.Documents.Upload
{
    public class UploadMissionDocumentCommandHandler : IRequestHandler<UploadMissionDocumentCommand>
    {
        private readonly IAppDbContext _dbContext;
        private readonly IBlobStorageService _storageService;
        private readonly IMapper _mapper;

        public UploadMissionDocumentCommandHandler(IAppDbContext dbContext, IBlobStorageService storageService, IMapper mapper)
        {
            _dbContext = dbContext;
            _storageService = storageService;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UploadMissionDocumentCommand request, CancellationToken cancellationToken)
        {
            var document = _mapper.Map<MissionDocument>(request);

            var fileURL = await _storageService.UploadFileAsync(request.File, ResourceFolderConstants.Document);

            if (fileURL == null) throw Exception("Failed to upload file");

            document.FileName = request.File.FileName;

            await _dbContext.Set<MissionDocument>().AddAsync(document);

            await _dbContext.SaveChangesAsync();

            return Unit.Value;
        }

        private Exception Exception(string v)
        {
            throw new NotImplementedException();
        }
    }
}
