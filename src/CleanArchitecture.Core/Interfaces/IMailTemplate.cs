using CleanArchitecture.SharedKernel;

namespace CleanArchitecture.Core.Interfaces
{
    public interface IMailTemplate<T>
    {
        string Key { get; }
        T Data { get; set; }
        BasicFileStream? Attachment { get; set; }
    }
}
