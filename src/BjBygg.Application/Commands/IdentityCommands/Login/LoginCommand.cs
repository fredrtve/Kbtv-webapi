using MediatR;
using System.ComponentModel.DataAnnotations;

namespace BjBygg.Application.Commands.IdentityCommands.Login
{
    public class LoginCommand : IRequest<LoginResponse>
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
