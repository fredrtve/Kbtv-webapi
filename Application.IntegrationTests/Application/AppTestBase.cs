using NUnit.Framework;
using System.Threading.Tasks;

namespace Application.IntegrationTests.Application
{
    using static AppTesting;

    public class AppTestBase
    {
        [SetUp]
        public void TestSetUp()
        {
            EnsureAppDbAsync();
        }
    }
}
