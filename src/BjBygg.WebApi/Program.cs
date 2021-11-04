using BjBygg.Application.Application.Common;
using BjBygg.Application.Application.Common.Interfaces;
using BjBygg.Application.Common.Interfaces;
using BjBygg.Application.Identity.Common.Models;
using BjBygg.Core;
using BjBygg.Core.Entities;
using BjBygg.Infrastructure.Api.FileStorage;
using BjBygg.Infrastructure.Data;
using BjBygg.Infrastructure.Identity;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.IO;
using System.Linq;
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

                    if (await context.Activities.FindAsync("default") == null)
                    {
                        context.Add(new Activity() { Id = "default", Name = "Annet" });
                    }

                    if (await context.GetLeaderSettingsAsync() == null)
                    {
                        context.Add(new LeaderSettings() { Id = "settings", ConfirmTimesheetsMonthly = false });

                    }

                    await context.SaveChangesAsync();
                }

                using (var context = services.GetService<AppIdentityDbContext>())
                {
                    context.Database.EnsureCreated();
                    var userManager = services.GetService<UserManager<ApplicationUser>>();
                    var roleManager = services.GetService<RoleManager<IdentityRole>>();
                    var authSettings = services.GetService<IOptions<AuthSettings>>().Value;
                    await AppIdentityDbContextSeed.SeedAsync(userManager, roleManager, authSettings);
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
