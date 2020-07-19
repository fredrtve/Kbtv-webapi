using NUnit.Framework;
using System.Threading.Tasks;

namespace Application.IntegrationTests.Application
{
    using static AppTesting;

    public class AppTestBase
    {
        [SetUp]
        public async Task TestSetUp()
        {
            await EnsureAppDbAsync();
        }
    }
}
