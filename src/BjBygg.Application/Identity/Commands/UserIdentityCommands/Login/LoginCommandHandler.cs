using AutoMapper;
using BjBygg.Application.Common.Exceptions;
using BjBygg.Application.Identity.Common;
using BjBygg.Application.Identity.Common.Interfaces;
using BjBygg.Application.Identity.Common.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BjBygg.Application.Identity.Commands.UserIdentityCommands.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginResponse>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        private readonly ITokenManager _tokenManager;

        public LoginCommandHandler(
            UserManager<ApplicationUser> userManager,
            IMapper mapper,
            ITokenManager tokenManager)
        {
            _userManager = userManager;
            _mapper = mapper;
            _tokenManager = tokenManager;
        }

        public async Task<LoginResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);

            if (user == null || !(await _userManager.CheckPasswordAsync(user, request.Password)))
                throw new UnauthorizedException("Brukernavn eller passord er feil!");

            var roles = await _userManager.GetRolesAsync(user);

            if (roles.Count == 0)
                throw new UnauthorizedException("Konto har ingen rolle!");

            var tokens = await _tokenManager.CreateTokensAsync(user, roles.First());

            var userReponse = _mapper.Map<UserDto>(user);

            userReponse.Role = roles.FirstOrDefault();

            return new LoginResponse()
            {
                User = userReponse,
                AccessToken = tokens.AccessToken,
                RefreshToken = tokens.RefreshToken
            };
        }
    }
}
