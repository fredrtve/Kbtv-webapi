using BjBygg.Application.Application.Common.Dto;
using BjBygg.Application.Application.Common.Interfaces;
using BjBygg.Application.Common.Interfaces;
using BjBygg.Core;
using BjBygg.Core.Entities;
using BjBygg.SharedKernel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BjBygg.Application.Application.Common
{
    public class PdfMissionExtractor
    {
        private readonly IIdGenerator _idGenerator;
        private readonly IBlobStorageService _storageService;

        public PdfMissionExtractor(
            IIdGenerator idGenerator,
            IBlobStorageService storageService)
        {
            _idGenerator = idGenerator;
            _storageService = storageService;
        }

        public async Task<Mission> TryExtractAsync(BasicFileStream pdf, IPdfMissionExtractionStrategy strategy)
        {
            
            MissionPdfDto missionPdfDto = strategy.TryExtract(pdf.Stream);
            BasicFileStream extractedDocument = pdf;

            if (missionPdfDto == null) return null;

            var dbMission = new Mission
            {
                Id = _idGenerator.Generate(),
                Address = missionPdfDto.Address,
                PhoneNumber = missionPdfDto.PhoneNumber
            };

            if (missionPdfDto.Image != null)
            {
                var fileStream = new BasicFileStream(missionPdfDto.Image, $"{dbMission.Id}.jpg");
                await _storageService.UploadFileAsync(fileStream, ResourceFolderConstants.MissionHeader);
                dbMission.FileName = fileStream.FileName;
            }

            var report = new MissionDocument
            {
                Id = _idGenerator.Generate(),
                Name = missionPdfDto.DocumentName
            };

            report.FileName = $"{report.Id}{extractedDocument.FileExtension}";

            var modifiedFile = new BasicFileStream(extractedDocument.Stream, report.FileName);
            await _storageService.UploadFileAsync(modifiedFile, ResourceFolderConstants.Document);

            dbMission.MissionDocuments = new List<MissionDocument>();
            dbMission.MissionDocuments.Add(report); //Add input report as document in new mission

            return dbMission;
        }
    }
}