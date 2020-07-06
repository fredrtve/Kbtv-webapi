using BjBygg.Application.Common;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace BjBygg.Application.Queries.DbSyncQueries.SyncAll
{
    public class SyncAllQuery : IRequest<SyncAllResponse>
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Role { get; set; }
        public int? InitialNumberOfMonths { get; set; }

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
