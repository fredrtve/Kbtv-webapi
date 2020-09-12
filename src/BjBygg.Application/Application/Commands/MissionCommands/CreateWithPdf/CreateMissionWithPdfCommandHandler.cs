using AutoMapper;
using BjBygg.Application.Application.Common.Dto;
using BjBygg.Application.Application.Common.Interfaces;
using BjBygg.Application.Common.Exceptions;
using BjBygg.Application.Common.Interfaces;
using CleanArchitecture.Core;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.SharedKernel;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BjBygg.Application.Application.Commands.MissionCommands.CreateWithPdf
{
    public class CreateMissionWithPdfCommandHandler : IRequestHandler<CreateMissionWithPdfCommand>
    {
        private readonly IIdGenerator _idGenerator;
        private readonly IAppDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IBlobStorageService _storageService;
        private readonly IPdfReportMissionExtractor _pdfReportMissionExtractor;

        public CreateMissionWithPdfCommandHandler(
            IIdGenerator idGenerator,
            IAppDbContext dbContext,
            IMapper mapper,
            IBlobStorageService storageService,
            IPdfReportMissionExtractor pdfReportMissionExtractor)          
        {
            _idGenerator = idGenerator;
            _dbContext = dbContext;
            _mapper = mapper;
            _storageService = storageService;
            _pdfReportMissionExtractor = pdfReportMissionExtractor;
        }

        public async Task<Unit> Handle(CreateMissionWithPdfCommand request, CancellationToken cancellationToken)
        {
            if (request.Files == null || request.Files.Count == 0)
                throw new BadRequestException("No file found.");

            MissionPdfDto missionPdfDto = null;
            BasicFileStream extractedFile = null;

            //Loop over files and break if one fits extraction scheme
            //Helps when email clients append additional files that get extracted instead.
            foreach (var file in request.Files)
            {
                missionPdfDto = _pdfReportMissionExtractor.TryExtract(file.Stream);
                if (missionPdfDto != null)
                {
                    extractedFile = file; //Save extracted file to add to mission later
                    break;
                };
            }

            if (missionPdfDto == null)
                throw new BadRequestException("Mission could not be extracted from file.");

            var dbMission = new Mission
            {
                Id = _idGenerator.Generate(),
                Address = missionPdfDto.Address,
                PhoneNumber = missionPdfDto.PhoneNumber
            };

            if (missionPdfDto.Image != null)
                dbMission.FileUri = await _storageService.UploadFileAsync(
                    new BasicFileStream(missionPdfDto.Image, $"{dbMission.Id}.jpg"),
                    ResourceFolderConstants.Image);

            var documentType = await _dbContext.Set<DocumentType>().Where(x => x.Name == "Skaderapport").FirstOrDefaultAsync();

            var report = new MissionDocument
            {
                FileUri = await _storageService.UploadFileAsync(extractedFile, ResourceFolderConstants.Document),
                DocumentType = documentType
            };

            dbMission.MissionDocuments = new List<MissionDocument>();
            dbMission.MissionDocuments.Add(report); //Add input report as document in new mission

            await _dbContext.Set<Mission>().AddAsync(dbMission);

            await _dbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
