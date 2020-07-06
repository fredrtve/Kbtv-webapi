using System;
using System.Collections.Generic;
using System.Text;

namespace BjBygg.Application.Common
{
    public abstract class DateRangeQuery
    {
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }
}
