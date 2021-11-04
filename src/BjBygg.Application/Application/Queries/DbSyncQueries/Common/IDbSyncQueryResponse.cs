namespace BjBygg.Application.Application.Queries.DbSyncQueries.Common
{
    public interface IDbSyncQueryResponse
    {
        public string Id { get; set; }
        public bool Deleted { get; set; }
    }
}
