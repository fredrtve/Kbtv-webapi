using BjBygg.Application.Application.Common.Interfaces;
using BjBygg.Application.Identity.Common;
using BjBygg.Application.Identity.Common.Interfaces;
using BjBygg.Application.Identity.Common.Models;
using BjBygg.Application.Identity.Queries.UserQueries.UserByUserName;
using BjBygg.WebApi;
using CleanArchitecture.Infrastructure.Data;
using CleanArchitecture.Infrastructure.Identity;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Application.IntegrationTests
{
    [SetUpFixture]
    public class Testing
    {
        private static IConfigurationRoot _configuration;
        private static IServiceScopeFactory _scopeFactory;

        [OneTimeSetUp]
        public async Task RunBeforeAnyTests()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true)
                .AddEnvironmentVariables();

            _configuration = builder.Build();

            var startup = new Startup(_configuration);

            var services = new ServiceCollection();

            services.AddSingleton(Mock.Of<IWebHostEnvironment>(w =>
                w.EnvironmentName == "Development" &&
                w.ApplicationName == "BjBygg.WebApi"));

            services.AddLogging();

            startup.ConfigureServices(services);

            var dbDescriptor = services.SingleOrDefault(
                d => d.ServiceType == typeof(DbContextOptions<AppDbContext>));

            services.Remove(dbDescriptor);

            services.AddDbContext<AppDbContext>(options =>
                     options.UseSqlite("Data Source=test.sqlite"));

            var identityDescriptor = services.SingleOrDefault(
                d => d.ServiceType == typeof(DbContextOptions<AppIdentityDbContext>));

            services.Remove(identityDescriptor);

            services.AddDbContext<AppIdentityDbContext>(options =>
                options.UseSqlite("Data Source=testidentity.sqlite"));

            services.AddSingleton<TestSeederCount>();

            _scopeFactory = services.BuildServiceProvider().GetService<IServiceScopeFactory>();

            await EnsureAppIdentityDb();
        }


        public static async Task EnsureAppIdentityDb()
        {
            using var scope = _scopeFactory.CreateScope();

            var idContext = scope.ServiceProvider.GetService<IAppIdentityDbContext>();
            var userManager = scope.ServiceProvider.GetService<UserManager<ApplicationUser>>();
            var roleManager = scope.ServiceProvider.GetService<RoleManager<IdentityRole>>();
            idContext.Database.EnsureDeleted();
            idContext.Database.Migrate();

            await AppIdentityDbContextSeed.SeedAsync(userManager, roleManager, idContext);
        }

        public static async Task EnsureAppDbAsync()
        {
            using var scope = _scopeFactory.CreateScope();

            var context = scope.ServiceProvider.GetService<IAppDbContext>();
            context.Database.EnsureDeleted();
            context.Database.Migrate();
            var seederCount = scope.ServiceProvider.GetService<TestSeederCount>();
            await TestSeeder.SeedAllAsync(context, seederCount);
        }

        public static Dictionary<Type, int> GetSeederCount()
        {
            using var scope = _scopeFactory.CreateScope();
            return scope.ServiceProvider.GetService<TestSeederCount>().SeedCounts;
        }

        public static async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request)
        {
            using var scope = _scopeFactory.CreateScope();

            var mediator = scope.ServiceProvider.GetService<IMediator>();

            return await mediator.Send(request);
        }

        public static async Task<UserDto> GetUser(string userName)
        {
            using var scope = _scopeFactory.CreateScope();

            var mediator = scope.ServiceProvider.GetService<IMediator>();

            return await mediator.Send(new UserByUserNameQuery() { UserName = userName });
        }

        public static async Task<ApplicationUser> RunAsUserAsync(string userName, string password)
        {
            using var scope = _scopeFactory.CreateScope();

            var userManager = scope.ServiceProvider.GetService<UserManager<ApplicationUser>>();

            var user = new ApplicationUser { UserName = userName, Email = userName };

            var result = await userManager.CreateAsync(user, password);

            return user;
        }

        public static async Task<TEntity> FindAsync<TEntity>(int id)
            where TEntity : class
        {
            using var scope = _scopeFactory.CreateScope();

            var context = scope.ServiceProvider.GetService<IAppDbContext>();

            return await context.Set<TEntity>().FindAsync(id);
        }

        public static async Task AddAsync<TEntity>(TEntity entity)
            where TEntity : class
        {
            using var scope = _scopeFactory.CreateScope();

            var context = scope.ServiceProvider.GetService<IAppDbContext>();

            context.Set<TEntity>().Add(entity);

            await context.SaveChangesAsync();
        }
        public static async Task<List<TEntity>> GetAllAsync<TEntity>()
            where TEntity : class
        {
            using var scope = _scopeFactory.CreateScope();

            var context = scope.ServiceProvider.GetService<IAppDbContext>();

            return await context.Set<TEntity>().ToListAsync();
        }

        [OneTimeTearDown]
        public void RunAfterAnyTests()
        {
        }
    }
}
