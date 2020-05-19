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
using BjBygg.Application.Shared;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BjBygg.WebApi.Controllers.User
{
    public class UsersController : BaseController
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

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
        public async Task<UserDto> Create([FromBody] CreateUserCommand command)
        {
            if (!ModelState.IsValid)
                throw new BadRequestException(ModelState.Values.ToString());

            return await _mediator.Send(command);
        }

        [Authorize(Roles = "Leder")]
        [HttpGet]
        [Route("api/[controller]/{UserName}")]
        public async Task<UserDto> GetUser(UserByUserNameQuery query)
        {
            return await _mediator.Send(query);
        }

        [Authorize(Roles = "Leder")]
        [HttpPut]
        [Route("api/[controller]/{UserName}")]
        public async Task<UserDto> Update([FromBody] UpdateUserCommand command)
        {
            if (!ModelState.IsValid)
                throw new BadRequestException(ModelState.Values.ToString());

            return await _mediator.Send(command);
        }

        [Authorize(Roles = "Leder")]
        [HttpDelete]
        [Route("api/[controller]/{UserName}")]
        public async Task<bool> Delete(DeleteUserCommand command)
        {
            return await _mediator.Send(command);
        }

    }
}
