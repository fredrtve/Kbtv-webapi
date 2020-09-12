using MediatR;

namespace BjBygg.Application.Common.BaseEntityCommands.Update
{
    public abstract class UpdateCommand : IRequest
    {
        public string Id { get; set; }
    }
}
