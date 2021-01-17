using BjBygg.Application.Identity.Common.Models;

namespace BjBygg.Application.Identity.Commands.UserIdentityCommands.RefreshToken
{
    public class RefreshTokenResponse
    {
        public AccessToken AccessToken { get; }

        public RefreshTokenResponse(AccessToken accessToken)
        {
            AccessToken = accessToken;
        }
    }
}