using MediatR;

namespace BjBygg.Application.Commands.BaseEntityCommands.Delete
{
    public abstract class DeleteCommand : IRequest
    {
        public int Id { get; set; }
    }
}
