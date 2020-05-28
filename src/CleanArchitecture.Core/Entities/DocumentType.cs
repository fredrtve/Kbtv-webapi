using CleanArchitecture.SharedKernel;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Core.Entities
{
    public class DocumentType : BaseEntity
    {
        public DocumentType() { }
        public string Name { get; set; }
        public List<MissionDocument> MissionDocuments { get; set; }
    }
}
