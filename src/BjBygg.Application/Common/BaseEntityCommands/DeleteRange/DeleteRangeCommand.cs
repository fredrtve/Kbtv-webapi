using BjBygg.Application.Common.Interfaces;
using System.Collections.Generic;

namespace BjBygg.Application.Common.BaseEntityCommands.DeleteRange
{
    public abstract class DeleteRangeCommand : IOptimisticCommand
    {
        public IEnumerable<string> Ids { get; set; }
    }
}
