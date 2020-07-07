using MediatR;

namespace BjBygg.Application.Commands.BaseEntityCommands.Create
{
    public abstract class CreateCommand<TResponse> : IRequest<TResponse>
    {

    }
}
