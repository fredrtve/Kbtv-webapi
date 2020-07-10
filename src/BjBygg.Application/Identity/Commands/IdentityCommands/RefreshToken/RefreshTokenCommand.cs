using MediatR;

namespace BjBygg.Application.Identity.Commands.IdentityCommands.RefreshToken
{
    public class RefreshTokenCommand : IRequest<RefreshTokenResponse>
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public string SigningKey { get; set; }

        public RefreshTokenCommand() { }
    }
}
