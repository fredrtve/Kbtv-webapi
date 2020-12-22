using BjBygg.Application.Common;
using BjBygg.Application.Common.Exceptions;
using BjBygg.Application.Identity.Commands.UserIdentityCommands.Login;
using BjBygg.Application.Identity.Commands.UserIdentityCommands.Logout;
using BjBygg.Application.Identity.Common.Models;
using FluentAssertions;
using NUnit.Framework;
using System.Threading.Tasks;

namespace Application.IntegrationTests.Identity.Commands.UserIdentityTests
{
    using static IdentityTesting;
    public class LogoutTests : IdentityTestBase
    {
        [Test]
        public void ShouldRequireMinimumFields()
        {
            var command = new LogoutCommand();

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<ValidationException>();
        }

        [Test]
        public async Task ShouldRemoveUserFromRefreshTokenOnLogout()
        {
            var userPassword = "password12";
            var user = await RunAsUserAsync("testUser", userPassword, Roles.Leader);

            var response = await SendAsync(new LoginCommand()
            {
                UserName = user.UserName,
                Password = userPassword
            });

            await SendAsync(new LogoutCommand { RefreshToken = response.RefreshToken });

            var dbRefreshTokenAfterLogout =
               (await GetAllAsync<RefreshToken>()).Find(x => x.Token == response.RefreshToken);

            dbRefreshTokenAfterLogout.UserId.Should().BeNull();
        }
    }
}
