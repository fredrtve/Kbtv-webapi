using MediatR;
using System.ComponentModel.DataAnnotations;

namespace BjBygg.Application.Commands.InboundEmailPasswordCommands.Verify
{
    public class VerifyInboundEmailPasswordCommand : IRequest<bool>
    {
        [Required(ErrorMessage = "{0} må fylles ut.")]
        [Display(Name = "Passord")]
        public string Password { get; set; }
    }
}
