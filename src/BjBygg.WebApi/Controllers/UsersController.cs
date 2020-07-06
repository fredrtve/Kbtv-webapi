using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BjBygg.Application.Commands.UserCommands.Delete;
using BjBygg.Application.Commands.UserCommands.Create;
using BjBygg.Application.Commands.UserCommands.Update;
using BjBygg.Application.Queries.UserQueries;
using BjBygg.Application.Queries.UserQueries.List;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using CleanArchitecture.Core.Exceptions;
using BjBygg.Application.Common;
using BjBygg.Application.Commands.UserCommands.NewPassword;
using Microsoft.Extensions.Logging;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BjBygg.WebApi.Controllers.User
{
    public class UsersController : BaseController
    {
        public UsersController(IMediator mediator, ILogger<UserTimesheetsController> logger) : 
            base(mediator, logger) {}

        [HttpGet]
        [Route("/")]
        public IActionResult Home()
        {
            return Redirect("/swagger");
        }

        [Authorize(Roles = "Leder")]
        [HttpGet]
        [Route("api/[controller]")]
        public Task<IEnumerable<UserListItemDto>> Index()
        {
            return _mediator.Send(new UserListQuery());
        }

        [Authorize(Roles = "Leder")]
        [HttpPost]
        [Route("api/[controller]")]
        public async Task<UserDto> Create([FromBody] CreateUserCommand request)
        {
            return await ValidateAndExecute(request);
        }

        [Authorize(Roles = "Leder")]
        [HttpGet]
        [Route("api/[controller]/{UserName}")]
        public async Task<UserDto> GetUser(UserByUserNameQuery request)
        {
            return await ValidateAndExecute(request);
        }

        [Authorize(Roles = "Leder")]
        [HttpPut]
        [Route("api/[controller]/{UserName}")]
        public async Task<UserDto> Update([FromBody] UpdateUserCommand request)
        {
            return await ValidateAndExecute(request);
        }

        [Authorize(Roles = "Leder")]
        [HttpPut]
        [Route("api/[controller]/{UserName}/[action]")]
        public async Task<bool> NewPassword([FromBody] NewPasswordCommand request)
        {
            return await ValidateAndExecute(request);
        }

        [Authorize(Roles = "Leder")]
        [HttpDelete]
        [Route("api/[controller]/{UserName}")]
        public async Task<bool> Delete(DeleteUserCommand request)
        {
            return await ValidateAndExecute(request);
        }

    }
}
