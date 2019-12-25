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

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BjBygg.WebApi.Controllers.User
{
    public class UsersController : Controller
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("api/[controller]")]
        public IActionResult Index(string? role)
        {
            return Ok(_mediator.Send(new UserListQuery() { Role = role }));
        }

        [HttpGet]
        [Route("api/[controller]/{UserName}")]
        public async Task<IActionResult> GetUser(UserByUserNameQuery query)
        {
            var result = await _mediator.Send(query);
            if (result == null) return NotFound($"User does not exist (username = {query.UserName})");
            return Ok(result);
        }

        [HttpPost]
        [Route("api/[controller]")]
        public async Task<IActionResult> Create([FromBody] CreateUserCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Values);

            return Ok(await _mediator.Send(command));
        }

        [HttpPut]
        [Route("api/[controller]/{UserName}")]
        public async Task<IActionResult> Update([FromBody] UpdateUserCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Values);

            return Ok(await _mediator.Send(command));
        }

        [HttpDelete]
        [Route("api/[controller]/{UserName}")]
        public async Task<IActionResult> Delete(DeleteUserCommand command)
        {
            var result = await _mediator.Send(command);

            if (!result) return NotFound($"User does not exist (username = {command.UserName})");

            return Ok(result);
        }

    }
}
