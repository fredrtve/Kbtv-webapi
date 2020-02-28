using CleanArchitecture.Core.SharedKernel;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Core.Entities
{
    public class Timesheet : BaseEntity
    {
        public string UserName { get; set; }

        public int MissionId { get; set; }

        public Mission Mission { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public Boolean Locked { get; set; }

        public double GetTotalHours()
        {
            return (EndTime - StartTime).TotalHours;
        }
    }
}
