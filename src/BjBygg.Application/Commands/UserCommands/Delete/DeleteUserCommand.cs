using MediatR;

namespace BjBygg.Application.Commands.UserCommands.Delete
{
    public class DeleteUserCommand : IRequest
    {
        public string UserName { get; set; }
    }
}
