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

namespace BjBygg.Application.Queries.DbSyncQueries.SyncAll
{
    public class SyncAllHandler : IRequestHandler<SyncAllQuery, SyncAllResponse>
    {
        private readonly IMediator _mediator;

        public SyncAllHandler(IMediator mediator) {
            this._mediator = mediator;
        }

        public async Task<SyncAllResponse> Handle(SyncAllQuery request, CancellationToken cancellationToken)
        {
            return new SyncAllResponse()
            {
                MissionSync = await _mediator.Send(
                    new MissionSyncQuery() { 
                        Timestamp =  request.MissionTimestamp, User = request.User, 
                        InitialNumberOfMonths = request.InitialNumberOfMonths 
                    }
                ),
                MissionImageSync = await _mediator.Send(
                    new MissionImageSyncQuery() { 
                        Timestamp = request.MissionImageTimestamp, User = request.User, 
                        InitialNumberOfMonths = request.InitialNumberOfMonths 
                    }
                ),
                MissionNoteSync = await _mediator.Send(
                    new MissionNoteSyncQuery() { 
                        Timestamp = request.MissionNoteTimestamp, User = request.User, 
                        InitialNumberOfMonths = request.InitialNumberOfMonths  
                    }
                ),
                MissionDocumentSync = await _mediator.Send(
                    new MissionDocumentSyncQuery() { 
                        Timestamp = request.MissionDocumentTimestamp, User = request.User,
                        InitialNumberOfMonths = request.InitialNumberOfMonths
                    }
                ),
                UserTimesheetSync = await _mediator.Send(
                    new UserTimesheetSyncQuery() { 
                        Timestamp = request.UserTimesheetTimestamp, User = request.User,
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
