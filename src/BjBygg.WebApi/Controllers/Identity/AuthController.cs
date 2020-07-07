using BjBygg.Application.Commands.IdentityCommands.Login;
using BjBygg.Application.Commands.IdentityCommands.Logout;
using BjBygg.Application.Commands.IdentityCommands.RefreshToken;
using BjBygg.Application.Commands.IdentityCommands.UpdatePassword;
using BjBygg.Application.Commands.IdentityCommands.UpdateProfile;
using BjBygg.Application.Common;
using BjBygg.Application.Queries.UserQueries.UserByUserName;
using CleanArchitecture.Infrastructure.Auth;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BjBygg.WebApi.Controllers.Identity
{
    public class AuthController : BaseController
    {
        private readonly AuthSettings _authSettings;

        public AuthController(IOptions<AuthSettings> authSettings)
        {
            _authSettings = authSettings.Value;
        }

        [HttpPost]
        [Route("api/[controller]/refresh")]
        public async Task<RefreshTokenResponse> Refresh([FromBody] RefreshTokenCommand request)
        {
            request.SigningKey = _authSettings.SecretKey;
            return await Mediator.Send(request);
        }

        [HttpPost]
        [Route("api/[controller]/login")]
        public async Task<LoginResponse> Login([FromBody] LoginCommand request)
        {
            return await Mediator.Send(request);
        }

        [Authorize]
        [HttpGet]
        [Route("api/[controller]")]
        public async Task<UserDto> Get()
        {
            var request = new UserByUserNameQuery() { UserName = User.FindFirstValue(ClaimTypes.Name) };
            return await Mediator.Send(request);
        }

        [Authorize]
        [HttpPut]
        [Route("api/[controller]")]
        public async Task<UserDto> UpdateProfile([FromBody] UpdateProfileCommand request)
        {
            return await Mediator.Send(request);
        }

        [Authorize]
        [HttpPut]
        [Route("api/[controller]/changePassword")]
        public async Task<Unit> UpdatePassword([FromBody] UpdatePasswordCommand request)
        {
            return await Mediator.Send(request);
        }

        [Authorize]
        [HttpPost]
        [Route("api/[controller]/logout")]
        public async Task<Unit> Logout([FromBody] LogoutCommand request)
        {
            return await Mediator.Send(request);
        }

    }
}
