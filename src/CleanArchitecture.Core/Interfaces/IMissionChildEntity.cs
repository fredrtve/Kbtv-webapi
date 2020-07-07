using CleanArchitecture.Core.Entities;

namespace CleanArchitecture.Core.Interfaces
{
    public interface IMissionChildEntity
    {
        public Mission Mission { get; set; }
        public int MissionId { get; set; }
    }
}
