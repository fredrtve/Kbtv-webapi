using CleanArchitecture.Core.Entities;

namespace CleanArchitecture.Core.Interfaces
{
    public interface IMissionChildEntity
    {
        public Mission Mission { get; set; }
        public string MissionId { get; set; }
    }
}
