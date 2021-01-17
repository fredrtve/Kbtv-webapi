using MediatR;

namespace BjBygg.Application.Identity.Commands.UserIdentityCommands.RefreshToken
{
    public class RefreshTokenCommand : IRequest<RefreshTokenResponse>
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }

        public RefreshTokenCommand()
        {
        }
    }
}