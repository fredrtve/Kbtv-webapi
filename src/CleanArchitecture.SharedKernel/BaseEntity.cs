using System;


namespace CleanArchitecture.SharedKernel
{
    public abstract class BaseEntity : ITrackable
    {
        public string Id { get; set; }
        public bool Deleted { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
    }
}
