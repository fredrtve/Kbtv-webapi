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
                MissionSync = await _mediator.Send(new MissionSyncQuery() { FromDate =  request.FromDate }),
                EmployerSync = await _mediator.Send(new EmployerSyncQuery() { FromDate = request.FromDate }),
                MissionImageSync = await _mediator.Send(new MissionImageSyncQuery() { FromDate = request.FromDate }),
                MissionNoteSync = await _mediator.Send(new MissionNoteSyncQuery() { FromDate = request.FromDate }),
                MissionReportSync = await _mediator.Send(new MissionReportSyncQuery() { FromDate = request.FromDate }),
                MissionReportTypeSync = await _mediator.Send(new MissionReportTypeSyncQuery() { FromDate = request.FromDate }),
                MissionTypeSync = await _mediator.Send(new MissionTypeSyncQuery() { FromDate = request.FromDate }),
                UserTimesheetSync = await _mediator.Send(new UserTimesheetSyncQuery() { FromDate = request.FromDate , UserName = request.UserName})
            };
        }
    }
}
