using BjBygg.SharedKernel;
using System.IO;

namespace BjBygg.Core.Interfaces
{
    public interface IMailTemplate<T>
    {
        string Key { get; }
        T Data { get; set; }
        Stream? Attachment { get; set; }

        string? AttachmentName { get; set; }
    }
}
