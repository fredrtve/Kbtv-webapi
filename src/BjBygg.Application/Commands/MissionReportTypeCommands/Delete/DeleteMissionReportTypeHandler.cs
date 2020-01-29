using AutoMapper;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Exceptions;
using CleanArchitecture.Infrastructure.Data;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BjBygg.Application.Commands.MissionReportTypeCommands.Delete
{
    public class DeleteMissionReportTypeHandler : IRequestHandler<DeleteMissionReportTypeCommand, bool>
    {
        private readonly AppDbContext _dbContext;

        public DeleteMissionReportTypeHandler(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> Handle(DeleteMissionReportTypeCommand request, CancellationToken cancellationToken)
        {
            var missionReportType = await _dbContext.Set<MissionReportType>().FindAsync(request.Id);
            if (missionReportType == null) throw new EntityNotFoundException($"Entity does not exist with id {request.Id}");

            _dbContext.Set<MissionReportType>().Remove(missionReportType);      
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
