using BjBygg.Application.Commands.IdentityCommands.Login;
using BjBygg.Application.Commands.IdentityCommands.Logout;
using BjBygg.Application.Commands.IdentityCommands.RefreshToken;
using BjBygg.Application.Commands.IdentityCommands.UpdatePassword;
using BjBygg.Application.Commands.IdentityCommands.UpdateProfile;
using BjBygg.Application.Queries.UserQueries;
using BjBygg.Application.Shared;
using CleanArchitecture.Core.Exceptions;
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
        private readonly IMediator _mediator;
        private readonly AuthSettings _authSettings;

        public AuthController(IMediator mediator, IOptions<AuthSettings> authSettings)
        {
            _mediator = mediator;
            _authSettings = authSettings.Value;
        }

        [HttpPost]
        [Route("api/[controller]/refresh")]
        public async Task<RefreshTokenResponse> Refresh([FromBody] RefreshTokenCommand command)
        {
            command.SigningKey = _authSettings.SecretKey;
            return await _mediator.Send(command);
        }

        [HttpPost]
        [Route("api/[controller]/login")]
        public async Task<LoginResponse> Login([FromBody] LoginCommand command)
        {
            return await _mediator.Send(command);
        }

        [Authorize]
        [HttpGet]
        [Route("api/[controller]")]
        public async Task<UserDto> Get()
        {
            return await _mediator.Send(new UserByUserNameQuery() { UserName = User.FindFirstValue(ClaimTypes.Name) });
        }

        [Authorize]
        [HttpPut]
        [Route("api/[controller]")]
        public async Task<UserDto> UpdateProfile([FromBody] UpdateProfileCommand command)
        {
            command.UserName = User.FindFirstValue(ClaimTypes.Name);

            if (!ModelState.IsValid)
                throw new BadRequestException(ModelState.Values.ToString());

            return await _mediator.Send(command);
        }

        [Authorize]
        [HttpPut]
        [Route("api/[controller]/changePassword")]
        public async Task<Unit> UpdatePassword([FromBody] UpdatePasswordCommand command)
        {
            command.UserName = User.FindFirstValue(ClaimTypes.Name);

            return await _mediator.Send(command);
        }

        [Authorize]
        [HttpPost]
        [Route("api/[controller]/logout")]
        public async Task<Unit> Logout([FromBody] LogoutCommand command)
        {
            command.UserName = User.FindFirstValue(ClaimTypes.Name);

            return await _mediator.Send(command);
        }

    }
}
