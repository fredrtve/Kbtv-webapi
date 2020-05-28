using BjBygg.Application.Shared;
using MediatR;


namespace BjBygg.Application.Queries.DbSyncQueries.SyncAll
{
    public class SyncAllQuery : IRequest<SyncAllResponse>
    {
        public UserDto? User { get; set; }

        public double? MissionTimestamp { get; set; }

        public double? EmployerTimestamp { get; set; }

        public double? MissionImageTimestamp { get; set; }

        public double? MissionNoteTimestamp { get; set; }

        public double? MissionDocumentTimestamp { get; set; }

        public double? DocumentTypeTimestamp { get; set; }

        public double? MissionTypeTimestamp { get; set; }

        public double? UserTimesheetTimestamp { get; set; }    

    }
}
