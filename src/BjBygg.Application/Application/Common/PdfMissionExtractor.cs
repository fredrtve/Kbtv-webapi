using BjBygg.Application.Application.Common.Dto;
using BjBygg.Application.Application.Common.Interfaces;
using BjBygg.Application.Common.Interfaces;
using BjBygg.Core;
using BjBygg.Core.Entities;
using BjBygg.SharedKernel;
using System;
using System.Collections.Generic;
using System.IO;
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

        public async Task<Mission> TryExtractAsync(Stream pdfStream, IPdfMissionExtractionStrategy strategy)
        {

            MissionPdfDto missionPdfDto = strategy.TryExtract(pdfStream);

            if (missionPdfDto == null) return null;

            var dbMission = new Mission
            {
                Id = _idGenerator.Generate(),
                Address = missionPdfDto.Address,
                PhoneNumber = missionPdfDto.PhoneNumber
            };

            if (missionPdfDto.Image != null)
            {
                dbMission.FileName = new AppImageFileName(missionPdfDto.Image, ".jpg").ToString();
                await _storageService.UploadFileAsync(missionPdfDto.Image, dbMission.FileName, ResourceFolderConstants.MissionHeader);
            }

            var report = new MissionDocument
            {
                Id = _idGenerator.Generate(),
                Name = missionPdfDto.DocumentName
            };

            report.FileName = Guid.NewGuid() + ".pdf"; 

            await _storageService.UploadFileAsync(pdfStream, report.FileName, ResourceFolderConstants.Document);

            dbMission.MissionDocuments = new List<MissionDocument>();
            dbMission.MissionDocuments.Add(report); //Add input report as document in new mission

            return dbMission;
        }
    }
}