using BjBygg.Application.Common.Interfaces;
using BjBygg.Application.Identity.Common;
using BjBygg.Application.Identity.Common.Interfaces;
using BjBygg.Application.Identity.Common.Models;
using BjBygg.Application.Identity.Queries.UserQueries.UserByUserName;
using BjBygg.WebApi;
using BjBygg.Infrastructure.Identity;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Application.IntegrationTests.Identity
{
    [SetUpFixture]
    public class IdentityTesting
    {
        private static IConfigurationRoot _configuration;
        private static IServiceScopeFactory _scopeFactory;
        private static UserDto _currentUser = new UserDto();


        [OneTimeSetUp]
        public void RunBeforeAnyTests()
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

            var identityDescriptor = services.SingleOrDefault(
                d => d.ServiceType == typeof(DbContextOptions<AppIdentityDbContext>));

            services.Remove(identityDescriptor);

            services.AddDbContext<AppIdentityDbContext>(options =>
                options.UseSqlite("Data Source=testidentity.sqlite"));

            services.AddScoped<IAppIdentityDbContext>(provider => provider.GetService<AppIdentityDbContext>());

            MockUserService(services);

            _scopeFactory = services.BuildServiceProvider().GetService<IServiceScopeFactory>();
        }
        public static async Task EnsureAppIdentityDb()
        {
            using var scope = _scopeFactory.CreateScope();

            var idContext = scope.ServiceProvider.GetService<IAppIdentityDbContext>();
            idContext.Database.EnsureDeleted();
            idContext.Database.Migrate();

            var roleManager = scope.ServiceProvider.GetService<RoleManager<IdentityRole>>();
            await TestIdentitySeeder.SeedRolesAsync(roleManager);
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
        public static async Task<ApplicationUser> RunAsDefaultUserAsync(string role)
        {
            return await RunAsUserAsync(role, "test1234", role);
        }
        public static async Task<ApplicationUser> RunAsUserAsync(string userName, string password, string role = null)
        {
            using var scope = _scopeFactory.CreateScope();

            var userManager = scope.ServiceProvider.GetService<UserManager<ApplicationUser>>();

            var user = new ApplicationUser { UserName = userName, Email = userName };

            var result = await userManager.CreateAsync(user, password);

            if (role != null)
                userManager.AddToRoleAsync(user, role).Wait();

            _currentUser = new UserDto { UserName = user.UserName, Role = role };

            return user;
        }
        public static async Task<TEntity> FindAsync<TEntity>(object id)
            where TEntity : class
        {
            using var scope = _scopeFactory.CreateScope();

            var context = scope.ServiceProvider.GetService<IAppIdentityDbContext>();

            return await context.Set<TEntity>().FindAsync(id);
        }
        public static async Task<ApplicationUser> FindUserByUserNameAsync(string userName)
        {
            using var scope = _scopeFactory.CreateScope();

            var userManager = scope.ServiceProvider.GetService<UserManager<ApplicationUser>>();

            return await userManager.FindByNameAsync(userName);
        }
        public static async Task<string> GetUserRole(ApplicationUser user)
        {
            using var scope = _scopeFactory.CreateScope();

            var userManager = scope.ServiceProvider.GetService<UserManager<ApplicationUser>>();

            return (await userManager.GetRolesAsync(user)).FirstOrDefault();
        }
        public static ClaimsPrincipal GetPrincipalFromAccessToken(string accessToken)
        {
            using var scope = _scopeFactory.CreateScope();

            var jwtTokenValidator = scope.ServiceProvider.GetService<IJwtTokenValidator>();
            var authOptions = scope.ServiceProvider.GetService<IOptions<AuthSettings>>().Value;

            var principal = jwtTokenValidator.GetPrincipalFromToken(accessToken, authOptions.SecretKey);

            return principal;
        }
        public static AuthSettings GetAuthOptions()
        {
            using var scope = _scopeFactory.CreateScope();

            var jwtTokenValidator = scope.ServiceProvider.GetService<IJwtTokenValidator>();

            return scope.ServiceProvider.GetService<IOptions<AuthSettings>>().Value;
        }
        public static async Task AddAsync<TEntity>(TEntity entity)
            where TEntity : class
        {
            using var scope = _scopeFactory.CreateScope();

            var context = scope.ServiceProvider.GetService<IAppIdentityDbContext>();

            context.Set<TEntity>().Add(entity);

            await context.SaveChangesAsync();
        }
        public static async Task<bool> CheckUserPassword(ApplicationUser user, string password)
        {
            using var scope = _scopeFactory.CreateScope();

            var userManager = scope.ServiceProvider.GetService<UserManager<ApplicationUser>>();

            return await userManager.CheckPasswordAsync(user, password);
        }

        public static async Task<List<TEntity>> GetAllAsync<TEntity>()
            where TEntity : class
        {
            using var scope = _scopeFactory.CreateScope();

            var context = scope.ServiceProvider.GetService<IAppIdentityDbContext>();

            return await context.Set<TEntity>().ToListAsync();
        }
        private static ServiceCollection MockUserService(ServiceCollection services)
        {
            var currentUserServiceDescriptor = services.FirstOrDefault(d =>
                d.ServiceType == typeof(ICurrentUserService));

            services.Remove(currentUserServiceDescriptor);

            services.AddTransient(provider =>
                Mock.Of<ICurrentUserService>(s => s.Role == _currentUser.Role && s.UserName == _currentUser.UserName));

            return services;
        }
        [OneTimeTearDown]
        public void RunAfterAnyTests()
        {
        }
    }
}
