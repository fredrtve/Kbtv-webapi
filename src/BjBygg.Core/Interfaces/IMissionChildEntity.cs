using BjBygg.Core.Entities;

namespace BjBygg.Core.Interfaces
{
    public interface IMissionChildEntity
    {
        public Mission Mission { get; set; }
        public string MissionId { get; set; }
    }
}
