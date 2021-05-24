using System;
using System.Collections.Generic;

namespace BjBygg.Application.Identity.Common.Models
{
    public class RefreshToken
    {
        public int Id { get; set; }
        public string Token { get; private set; }
        public DateTime Expires { get; private set; }
        public string UserId { get; private set; }
        public ApplicationUser User { get; private set; }
        public bool Revoked{ get; set; }
        public bool Active => DateTime.UtcNow <= Expires;
        public int? RootTokenId { get; private set; }
        public RefreshToken RootToken { get; private set; }
        public List<RefreshToken> ChildTokens { get; private set; }

        public RefreshToken(string token, DateTime expires, string userId, int? rootTokenId)
        {
            Token = token;
            Expires = expires;
            UserId = userId;
            RootTokenId = rootTokenId;
        }
    }
}
