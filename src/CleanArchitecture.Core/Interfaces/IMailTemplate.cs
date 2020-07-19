using System;
using System.Collections.Generic;

namespace CleanArchitecture.Core.Interfaces
{
    public interface IMailTemplate<T>
    {
        string Key { get; }
        T Data { get; set; }
    }
}
