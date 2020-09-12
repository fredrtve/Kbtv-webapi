using CleanArchitecture.Core.Entities;

namespace CleanArchitecture.Core.Interfaces
{
    public interface IDocument : IFile
    {
        public DocumentType DocumentType { get; set; }
        public string DocumentTypeId { get; set; }
    }
}
