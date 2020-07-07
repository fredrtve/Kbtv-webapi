using CleanArchitecture.Infrastructure.Data;
using CleanArchitecture.Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Infrastructure
{
    public static class StartupSetup
    {
        public static void AddAppDbContext(this IServiceCollection services) =>
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlite("Data Source=data/db/main/maindb.sqlite")); // will be created in web project root

        public static void AddIdentityDbContext(this IServiceCollection services) =>
            services.AddDbContext<AppIdentityDbContext>(options =>
                options.UseSqlite("Data Source=data/db/identity/identitydb.sqlite")); // will be created in web project root

    }
}
