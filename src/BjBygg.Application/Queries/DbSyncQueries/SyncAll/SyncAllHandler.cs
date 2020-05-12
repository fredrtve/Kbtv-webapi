using BjBygg.Application.Queries.DbSyncQueries.EmployerQuery;
using BjBygg.Application.Queries.DbSyncQueries.MissionImageQuery;
using BjBygg.Application.Queries.DbSyncQueries.MissionNoteQuery;
using BjBygg.Application.Queries.DbSyncQueries.MissionQuery;
using BjBygg.Application.Queries.DbSyncQueries.MissionReportQuery;
using BjBygg.Application.Queries.DbSyncQueries.MissionReportTypeQuery;
using BjBygg.Application.Queries.DbSyncQueries.MissionTypeQuery;
using BjBygg.Application.Queries.DbSyncQueries.TimesheetQuery;
using CleanArchitecture.Infrastructure.Data;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

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
                MissionSync = await _mediator.Send(new MissionSyncQuery() { Timestamp =  request.Timestamp }),
                EmployerSync = await _mediator.Send(new EmployerSyncQuery() { Timestamp = request.Timestamp }),
                MissionImageSync = await _mediator.Send(new MissionImageSyncQuery() { Timestamp = request.Timestamp }),
                MissionNoteSync = await _mediator.Send(new MissionNoteSyncQuery() { Timestamp = request.Timestamp }),
                MissionReportSync = await _mediator.Send(new MissionReportSyncQuery() { Timestamp = request.Timestamp }),
                MissionReportTypeSync = await _mediator.Send(new MissionReportTypeSyncQuery() { Timestamp = request.Timestamp }),
                MissionTypeSync = await _mediator.Send(new MissionTypeSyncQuery() { Timestamp = request.Timestamp }),
                UserTimesheetSync = await _mediator.Send(new UserTimesheetSyncQuery() { Timestamp = request.Timestamp, UserName = request.UserName})
            };
        }
    }
}
