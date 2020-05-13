using AutoMapper;
using BjBygg.Application.Shared;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Exceptions;
using CleanArchitecture.Core.Interfaces;
using CleanArchitecture.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BjBygg.Application.Commands.MissionCommands.Reports.Upload
{
    public class UploadMissionReportHandler : IRequestHandler<UploadMissionReportCommand, MissionReportDto>
    {
        private readonly AppDbContext _dbContext;
        private readonly IBlobStorageService _storageService;
        private readonly IMapper _mapper;

        public UploadMissionReportHandler(AppDbContext dbContext, IBlobStorageService storageService, IMapper mapper)
        {
            _dbContext = dbContext;
            _storageService = storageService;
            _mapper = mapper;
        }

        public async Task<MissionReportDto> Handle(UploadMissionReportCommand request, CancellationToken cancellationToken)
        {
            var report = _mapper.Map<MissionReport>(request);

            report.FileURL = await _storageService.UploadFileAsync(request.File, "report");

            if (report.MissionReportType.Id != 0)
            {
                report.MissionReportTypeId = report.MissionReportType.Id;
                report.MissionReportType = null;
            }
            
            try
            {
                await _dbContext.Set<MissionReport>().AddAsync(report);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new EntityNotFoundException($"Invalid foreign key");
            }

            return _mapper.Map<MissionReportDto>(report);
        }
    }
}
