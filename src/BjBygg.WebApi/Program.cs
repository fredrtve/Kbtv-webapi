using BjBygg.Application.Common.Interfaces;
using BjBygg.Application.Identity.Common.Models;
using BjBygg.Infrastructure.Data;
using BjBygg.Infrastructure.Identity;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace BjBygg.WebApi
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                using (var context = services.GetService<AppDbContext>())
                {
                    context.Database.EnsureCreated();
                    var idGenerator = services.GetService<IIdGenerator>();
                    await AppDbContextSeed.SeedAllAsync(context, idGenerator, new SeederCount());
                }

                using (var context = services.GetService<AppIdentityDbContext>())
                {

                    context.Database.EnsureCreated();

                    var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
                    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
                    var idGenerator = services.GetService<IIdGenerator>();

                    await AppIdentityDbContextSeed.SeedAsync(userManager, roleManager, context, idGenerator);
                }
            }

            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureLogging((context, logging) =>
                {
                    logging.ClearProviders();
                    logging.AddConfiguration(context.Configuration.GetSection("Logging"));
                    logging.AddDebug();
                    logging.AddConsole();
                })
                .UseStartup<Startup>();
    }
}
