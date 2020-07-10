using BjBygg.Application.Common.Interfaces;
using BjBygg.Application.Identity.Common.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BjBygg.Application.Identity.Common.Interfaces
{
    public interface IAppIdentityDbContext : IDbContext
    {
        DbSet<RefreshToken> RefreshTokens { get; set; }
        DbSet<InboundEmailPassword> InboundEmailPasswords { get; set; }
        DbSet<ApplicationUser> Users { get; set; }
        DbSet<IdentityRole> Roles { get; set; }
    }
}
