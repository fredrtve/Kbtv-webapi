using CleanArchitecture.SharedKernel;


namespace CleanArchitecture.Core.Entities
{
    public class MissionNote : BaseEntity
    {
        public string? Title { get; set; }
        public string Content { get; set; }
        public int MissionId { get; set; }
        public bool Pinned { get; set; }
    }
}
