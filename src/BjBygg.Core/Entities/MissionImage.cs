using BjBygg.Core.Interfaces;
using BjBygg.SharedKernel;

namespace BjBygg.Core.Entities
{
    public class MissionImage : BaseEntity, IMissionChildEntity, IFile
    {
        public MissionImage() { }

        public Mission Mission { get; set; }
        public string MissionId { get; set; }
        public string FileName { get; set; }
    }
}
