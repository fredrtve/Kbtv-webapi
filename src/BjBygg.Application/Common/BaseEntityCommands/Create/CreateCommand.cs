using BjBygg.Application.Common.Interfaces;

namespace BjBygg.Application.Common.BaseEntityCommands.Create
{
    public abstract class CreateCommand : IOptimisticCommand
    {
        public string Id { get; set; }
    }
}
