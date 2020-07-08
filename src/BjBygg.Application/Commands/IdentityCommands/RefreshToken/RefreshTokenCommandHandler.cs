using BjBygg.Application.Common.Exceptions;
using BjBygg.Application.Common.Exceptions;
using CleanArchitecture.Core.Interfaces.Services;
using CleanArchitecture.Infrastructure.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BjBygg.Application.Commands.IdentityCommands.RefreshToken
{
    public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, RefreshTokenResponse>
    {
        private readonly IJwtTokenValidator _jwtTokenValidator;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly AppIdentityDbContext _dbContext;
        private readonly IJwtFactory _jwtFactory;

        public RefreshTokenCommandHandler(
            IJwtTokenValidator jwtTokenValidator,
            UserManager<ApplicationUser> userManager,
            AppIdentityDbContext dbContext,
            IJwtFactory jwtFactory)
        {
            _jwtTokenValidator = jwtTokenValidator;
            _userManager = userManager;
            _dbContext = dbContext;
            _jwtFactory = jwtFactory;
        }

        public async Task<RefreshTokenResponse> Handle(RefreshTokenCommand command, CancellationToken cancellationToken)
        {
            var principal = _jwtTokenValidator.GetPrincipalFromToken(command.AccessToken, command.SigningKey);

            // invalid token/signing key was passed and we can't extract user claims
            if (principal == null) throw new BadRequestException("invalid_grant");

            var id = principal.Claims.First(c => c.Type == "id");

            var user = await _dbContext.Users
                .Include(x => x.RefreshTokens)
                .FirstOrDefaultAsync(x => x.Id == id.Value);

            var roles = await _userManager.GetRolesAsync(user);

            if (roles == null || roles.Count == 0)
                throw new UnauthorizedException("User has no roles");

            if (!user.HasValidRefreshToken(command.RefreshToken))
                throw new BadRequestException("invalid_grant");

            var jwtToken = await _jwtFactory.GenerateEncodedToken(user.Id, user.UserName, roles.First());

            await _userManager.UpdateAsync(user);

            return new RefreshTokenResponse(jwtToken, command.RefreshToken);
        }

    }
}
