using MediatR;

namespace BjBygg.Application.Commands.IdentityCommands.Login
{
    public class LoginCommand : IRequest<LoginResponse>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
