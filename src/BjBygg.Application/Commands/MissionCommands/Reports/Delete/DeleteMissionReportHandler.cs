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

namespace BjBygg.Application.Commands.MissionCommands.Images.Delete
{
    public class DeleteMissionReportHandler : IRequestHandler<DeleteMissionReportCommand, bool>
    {
        private readonly AppDbContext _dbContext;
        private readonly IBlobStorageService _storageService;

        public DeleteMissionReportHandler(AppDbContext dbContext, IBlobStorageService storageService)
        {
            _dbContext = dbContext;
            _storageService = storageService;
        }

        public async Task<bool> Handle(DeleteMissionReportCommand request, CancellationToken cancellationToken)
        {
            var missionReport =  await _dbContext.Set<MissionReport>().FindAsync(request.Id);

            if (missionReport == null) throw new EntityNotFoundException($"Entity does not exist with id {request.Id}"); ;

            _dbContext.Set<MissionReport>().Remove(missionReport);
            await _dbContext.SaveChangesAsync();

           // await _storageService.DeleteAsync(missionReport.FileURL.ToString(), "report");

            return true;
        }
    }
}
