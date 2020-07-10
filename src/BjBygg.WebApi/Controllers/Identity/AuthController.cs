using BjBygg.Application.Identity.Commands.IdentityCommands.Login;
using BjBygg.Application.Identity.Commands.IdentityCommands.Logout;
using BjBygg.Application.Identity.Commands.IdentityCommands.RefreshToken;
using BjBygg.Application.Identity.Commands.IdentityCommands.UpdatePassword;
using BjBygg.Application.Identity.Commands.IdentityCommands.UpdateProfile;
using BjBygg.Application.Identity.Common;
using BjBygg.Application.Identity.Common.Models;
using BjBygg.Application.Identity.Queries.UserQueries.UserByUserName;
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
        public async Task<ActionResult<RefreshTokenResponse>> Refresh([FromBody] RefreshTokenCommand request)
        {
            request.SigningKey = _authSettings.SecretKey;
            return await Mediator.Send(request);
        }

        [HttpPost]
        [Route("api/[controller]/login")]
        public async Task<ActionResult<LoginResponse>> Login([FromBody] LoginCommand request)
        {
            return await Mediator.Send(request);
        }

        [Authorize]
        [HttpGet]
        [Route("api/[controller]")]
        public async Task<ActionResult<UserDto>> Get()
        {
            var request = new UserByUserNameQuery() { UserName = User.FindFirstValue(ClaimTypes.Name) };
            return await Mediator.Send(request);
        }

        [Authorize]
        [HttpPut]
        [Route("api/[controller]")]
        public async Task<ActionResult<UserDto>> UpdateProfile([FromBody] UpdateProfileCommand request)
        {
            return await Mediator.Send(request);
        }

        [Authorize]
        [HttpPut]
        [Route("api/[controller]/changePassword")]
        public async Task<ActionResult> UpdatePassword([FromBody] UpdatePasswordCommand request)
        {
            await Mediator.Send(request);
            return NoContent();
        }

        [Authorize]
        [HttpPost]
        [Route("api/[controller]/logout")]
        public async Task<ActionResult> Logout([FromBody] LogoutCommand request)
        {
            await Mediator.Send(request);
            return NoContent();
        }

    }
}
