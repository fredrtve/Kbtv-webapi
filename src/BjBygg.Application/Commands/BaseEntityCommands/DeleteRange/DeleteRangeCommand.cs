using CleanArchitecture.Core.Entities;
using MediatR;
using System.Collections.Generic;

namespace BjBygg.Application.Commands.BaseEntityCommands.DeleteRange
{
    public abstract class DeleteRangeCommand : IRequest<bool>
    {
        public IEnumerable<int> Ids { get; set; }
    }
}
