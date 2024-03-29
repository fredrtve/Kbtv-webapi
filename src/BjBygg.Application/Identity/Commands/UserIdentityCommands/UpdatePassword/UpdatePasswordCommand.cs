using MediatR;

namespace BjBygg.Application.Identity.Commands.UserIdentityCommands.UpdatePassword
{
    public class UpdatePasswordCommand : IRequest<Unit>
    {
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
