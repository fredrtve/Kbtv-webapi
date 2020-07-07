using System;

namespace BjBygg.Application.Common
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
