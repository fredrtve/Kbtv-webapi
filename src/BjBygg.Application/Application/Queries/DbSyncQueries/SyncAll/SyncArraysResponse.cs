using BjBygg.Application.Application.Common.Dto;
using BjBygg.Application.Application.Queries.DbSyncQueries.Common;

namespace BjBygg.Application.Application.Queries.DbSyncQueries.SyncAll
{
    public class SyncArraysResponse
    {
        public DbSyncArrayResponse<MissionDto> Missions { get; set; }

        public DbSyncArrayResponse<EmployerDto> Employers { get; set; }

        public DbSyncArrayResponse<MissionImageDto> MissionImages { get; set; }

        public DbSyncArrayResponse<MissionNoteDto> MissionNotes { get; set; }

        public DbSyncArrayResponse<MissionDocumentDto> MissionDocuments { get; set; }

        public DbSyncArrayResponse<MissionTypeDto> MissionTypes { get; set; }

        public DbSyncArrayResponse<TimesheetDto> UserTimesheets { get; set; }

    }
}
