using BjBygg.Application.Application.Common.Interfaces;
using BjBygg.Application.Common.BaseEntityCommands.MailEntitiesCommand;
using BjBygg.Application.Common.Exceptions;
using BjBygg.Application.Common.Interfaces;
using BjBygg.Core;
using BjBygg.Core.Entities;
using BjBygg.SharedKernel;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BjBygg.Application.Application.Commands.MissionCommands.Documents.Mail
{
    public class MailMissionDocumentsCommandHandler : MailEntitiesCommandHandler<MailMissionDocumentsCommand>
    {
        private readonly IAppDbContext _dbContext;
        private readonly IMailService _mailService;
        private readonly IFileZipper _fileZipper;

        public MailMissionDocumentsCommandHandler(IAppDbContext dbContext, IMailService mailService, IFileZipper fileZipper)
        {
            _dbContext = dbContext;
            _mailService = mailService;
            _fileZipper = fileZipper;
        }

        public override async Task<Unit> Handle(MailMissionDocumentsCommand request, CancellationToken cancellationToken)
        {
            var documents = await _dbContext.Set<MissionDocument>()
                .Where(x => request.Ids.Contains(x.Id)).Include(x => x.Mission).ToListAsync();

            var fileNames = documents.Select(x => x.FileName).ToArray();

            using var zipStream = new MemoryStream();

            try
            {
                await _fileZipper.ZipAsync(zipStream, fileNames, ResourceFolderConstants.Document);
            }
            catch (Exception ex)
            {
                throw new BadRequestException("Noe er feil med en av dokumentene");
            }
              
            var template = new MissionDocumentsTemplate(documents, new BasicFileStream(zipStream, "dokumenter_fra_oppdrag.zip"));

            await _mailService.SendTemplateEmailAsync(request.ToEmail, template);

            return Unit.Value;
        }
    }
}
