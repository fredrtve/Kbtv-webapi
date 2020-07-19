using BjBygg.Application.Common;
using BjBygg.Application.Common.Exceptions;
using BjBygg.Application.Identity.Commands.UserIdentityCommands.Login;
using BjBygg.Application.Identity.Common.Models;
using CleanArchitecture.Core;
using CleanArchitecture.Infrastructure.Auth;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IntegrationTests.Identity.Commands.UserIdentityTests
{
    using static IdentityTesting;
    public class LoginTests : IdentityTestBase
    {
        [Test]
        public void ShouldRequireMinimumFields()
        {
            var command = new LoginCommand();

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<ValidationException>();
        }

        [Test]
        public void ShouldThrowUnauthorizedExceptionIfUserNotFound()
        {
            var command = new LoginCommand() { 
                UserName = "wrongusername", 
                Password = "wrongpassword" 
            };

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<UnauthorizedException>();
        }

        [Test]
        public async Task ShouldThrowUnauthorizedExceptionIfInvalidPassword()
        {
            var user = await RunAsDefaultUserAsync(Roles.Leader);

            var command = new LoginCommand()
            {
                UserName = user.UserName,
                Password = "wrongpassword"
            };

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<UnauthorizedException>();
        }

        [Test]
        public async Task ShouldThrowUnauthorizedExceptionIfUserHasNoRole()
        {
            var userPw = "test1234";
            var user = await RunAsUserAsync("test", userPw);

            var command = new LoginCommand()
            {
                UserName = user.UserName,
                Password = userPw
            };

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<UnauthorizedException>();
        }

        [Test]
        public async Task ShouldReturnValidLoginResponse()
        {
            var userPassword = "password12";
            var user = await RunAsUserAsync("testUser", userPassword, Roles.Leader);

            var response = await SendAsync(new LoginCommand()
            {
                UserName = user.UserName,
                Password = userPassword
            });

            var accessTokenValidFor = new JwtIssuerOptions().ValidFor;

            var principal = GetPrincipalFromAccessToken(response.AccessToken.Token);

            var principalUser = await FindAsync<ApplicationUser>(principal.Claims.First(c => c.Type == "id").Value);

            var dbRefreshToken = (await GetAllAsync<RefreshToken>()).Find(x => x.Token == response.RefreshToken);
            var refreshTokenExpiration = DateTimeHelper.Now().AddDays(GetAuthOptions().RefreshTokenLifeTimeInDays);

            response.Should().NotBeNull();

            response.AccessToken.Should().NotBeNull();        
            response.AccessToken.ExpiresIn.Should().Be((int) accessTokenValidFor.TotalSeconds);
            principal.Should().NotBeNull();
            principal.Claims.Should().NotBeEmpty();
            principalUser.Should().NotBeNull();
            principalUser.UserName.Should().Be(user.UserName);

            response.RefreshToken.Should().NotBeNull();
            dbRefreshToken.Should().NotBeNull();
            dbRefreshToken.Active.Should().BeTrue();
            dbRefreshToken.Expires.Should().BeCloseTo(refreshTokenExpiration, 10000);
            dbRefreshToken.UserId.Should().Be(user.Id);

            response.User.Should().NotBeNull();
            response.User.UserName.Should().Be(user.UserName);
        }
    }
}
