using BjBygg.Application.Identity.Common.Models;

namespace BjBygg.Application.Identity.Commands.IdentityCommands.RefreshToken
{
    public class RefreshTokenResponse
    {
        public AccessToken AccessToken { get; }
        public string RefreshToken { get; }

        public RefreshTokenResponse(AccessToken accessToken, string refreshToken)
        {
            AccessToken = accessToken;
            RefreshToken = refreshToken;
        }
    }
}
