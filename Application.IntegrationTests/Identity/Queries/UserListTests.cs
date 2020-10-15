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
            await RunAsDefaultUserAsync(Roles.Employee);
            await RunAsDefaultUserAsync(Roles.Employee); 
            await RunAsDefaultUserAsync(Roles.Leader);

            var list = await SendAsync(new UserListQuery());

            list.Should().NotBeNull();
            list.Should().HaveCount(2);
        }
    }
}
