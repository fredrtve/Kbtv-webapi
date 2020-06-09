using CleanArchitecture.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BjBygg.Application.Shared
{
    public class MissionDocumentDto : DbSyncDto
    {
        public int Id { get; set; }

        public int MissionId { get; set; }

        public Uri FileURL { get; set; }

        public int DocumentTypeId { get; set; }

        public DocumentTypeDto DocumentType { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
