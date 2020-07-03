using MediatR;
using System.ComponentModel.DataAnnotations;

namespace BjBygg.Application.Commands.MissionCommands.Images.Delete
{
    public class DeleteMissionImageCommand : IRequest<bool>
    {
        [Required]
        public int Id { get; set; }
    }
}
