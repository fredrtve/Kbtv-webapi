using MediatR;

namespace BjBygg.Application.Commands.MissionCommands.Images.Delete
{
    public class DeleteMissionReportCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }
}
