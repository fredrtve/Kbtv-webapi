using AutoMapper;
using BjBygg.Application.Common;
using CleanArchitecture.Core;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Interfaces.Services;
using CleanArchitecture.Infrastructure.Data;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BjBygg.Application.Commands.MissionCommands.Documents.Upload
{
    public class UploadMissionDocumentCommandHandler : IRequestHandler<UploadMissionDocumentCommand, MissionDocumentDto>
    {
        private readonly AppDbContext _dbContext;
        private readonly IBlobStorageService _storageService;
        private readonly IMapper _mapper;

        public UploadMissionDocumentCommandHandler(AppDbContext dbContext, IBlobStorageService storageService, IMapper mapper)
        {
            _dbContext = dbContext;
            _storageService = storageService;
            _mapper = mapper;
        }

        public async Task<MissionDocumentDto> Handle(UploadMissionDocumentCommand request, CancellationToken cancellationToken)
        {
            var document = _mapper.Map<MissionDocument>(request);

            document.FileURL = await _storageService.UploadFileAsync(request.File, ResourceFolderConstants.Document);

            if (document.DocumentType.Id != 0)
            {
                document.DocumentTypeId = document.DocumentType.Id;
                document.DocumentType = null;
            }

            await _dbContext.Set<MissionDocument>().AddAsync(document);

            await _dbContext.SaveChangesAsync();

            return _mapper.Map<MissionDocumentDto>(document);
        }
    }
}
