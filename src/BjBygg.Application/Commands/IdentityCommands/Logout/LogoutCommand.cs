using MediatR;

namespace BjBygg.Application.Commands.IdentityCommands.Logout
{
    public class LogoutCommand : IRequest<Unit>
    {
        public string RefreshToken { get; set; }
    }
}
