using System;

namespace BjBygg.Application.Common
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
