﻿using AutoMapper;
using BjBygg.Application.Application.Common.Dto;
using BjBygg.Application.Application.Common.Interfaces;
using BjBygg.Application.Application.Queries.DbSyncQueries.Common;
using BjBygg.Application.Common;
using BjBygg.Application.Common.Interfaces;
using BjBygg.Application.Identity.Common.Models;
using BjBygg.Core;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace BjBygg.Application.Application.Queries.DbSyncQueries.SyncAll
{
    public class SyncAllHandler : IRequestHandler<SyncAllQuery, SyncAllResponse>
    {
        private readonly IMediator _mediator;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IAppDbContext _dbContext;
        private readonly ICurrentUserService _currentUserService;

        public SyncAllHandler(
            IMediator mediator,
            UserManager<ApplicationUser> userManager,
            IMapper mapper,
            IAppDbContext dbContext,
            ICurrentUserService currentUserService)
        {
            _mediator = mediator;
            _userManager = userManager;
            _mapper = mapper;
            _dbContext = dbContext;
            _currentUserService = currentUserService;
        }

        public async Task<SyncAllResponse> Handle(SyncAllQuery request, CancellationToken cancellationToken)
        {
            var user = _mapper.Map<ApplicationUserDto>(await _userManager.FindByNameAsync(_currentUserService.UserName));
            user.Role = _currentUserService.Role;

            if(user.Role == Roles.Employer) 
            { 
                var employerUser = await _dbContext.EmployerUsers.FirstOrDefaultAsync(x => x.UserName == user.UserName);
                user.EmployerId = employerUser?.EmployerId;
            }

            var queryPayload = new DbSyncQueryPayload()
            {
                Timestamp = request.Timestamp,
                User = user,
                InitialSync = request.InitialSync
            };

            var missionSyncResponse = await _mediator.Send(new MissionSyncQuery(queryPayload));

            return new SyncAllResponse()
            {
                Values = await SyncValuesAsync(user, request.Timestamp),
                Arrays = new SyncEntitiesResponse()
                {
                    Missions = missionSyncResponse?.Missions,
                    MissionNotes = missionSyncResponse?.MissionNotes,
                    MissionDocuments = missionSyncResponse?.MissionDocuments,
                    MissionImages = missionSyncResponse?.MissionImages,
                    MissionActivities = missionSyncResponse?.MissionActivities,
                    Employers = await _mediator.Send(new EmployerSyncQuery(queryPayload)),
                    UserTimesheets = await _mediator.Send(new UserTimesheetSyncQuery(queryPayload)),
                    Activities = await _mediator.Send(new ActivitySyncQuery(queryPayload))
                }
            };
        }

        private async  Task<SyncValuesResponse> SyncValuesAsync(ApplicationUserDto user, long? timestamp)
        {
            var syncResponse = new SyncValuesResponse();
            if (timestamp == null || user.UpdatedAt >= timestamp) syncResponse.CurrentUser = user;
            if(user.Role == Roles.Leader)
            {
                var dbSettings = await _dbContext.GetLeaderSettingsAsync();
                if(dbSettings != null)
                {
                    var dbTimestamp = DateTimeHelper.ConvertDateToEpoch(dbSettings.UpdatedAt) * 1000;
                    if (timestamp == null || dbTimestamp >= timestamp)
                        syncResponse.LeaderSettings = _mapper.Map<LeaderSettingsDto>(dbSettings);
                }
            }
            return syncResponse;
        }
    }
}
