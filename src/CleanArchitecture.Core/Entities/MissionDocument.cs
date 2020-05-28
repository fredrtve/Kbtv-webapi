using CleanArchitecture.Core.Interfaces;
using CleanArchitecture.SharedKernel;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Core.Entities
{
    public class MissionDocument : BaseEntity, IMissionChildEntity, IDocument
    {
        public MissionDocument()
        {
        }

        public Mission Mission { get; set; }
        public int MissionId { get; set; }
        public Uri FileURL { get; set; }
        public DocumentType DocumentType { get; set; }
        public int DocumentTypeId { get; set; }
    }
}
