using AutoMapper;
using BjBygg.Application.Common;
using CleanArchitecture.Core.Interfaces.Services;
using CleanArchitecture.Infrastructure.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Threading;
using System.Threading.Tasks;

namespace BjBygg.Application.Queries.DbSyncQueries.SyncAll
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
                MissionTypeSync = await _mediator.Send(new MissionTypeSyncQuery() { Timestamp = request.MissionTypeTimestamp }),
                EmployerSync = await _mediator.Send(new EmployerSyncQuery() { Timestamp = request.EmployerTimestamp }),
                DocumentTypeSync = await _mediator.Send(new DocumentTypeSyncQuery() { Timestamp = request.DocumentTypeTimestamp }),
            };
        }
    }
}
