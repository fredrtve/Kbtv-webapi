using MediatR;

namespace BjBygg.Application.Identity.Commands.UserIdentityCommands.Login
{
    public class LoginCommand : IRequest<LoginResponse>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
