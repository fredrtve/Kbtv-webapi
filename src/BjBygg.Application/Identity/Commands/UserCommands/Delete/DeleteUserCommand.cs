using BjBygg.Application.Common.Interfaces;
using MediatR;

namespace BjBygg.Application.Identity.Commands.UserCommands.Delete
{
    public class DeleteUserCommand : IOptimisticCommand
    {
        public string UserName { get; set; }
    }
}
