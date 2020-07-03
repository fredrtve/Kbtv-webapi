using MediatR;
using System.ComponentModel.DataAnnotations;

namespace BjBygg.Application.Commands.MissionCommands.Documents.Delete
{
    public class DeleteMissionDocumentCommand : IRequest<bool>
    {
        [Required]
        public int Id { get; set; }
    }
}
