using BjBygg.Application.Application.Common.Interfaces;
using BjBygg.Application.Common.BaseEntityCommands.MailEntitiesCommand;
using BjBygg.Application.Common.Interfaces;
using CleanArchitecture.Core.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BjBygg.Application.Application.Commands.MissionCommands.Images.Mail
{
    public class MailMissionImagesCommandHandler : MailEntitiesCommandHandler<MailMissionImagesCommand>
    {
        private readonly IAppDbContext _dbContext;
        private readonly IMailService _mailService;

        public MailMissionImagesCommandHandler(IAppDbContext dbContext, IMailService mailService)
        {
            _dbContext = dbContext;
            _mailService = mailService;
        }

        public override async Task<Unit> Handle(MailMissionImagesCommand request, CancellationToken cancellationToken)
        {
            var images = await _dbContext.Set<MissionImage>()
                .Where(x => request.Ids.Contains(x.Id))
                .Include(x => x.Mission)
                .ToListAsync();

            await _mailService.SendTemplateEmailAsync(request.ToEmail, new MissionImagesTemplate(images));

            return Unit.Value;
        }
    }
}
