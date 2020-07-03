using MediatR;
using System.ComponentModel.DataAnnotations;

namespace BjBygg.Application.Commands.DocumentTypeCommands.Delete
{
    public class DeleteDocumentTypeCommand : IRequest<bool>
    {
        [Required]
        public int Id { get; set; }
    }
}
