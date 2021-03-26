namespace BjBygg.Application.Application.Queries.DbSyncQueries.Common
{
    public abstract class DbSyncQuery
    {
        public long? Timestamp { get; set; }
        public bool InitialSync { get; set; }
    }
}
