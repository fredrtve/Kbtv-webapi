using CleanArchitecture.SharedKernel;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace CleanArchitecture.Infrastructure.Identity
{
    public class ApplicationUser : IdentityUser, ITrackable
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public List<IdentityUserRole<string>> Roles { get; set; }

        public IdentityRole Role { get; set; }

        public int? EmployerId { get; set; }

        public bool Deleted { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
    }
}
