using MediatR;

namespace BjBygg.Application.Identity.Commands.IdentityCommands.Logout
{
    public class LogoutCommand : IRequest<Unit>
    {
        public string RefreshToken { get; set; }
    }
}
