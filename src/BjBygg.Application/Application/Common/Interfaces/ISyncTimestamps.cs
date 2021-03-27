using BjBygg.SharedKernel;
using System;
using System.Collections.Generic;

namespace BjBygg.Application.Application.Common.Interfaces
{
    public interface ISyncTimestamps
    {
        Dictionary<Type, long?> Timestamps { get; }
    }
}
