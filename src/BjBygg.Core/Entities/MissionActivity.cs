using BjBygg.Core.Interfaces;
using BjBygg.SharedKernel;

namespace BjBygg.Core.Entities
{
    public class MissionActivity : BaseEntity, IMissionChildEntity
    {
        public Activity Activity { get; set; }
        public string ActivityId { get; set; }
        public Mission Mission { get; set; }
        public string MissionId { get; set; }
    }
}
