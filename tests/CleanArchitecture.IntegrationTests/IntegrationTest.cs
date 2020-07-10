using System.Data.Common;
using System.Net.Http;

namespace CleanArchitecture.IntegrationTests
{
    public class IntegrationTest
    {
        protected readonly HttpClient TestClient;
        private readonly DbConnection _appConnection;
        private readonly DbConnection _identityConnection;

        //protected IntegrationTest()
        //{
        //    var appOptions = new DbContextOptionsBuilder<IAppDbContext>().UseSqlite(CreateInMemoryDatabase()).Options;
        //    var identityOptions = new DbContextOptionsBuilder().UseSqlite(CreateInMemoryDatabase()).Options;

        //    var appFactory = new WebApplicationFactory<Startup>()
        //        .WithWebHostBuilder(builder =>
        //        {
        //            builder.ConfigureServices(services =>
        //            {
        //                services.RemoveAll(typeof(IAppDbContext));
        //                services.AddDbContext<IAppDbContext>().AddOptions<DbContextOptions>(appOptions);
        //                services.AddDbContext<IAppIdentityDbContext>(identityOptions)    ; 
        //            });
        //        });

        //    //_connection = RelationalOptionsExtension.Extract(ContextOptions).Connection;
        //    _appConnection = RelationalOptionsExtension.Extract(appOptions).Connection;
        //    using (var scope = appFactory.Services.CreateScope())
        //    {
        //        var services = scope.ServiceProvider;

        //        using (var context = services.GetService<IAppDbContext>())
        //        {
        //            context.Database.EnsureCreated();
        //            context.Database.Migrate();
        //            IAppDbContextSeed.Seed(context, 5);
        //        }

        //        using (var context = services.GetService<IAppIdentityDbContext>())
        //        {
        //            context.Database.EnsureCreated();
        //            context.Database.Migrate();
        //            var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
        //            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        //            IAppIdentityDbContextSeed.SeedAsync(userManager, roleManager);
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
