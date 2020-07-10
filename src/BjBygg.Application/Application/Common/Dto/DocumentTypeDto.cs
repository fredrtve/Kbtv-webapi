using BjBygg.Application.Application.Queries.DbSyncQueries.Common;

namespace BjBygg.Application.Application.Common.Dto
{
    public class DocumentTypeDto : DbSyncDto
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
