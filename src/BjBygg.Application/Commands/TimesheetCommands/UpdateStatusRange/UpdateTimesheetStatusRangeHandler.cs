using AutoMapper;
using BjBygg.Application.Common;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Enums;
using CleanArchitecture.Core.Exceptions;
using CleanArchitecture.Infrastructure.Data;
using CleanArchitecture.SharedKernel;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BjBygg.Application.Commands.TimesheetCommands.UpdateStatusRange
{
    public class UpdateTimesheetStatusRangeHandler : IRequestHandler<UpdateTimesheetStatusRangeCommand, IEnumerable<TimesheetDto>>
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;

        public UpdateTimesheetStatusRangeHandler(AppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TimesheetDto>> Handle(UpdateTimesheetStatusRangeCommand request, CancellationToken cancellationToken)
        {
            //Exclude timesheets that dont belong to user (if there are any), except for leader role
            var dbTimesheets = await _dbContext.Set<Timesheet>()
                .Where(x => request.Ids.Contains(x.Id)).ToListAsync();

            //Update status to confirmed
            dbTimesheets = dbTimesheets.Select(x => { x.Status = request.Status; return x; }).ToList();

            //_dbContext.Timesheets.UpdateRange(timesheets);
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<IEnumerable<TimesheetDto>>(dbTimesheets);
        }
    }
}
