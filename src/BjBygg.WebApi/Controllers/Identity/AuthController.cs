using CleanArchitecture.Infrastructure.Identity;
using Microsoft.AspNetCore.Cors;
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

        public AuthController(UserManager<ApplicationUser> userManager)
        {
            this._userManager = userManager;
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

                var token = new JwtSecurityToken(
                    issuer: "https://localhost:44379",
                    audience: "https://localhost:44379",
                    claims: new List<Claim>() {
                        new Claim(ClaimTypes.Role, roles.FirstOrDefault())
                    },
                    expires: DateTime.Now.AddDays(10),
                    signingCredentials: signinCredentials
                );

                var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

                return Ok(new
                {
                    Token = tokenString,
                    ExpiresIn = token.ValidTo,
                    Username = user.UserName
                });
            }

            return Unauthorized(new { message = "Brukernavn eller passord er feil!" });
        }
    }
}
