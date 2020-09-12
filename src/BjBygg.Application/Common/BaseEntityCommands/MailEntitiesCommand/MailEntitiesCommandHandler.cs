using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BjBygg.Application.Common.BaseEntityCommands.MailEntitiesCommand
{
    public abstract class MailEntitiesCommandHandler<TCommand> : IRequestHandler<TCommand>
        where TCommand : MailEntitiesCommand
    {
        public MailEntitiesCommandHandler(){}

        public virtual async Task<Unit> Handle(TCommand request, CancellationToken cancellationToken)
        {
            return await Task.FromResult(Unit.Value);
        }
    }
}