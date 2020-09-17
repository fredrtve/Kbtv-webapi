using BjBygg.Application.Common.Interfaces;

namespace BjBygg.Application.Common.BaseEntityCommands.Update
{
    public abstract class UpdateCommand : IOptimisticCommand
    {
        public string Id { get; set; }
    }
}
