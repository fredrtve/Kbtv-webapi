using BjBygg.Application.Common;
using BjBygg.Application.Identity.Commands.UserCommands.Create;
using BjBygg.Application.Identity.Queries.UserQueries;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.IntegrationTests.Identity.Queries
{
    using static IdentityTesting;
    public class UserListTests : IdentityTestBase
    {
        [Test]
        public async Task ShouldReturnAllUsersWithRole()
        {
            var baseUserCommand = new CreateUserCommand()
            {
                UserName = "UserName",
                FirstName = "FirstName",
                LastName = "LastName",
                PhoneNumber = "PhoneNumber",
                Email = "email@test.com",
                Role = Roles.Employee,
                Password = "Password",
            };

            await SendAsync(baseUserCommand);

            baseUserCommand.UserName = "UserName2";

            await SendAsync(baseUserCommand);

            var list = await SendAsync(new UserListQuery());

            list.Should().NotBeNull();
            list.Should().HaveCount(2);
            list[0].Role.Should().Be(baseUserCommand.Role);
            list[1].Role.Should().Be(baseUserCommand.Role);
        }
    }
}
