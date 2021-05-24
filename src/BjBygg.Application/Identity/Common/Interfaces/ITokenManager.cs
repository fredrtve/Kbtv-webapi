using BjBygg.Application.Identity.Commands.UserIdentityCommands.Login;
using BjBygg.Application.Identity.Commands.UserIdentityCommands.RefreshToken;
using BjBygg.Application.Identity.Common.Models;
using System.Threading.Tasks;

namespace BjBygg.Application.Identity.Common.Interfaces
{
    public interface ITokenManager
    {
        Task<CreateTokensResponse> CreateTokensAsync(ApplicationUser user, string role);

        Task<RefreshTokenResponse> RefreshTokensAsync(string accessToken, string refreshToken);

        Task RevokeRefreshTokenAsync(string refreshToken);
    }
}
