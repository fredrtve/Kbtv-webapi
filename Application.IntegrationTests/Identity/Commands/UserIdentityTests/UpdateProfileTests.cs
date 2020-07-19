using BjBygg.Application.Common;
using BjBygg.Application.Common.Exceptions;
using BjBygg.Application.Identity.Commands.UserIdentityCommands.UpdateProfile;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.IntegrationTests.Identity.Commands.UserIdentityTests
{
    using static IdentityTesting;
    public class UpdateProfileTests : IdentityTestBase
    {
        [Test]
        public async Task ShouldUpdateProfile()
        {
            var user = await RunAsDefaultUserAsync(Roles.Leader);

            var command = new UpdateProfileCommand
            {
                Email = "newtestemail@test.com",
                PhoneNumber = "99995555"
            };

            await SendAsync(command);

            var dbUser = await FindUserByUserNameAsync(user.UserName);

            dbUser.Email.Should().Be(command.Email);
            dbUser.PhoneNumber.Should().Be(command.PhoneNumber);
        }
    }
}
