using MediatR;

namespace BjBygg.Application.Identity.Commands.UserIdentityCommands.Logout
{
    public class LogoutCommand : IRequest<Unit>
    {
        public string RefreshToken { get; set; }
    }
}
