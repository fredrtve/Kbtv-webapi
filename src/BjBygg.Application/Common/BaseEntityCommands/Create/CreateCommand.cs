using MediatR;

namespace BjBygg.Application.Common.BaseEntityCommands.Create
{
    public abstract class CreateCommand : IRequest
    {
        public string Id { get; set; }
    }
}
