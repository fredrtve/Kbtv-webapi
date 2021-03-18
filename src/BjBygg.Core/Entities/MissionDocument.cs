using BjBygg.Core.Interfaces;
using BjBygg.SharedKernel;

namespace BjBygg.Core.Entities
{
    public class MissionDocument : BaseEntity, IMissionChildEntity, IFile
    {
        public MissionDocument()
        {
        }

        public string Name { get; set; }
        public Mission Mission { get; set; }
        public string MissionId { get; set; }
        public string FileName { get; set; }
    }
}
