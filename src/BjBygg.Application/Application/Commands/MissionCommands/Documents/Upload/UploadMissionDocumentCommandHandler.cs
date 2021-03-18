using AutoMapper;
using BjBygg.Application.Application.Common.Interfaces;
using BjBygg.Application.Common.Exceptions;
using BjBygg.Core;
using BjBygg.Core.Entities;
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

            if (fileURL == null) throw new Exception("Opplasting av fil mislyktes");

            document.FileName = request.File.FileName;

            await _dbContext.Set<MissionDocument>().AddAsync(document);

            await _dbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
