using AutoMapper;
using BjBygg.Application.Commands.IdentityCommands.Login;
using BjBygg.Application.Commands.IdentityCommands.UpdatePassword;
using BjBygg.Application.Commands.IdentityCommands.UpdateProfile;
using BjBygg.Application.Commands.UserCommands.Update;
using BjBygg.Application.Queries.UserQueries;
using BjBygg.Application.Shared;
using CleanArchitecture.Core.Exceptions;
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
    public class AuthController : BaseController
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        [EnableCors]
        [HttpPost]
        [Route("api/[controller]/login")]
        public async Task<LoginResponse> Login([FromBody] LoginCommand command)
        {
            return await _mediator.Send(command);
        }

        [EnableCors]
        [Authorize]
        [HttpGet]
        [Route("api/[controller]")]
        public async Task<UserDto> Get()
        {
            return await _mediator.Send(new UserByUserNameQuery() { UserName = User.FindFirstValue("UserName") });
        }

        [Authorize]
        [HttpPut]
        [Route("api/[controller]")]
        public async Task<UserDto> UpdateProfile([FromBody] UpdateProfileCommand command)
        {
            command.UserName = User.FindFirstValue("UserName");

            if (!ModelState.IsValid)
                throw new BadRequestException(ModelState.Values.ToString());

            return await _mediator.Send(command);
        }

        [Authorize]
        [HttpPut]
        [Route("api/[controller]/changePassword")]
        public async Task<Unit> UpdatePassword([FromBody] UpdatePasswordCommand command)
        {
            command.UserName = User.FindFirstValue("UserName");

            return await _mediator.Send(command);
        }


    }
}
