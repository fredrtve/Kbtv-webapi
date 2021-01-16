using BjBygg.Application.Common;
using BjBygg.Application.Common.Exceptions;
using BjBygg.Application.Identity.Commands.UserIdentityCommands.UpdatePassword;
using BjBygg.Application.Identity.Common.Models;
using FluentAssertions;
using NUnit.Framework;
using System.Threading.Tasks;

namespace Application.IntegrationTests.Identity.Commands.UserIdentityTests
{
    using static IdentityTesting;
    public class UpdatePasswordTests : IdentityTestBase
    {
        [Test]
        public void ShouldRequireMinimumFields()
        {
            var command = new UpdatePasswordCommand();

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<ValidationException>();
        }

        [Test]
        public async Task ShouldThrowValidationExceptionIfOldPasswordIsInvalid()
        {
            var userPassword = "password12";
            var user = await RunAsUserAsync("testUser", userPassword, Roles.Leader);

            var command = new UpdatePasswordCommand
            {
                OldPassword = "test4322",
                NewPassword = "test1234"
            };

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<ValidationException>();
        }

        [Test]
        public async Task ShouldUpdatePassword()
        {
            var userPassword = "password12";

            var user = await RunAsUserAsync("testUser", userPassword, Roles.Leader);

            var command = new UpdatePasswordCommand
            {
                OldPassword = userPassword,
                NewPassword = "newpassword"
            };

            await SendAsync(command);

            var dbUser = await FindAsync<ApplicationUser>(user.Id);

            var passwordHasChanged = await CheckUserPassword(dbUser, command.NewPassword);

            passwordHasChanged.Should().BeTrue();
        }
    }
}
