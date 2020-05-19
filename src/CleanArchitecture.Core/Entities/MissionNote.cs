using CleanArchitecture.Core.Interfaces;
using CleanArchitecture.SharedKernel;


namespace CleanArchitecture.Core.Entities
{
    public class MissionNote : BaseEntity, IMissionChildEntity
    {
        public string? Title { get; set; }
        public string Content { get; set; }
        public Mission Mission { get; set; }
        public int MissionId { get; set; }
        public bool Pinned { get; set; }
    }
}
