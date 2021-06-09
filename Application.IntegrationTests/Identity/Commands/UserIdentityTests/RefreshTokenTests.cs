using BjBygg.Application.Common;
using BjBygg.Application.Common.Exceptions;
using BjBygg.Application.Identity.Commands.UserIdentityCommands.Login;
using BjBygg.Application.Identity.Commands.UserIdentityCommands.RefreshToken;
using BjBygg.Application.Identity.Common.Models;
using BjBygg.Infrastructure.Auth;
using FluentAssertions;
using NUnit.Framework;
using System;
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

            var refreshTokens = await GetAllAsync<RefreshToken>();
            var currRefreshToken = refreshTokens.FirstOrDefault(x => x.Token == refreshResponse.RefreshToken);
            var rootRefreshToken = refreshTokens.FirstOrDefault(x => x.Token == loginResponse.RefreshToken);

            var authOptions = GetAuthOptions();

            refreshResponse.Should().NotBeNull();

            currRefreshToken.Expires.Should().BeCloseTo(DateTime.Now.AddDays(authOptions.RefreshTokenLifeTimeInDays), 1000*60*60*24);
            currRefreshToken.RootTokenId.Should().Be(rootRefreshToken.Id);

            rootRefreshToken.Revoked.Should().BeTrue();

            refreshResponse.AccessToken.Should().NotBeNull();
            refreshResponse.AccessToken.ExpiresIn.Should().Be((int)accessTokenValidFor.TotalSeconds);
            principal.Should().NotBeNull();
            principal.Claims.Should().NotBeEmpty();
            principalUser.Should().NotBeNull();
            principalUser.UserName.Should().Be(user.UserName);

        }

        [Test]
        public async Task ShouldRevokeAllTokensInChainAndThrowBadRequestExceptionIfRefreshTokenIsRevoked()
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
                RefreshToken = loginResponse.RefreshToken
            };

            var refreshResponse = await SendAsync(refreshCommand);

            var revokedRefreshCommand = new RefreshTokenCommand()
            {
                AccessToken = refreshCommand.AccessToken,
                RefreshToken = loginResponse.RefreshToken
            };

            FluentActions.Invoking(() => SendAsync(revokedRefreshCommand))
                .Should().Throw<BadRequestException>().WithMessage("invalid_grant");

            var tokens = await GetAllAsync<RefreshToken>();
            var rootRefreshToken = tokens.FirstOrDefault(x => x.Token == loginResponse.RefreshToken);
            var tokenChain = tokens.Where(x => x.RootTokenId == rootRefreshToken.Id);

            rootRefreshToken.Revoked.Should().BeTrue();
            foreach(var token in tokenChain) token.Revoked.Should().BeTrue();
        }
    }
}
