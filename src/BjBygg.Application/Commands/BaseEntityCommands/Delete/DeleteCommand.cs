using MediatR;

namespace BjBygg.Application.Commands.BaseEntityCommands.Delete
{
    public abstract class DeleteCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }
}
