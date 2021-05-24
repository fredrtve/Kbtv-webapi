using BjBygg.SharedKernel;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BjBygg.Application.Identity.Common.Models
{
    public class ApplicationUser : IdentityUser, ITrackable, IContactable
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<IdentityUserRole<string>> Roles { get; set; }
        public IdentityRole Role { get; set; }
        public List<RefreshToken> RefreshTokens { get; set; }
        public bool Deleted { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
    }
}
