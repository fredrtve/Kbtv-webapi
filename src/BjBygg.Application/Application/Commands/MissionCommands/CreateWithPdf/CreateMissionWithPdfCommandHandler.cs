using BjBygg.Application.Application.Common;
using BjBygg.Application.Application.Common.Dto;
using BjBygg.Application.Application.Common.Interfaces;
using BjBygg.Application.Common.Exceptions;
using BjBygg.Application.Common.Interfaces;
using BjBygg.Core;
using BjBygg.Core.Entities;
using BjBygg.Infrastructure.Services;
using BjBygg.SharedKernel;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BjBygg.Application.Application.Commands.MissionCommands.CreateWithPdf
{
    public class CreateMissionWithPdfCommandHandler : IRequestHandler<CreateMissionWithPdfCommand>
    {
        private readonly IIdGenerator _idGenerator;
        private readonly IAppDbContext _dbContext;
        private readonly IBlobStorageService _storageService;
        private readonly PdfMissionExtractor _pdfMissionExtractor;

        public CreateMissionWithPdfCommandHandler(
            IIdGenerator idGenerator,
            IAppDbContext dbContext,
            IBlobStorageService storageService,
            PdfMissionExtractor pdfMissionExtractor)
        {
            _idGenerator = idGenerator;
            _dbContext = dbContext;
            _storageService = storageService;
            _pdfMissionExtractor = pdfMissionExtractor;
        }

        public async Task<Unit> Handle(CreateMissionWithPdfCommand request, CancellationToken cancellationToken)
        {
            if (request.Files == null || request.Files.Count == 0)
                throw new BadRequestException("No file found.");

            Mission extractedMission = null;

            //Loop over files and break if one fits extraction scheme
            //Helps when email clients append additional files that get extracted instead.
            foreach (var file in request.Files)
            {
                extractedMission = await _pdfMissionExtractor.TryExtractAsync(file, new PdfMissionExtractionOneStrategy());
                if (extractedMission != null) break;
            }

            if (extractedMission == null)
                throw new BadRequestException("Mission could not be extracted from file.");

            await _dbContext.Set<Mission>().AddAsync(extractedMission);

            await _dbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
