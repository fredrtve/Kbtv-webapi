using MediatR;

namespace BjBygg.Application.Common.BaseEntityCommands.Update
{
    public abstract class UpdateCommand<TResponse> : IRequest<TResponse>
    {
        public int Id { get; set; }
    }
}
