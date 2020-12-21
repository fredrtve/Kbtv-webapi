using BjBygg.Application.Common.Interfaces;

namespace BjBygg.Application.Common.BaseEntityCommands.Delete
{
    public abstract class DeleteCommand : IOptimisticCommand
    {
        public string Id { get; set; }
    }
}
