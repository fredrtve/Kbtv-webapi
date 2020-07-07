using CleanArchitecture.SharedKernel;
using System.Collections.Generic;

namespace CleanArchitecture.Core.Entities
{
    public class DocumentType : BaseEntity
    {
        public DocumentType() { }
        public string Name { get; set; }
        public List<MissionDocument> MissionDocuments { get; set; }
    }
}
