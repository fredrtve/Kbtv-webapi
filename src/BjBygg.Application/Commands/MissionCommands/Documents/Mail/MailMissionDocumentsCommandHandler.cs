using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Interfaces.Services;
using CleanArchitecture.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BjBygg.Application.Commands.MissionCommands.Documents.Mail
{
    public class MailMissionDocumentsCommandHandler : IRequestHandler<MailMissionDocumentsCommand>
    {
        private readonly AppDbContext _dbContext;
        private readonly IMailService _mailService;
        private readonly IConfiguration _configuration;

        public MailMissionDocumentsCommandHandler(AppDbContext dbContext, IMailService mailService, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _mailService = mailService;
            _configuration = configuration;
        }

        public async Task<Unit> Handle(MailMissionDocumentsCommand request, CancellationToken cancellationToken)
        {
            var documents = await _dbContext.Set<MissionDocument>()
                .Include(x => x.DocumentType)
                .Where(x => request.Ids.Contains(x.Id))
                .Select(x => new MissionDocumentsTemplateDocumentDto()
                {
                    Id = x.Id,
                    DocumentTypeName = x.DocumentType == null ? "Ukategorisert" : x.DocumentType.Name,
                    Url = x.FileURL.ToString()
                }).ToListAsync();

            var templateData = new MissionDocumentsTemplateData() { Documents = documents };

            var templateId = _configuration.GetValue<string>("SendGridMissionDocumentsTemplateId");

            await _mailService.SendTemplateEmailAsync(request.ToEmail, templateId, templateData);

            return Unit.Value;
        }
    }
}
