using AutoMapper;
using BjBygg.Application.Application.Common.Dto;
using BjBygg.Application.Application.Common.Interfaces;
using CleanArchitecture.Core.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BjBygg.Application.Application.Commands.TimesheetCommands.UpdateStatusRange
{
    public class UpdateTimesheetStatusRangeCommandHandler : IRequestHandler<UpdateTimesheetStatusRangeCommand, List<TimesheetDto>>
    {
        private readonly IAppDbContext _dbContext;
        private readonly IMapper _mapper;

        public UpdateTimesheetStatusRangeCommandHandler(IAppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<List<TimesheetDto>> Handle(UpdateTimesheetStatusRangeCommand request, CancellationToken cancellationToken)
        {
            //Exclude timesheets that dont belong to user (if there are any), except for leader role
            var dbTimesheets = await _dbContext.Set<Timesheet>()
                .Where(x => request.Ids.Contains(x.Id)).ToListAsync();

            //Update status to confirmed
            dbTimesheets = dbTimesheets.Select(x => { x.Status = request.Status; return x; }).ToList();

            //_dbContext.Timesheets.UpdateRange(timesheets);
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<List<TimesheetDto>>(dbTimesheets);
        }
    }
}
