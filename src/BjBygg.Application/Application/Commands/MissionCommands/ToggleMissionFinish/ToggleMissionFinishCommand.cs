using MediatR;

namespace BjBygg.Application.Application.Commands.MissionCommands.ToggleMissionFinish
{
    public class ToggleMissionFinishCommand : IRequest
    {
        public string Id { get; set; }
    }
}
