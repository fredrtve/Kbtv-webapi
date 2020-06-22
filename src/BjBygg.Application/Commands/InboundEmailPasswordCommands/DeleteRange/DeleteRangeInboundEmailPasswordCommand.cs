using MediatR;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BjBygg.Application.Commands.InboundEmailPasswordCommands.DeleteRange
{
    public class DeleteRangeInboundEmailPasswordCommand : IRequest<bool>
    {
        [Required(ErrorMessage = "{0} må fylles ut.")]
        [Display(Name = "Ids")]
        public IEnumerable<int> Ids { get; set; }
    }
}
