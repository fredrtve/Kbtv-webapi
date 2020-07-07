using CleanArchitecture.SharedKernel;

namespace CleanArchitecture.Infrastructure.Identity
{
    public class InboundEmailPassword : BaseEntity, ITrackable
    {
        public string Password { get; set; }
    }
}
