using BjBygg.Application.Shared;


namespace BjBygg.Application.Queries.DbSyncQueries.TimesheetQuery
{
    public class TimesheetWeekSyncQuery : DbSyncQuery<TimesheetWeekDto>
    {
        public string UserName { get; set; }

        public string Role { get; set; }
    }
}
