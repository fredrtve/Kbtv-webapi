using MediatR;

namespace BjBygg.Application.Identity.Commands.UserCommands.Delete
{
    public class DeleteUserCommand : IRequest
    {
        public string UserName { get; set; }
    }
}
