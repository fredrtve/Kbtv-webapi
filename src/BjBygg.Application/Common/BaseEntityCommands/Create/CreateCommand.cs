using BjBygg.Application.Common.Interfaces;
using MediatR;

namespace BjBygg.Application.Common.BaseEntityCommands.Create
{
    public abstract class CreateCommand : IOptimisticCommand
    {
        public string Id { get; set; }
    }
}
