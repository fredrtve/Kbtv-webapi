using BjBygg.Application.Application.Queries.DbSyncQueries.Common;
using System;

namespace BjBygg.Application.Application.Common.Dto
{
    public class MissionNoteDto : DbSyncDto
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public bool Pinned { get; set; }
        public int MissionId { get; set; }
    }
}
