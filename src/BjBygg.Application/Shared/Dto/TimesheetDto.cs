using System;
using System.Collections.Generic;
using System.Text;

namespace BjBygg.Application.Shared
{
    public class TimesheetDto : DbSyncDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }

        public int MissionId { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public Boolean Locked { get; set; }
    }
}
