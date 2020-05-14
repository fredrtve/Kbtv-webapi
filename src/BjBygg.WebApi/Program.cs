using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArchitecture.Infrastructure.Data;
using CleanArchitecture.Infrastructure.Identity;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace BjBygg.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();

            //using (var scope = host.Services.CreateScope())
            //{
            //    var services = scope.ServiceProvider;

            //    using (var context = services.GetService<AppDbContext>())
            //    {
            //        context.Database.Migrate();
            //        AppDbContextSeed.Seed(context);
            //    }

            //    using (var context = services.GetService<AppIdentityDbContext>())
            //    {

            //        context.Database.Migrate();

            //        var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
            //        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

            //        AppIdentityDbContextSeed.SeedAsync(userManager, roleManager);
            //    }
            //}

            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
