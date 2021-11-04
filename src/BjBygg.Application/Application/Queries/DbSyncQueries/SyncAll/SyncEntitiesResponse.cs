using BjBygg.Application.Application.Common.Dto;
using BjBygg.Application.Application.Queries.DbSyncQueries.Common;
using BjBygg.Application.Application.Queries.DbSyncQueries.Dto;

namespace BjBygg.Application.Application.Queries.DbSyncQueries.SyncAll
{
    public class SyncEntitiesResponse : SyncMissionResponse
    {
        public SyncEntityResponse<SyncEmployerDto>? Employers { get; set; }

        public SyncEntityResponse<SyncMissionTypeDto>? MissionTypes { get; set; }
        public SyncEntityResponse<SyncActivityDto>? Activities { get; set; }
        public SyncEntityResponse<SyncUserTimesheetDto>? UserTimesheets { get; set; }

    }
}
