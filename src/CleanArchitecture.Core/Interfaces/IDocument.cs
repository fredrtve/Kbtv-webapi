using CleanArchitecture.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Core.Interfaces
{
    public interface IDocument : IFile
    {
        public DocumentType DocumentType { get; set; }
        public int DocumentTypeId { get; set; }
    }
}
