using BjBygg.Application.Identity.Common.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BjBygg.Application.Identity.Commands.UserIdentityCommands.RefreshToken
{
    public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, RefreshTokenResponse>
    {
        private readonly ITokenManager _tokenManager;

        public RefreshTokenCommandHandler(ITokenManager tokenManager)
        {
            _tokenManager = tokenManager;
        }

        public async Task<RefreshTokenResponse> Handle(RefreshTokenCommand command, CancellationToken cancellationToken)
        {
            return await _tokenManager.RefreshTokensAsync(command.AccessToken, command.RefreshToken); 
        }
    }
}