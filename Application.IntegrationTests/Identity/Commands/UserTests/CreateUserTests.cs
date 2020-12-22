using BjBygg.Application.Common;
using BjBygg.Application.Common.Exceptions;
using BjBygg.Application.Identity.Commands.UserCommands.Create;
using CleanArchitecture.Core;
using FluentAssertions;
using NUnit.Framework;
using System.Threading.Tasks;

namespace Application.IntegrationTests.Identity.Commands.UserTests
{
    using static IdentityTesting;
    //5 x all entities, created 2 yrs apart
    public class CreateUserTests : IdentityTestBase
    {
        [Test]
        public async Task ShouldRequireMinimumFields()
        {
            await RunAsDefaultUserAsync(Roles.Leader);

            var command = new CreateUserCommand();

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<ValidationException>();
        }

        [Test]
        public async Task ShouldThrowForbiddenExceptionIfCreatingUserWithLeaderRole()
        {
            await RunAsDefaultUserAsync(Roles.Leader);

            var command = new CreateUserCommand()
            {
                UserName = "UserName",
                FirstName = "FirstName",
                LastName = "LastName",
                PhoneNumber = "PhoneNumber",
                Email = "email@test.com",
                Role = Roles.Leader,
                Password = "Password",
            };

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<ForbiddenException>();
        }

        [Test]
        public async Task ShouldRequireValidRole()
        {
            await RunAsDefaultUserAsync(Roles.Leader);

            var command = new CreateUserCommand()
            {
                UserName = "UserName",
                FirstName = "FirstName",
                LastName = "LastName",
                PhoneNumber = "PhoneNumber",
                Email = "email@test.com",
                Role = "InvalidRole",
                Password = "Password",
            };

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<EntityNotFoundException>();
        }

        [Test]
        public async Task ShouldCreateUser()
        {
            var currentUser = await RunAsDefaultUserAsync(Roles.Leader);

            var command = new CreateUserCommand()
            {
                UserName = "UserName",
                FirstName = "FirstName",
                LastName = "LastName",
                PhoneNumber = "PhoneNumber",
                Email = "email@test.com",
                Role = Roles.Employee,
                Password = "Password",
            };

            await SendAsync(command);

            var dbUser = await FindUserByUserNameAsync(command.UserName);
            var dbRole = await GetUserRole(dbUser);

            dbRole.Should().NotBeNull();
            dbRole.Should().Be(command.Role);

            dbUser.Should().NotBeNull();
            dbUser.UserName.Should().Be(command.UserName);
            dbUser.FirstName.Should().Be(command.FirstName);
            dbUser.LastName.Should().Be(command.LastName);
            dbUser.PhoneNumber.Should().Be(command.PhoneNumber);
            dbUser.Email.Should().Be(command.Email);

            dbUser.CreatedBy.Should().Be(currentUser.UserName);
            dbUser.UpdatedAt.Should().BeCloseTo(DateTimeHelper.Now(), 10000);
        }
    }
}
