using MediatR;


namespace BjBygg.Application.Queries.DbSyncQueries.SyncAll
{
    public class SyncAllQuery : IRequest<SyncAllResponse>
    {
        public string? UserName { get; set; }
        public string? Role { get; set; }
        public double? MissionTimestamp { get; set; }

        public double? EmployerTimestamp { get; set; }

        public double? MissionImageTimestamp { get; set; }

        public double? MissionNoteTimestamp { get; set; }

        public double? MissionReportTimestamp { get; set; }

        public double? ReportTypeTimestamp { get; set; }

        public double? MissionTypeTimestamp { get; set; }

        public double? UserTimesheetTimestamp { get; set; }    

    }
}
