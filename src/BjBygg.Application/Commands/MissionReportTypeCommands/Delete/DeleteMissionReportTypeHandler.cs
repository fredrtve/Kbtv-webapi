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

namespace BjBygg.Application.Commands.ReportTypeCommands.Delete
{
    public class DeleteReportTypeHandler : IRequestHandler<DeleteReportTypeCommand, bool>
    {
        private readonly AppDbContext _dbContext;

        public DeleteReportTypeHandler(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> Handle(DeleteReportTypeCommand request, CancellationToken cancellationToken)
        {
            var ReportType = await _dbContext.Set<ReportType>().FindAsync(request.Id);
            if (ReportType == null) throw new EntityNotFoundException($"Entity does not exist with id {request.Id}");

            _dbContext.Set<ReportType>().Remove(ReportType);      
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
