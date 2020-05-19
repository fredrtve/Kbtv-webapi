using System;
using System.Collections.Generic;
using System.Text;

namespace BjBygg.Application.Shared
{
    public class ReportTypeDto : DbSyncDto
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
