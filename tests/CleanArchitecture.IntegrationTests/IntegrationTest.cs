using BjBygg.WebApi;
using CleanArchitecture.Infrastructure.Data;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using BjBygg.Application.Commands.IdentityCommands.Login;
using Newtonsoft.Json;
using CleanArchitecture.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using System.Data.Common;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;

namespace CleanArchitecture.IntegrationTests
{
    public class IntegrationTest
    {
        protected readonly HttpClient TestClient;
        private readonly DbConnection _appConnection;
        private readonly DbConnection _identityConnection;

        //protected IntegrationTest()
        //{
        //    var appOptions = new DbContextOptionsBuilder<AppDbContext>().UseSqlite(CreateInMemoryDatabase()).Options;
        //    var identityOptions = new DbContextOptionsBuilder().UseSqlite(CreateInMemoryDatabase()).Options;

        //    var appFactory = new WebApplicationFactory<Startup>()
        //        .WithWebHostBuilder(builder =>
        //        {
        //            builder.ConfigureServices(services =>
        //            {
        //                services.RemoveAll(typeof(AppDbContext));
        //                services.AddDbContext<AppDbContext>().AddOptions<DbContextOptions>(appOptions);
        //                services.AddDbContext<AppIdentityDbContext>(identityOptions)    ; 
        //            });
        //        });

        //    //_connection = RelationalOptionsExtension.Extract(ContextOptions).Connection;
        //    _appConnection = RelationalOptionsExtension.Extract(appOptions).Connection;
        //    using (var scope = appFactory.Services.CreateScope())
        //    {
        //        var services = scope.ServiceProvider;

        //        using (var context = services.GetService<AppDbContext>())
        //        {
        //            context.Database.EnsureCreated();
        //            context.Database.Migrate();
        //            AppDbContextSeed.Seed(context, 5);
        //        }

        //        using (var context = services.GetService<AppIdentityDbContext>())
        //        {
        //            context.Database.EnsureCreated();
        //            context.Database.Migrate();
        //            var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
        //            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        //            AppIdentityDbContextSeed.SeedAsync(userManager, roleManager);
        //        }
        //    }

        //    TestClient = appFactory.CreateClient();
        //}

        //protected async Task AuthenticateAsync()
        //{
        //    TestClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", await GetJwtAsync());
        //}

        //private async Task<string> GetJwtAsync()
        //{
        //    var command = new LoginCommand()
        //    {
        //        UserName = "leder",
        //        Password = "passord1"
        //    };

        //    var response = await TestClient.PostAsync("login", ContentHelper.GetStringContent(command));

        //    var responseValue = await response.Content.ReadAsStringAsync();
        //    var loginResponse = JsonConvert.DeserializeObject<LoginResponse>(responseValue);

        //    return loginResponse?.AccessToken?.Token;
        //}

        //private static DbConnection CreateInMemoryDatabase()
        //{
        //    var connection = new SqliteConnection("Filename=:memory:");

        //    connection.Open();

        //    return connection;
        //}

        //public void Dispose(){
        //    _appConnection.Dispose();
        //    _identityConnection.Dispose();
        //}

    }
}
