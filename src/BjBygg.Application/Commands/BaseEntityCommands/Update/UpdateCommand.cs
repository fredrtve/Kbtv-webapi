using MediatR;

namespace BjBygg.Application.Commands.BaseEntityCommands.Update
{
    public abstract class UpdateCommand<TResponse> : IRequest<TResponse>
    {
        public int Id { get; set; }
    }
}
