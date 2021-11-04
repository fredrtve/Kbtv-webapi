using BjBygg.Application.Application.Common.Dto;
using BjBygg.Application.Application.Queries.DbSyncQueries.Common;
using BjBygg.Application.Application.Queries.DbSyncQueries.Dto;

namespace BjBygg.Application.Application.Queries.DbSyncQueries
{
    public class SyncMissionResponse
    {
        public SyncEntityResponse<SyncMissionDto>? Missions { get; set; }

        public SyncEntityResponse<SyncMissionImageDto>? MissionImages { get; set; }

        public SyncEntityResponse<SyncMissionNoteDto>? MissionNotes { get; set; }

        public SyncEntityResponse<SyncMissionDocumentDto>? MissionDocuments { get; set; }
        public SyncEntityResponse<SyncMissionActivityDto>? MissionActivities { get; set; }

    }
}
