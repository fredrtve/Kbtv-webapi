using System;

namespace BjBygg.Application.Common
{
    public abstract class DateRangeQuery
    {
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }
}
