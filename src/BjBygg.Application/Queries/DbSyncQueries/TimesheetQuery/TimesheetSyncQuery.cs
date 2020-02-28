using BjBygg.Application.Shared;


namespace BjBygg.Application.Queries.DbSyncQueries.TimesheetQuery
{
    public class TimesheetSyncQuery : DbSyncQuery<TimesheetDto>
    {
        public string UserName { get; set; }

        public string Role { get; set; }
    }
}
