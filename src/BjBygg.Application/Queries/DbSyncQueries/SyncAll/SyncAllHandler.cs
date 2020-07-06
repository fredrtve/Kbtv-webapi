using BjBygg.Application.Queries.DbSyncQueries.EmployerQuery;
using BjBygg.Application.Queries.DbSyncQueries.MissionImageQuery;
using BjBygg.Application.Queries.DbSyncQueries.MissionNoteQuery;
using BjBygg.Application.Queries.DbSyncQueries.MissionQuery;
using BjBygg.Application.Queries.DbSyncQueries.MissionTypeQuery;
using BjBygg.Application.Queries.DbSyncQueries.TimesheetQuery;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using BjBygg.Application.Queries.DbSyncQueries.MissionDocumentQuery;
using BjBygg.Application.Queries.DbSyncQueries.DocumentTypeQuery;
using Microsoft.AspNetCore.Identity;
using CleanArchitecture.Infrastructure.Identity;
using AutoMapper;
using CleanArchitecture.Core.Exceptions;
using BjBygg.Application.Common;

namespace BjBygg.Application.Queries.DbSyncQueries.SyncAll
{
    public class SyncAllHandler : IRequestHandler<SyncAllQuery, SyncAllResponse>
    {
        private readonly IMediator _mediator;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public SyncAllHandler(IMediator mediator, UserManager<ApplicationUser> userManager, IMapper mapper) {
            _mediator = mediator;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<SyncAllResponse> Handle(SyncAllQuery request, CancellationToken cancellationToken)
        {
            if (request.UserName == null) throw new BadRequestException("No username provided");

            var user = _mapper.Map<UserDto>(await _userManager.FindByNameAsync(request.UserName));
            user.Role = request.Role;

            return new SyncAllResponse()
            {
                MissionSync = await _mediator.Send(
                    new MissionSyncQuery() { 
                        Timestamp =  request.MissionTimestamp, User = user, 
                        InitialNumberOfMonths = request.InitialNumberOfMonths 
                    }
                ),
                MissionImageSync = await _mediator.Send(
                    new MissionImageSyncQuery() { 
                        Timestamp = request.MissionImageTimestamp, User = user, 
                        InitialNumberOfMonths = request.InitialNumberOfMonths 
                    }
                ),
                MissionNoteSync = await _mediator.Send(
                    new MissionNoteSyncQuery() { 
                        Timestamp = request.MissionNoteTimestamp, User = user, 
                        InitialNumberOfMonths = request.InitialNumberOfMonths  
                    }
                ),
                MissionDocumentSync = await _mediator.Send(
                    new MissionDocumentSyncQuery() { 
                        Timestamp = request.MissionDocumentTimestamp, User = user,
                        InitialNumberOfMonths = request.InitialNumberOfMonths
                    }
                ),
                UserTimesheetSync = await _mediator.Send(
                    new UserTimesheetSyncQuery() { 
                        Timestamp = request.UserTimesheetTimestamp, User = user,
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
