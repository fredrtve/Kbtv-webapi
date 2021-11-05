using BjBygg.Application.Application.Common.Interfaces;
using BjBygg.Core.Entities;
using System;
using System.Collections.Generic;

namespace BjBygg.Infrastructure.Services
{
    public class SyncTimestamps : ISyncTimestamps
    {
        public Dictionary<Type, long?> Timestamps { get; } = new Dictionary<Type, long?> 
        {
            { typeof(Mission), null },
            { typeof(Employer), null },
            { typeof(Timesheet), null },
            { typeof(Activity), null }
        };
    }
}
