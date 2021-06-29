using BjBygg.Application.Common;
using BjBygg.Application.Common.Exceptions;
using BjBygg.Application.Identity.Commands.UserCommands.NewPassword;
using BjBygg.Application.Identity.Common.Models;
using FluentAssertions;
using NUnit.Framework;
using System.Threading.Tasks;

namespace Application.IntegrationTests.Identity.Commands.UserTests
{
    using static IdentityTesting;

    //5 x all entities, created 2 yrs apart
    public class NewPasswordTests : IdentityTestBase
    {
        [Test]
        public void ShouldRequireMinimumFields()
        {
            var command = new NewPasswordCommand();

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<ValidationException>();
        }

        [Test]
        public void ShouldRequireValidUserName()
        {
            var command = new NewPasswordCommand { UserName = "asdadas", NewPassword = "Thisisanewpassword" };

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<EntityNotFoundException>();
        }

        [Test]
        public async Task ShouldUpdatePassword()
        {
            var employee = await RunAsDefaultUserAsync(Roles.Employee);
            await RunAsDefaultUserAsync(Roles.Leader); 

            var command = new NewPasswordCommand()
            {
                UserName = employee.UserName,
                NewPassword = "Thisisanewpassword"
            };

            await SendAsync(command);

            var dbEmployee = await FindAsync<ApplicationUser>(employee.Id);

            var passwordHasChanged = await CheckUserPassword(dbEmployee, command.NewPassword);

            passwordHasChanged.Should().BeTrue();
        }

        [Test]
        public async Task ShouldThrowForbiddenExceptionIfRoleIsForbidden()
        {
            var user = await RunAsDefaultUserAsync(Roles.Leader);

            var command = new NewPasswordCommand()
            {
                UserName = user.UserName,
                NewPassword = "Thisisanewpassword"
            };

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<ForbiddenException>();
        }
    }
}
