using CleanArchitecture.Core;
using CleanArchitecture.SharedKernel;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BjBygg.Application.Identity.Common.Models
{
    public class ApplicationUser : IdentityUser, ITrackable
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public List<IdentityUserRole<string>> Roles { get; set; }

        public IdentityRole Role { get; set; }

        public List<RefreshToken> RefreshTokens { get; set; }

        public string? EmployerId { get; set; }

        public bool Deleted { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }

        public bool HasValidRefreshToken(string refreshToken)
        {
            return RefreshTokens.Any(rt =>
                rt.Token == refreshToken &&
                rt.Active &&
                DateTime.Compare(rt.Expires, DateTime.UtcNow) >= 0
            );
        }

        public void AddRefreshToken(string token, string userId, double daysToExpire = 180)
        {
            RefreshTokens.Add(new RefreshToken(token, DateTime.UtcNow.AddDays(daysToExpire), userId));
        }

        public void RemoveRefreshToken(string refreshToken)
        {
            RefreshTokens.Remove(RefreshTokens.First(t => t.Token == refreshToken));
        }
    }
}
