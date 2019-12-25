using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace CleanArchitecture.Infrastructure.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
