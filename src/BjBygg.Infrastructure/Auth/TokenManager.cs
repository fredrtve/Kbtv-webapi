using AutoMapper;
using BjBygg.Application.Common.Exceptions;
using BjBygg.Application.Identity.Commands.UserIdentityCommands.Login;
using BjBygg.Application.Identity.Commands.UserIdentityCommands.RefreshToken;
using BjBygg.Application.Identity.Common;
using BjBygg.Application.Identity.Common.Interfaces;
using BjBygg.Application.Identity.Common.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BjBygg.Infrastructure.Auth
{
    public class TokenManager : ITokenManager
    {
        private readonly AuthSettings _authSettings;
        private readonly ILogger<TokenManager> _logger;
        private readonly JwtTokenValidator _jwtTokenValidator;
        private readonly IAppIdentityDbContext _dbContext;
        private readonly JwtFactory _jwtFactory;
        private readonly TokenFactory _tokenFactory;

        public TokenManager(
            ILogger<TokenManager> logger,
            JwtTokenValidator jwtTokenValidator,
            IOptions<AuthSettings> authSettings,
            IAppIdentityDbContext dbContext,
            JwtFactory jwtFactory,
            TokenFactory tokenFactory)
        {
            _authSettings = authSettings.Value;
            _logger = logger;
            _jwtTokenValidator = jwtTokenValidator;
            _dbContext = dbContext;
            _jwtFactory = jwtFactory;
            _tokenFactory = tokenFactory;
        }

        public async Task<CreateTokensResponse> CreateTokensAsync(ApplicationUser user, string role)
        {
            var refreshToken = _tokenFactory.GenerateToken();

            var daysToExpire = _authSettings.RefreshTokenLifeTimeInDays;

            _dbContext.RefreshTokens.Add(new RefreshToken(refreshToken, DateTime.UtcNow.AddDays(daysToExpire), user.Id, null));

            await _dbContext.SaveChangesAsync();

            return new CreateTokensResponse()
            {
                AccessToken = await _jwtFactory.GenerateEncodedToken(user.Id, user.UserName, role),
                RefreshToken = refreshToken
            };
        }

        public async Task<RefreshTokenResponse> RefreshTokensAsync(string accessToken, string refreshToken)
        {
            var signingKey = _authSettings.SecretKey;
            var principal = _jwtTokenValidator.GetPrincipalFromToken(accessToken, _authSettings.SecretKey);

            // invalid token/signing key was passed and we can't extract user claims
            if (principal == null) throw new BadRequestException("invalid_grant");

            var dbToken = await _dbContext.RefreshTokens
                .Where(x => x.Token == refreshToken)
                .FirstOrDefaultAsync();

            var userId = principal.Claims.First(c => c.Type == "id").Value;

            if (dbToken == null || dbToken.UserId != userId || !dbToken.Active)
                throw new BadRequestException("invalid_grant");

            if (dbToken.Revoked) //Watch for token replays
            {
                await RevokeDescendantTokensAsync(dbToken);
                _logger.LogCritical($"Potential replay attack mitigated on token with id '{dbToken.Id}'");
                throw new BadRequestException("invalid_grant");
            }

            dbToken.Revoked = true; 

            var userName = principal.Claims.First(c => c.Type == ClaimTypes.Role).Value;
            var role = principal.Claims.First(c => c.Type == ClaimTypes.Name).Value;

            var newAccessToken = await _jwtFactory.GenerateEncodedToken(userId, userName, role);

            var newRefreshToken = _tokenFactory.GenerateToken();

            _dbContext.RefreshTokens.Add(new RefreshToken(newRefreshToken, dbToken.Expires, userId, dbToken.RootTokenId ?? dbToken.Id));

            await _dbContext.SaveChangesAsync();

            return new RefreshTokenResponse(newAccessToken, newRefreshToken);
        }

        public async Task RevokeRefreshTokenAsync(string refreshToken)
        {
            var dbToken = await _dbContext.RefreshTokens
                .Where(x => x.Token == refreshToken)
                .FirstOrDefaultAsync();

            if (dbToken == null) return;

            dbToken.Revoked = true;

            await _dbContext.SaveChangesAsync();
        }

        private async Task RevokeDescendantTokensAsync(RefreshToken token)
        {
            var rootId = token.RootTokenId ?? token.Id;

            var tokensToRevoke = await _dbContext.RefreshTokens.Where(x => x.RootTokenId == rootId && !x.Revoked).ToListAsync();

            foreach (var tokenToRevoke in tokensToRevoke) tokenToRevoke.Revoked = true;

            await _dbContext.SaveChangesAsync();
        }
    }
}
