using BjBygg.Application.Application.Commands.DocumentTypeCommands.Update;
using BjBygg.Application.Common;
using BjBygg.Application.Common.Exceptions;
using BjBygg.Application.Identity.Commands.UserCommands.Update;
using CleanArchitecture.Core;
using CleanArchitecture.Core.Entities;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace Application.IntegrationTests.Identity.Commands.UserTests
{
    using static IdentityTesting;

    public class UpdateUserTests : IdentityTestBase
    {
        [Test]
        public async Task ShouldRequireValidUserName()
        {
            await RunAsDefaultUserAsync(Roles.Leader);

            var command = new UpdateUserCommand{ UserName = "asdasd" };

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<EntityNotFoundException>();
        }

        [Test]
        public async Task ShouldRequireValidRoleIfRoleIsNotNull()
        {
            var user = await RunAsDefaultUserAsync(Roles.Employee);

            var command = new UpdateUserCommand()
            {
                UserName = user.UserName,
                Role = "InvalidRole"
            };

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<EntityNotFoundException>();
        }

        [Test]
        public async Task ShouldThrowForbiddenExceptionIfUpdatingUserRoleToLeaderRole()
        {
            var user = await RunAsDefaultUserAsync(Roles.Management);

            var command = new UpdateUserCommand()
            {
                UserName = user.UserName,
                Role = Roles.Leader
            };

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<ForbiddenException>();
        }

        [Test]
        public async Task ShouldThrowForbiddenExceptionIfUpdatingRoleOfUserWithLeaderRole()
        {
            var user = await RunAsDefaultUserAsync(Roles.Leader);

            var command = new UpdateUserCommand()
            {
                UserName = user.UserName,
                Role = Roles.Employee
            };

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<ForbiddenException>();
        }

        [Test]
        public async Task ShouldUpdateUser()
        {
            var currentUser = await RunAsDefaultUserAsync(Roles.Management);

            var command = new UpdateUserCommand()
            {
                UserName = currentUser.UserName,
                FirstName = "FirstName",
                LastName = "LastName",
                PhoneNumber = "PhoneNumber",
                Email = "email@test.com",
                Role = Roles.Employee,
            };

            await SendAsync(command);

            var user = await FindUserByUserNameAsync(command.UserName);
            var role = await GetUserRole(user);

            role.Should().NotBeNull();
            role.Should().Be(command.Role);

            user.Should().NotBeNull();
            user.FirstName.Should().Be(command.FirstName);
            user.LastName.Should().Be(command.LastName);
            user.PhoneNumber.Should().Be(command.PhoneNumber);
            user.Email.Should().Be(command.Email);

            user.UpdatedBy.Should().Be(currentUser.UserName);
            user.UpdatedAt.Should().BeCloseTo(DateTimeHelper.Now(), 10000);
        }
   
    }
}
