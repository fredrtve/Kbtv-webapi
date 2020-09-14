using BjBygg.Application.Application.Queries.DbSyncQueries.Common;
using System;

namespace BjBygg.Application.Application.Common.Dto
{
    public class MissionDocumentDto : DbSyncDto
    {
        public string Id { get; set; }

        public string MissionId { get; set; }

        public Uri FileName { get; set; }

        public string DocumentTypeId { get; set; }

        public DocumentTypeDto DocumentType { get; set; }

    }
}
