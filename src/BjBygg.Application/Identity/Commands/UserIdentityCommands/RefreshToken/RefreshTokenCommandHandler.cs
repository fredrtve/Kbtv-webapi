using BjBygg.Application.Common.Exceptions;
using BjBygg.Application.Identity.Common.Interfaces;
using BjBygg.Application.Identity.Common.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BjBygg.Application.Identity.Commands.UserIdentityCommands.RefreshToken
{
    public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, RefreshTokenResponse>
    {
        private readonly IJwtTokenValidator _jwtTokenValidator;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IAppIdentityDbContext _dbContext;
        private readonly IJwtFactory _jwtFactory;
        private readonly AuthSettings _authSettings;

        public RefreshTokenCommandHandler(
            IJwtTokenValidator jwtTokenValidator,
            UserManager<ApplicationUser> userManager,
            IAppIdentityDbContext dbContext,
            IJwtFactory jwtFactory,
            IOptions<AuthSettings> authSettings)
        {
            _jwtTokenValidator = jwtTokenValidator;
            _userManager = userManager;
            _dbContext = dbContext;
            _jwtFactory = jwtFactory;
            _authSettings = authSettings.Value;
        }

        public async Task<RefreshTokenResponse> Handle(RefreshTokenCommand command, CancellationToken cancellationToken)
        {
            var signingKey = _authSettings.SecretKey;
            var principal = _jwtTokenValidator.GetPrincipalFromToken(command.AccessToken, _authSettings.SecretKey);

            // invalid token/signing key was passed and we can't extract user claims
            if (principal == null) throw new BadRequestException("invalid_grant");

            var id = principal.Claims.First(c => c.Type == "id");

            var user = await _dbContext.Users
                .Include(x => x.RefreshTokens)
                .FirstOrDefaultAsync(x => x.Id == id.Value);

            var roles = await _userManager.GetRolesAsync(user);

            if (!user.HasValidRefreshToken(command.RefreshToken) || roles == null || roles.Count == 0)
                throw new BadRequestException("invalid_grant");

            var jwtToken = await _jwtFactory.GenerateEncodedToken(user.Id, user.UserName, roles.First());

            //await _userManager.UpdateAsync(user);

            return new RefreshTokenResponse(jwtToken);
        }
    }
}