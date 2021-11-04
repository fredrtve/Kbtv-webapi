using BjBygg.SharedKernel;
using System.Collections.Generic;

namespace BjBygg.Core.Entities
{
    public class Activity : BaseEntity, IName
    {
        public Activity() { }

        public string Name { get; set; }

        public List<MissionActivity> MissionActivities { get; set; }

    }
}
