using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Exceptions;
using CleanArchitecture.Core.Interfaces;
using CleanArchitecture.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BjBygg.Application.Commands.MissionCommands.Reports.Mail
{
    public class MailMissionImagesHandler : IRequestHandler<MailMissionReportsCommand, Boolean>
    {
        private readonly AppDbContext _dbContext;
        private readonly IMailService _mailService;
        private readonly IConfiguration _configuration;

        public MailMissionImagesHandler(AppDbContext dbContext, IMailService mailService, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _mailService = mailService;
            _configuration = configuration;
        }

        public async Task<bool> Handle(MailMissionReportsCommand request, CancellationToken cancellationToken)
        {
            if(request.MissionReportIds.Count() == 0)
                throw new BadRequestException($"No reports selected");

            var reports = await _dbContext.Set<MissionReport>()
                .Include(x => x.ReportType)
                .Where(x => request.MissionReportIds.Contains(x.Id))
                .Select(x => new MissionReportsTemplateReportDto()
                {
                    Id = x.Id,
                    ReportTypeName = x.ReportType == null ? "Ukategorisert" : x.ReportType.Name,
                    Url = x.FileURL.ToString()
                }).ToListAsync();
       
            var templateData = new MissionReportsTemplateData() { Reports = reports };

            var templateId = _configuration.GetValue<string>("SendGridMissionReportsTemplateId");

            await _mailService.SendTemplateEmailAsync(request.ToEmail, templateId, templateData);
     
            return true;
        }
    }
}
