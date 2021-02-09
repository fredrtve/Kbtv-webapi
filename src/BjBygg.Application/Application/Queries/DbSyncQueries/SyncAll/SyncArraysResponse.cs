using BjBygg.Application.Application.Common.Dto;
using BjBygg.Application.Application.Queries.DbSyncQueries.Common;

namespace BjBygg.Application.Application.Queries.DbSyncQueries.SyncAll
{
    public class SyncArraysResponse : MissionSyncArraysResponse
    {
        public DbSyncArrayResponse<EmployerDto> Employers { get; set; }

        public DbSyncArrayResponse<MissionTypeDto> MissionTypes { get; set; }

        public DbSyncArrayResponse<TimesheetDto> UserTimesheets { get; set; }

    }
}
