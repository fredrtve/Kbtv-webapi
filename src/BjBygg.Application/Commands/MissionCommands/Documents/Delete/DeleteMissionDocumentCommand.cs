using MediatR;

namespace BjBygg.Application.Commands.MissionCommands.Documents.Delete
{
    public class DeleteMissionDocumentCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }
}
