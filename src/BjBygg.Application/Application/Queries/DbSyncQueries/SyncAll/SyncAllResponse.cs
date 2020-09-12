using BjBygg.Application.Application.Common.Dto;
using BjBygg.Application.Application.Queries.DbSyncQueries.Common;
using BjBygg.Application.Identity.Common;

namespace BjBygg.Application.Application.Queries.DbSyncQueries.SyncAll
{
    public class SyncAllResponse
    {
        public DbSyncResponse<UserDto> CurrentUserSync { get; set; }
        public DbSyncResponse<MissionDto> MissionSync { get; set; }

        public DbSyncResponse<EmployerDto> EmployerSync { get; set; }

        public DbSyncResponse<MissionImageDto> MissionImageSync { get; set; }

        public DbSyncResponse<MissionNoteDto> MissionNoteSync { get; set; }

        public DbSyncResponse<MissionDocumentDto> MissionDocumentSync { get; set; }

        public DbSyncResponse<DocumentTypeDto> DocumentTypeSync { get; set; }

        public DbSyncResponse<MissionTypeDto> MissionTypeSync { get; set; }

        public DbSyncResponse<TimesheetDto> UserTimesheetSync { get; set; }

    }
}
