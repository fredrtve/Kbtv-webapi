using AutoMapper;
using BjBygg.Application.Shared;
using CleanArchitecture.Core.Exceptions;
using CleanArchitecture.Core.Interfaces.Services;
using CleanArchitecture.Infrastructure.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BjBygg.Application.Commands.IdentityCommands.Login
{
    public class LoginHandler : IRequestHandler<LoginCommand, LoginResponse>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly AppIdentityDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IJwtFactory _jwtFactory;
        private readonly ITokenFactory _tokenFactory;

        public LoginHandler(
            UserManager<ApplicationUser> userManager,
            AppIdentityDbContext dbContext,
            IMapper mapper,
            IJwtFactory jwtFactory, 
            ITokenFactory tokenFactory)
        {
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
                .FirstOrDefaultAsync(x => x.UserName == request.UserName);

            if (user == null || !(await this._userManager.CheckPasswordAsync(user, request.Password)))
                throw new UnauthorizedException("Brukernavn eller passord er feil!");

            var roles = await _userManager.GetRolesAsync(user);

            if (roles == null)
                throw new UnauthorizedException("Konto har ingen rolle!");

            var refreshToken = _tokenFactory.GenerateToken();
            user.AddRefreshToken(refreshToken, user.Id, 360);
            await _userManager.UpdateAsync(user);

   
            var userReponse = _mapper.Map<UserDto>(user);
            userReponse.Role = roles.FirstOrDefault();

            return new LoginResponse() { 
                User = userReponse, 
                AccessToken = await _jwtFactory.GenerateEncodedToken(user.Id, user.UserName, userReponse.Role),
                RefreshToken = refreshToken 
            };
        }
    }
}
