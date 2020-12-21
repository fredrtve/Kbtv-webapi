using AutoMapper;
using BjBygg.Application.Application.Common.Interfaces;
using BjBygg.Application.Common.Exceptions;
using CleanArchitecture.Core.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BjBygg.Application.Application.Commands.TimesheetCommands.UpdateStatusRange
{
    public class UpdateTimesheetStatusRangeCommandHandler : IRequestHandler<UpdateTimesheetStatusRangeCommand>
    {
        private readonly IAppDbContext _dbContext;
        private readonly IMapper _mapper;

        public UpdateTimesheetStatusRangeCommandHandler(IAppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateTimesheetStatusRangeCommand request, CancellationToken cancellationToken)
        {
            //Exclude timesheets that dont belong to user (if there are any), except for leader role
            var dbTimesheets = await _dbContext.Set<Timesheet>()
                .Where(x => request.Ids.Contains(x.Id)).ToListAsync();

            if (dbTimesheets.Count() != request.Ids.Count())
                throw new EntityNotFoundException(nameof(Timesheet), String.Join(", ", request.Ids.ToArray()));

            //Update status to confirmed
            dbTimesheets = dbTimesheets.Select(x => { x.Status = request.Status; return x; }).ToList();

            //_dbContext.Timesheets.UpdateRange(timesheets);
            await _dbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
