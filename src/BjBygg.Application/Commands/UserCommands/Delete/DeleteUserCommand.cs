using MediatR;

namespace BjBygg.Application.Commands.UserCommands.Delete
{
    public class DeleteUserCommand : IRequest<bool>
    {
        public string UserName { get; set; }
    }
}
