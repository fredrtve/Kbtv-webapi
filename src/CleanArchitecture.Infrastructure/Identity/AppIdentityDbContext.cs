using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using CleanArchitecture.SharedKernel;
using Microsoft.AspNetCore.Http;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Infrastructure.Identity
{
    public class AppIdentityDbContext : IdentityDbContext<ApplicationUser>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options, IHttpContextAccessor httpContextAccessor)
            : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<ApplicationUser>().HasMany(p => p.Roles).WithOne().HasForeignKey(p => p.UserId).IsRequired();
            builder.Entity<ApplicationUser>().Ignore(p => p.Role);
        }
        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            OnBeforeSaving();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            OnBeforeSaving();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        private void OnBeforeSaving()
        {
            var now = DateTime.Now;
            var user = GetCurrentUser();

            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.Entity is ITrackable trackable)
                {
                    switch (entry.State)
                    {
                        case EntityState.Modified:
                            trackable.UpdatedAt = now;
                            trackable.UpdatedBy = user;
                            entry.Property("CreatedBy").IsModified = false;
                            entry.Property("CreatedAt").IsModified = false;
                            break;
                        case EntityState.Added:
                            trackable.CreatedAt = now;
                            trackable.UpdatedAt = now;
                            trackable.CreatedBy = user;
                            trackable.Deleted = false;
                            break;
                        case EntityState.Deleted:
                            entry.State = EntityState.Modified;
                            trackable.Deleted = true;
                            trackable.UpdatedAt = now;
                            break;
                    }
                }
            }
        }

        private string GetCurrentUser()
        {
            var httpContext = _httpContextAccessor.HttpContext;
            string user = null;

            if (httpContext != null) user = httpContext.User.Identity.Name;

            return user;
        }
    }

}
