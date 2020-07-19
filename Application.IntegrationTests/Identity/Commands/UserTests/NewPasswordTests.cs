using BjBygg.Application.Common;
using BjBygg.Application.Common.Exceptions;
using BjBygg.Application.Identity.Commands.UserCommands.NewPassword;
using BjBygg.Application.Identity.Commands.UserIdentityCommands.Login;
using BjBygg.Application.Identity.Common.Models;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
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
            var user = await RunAsDefaultUserAsync(Roles.Leader);

            var command = new NewPasswordCommand()
            {
                UserName = user.UserName,
                NewPassword = "Thisisanewpassword"
            };

            await SendAsync(command);

            var dbUser = await FindAsync<ApplicationUser>(user.Id);

            var passwordHasChanged = await CheckUserPassword(dbUser, command.NewPassword);

            passwordHasChanged.Should().BeTrue();
        }
    }
}
