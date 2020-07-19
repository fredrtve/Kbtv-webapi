using NUnit.Framework;
using System.Threading.Tasks;

namespace Application.IntegrationTests.Identity
{
    using static IdentityTesting;

    public class IdentityTestBase
    {
        [SetUp]
        public async Task TestSetUp()
        {
            await EnsureAppIdentityDb();
        }
    }
}
