using BjBygg.Application.Application.Queries.DbSyncQueries.Common;
using BjBygg.Core.Enums;

namespace BjBygg.Application.Application.Common.Dto
{
    public class TimesheetDto : UserTimesheetDto
    {
        public string? UserName { get; set; }
    }
}
