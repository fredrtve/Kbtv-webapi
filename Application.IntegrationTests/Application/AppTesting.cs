using BjBygg.Application.Application.Common.Interfaces;
using BjBygg.Application.Common;
using BjBygg.Application.Common.Interfaces;
using BjBygg.Application.Identity.Common;
using BjBygg.Application.Identity.Common.Interfaces;
using BjBygg.Application.Identity.Common.Models;
using BjBygg.Application.Identity.Queries.UserQueries.UserByUserName;
using BjBygg.WebApi;
using CleanArchitecture.Infrastructure.Data;
using CleanArchitecture.Infrastructure.Identity;
using CleanArchitecture.SharedKernel;
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

namespace Application.IntegrationTests.Application
{
    [SetUpFixture]
    public class AppTesting
    {
        private static IConfigurationRoot _configuration;
        private static IServiceScopeFactory _scopeFactory;
        private static UserDto _currentUser = new UserDto();

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

            services.AddScoped<IAppDbContext>(provider => provider.GetService<AppDbContext>());

            var identityDescriptor = services.SingleOrDefault(
                d => d.ServiceType == typeof(DbContextOptions<AppIdentityDbContext>));

            services.Remove(identityDescriptor);

            services.AddDbContext<AppIdentityDbContext>(options =>
                options.UseSqlite("Data Source=testidentity.sqlite"));

            services.AddScoped<IAppIdentityDbContext>(provider => provider.GetService<AppIdentityDbContext>());

            services.AddSingleton<TestSeederCount>();

            MockUserService(services); 

            MockBlobStorage(services);

            MockMailService(services);

            _scopeFactory = services.BuildServiceProvider().GetService<IServiceScopeFactory>();

            await EnsureAppIdentityDb();
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
        public static async Task<UserDto> RunAsDefaultUserAsync(string role)
        {
            return await RunAsUserAsync(role, "test1234", role);
        }
        public static async Task<UserDto> RunAsUserAsync(string userName, string password, string role)
        {
            using var scope = _scopeFactory.CreateScope();

            var userManager = scope.ServiceProvider.GetService<UserManager<ApplicationUser>>();

            var user = new ApplicationUser { UserName = userName, Email = userName, EmployerId = 1 };

            var result = await userManager.CreateAsync(user, password);

            userManager.AddToRoleAsync(user, role).Wait();

            _currentUser = new UserDto { UserName = user.UserName, Role = role };

            return _currentUser;
        }
        public static async Task<TEntity> FindAsync<TEntity>(int? id)
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
        private static ServiceCollection MockBlobStorage(ServiceCollection services)
        {
            var blobStorageServiceDescriptor = services.FirstOrDefault(d =>
                d.ServiceType == typeof(IBlobStorageService));

            services.Remove(blobStorageServiceDescriptor);

            Mock<IBlobStorageService> mockClient = new Mock<IBlobStorageService>();

            mockClient.Setup(x => x.UploadFileAsync(It.IsAny<BasicFileStream>(), It.IsAny<string>()))
                .ReturnsAsync(new Uri("https://test.no"));

            mockClient.Setup(x => x.UploadFilesAsync(It.IsAny<DisposableList<BasicFileStream>>(), It.IsAny<string>()))
                .ReturnsAsync((DisposableList<BasicFileStream> x, string y) => GenerateUriArray(x.Count));

            services.AddTransient(provider => mockClient.Object);

            return services;
        }
        private static ServiceCollection MockMailService(ServiceCollection services)
        {
            var mailServiceDescriptor = services.FirstOrDefault(d =>
                d.ServiceType == typeof(IMailService));

            services.Remove(mailServiceDescriptor);

            services.AddTransient(provider => Mock.Of<IMailService>());

            return services;
        }
        private static IEnumerable<Uri> GenerateUriArray(int count)
        {
            var uriArray = new Uri[count];

            for (var x = 0; x < count; x++)
            {
                uriArray[x] = new Uri("https://testuri.net");
            }

            return uriArray;
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
