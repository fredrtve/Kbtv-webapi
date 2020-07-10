using BjBygg.Application.Application.Queries.DbSyncQueries.Common;
using System;

namespace BjBygg.Application.Application.Common.Dto
{
    public class MissionDocumentDto : DbSyncDto
    {
        public int Id { get; set; }

        public int MissionId { get; set; }

        public Uri FileURL { get; set; }

        public int DocumentTypeId { get; set; }

        public DocumentTypeDto DocumentType { get; set; }

    }
}
