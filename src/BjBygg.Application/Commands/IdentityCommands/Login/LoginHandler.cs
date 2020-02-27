using AutoMapper;
using BjBygg.Application.Queries.UserQueries;
using BjBygg.Application.Shared;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Exceptions;
using CleanArchitecture.Infrastructure.Data;
using CleanArchitecture.Infrastructure.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BjBygg.Application.Commands.IdentityCommands.Login
{
    public class LoginHandler : IRequestHandler<LoginCommand, LoginResponse>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IMapper _mapper;

        public LoginHandler(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
        }

        public async Task<LoginResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await this._userManager.FindByNameAsync(request.UserName);

            if (user == null || !(await this._userManager.CheckPasswordAsync(user, request.Password)))
                throw new UnauthorizedException("Brukernavn eller passord er feil!");

            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("karlbredetvetesecretkey"));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var roles = await _userManager.GetRolesAsync(user);

            if (roles == null)
                throw new UnauthorizedException("Konto har ingen rolle!");

            var token = new JwtSecurityToken(
                issuer: "https://localhost:44379",
                audience: "https://localhost:44379",
                claims: new List<Claim>() {
                        new Claim(ClaimTypes.Role, roles.FirstOrDefault()),
                        new Claim("UserName", user.UserName)
                },
                expires: DateTime.Now.AddDays(180),
                signingCredentials: signinCredentials
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
            var userReponse = _mapper.Map<UserDto>(user);
            userReponse.Role = roles.FirstOrDefault();

            return new LoginResponse() { User = userReponse, Token = tokenString };
        }
    }
}
