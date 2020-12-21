using BjBygg.Application.Common.Interfaces;

namespace BjBygg.Application.Application.Commands.MissionCommands.ToggleMissionFinish
{
    public class ToggleMissionFinishCommand : IOptimisticCommand
    {
        public string Id { get; set; }
    }
}
