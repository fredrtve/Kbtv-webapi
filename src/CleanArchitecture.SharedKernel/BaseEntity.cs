using CleanArchitecture.SharedKernel;
using System;
using System.Collections.Generic;

namespace CleanArchitecture.Core.SharedKernel
{
    public abstract class BaseEntity : ITrackable
    {
        public int Id { get; set; }
        public bool Deleted { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
    }
}
