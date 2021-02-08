﻿using AutoMapper;
using BjBygg.Application.Application.Common.Dto;
using BjBygg.Application.Application.Common.Interfaces;
using BjBygg.Application.Application.Queries.DbSyncQueries.Common;
using BjBygg.Application.Common;
using CleanArchitecture.Core.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BjBygg.Application.Application.Queries.DbSyncQueries
{
    public class UserTimesheetSyncQuery : UserDbSyncQuery<TimesheetDto>
    {
    }
    public class UserTimesheetSyncQueryHandler : IRequestHandler<UserTimesheetSyncQuery, DbSyncArrayResponse<TimesheetDto>>
    {
        private readonly IAppDbContext _dbContext;
        private readonly IMapper _mapper;

        public UserTimesheetSyncQueryHandler(IAppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _dbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }
        public async Task<DbSyncArrayResponse<TimesheetDto>> Handle(UserTimesheetSyncQuery request, CancellationToken cancellationToken)
        {
            var query = _dbContext.Set<Timesheet>().AsQueryable().GetSyncItems(request);

            query = query.Where(x => x.UserName == request.User.UserName); //Only users entities

            return await query.ToSyncArrayResponseAsync<Timesheet, TimesheetDto>(request.Timestamp == null, _mapper);
        }
    }
}
