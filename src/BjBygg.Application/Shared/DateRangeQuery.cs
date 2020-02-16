using System;
using System.Collections.Generic;
using System.Text;

namespace BjBygg.Application.Shared
{
    public abstract class DateRangeQuery
    {
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }
}
