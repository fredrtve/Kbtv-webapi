using AutoMapper;
using BjBygg.Application.Commands.UserCommands.Update;
using BjBygg.Application.Queries.UserQueries;
using BjBygg.Application.Shared;
using CleanArchitecture.Infrastructure.Identity;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BjBygg.WebApi.Controllers.Identity
{
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IMediator _mediator;
        private IMapper _mapper;

        public AuthController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IMediator mediator, IMapper mapper)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._mediator = mediator;
            this._mapper = mapper;
        }

        [EnableCors]
        [Authorize]
        [HttpGet]
        [Route("api/[controller]")]
        public async Task<ActionResult> Get()
        {
            var result = await _mediator.Send(
                new UserByUserNameQuery() { UserName = User.FindFirst("UserName").Value });

            if (result == null) return NotFound($"Finner ikke bruker med brukernavn '{User.FindFirst("UserName").Value})'");

            return Ok(new
            {
                Token = Request.Headers["Authorization"].FirstOrDefault(),
                User = result
            });
        }

        [Authorize]
        [HttpPut]
        [Route("api/[controller]")]
        public async Task<IActionResult> Update([FromBody] UpdateUserCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Values);

            return Ok(await _mediator.Send(command));
        }

        [Authorize]
        [HttpPut]
        [Route("api/[controller]/changePassword")]
        public async Task<IActionResult> UpdatePassword([FromBody] UpdatePasswordModel model)
        {
            var user = await this._userManager.FindByNameAsync(User.FindFirstValue("UserName"));

            if (user == null)
            {
                return NotFound($"Finner ikke bruker med ID '{_userManager.GetUserId(User)}'.");
            }

            var changePasswordResult = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);

            if (!changePasswordResult.Succeeded)
            {
                return BadRequest("Feil passord!");
            }

            await _signInManager.RefreshSignInAsync(user);

            return Ok();
        }

        [EnableCors]
        [HttpPost]
        [Route("api/[controller]/login")]
        public async Task<ActionResult> Login([FromBody] LoginModel model)
        {
            var user = await this._userManager.FindByNameAsync(model.Username);

            if (user == null)
            {
                return Unauthorized(new { message = "Brukernavn eller passord er feil!" });
            }

            if (await this._userManager.CheckPasswordAsync(user, model.Password))
            {
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("karlbredetvetesecretkey"));
                var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

                var roles = await _userManager.GetRolesAsync(user);

                if(roles == null) return Unauthorized(new { message = "Konto har ingen rolle!" });

                var token = new JwtSecurityToken(
                    issuer: "https://localhost:44379",
                    audience: "https://localhost:44379",
                    claims: new List<Claim>() {
                        new Claim(ClaimTypes.Role, roles.FirstOrDefault()),
                        new Claim("UserName", user.UserName)
                    },
                    expires: DateTime.Now.AddDays(10),
                    signingCredentials: signinCredentials
                );

                var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
                var userReponse = _mapper.Map<UserDto>(user);
                userReponse.Role = roles.FirstOrDefault();

                return Ok(new
                {
                    Token = tokenString,
                    User = userReponse
                });
            }

            return Unauthorized("Brukernavn eller passord er feil!");
        }
    }
}
