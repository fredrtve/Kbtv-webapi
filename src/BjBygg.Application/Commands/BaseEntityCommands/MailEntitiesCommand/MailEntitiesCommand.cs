using MediatR;
using System.Collections.Generic;

namespace BjBygg.Application.Commands.BaseEntityCommands.MailEntitiesCommand
{
    public abstract class MailEntitiesCommand : IRequest
    {
        public string ToEmail { get; set; }
        public IEnumerable<int> Ids { get; set; }
    }
}
