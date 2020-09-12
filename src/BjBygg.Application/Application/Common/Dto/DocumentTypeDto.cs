using BjBygg.Application.Application.Queries.DbSyncQueries.Common;

namespace BjBygg.Application.Application.Common.Dto
{
    public class DocumentTypeDto : DbSyncDto
    {
        public string Id { get; set; }

        public string Name { get; set; }
    }
}
