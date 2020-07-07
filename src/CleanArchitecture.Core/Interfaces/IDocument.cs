using CleanArchitecture.Core.Entities;

namespace CleanArchitecture.Core.Interfaces
{
    public interface IDocument : IFile
    {
        public DocumentType DocumentType { get; set; }
        public int DocumentTypeId { get; set; }
    }
}
