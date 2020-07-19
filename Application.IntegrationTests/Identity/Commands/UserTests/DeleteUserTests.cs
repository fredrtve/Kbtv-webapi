using BjBygg.Application.Application.Commands.DocumentTypeCommands;
using BjBygg.Application.Common;
using BjBygg.Application.Common.Exceptions;
using BjBygg.Application.Identity.Commands.UserCommands.Delete;
using CleanArchitecture.Core.Entities;
using FluentAssertions;
using NUnit.Framework;
using System.Linq;
using System.Threading.Tasks;

namespace Application.IntegrationTests.Identity.Commands.UserTests
{
    using static IdentityTesting;
    public class DeleteUserTests : IdentityTestBase
    {
        [Test]
        public void ShouldRequireValidUserName()
        {
            var command = new DeleteUserCommand { UserName = "asdadas" };

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<EntityNotFoundException>();
        }

        [Test]
        public async Task ShouldThrowForbiddenExceptionIfDeletingUserWithLeaderRole()
        {
            var currentUser = await RunAsDefaultUserAsync(Roles.Leader);

            var command = new DeleteUserCommand { UserName = Roles.Leader };

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<ForbiddenException>();
        }

        [Test]
        public async Task ShouldDeleteUser()
        {
            await RunAsDefaultUserAsync(Roles.Employee);

            var command = new DeleteUserCommand { UserName = Roles.Employee };

            await SendAsync(command);

            var user = await FindUserByUserNameAsync(command.UserName);

            user.Should().BeNull();
        }
    }
}
