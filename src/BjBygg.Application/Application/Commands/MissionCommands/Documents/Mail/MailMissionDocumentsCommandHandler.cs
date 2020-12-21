using BjBygg.Application.Application.Common.Interfaces;
using BjBygg.Application.Common.BaseEntityCommands.MailEntitiesCommand;
using BjBygg.Application.Common.Interfaces;
using CleanArchitecture.Core.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BjBygg.Application.Application.Commands.MissionCommands.Documents.Mail
{
    public class MailMissionDocumentsCommandHandler : MailEntitiesCommandHandler<MailMissionDocumentsCommand>
    {
        private readonly IAppDbContext _dbContext;
        private readonly IMailService _mailService;

        public MailMissionDocumentsCommandHandler(IAppDbContext dbContext, IMailService mailService)
        {
            _dbContext = dbContext;
            _mailService = mailService;
        }

        public override async Task<Unit> Handle(MailMissionDocumentsCommand request, CancellationToken cancellationToken)
        {
            var documents = await _dbContext.Set<MissionDocument>()
                .Include(x => x.DocumentType)
                .Where(x => request.Ids.Contains(x.Id)).ToListAsync();

            var template = new MissionDocumentsTemplate(documents);

            await _mailService.SendTemplateEmailAsync(request.ToEmail, template);

            return Unit.Value;
        }
    }
}
