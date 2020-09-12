using MediatR;

namespace BjBygg.Application.Common.BaseEntityCommands.Delete
{
    public abstract class DeleteCommand : IRequest
    {
        public string Id { get; set; }
    }
}
