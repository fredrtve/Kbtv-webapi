using MediatR;

namespace BjBygg.Application.Application.Queries.DbSyncQueries.SyncAll
{
    public class SyncAllQuery : IRequest<SyncAllResponse>
    {
        public int? InitialNumberOfMonths { get; set; }

        public long? MissionTimestamp { get; set; }

        public long? EmployerTimestamp { get; set; }

        public long? MissionImageTimestamp { get; set; }

        public long? MissionNoteTimestamp { get; set; }

        public long? MissionDocumentTimestamp { get; set; }

        public long? DocumentTypeTimestamp { get; set; }

        public long? MissionTypeTimestamp { get; set; }

        public long? UserTimesheetTimestamp { get; set; }

        public long? CurrentUserTimestamp { get; set; }

    }
}
