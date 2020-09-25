using CleanArchitecture.SharedKernel;
using System;
using System.Collections.Generic;
using System.IO;

namespace CleanArchitecture.Core.Interfaces
{
    public interface IMailTemplate<T>
    {
        string Key { get; }
        T Data { get; set; }
        BasicFileStream? Attachment { get; set; }
    }
}
