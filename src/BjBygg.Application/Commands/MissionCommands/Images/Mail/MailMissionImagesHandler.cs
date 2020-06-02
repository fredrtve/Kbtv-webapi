using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Exceptions;
using CleanArchitecture.Core.Interfaces;
using CleanArchitecture.Core.Interfaces.Services;
using CleanArchitecture.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BjBygg.Application.Commands.MissionCommands.Images.Mail
{
    public class MailMissionImagesHandler : IRequestHandler<MailMissionImagesCommand, Boolean>
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

        public async Task<bool> Handle(MailMissionImagesCommand request, CancellationToken cancellationToken)
        {
            if(request.MissionImageIds.Count() == 0)
                throw new BadRequestException($"No images selected");

            var images = await _dbContext.Set<MissionImage>()
                .Include(x => x.Mission)
                .Where(x => request.MissionImageIds.Contains(x.Id))
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
     
            return true;
        }
    }
}
