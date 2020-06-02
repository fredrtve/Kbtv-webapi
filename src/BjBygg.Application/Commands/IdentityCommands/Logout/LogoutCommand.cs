using MediatR;
using System.ComponentModel.DataAnnotations;

namespace BjBygg.Application.Commands.IdentityCommands.Logout
{
    public class LogoutCommand : IRequest<Unit>
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string RefreshToken { get; set; }
    }
}
