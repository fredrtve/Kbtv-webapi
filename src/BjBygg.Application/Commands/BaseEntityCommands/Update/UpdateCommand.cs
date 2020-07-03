using MediatR;
using System.ComponentModel.DataAnnotations;

namespace BjBygg.Application.Commands.BaseEntityCommands.Update
{
    public class UpdateCommand<TResponse> : IRequest<TResponse>
    {
        [Required]
        public int Id { get; set; }
    }
}
