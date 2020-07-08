using CleanArchitecture.Core;

namespace BjBygg.Application.Commands.IdentityCommands.RefreshToken
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
