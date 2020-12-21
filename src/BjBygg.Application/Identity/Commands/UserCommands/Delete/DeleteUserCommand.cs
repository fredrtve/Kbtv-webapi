using BjBygg.Application.Common.Interfaces;

namespace BjBygg.Application.Identity.Commands.UserCommands.Delete
{
    public class DeleteUserCommand : IOptimisticCommand
    {
        public string UserName { get; set; }
    }
}
