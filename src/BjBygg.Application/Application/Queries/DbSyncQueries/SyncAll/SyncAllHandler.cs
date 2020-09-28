using AutoMapper;
using BjBygg.Application.Application.Queries.DbSyncQueries.Common;
using BjBygg.Application.Common.Interfaces;
using BjBygg.Application.Identity.Common;
using BjBygg.Application.Identity.Common.Models;
using CleanArchitecture.Core;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BjBygg.Application.Application.Queries.DbSyncQueries.SyncAll
{
    public class SyncAllHandler : IRequestHandler<SyncAllQuery, SyncAllResponse>
    {
        private readonly IMediator _mediator;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;

        public SyncAllHandler(
            IMediator mediator,
            UserManager<ApplicationUser> userManager,
            IMapper mapper,
            ICurrentUserService currentUserService)
        {
            _mediator = mediator;
            _userManager = userManager;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }

        public async Task<SyncAllResponse> Handle(SyncAllQuery request, CancellationToken cancellationToken)
        {
            var user = _mapper.Map<UserDto>(await _userManager.FindByNameAsync(_currentUserService.UserName));
            user.Role = _currentUserService.Role;

            return new SyncAllResponse()
            {
                CurrentUserSync = SyncCurrentUser(user, request.CurrentUserTimestamp ?? 0),
                MissionSync = await _mediator.Send(
                    new MissionSyncQuery()
                    {
                        Timestamp = request.MissionTimestamp,
                        User = user,
                        InitialNumberOfMonths = request.InitialNumberOfMonths
                    }
                ),
                MissionImageSync = await _mediator.Send(
                    new MissionImageSyncQuery()
                    {
                        Timestamp = request.MissionImageTimestamp,
                        User = user,
                        InitialNumberOfMonths = request.InitialNumberOfMonths
                    }
                ),
                MissionNoteSync = await _mediator.Send(
                    new MissionNoteSyncQuery()
                    {
                        Timestamp = request.MissionNoteTimestamp,
                        User = user,
                        InitialNumberOfMonths = request.InitialNumberOfMonths
                    }
                ),
                MissionDocumentSync = await _mediator.Send(
                    new MissionDocumentSyncQuery()
                    {
                        Timestamp = request.MissionDocumentTimestamp,
                        User = user,
                        InitialNumberOfMonths = request.InitialNumberOfMonths
                    }
                ),
                UserTimesheetSync = await _mediator.Send(
                    new UserTimesheetSyncQuery()
                    {
                        Timestamp = request.UserTimesheetTimestamp,
                        User = user,
                        InitialNumberOfMonths = request.InitialNumberOfMonths
                    }
                ),
                EmployerSync = await _mediator.Send(new EmployerSyncQuery() { Timestamp = request.EmployerTimestamp, User = user }),
                MissionTypeSync = await _mediator.Send(new MissionTypeSyncQuery() { Timestamp = request.MissionTypeTimestamp }),
                DocumentTypeSync = await _mediator.Send(new DocumentTypeSyncQuery() { Timestamp = request.DocumentTypeTimestamp }),
            };
        }

        private DbSyncResponse<UserDto> SyncCurrentUser(UserDto user, long timestamp)      
        {
            var syncResponse = new DbSyncResponse<UserDto>(new UserDto[] { }, null);

            if (user.UpdatedAt >= timestamp) syncResponse.Entities = new [] {user};

            return syncResponse;
        }
    }
}
