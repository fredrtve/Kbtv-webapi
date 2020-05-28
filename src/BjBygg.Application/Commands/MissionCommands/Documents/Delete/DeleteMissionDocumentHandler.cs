using AutoMapper;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Exceptions;
using CleanArchitecture.Core.Interfaces;
using CleanArchitecture.Infrastructure.Data;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BjBygg.Application.Commands.MissionCommands.Documents.Delete
{
    public class DeleteMissionDocumentHandler : IRequestHandler<DeleteMissionDocumentCommand, bool>
    {
        private readonly AppDbContext _dbContext;
        private readonly IBlobStorageService _storageService;

        public DeleteMissionDocumentHandler(AppDbContext dbContext, IBlobStorageService storageService)
        {
            _dbContext = dbContext;
            _storageService = storageService;
        }

        public async Task<bool> Handle(DeleteMissionDocumentCommand request, CancellationToken cancellationToken)
        {
            var missionDocument =  await _dbContext.Set<MissionDocument>().FindAsync(request.Id);

            if (missionDocument == null) throw new EntityNotFoundException($"Entity does not exist with id {request.Id}"); ;

            _dbContext.Set<MissionDocument>().Remove(missionDocument);
            await _dbContext.SaveChangesAsync();

            // await _storageService.DeleteAsync(missionDocument.FileURL.ToString(), FileType.Document);

            return true;
        }
    }
}
