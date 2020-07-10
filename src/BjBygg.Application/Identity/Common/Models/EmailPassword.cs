using CleanArchitecture.SharedKernel;

namespace BjBygg.Application.Identity.Common.Models
{
    public class InboundEmailPassword : BaseEntity, ITrackable
    {
        public string Password { get; set; }
    }
}
