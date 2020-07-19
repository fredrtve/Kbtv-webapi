using CleanArchitecture.Core;
using CleanArchitecture.SharedKernel;
using System;

namespace BjBygg.Application.Identity.Common.Models
{
    public class RefreshToken : BaseEntity
    {
        public string Token { get; private set; }
        public DateTime Expires { get; private set; }
        public string UserId { get; private set; }
        public ApplicationUser User { get; private set; }
        public bool Active => DateTimeHelper.Now() <= Expires;

        public RefreshToken(string token, DateTime expires, string userId)
        {
            Token = token;
            Expires = expires;
            UserId = userId;
        }
    }
}
