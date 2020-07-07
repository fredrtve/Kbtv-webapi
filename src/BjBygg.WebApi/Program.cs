using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
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
            //        AppDbContextSeed.Seed(context, 1500);
            //    }

            //    //using (var context = services.GetService<AppIdentityDbContext>())
            //    //{

            //    //    context.Database.Migrate();

            //    //    var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
            //    //    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

            //    //    AppIdentityDbContextSeed.SeedAsync(userManager, roleManager, context);
            //    //}
            //}

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
