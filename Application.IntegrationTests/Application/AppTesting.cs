﻿using BjBygg.Application.Application.Common.Interfaces;
using BjBygg.Application.Common.Interfaces;
using BjBygg.Application.Identity.Common;
using BjBygg.Application.Identity.Common.Interfaces;
using BjBygg.Application.Identity.Common.Models;
using BjBygg.Application.Identity.Queries.UserQueries.UserByUserName;
using BjBygg.Core.Entities;
using BjBygg.Infrastructure.Data;
using BjBygg.Infrastructure.Identity;
using BjBygg.SharedKernel;
using BjBygg.WebApi;
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
            var projectDir = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
            var builder = new ConfigurationBuilder()
                .SetBasePath(projectDir)
                .AddJsonFile("appsettings.json", false, true)
                .AddEnvironmentVariables();

            _configuration = builder.Build();

            var startup = new Startup(_configuration);
          
            var services = new ServiceCollection();

            services.AddSingleton(Mock.Of<IWebHostEnvironment>(w =>
                w.EnvironmentName == "Development" &&
                w.ApplicationName == "BjBygg.WebApi"));

            services.AddLogging();

            startup.ConfigureServices(services); 

            MockUserService(services);

            MockBlobStorage(services);

            MockMailService(services);

            MockFileZipper(services);

            MockGeocodeService(services);

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
        public static void EnsureAppDb()
        {
            using var scope = _scopeFactory.CreateScope();

            var context = scope.ServiceProvider.GetService<IAppDbContext>();
            context.Database.EnsureCreated();

            new List<string>() {
                "MissionImages", "MissionDocuments", "MissionNotes", "Timesheets", "MissionActivities",
                 "Missions", "EmployerUsers", "Employers", "Activities", "LeaderSettings", "UserCommandStatuses"         
            }.ForEach(table => {
                context.Database.BeginTransaction();
                context.Database.ExecuteSqlRaw($"DELETE FROM {table}");
                context.Database.CommitTransaction();
            });

            context.Set<Activity>().Add(new Activity() { Id = "default", Name = "Annet" });
            context.SaveChanges();
        }

        public static async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request)
        {
            using var scope = _scopeFactory.CreateScope();

            var mediator = scope.ServiceProvider.GetService<IMediator>();

            return await mediator.Send(request);
        }
        public static async Task SaveChangesAsync()
        {
            using var scope = _scopeFactory.CreateScope();

            var context = scope.ServiceProvider.GetService<IAppDbContext>();

            await context.SaveChangesAsync();
        }
        public static async Task<UserDto> GetUser(string userName)
        {
            using var scope = _scopeFactory.CreateScope();

            var mediator = scope.ServiceProvider.GetService<IMediator>();

            return await mediator.Send(new UserByUserNameQuery() { UserName = userName });
        }
        public static async Task<UserDto> RunAsDefaultUserAsync(string role, string employerId = null)
        {
            return await RunAsUserAsync(role, "test1234", role, employerId);
        }
        public static async Task<UserDto> RunAsUserAsync(string userName, string password, string role, string employerId = null)
        {
            using var scope = _scopeFactory.CreateScope();

            var userManager = scope.ServiceProvider.GetService<UserManager<ApplicationUser>>();

            var user = new ApplicationUser { UserName = userName, Email = userName };

            var result = await userManager.CreateAsync(user, password);

            userManager.AddToRoleAsync(user, role).Wait();

            _currentUser = new UserDto { UserName = user.UserName, Role = role };

            if (employerId != null)
            {
                var dbContext = scope.ServiceProvider.GetService<IAppDbContext>();
                dbContext.EmployerUsers.Add(new EmployerUser() { Id = "test", UserName = user.UserName, EmployerId = employerId });
                dbContext.SaveChanges();
            }

            return _currentUser;
        }
        public static async Task<TEntity> FindAsync<TEntity>(string id)
            where TEntity : class
        {
            using var scope = _scopeFactory.CreateScope();

            var context = scope.ServiceProvider.GetService<IAppDbContext>();

            return await context.Set<TEntity>().FindAsync(id);
        }
        public static async Task RemoveAsync<TEntity>(TEntity entity)
             where TEntity : class
        {
            using var scope = _scopeFactory.CreateScope();

            var context = scope.ServiceProvider.GetService<IAppDbContext>();

            context.Set<TEntity>().Remove(entity);

            await context.SaveChangesAsync();
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
        public static async Task<LeaderSettings> GetLeaderSettingsAsync()
        {
            using var scope = _scopeFactory.CreateScope();

            var context = scope.ServiceProvider.GetService<IAppDbContext>();

            return await context.GetLeaderSettingsAsync();
        }

        public static async Task AddSqlRaw(string rawSql)
        {
            using var scope = _scopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetService<IAppDbContext>();
            await context.Database.ExecuteSqlRawAsync(rawSql);
        }

        private static ServiceCollection MockBlobStorage(ServiceCollection services)
        {
            var blobStorageServiceDescriptor = services.FirstOrDefault(d =>
                d.ServiceType == typeof(IBlobStorageService));

            services.Remove(blobStorageServiceDescriptor);

            Mock<IBlobStorageService> mockClient = new Mock<IBlobStorageService>();

            mockClient.Setup(x => x.UploadFileAsync(It.IsAny<Stream>(), It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(new Uri("https://test.no"));

            services.AddTransient(provider => mockClient.Object);

            return services;
        }

        private static ServiceCollection MockMailService(ServiceCollection services)
        {
            var serviceDescriptor = services.FirstOrDefault(d =>
                d.ServiceType == typeof(IMailService));

            services.Remove(serviceDescriptor);

            services.AddTransient(provider => Mock.Of<IMailService>());

            return services;
        }

        private static ServiceCollection MockUserService(ServiceCollection services)
        {
            var serviceDescriptor = services.FirstOrDefault(d =>
                d.ServiceType == typeof(ICurrentUserService));

            services.Remove(serviceDescriptor);

            services.AddTransient(provider =>
                Mock.Of<ICurrentUserService>(s => s.Role == _currentUser.Role && s.UserName == _currentUser.UserName));

            return services;
        }
        private static ServiceCollection MockFileZipper(ServiceCollection services)
        {
            var serviceDescriptor = services.FirstOrDefault(d =>
                d.ServiceType == typeof(IFileZipper));

            services.Remove(serviceDescriptor);

            services.AddTransient(provider =>
                Mock.Of<IFileZipper>());

            return services;
        }

        private static ServiceCollection MockGeocodeService(ServiceCollection services)
        {
            var serviceDescriptor = services.FirstOrDefault(d =>
                d.ServiceType == typeof(IGeocodeService));

            services.Remove(serviceDescriptor);

            services.AddTransient(provider =>
                Mock.Of<IGeocodeService>());

            return services;
        }
        
        [OneTimeTearDown]
        public void RunAfterAnyTests()
        {
        }
    }
}
