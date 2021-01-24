using BjBygg.Application.Common;
using BjBygg.Application.Common.Exceptions;
using BjBygg.Application.Identity.Commands.UserIdentityCommands.Login;
using BjBygg.Application.Identity.Commands.UserIdentityCommands.RefreshToken;
using BjBygg.Application.Identity.Common.Models;
using CleanArchitecture.Infrastructure.Auth;
using FluentAssertions;
using NUnit.Framework;
using System.Linq;
using System.Threading.Tasks;

namespace Application.IntegrationTests.Identity.Commands.UserIdentityTests
{
    using static IdentityTesting;
    public class RefreshTokenTests : IdentityTestBase
    {
        [Test]
        public void ShouldRequireMinimumFields()
        {
            var command = new RefreshTokenCommand();

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<ValidationException>();
        }

        [Test]
        public async Task ShouldThrowBadRequestExceptionIfAccessTokenIsInvalid()
        {
            var userPassword = "password12";
            var user = await RunAsUserAsync("testUser", userPassword, Roles.Leader);

            var loginResponse = await SendAsync(new LoginCommand()
            {
                UserName = user.UserName,
                Password = userPassword
            });

            var refreshCommand = new RefreshTokenCommand()
            {
                AccessToken = loginResponse.AccessToken.Token + "a2f",
                RefreshToken = loginResponse.RefreshToken
            };

            FluentActions.Invoking(() => SendAsync(refreshCommand))
                .Should().Throw<BadRequestException>().WithMessage("invalid_grant");
        }

        [Test]
        public async Task ShouldThrowBadRequestExceptionIfRefreshTokenIsInvalid()
        {
            var userPassword = "password12";
            var user = await RunAsUserAsync("testUser", userPassword, Roles.Leader);

            var loginResponse = await SendAsync(new LoginCommand()
            {
                UserName = user.UserName,
                Password = userPassword
            });

            var refreshCommand = new RefreshTokenCommand()
            {
                AccessToken = loginResponse.AccessToken.Token,
                RefreshToken = loginResponse.RefreshToken + "asd"
            };

            FluentActions.Invoking(() => SendAsync(refreshCommand))
                .Should().Throw<BadRequestException>().WithMessage("invalid_grant");
        }

        [Test]
        public async Task ShouldReturnValidRefreshTokenResponse()
        {
            var userPassword = "password12";
            var user = await RunAsUserAsync("testUser", userPassword, Roles.Leader);

            var loginResponse = await SendAsync(new LoginCommand()
            {
                UserName = user.UserName,
                Password = userPassword
            });

            var refreshResponse = await SendAsync(new RefreshTokenCommand()
            {
                AccessToken = loginResponse.AccessToken.Token,
                RefreshToken = loginResponse.RefreshToken
            });

            var accessTokenValidFor = new JwtIssuerOptions().ValidFor;

            var principal = GetPrincipalFromAccessToken(refreshResponse.AccessToken.Token);

            var principalUser = await FindAsync<ApplicationUser>(principal.Claims.First(c => c.Type == "id").Value);


            refreshResponse.Should().NotBeNull();

            refreshResponse.AccessToken.Should().NotBeNull();
            refreshResponse.AccessToken.ExpiresIn.Should().Be((int)accessTokenValidFor.TotalSeconds);
            principal.Should().NotBeNull();
            principal.Claims.Should().NotBeEmpty();
            principalUser.Should().NotBeNull();
            principalUser.UserName.Should().Be(user.UserName);

        }
    }
}
