using AutoMapper;
using BjBygg.Application.Common.Interfaces;
using BjBygg.Application.Identity.Common;
using BjBygg.Application.Identity.Common.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
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
                Values = SyncValues(user, request.Timestamp),
                Arrays = new SyncArraysResponse()
                {
                    Missions = await _mediator.Send(
                        new MissionSyncQuery()
                        {
                            Timestamp = request.Timestamp,
                            User = user,
                            InitialTimestamp = request.InitialTimestamp
                        }
                    ),
                    MissionImages = await _mediator.Send(
                        new MissionImageSyncQuery()
                        {
                            Timestamp = request.Timestamp,
                            User = user,
                            InitialTimestamp = request.InitialTimestamp
                        }
                    ),
                    MissionNotes = await _mediator.Send(
                        new MissionNoteSyncQuery()
                        {
                            Timestamp = request.Timestamp,
                            User = user,
                            InitialTimestamp = request.InitialTimestamp
                        }
                    ),
                    MissionDocuments = await _mediator.Send(
                        new MissionDocumentSyncQuery()
                        {
                            Timestamp = request.Timestamp,
                            User = user,
                            InitialTimestamp = request.InitialTimestamp
                        }
                    ),
                    UserTimesheets = await _mediator.Send(
                        new UserTimesheetSyncQuery()
                        {
                            Timestamp = request.Timestamp,
                            User = user,
                            InitialTimestamp = request.InitialTimestamp
                        }
                    ),
                    Employers = await _mediator.Send(new EmployerSyncQuery() { Timestamp = request.Timestamp, User = user }),
                    MissionTypes = await _mediator.Send(new MissionTypeSyncQuery() { Timestamp = request.Timestamp }),
                }
            };
        }

        private SyncValuesResponse SyncValues(UserDto user, long? timestamp)
        {
            var syncResponse = new SyncValuesResponse();
            if (timestamp == null || user.UpdatedAt >= timestamp) syncResponse.CurrentUser = user;
            return syncResponse;
        }
    }
}
