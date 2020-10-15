using BjBygg.Application.Application.Common.Interfaces;
using BjBygg.Application.Common.BaseEntityCommands.MailEntitiesCommand;
using BjBygg.Application.Common.Interfaces;
using CleanArchitecture.Core;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.SharedKernel;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BjBygg.Application.Application.Commands.MissionCommands.Images.Mail
{
    public class MailMissionImagesCommandHandler : MailEntitiesCommandHandler<MailMissionImagesCommand>
    {
        private readonly IAppDbContext _dbContext;
        private readonly IMailService _mailService;
        private readonly IFileZipper _fileZipper;

        public MailMissionImagesCommandHandler(IAppDbContext dbContext, IMailService mailService, IFileZipper fileZipper)
        {
            _dbContext = dbContext;
            _mailService = mailService;
            _fileZipper = fileZipper;
        }

        public override async Task<Unit> Handle(MailMissionImagesCommand request, CancellationToken cancellationToken)
        {
            var images = await _dbContext.Set<MissionImage>()
                .Where(x => request.Ids.Contains(x.Id))
                .Include(x => x.Mission)
                .ToListAsync();

            var fileNames = images.Select(x => x.FileName).ToArray();

            using var zipStream = new MemoryStream();

            await _fileZipper.ZipAsync(zipStream, fileNames, ResourceFolderConstants.Image);

            var template = new MissionImagesTemplate(images, new BasicFileStream(zipStream, "bilder_fra_oppdrag.zip"));

            await _mailService.SendTemplateEmailAsync(request.ToEmail, template);

            return Unit.Value;
        }
    }
}
