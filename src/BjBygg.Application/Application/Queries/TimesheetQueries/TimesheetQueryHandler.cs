﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using BjBygg.Application.Application.Common.Dto;
using BjBygg.Application.Application.Common.Interfaces;
using BjBygg.Core;
using BjBygg.Core.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BjBygg.Application.Application.Queries.TimesheetQueries
{
    public class TimesheetQueryHandler : IRequestHandler<TimesheetQuery, List<TimesheetDto>>
    {
        private readonly IAppDbContext _dbContext;
        private readonly IMapper _mapper;

        public TimesheetQueryHandler(IAppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            dbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public async Task<List<TimesheetDto>> Handle(TimesheetQuery request, CancellationToken cancellationToken)
        {
            var query = _dbContext.Set<Timesheet>().AsQueryable();

            if (request.UserName != null)
                query = query.Where(x => x.UserName == request.UserName);

            if (request.StartDate != null)
            {
                var startDate = DateTimeHelper.ConvertEpochToDate((request.StartDate / 1000) ?? 0);
                query = query.Where(x => x.StartTime.Date >= startDate.Date);
            }

            if (request.EndDate != null)
            {
                var endDate = DateTimeHelper.ConvertEpochToDate((request.EndDate / 1000) ?? 0);
                query = query.Where(x => x.StartTime.Date <= endDate.Date);
            }

            query = query.Include(x => x.MissionActivity);
            if(request.MissionId != null) query = query.Where(x => x.MissionActivity.MissionId == request.MissionId);
            if(request.ActivityId != null) query = query.Where(x => x.MissionActivity.ActivityId == request.ActivityId);

            var queryResponse = await query.ProjectTo<TimesheetQueryDto>(_mapper.ConfigurationProvider).ToListAsync();

            return _mapper.Map<List<TimesheetDto>>(queryResponse);
        }

    }
}
