using AutoMapper;
using BjBygg.Application.Common.Exceptions;
using BjBygg.Application.Identity.Common;
using BjBygg.Application.Identity.Common.Interfaces;
using BjBygg.Application.Identity.Common.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BjBygg.Application.Identity.Commands.UserIdentityCommands.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginResponse>
    {
        private readonly AuthSettings _authSettings;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IAppIdentityDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IJwtFactory _jwtFactory;
        private readonly ITokenFactory _tokenFactory;

        public LoginCommandHandler(
            IOptions<AuthSettings> authSettings,
            UserManager<ApplicationUser> userManager,
            IAppIdentityDbContext dbContext,
            IMapper mapper,
            IJwtFactory jwtFactory,
            ITokenFactory tokenFactory)
        {
            _authSettings = authSettings.Value;
            _userManager = userManager;
            _dbContext = dbContext;
            _mapper = mapper;
            _jwtFactory = jwtFactory;
            _tokenFactory = tokenFactory;
        }

        public async Task<LoginResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users
                .Include(x => x.RefreshTokens)
                .FirstOrDefaultAsync(x => x.NormalizedUserName == request.UserName.ToUpper());

            if (user == null || !(await _userManager.CheckPasswordAsync(user, request.Password)))
                throw new UnauthorizedException("Brukernavn eller passord er feil!");

            var roles = await _userManager.GetRolesAsync(user);

            if (roles.Count == 0)
                throw new UnauthorizedException("Konto har ingen rolle!");

            var refreshToken = _tokenFactory.GenerateToken();
            user.AddRefreshToken(refreshToken, user.Id, _authSettings.RefreshTokenLifeTimeInDays);
            await _userManager.UpdateAsync(user);


            var userReponse = _mapper.Map<UserDto>(user);
            userReponse.Role = roles.FirstOrDefault();

            return new LoginResponse()
            {
                User = userReponse,
                AccessToken = await _jwtFactory.GenerateEncodedToken(user.Id, user.UserName, userReponse.Role),
                RefreshToken = refreshToken
            };
        }
    }
}
