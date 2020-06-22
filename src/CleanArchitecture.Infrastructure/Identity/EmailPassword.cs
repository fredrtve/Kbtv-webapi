using CleanArchitecture.SharedKernel;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Infrastructure.Identity
{
    public class InboundEmailPassword : BaseEntity, ITrackable
    {
        public string Password { get; set; }
    }
}
