using MediatR;

namespace BjBygg.Application.Common.BaseEntityCommands.Create
{
    public abstract class CreateCommand<TResponse> : IRequest<TResponse>
    {

    }
}
