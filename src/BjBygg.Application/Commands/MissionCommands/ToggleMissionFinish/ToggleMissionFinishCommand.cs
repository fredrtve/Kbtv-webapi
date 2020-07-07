using MediatR;

namespace BjBygg.Application.Commands.MissionCommands.ToggleMissionFinish
{
    public class ToggleMissionFinishCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }
}
