using BjBygg.SharedKernel;
using System.Collections.Generic;

namespace BjBygg.Core.Entities
{
    public class MissionType : BaseEntity, IName
    {
        public MissionType() { }
        public string Name { get; set; }
        public List<Mission> Missions { get; set; }
    }
}
