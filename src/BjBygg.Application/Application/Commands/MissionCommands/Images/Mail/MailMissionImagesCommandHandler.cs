using BjBygg.Application.Application.Common.Interfaces;
using BjBygg.Application.Common.Interfaces;
using CleanArchitecture.Core.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BjBygg.Application.Application.Commands.MissionCommands.Images.Mail
{
    public class MailMissionImagesCommandHandler : IRequestHandler<MailMissionImagesCommand>
    {
        private readonly IAppDbContext _dbContext;
        private readonly IMailService _mailService;
        private readonly IConfiguration _configuration;

        public MailMissionImagesCommandHandler(IAppDbContext dbContext, IMailService mailService, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _mailService = mailService;
            _configuration = configuration;
        }

        public async Task<Unit> Handle(MailMissionImagesCommand request, CancellationToken cancellationToken)
        {

            var images = await _dbContext.Set<MissionImage>()
                .Include(x => x.Mission)
                .Where(x => request.Ids.Contains(x.Id))
                .ToListAsync();

            var missionTemplates = images.GroupBy(x => x.Mission).Select(x => new MissionImagesTemplateMissionDto
            {
                Id = x.Key.Id,
                Address = x.Key.Address,
                Images = x.Select(x => x.FileURL.ToString()).ToList(),
            }).ToList();

            var templateData = new MissionImagesTemplateData() { Missions = missionTemplates };

            var templateId = _configuration.GetValue<string>("SendGridMissionImagesTemplateId");

            await _mailService.SendTemplateEmailAsync(request.ToEmail, templateId, templateData);

            return Unit.Value;
        }
    }
}
