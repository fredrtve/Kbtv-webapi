using BjBygg.Application.Application.Queries.DbSyncQueries.Common;

namespace BjBygg.Application.Application.Common.Dto
{
    public class MissionNoteDto : DbSyncDto
    {
        public string Id { get; set; }
        public string? Title { get; set; }
        public string Content { get; set; }
        public string CreatedBy { get; set; }
        public string MissionId { get; set; }
    }
}
