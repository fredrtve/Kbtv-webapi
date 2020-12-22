using BjBygg.Application.Common;
using BjBygg.Application.Common.Exceptions;
using BjBygg.Application.Identity.Queries.UserQueries.UserByUserName;
using FluentAssertions;
using NUnit.Framework;
using System.Threading.Tasks;

namespace Application.IntegrationTests.Identity.Queries
{
    using static IdentityTesting;
    public class UserByUserNameTests : IdentityTestBase
    {
        [Test]
        public void ShouldRequireValidUserName()
        {
            var command = new UserByUserNameQuery { UserName = "asdasd" };

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<EntityNotFoundException>();
        }

        [Test]
        public async Task ShouldReturnUserWithRole()
        {
            var user = await RunAsDefaultUserAsync(Roles.Management);

            var command = new UserByUserNameQuery()
            {
                UserName = Roles.Management
            };

            var userResponse = await SendAsync(command);

            userResponse.Should().NotBeNull();
            userResponse.UserName.Should().Be(Roles.Management);
            userResponse.Role.Should().Be(Roles.Management);
        }
    }
}
