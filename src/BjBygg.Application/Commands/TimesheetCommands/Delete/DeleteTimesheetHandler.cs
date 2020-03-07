using AutoMapper;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Enums;
using CleanArchitecture.Core.Exceptions;
using CleanArchitecture.Infrastructure.Data;
using CleanArchitecture.SharedKernel;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BjBygg.Application.Commands.TimesheetCommands.Delete
{
    public class DeleteTimesheetHandler : IRequestHandler<DeleteTimesheetCommand, bool>
    {
        private readonly AppDbContext _dbContext;

        public DeleteTimesheetHandler(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> Handle(DeleteTimesheetCommand request, CancellationToken cancellationToken)
        {
            var timesheet = await _dbContext.Set<Timesheet>().FindAsync(request.Id);

            if (timesheet == null) 
                throw new EntityNotFoundException($"Timesheet does not exist with id {request.Id}");

            if (request.UserName != timesheet.UserName && request.Role != "Leder") //Allow leader
                throw new UnauthorizedException("Timesheet does not belong to user");

            if (timesheet.Status != TimesheetStatus.Open && request.Role != "Leder") //Allow leader
                throw new BadRequestException("Timesheet is not open & can't be deleted.");

            _dbContext.Set<Timesheet>().Remove(timesheet);      
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
