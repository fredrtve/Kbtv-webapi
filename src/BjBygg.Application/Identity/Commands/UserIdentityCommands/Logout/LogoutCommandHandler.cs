using BjBygg.Application.Identity.Common.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BjBygg.Application.Identity.Commands.UserIdentityCommands.Logout
{
    public class LogoutCommandHandler : IRequestHandler<LogoutCommand, Unit>
    {
        private readonly ITokenManager _tokenManager;

        public LogoutCommandHandler(ITokenManager tokenManager)
        {
            _tokenManager = tokenManager;
        }

        public async Task<Unit> Handle(LogoutCommand request, CancellationToken cancellationToken)
        {
            await _tokenManager.RevokeRefreshTokenAsync(request.RefreshToken);

            return Unit.Value;
        }
    }
}
