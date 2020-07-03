using BjBygg.Application.Commands.IdentityCommands.Login;
using BjBygg.Application.Commands.IdentityCommands.Logout;
using BjBygg.Application.Commands.IdentityCommands.RefreshToken;
using BjBygg.Application.Commands.IdentityCommands.UpdatePassword;
using BjBygg.Application.Commands.IdentityCommands.UpdateProfile;
using BjBygg.Application.Queries.UserQueries;
using BjBygg.Application.Shared;
using CleanArchitecture.Infrastructure.Auth;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BjBygg.WebApi.Controllers.Identity
{
    public class AuthController : BaseController
    {
        private readonly AuthSettings _authSettings;

        public AuthController(IMediator mediator, ILogger<AuthController> logger, IOptions<AuthSettings> authSettings) : 
            base(mediator, logger)
        {
            _authSettings = authSettings.Value;
        }

        [HttpPost]
        [Route("api/[controller]/refresh")]
        public async Task<RefreshTokenResponse> Refresh([FromBody] RefreshTokenCommand request)
        {
            request.SigningKey = _authSettings.SecretKey;
            return await ValidateAndExecute(request);
        }

        [HttpPost]
        [Route("api/[controller]/login")]
        public async Task<LoginResponse> Login([FromBody] LoginCommand request)
        {
            return await ValidateAndExecute(request);
        }

        [Authorize]
        [HttpGet]
        [Route("api/[controller]")]
        public async Task<UserDto> Get()
        {
            var request = new UserByUserNameQuery() { UserName = User.FindFirstValue(ClaimTypes.Name) };
            return await ValidateAndExecute(request);
        }

        [Authorize]
        [HttpPut]
        [Route("api/[controller]")]
        public async Task<UserDto> UpdateProfile([FromBody] UpdateProfileCommand request)
        {
            request.UserName = User.FindFirstValue(ClaimTypes.Name);

            return await ValidateAndExecute(request);
        }

        [Authorize]
        [HttpPut]
        [Route("api/[controller]/changePassword")]
        public async Task<Unit> UpdatePassword([FromBody] UpdatePasswordCommand request)
        {
            request.UserName = User.FindFirstValue(ClaimTypes.Name);

            return await ValidateAndExecute(request);
        }

        [Authorize]
        [HttpPost]
        [Route("api/[controller]/logout")]
        public async Task<Unit> Logout([FromBody] LogoutCommand request)
        {
            request.UserName = User.FindFirstValue(ClaimTypes.Name);

            return await ValidateAndExecute(request);
        }

    }
}
