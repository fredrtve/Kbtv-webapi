using MediatR;
using System.ComponentModel.DataAnnotations;

namespace BjBygg.Application.Commands.BaseEntityCommands.Delete
{
    public abstract class DeleteCommand : IRequest<bool>
    {
        [Required]
        public int Id { get; set; }
    }
}
