using BjBygg.Application.Commands.UserCommands.Create;
using BjBygg.Application.Commands.UserCommands.Delete;
using BjBygg.Application.Commands.UserCommands.NewPassword;
using BjBygg.Application.Commands.UserCommands.Update;
using BjBygg.Application.Common;
using BjBygg.Application.Queries.UserQueries;
using BjBygg.Application.Queries.UserQueries.UserByUserName;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BjBygg.WebApi.Controllers.User
{
    public class UsersController : BaseController
    {
        public UsersController() { }

        [HttpGet]
        [Route("/")]
        public IActionResult Home()
        {
            return Redirect("/swagger");
        }

        [Authorize(Roles = "Leder")]
        [HttpGet]
        [Route("api/[controller]")]
        public async Task<IEnumerable<UserDto>> Index()
        {
            return await Mediator.Send(new UserListQuery());
        }

        [Authorize(Roles = "Leder")]
        [HttpPost]
        [Route("api/[controller]")]
        public async Task<UserDto> Create([FromBody] CreateUserCommand request)
        {
            return await Mediator.Send(request);
        }

        [Authorize(Roles = "Leder")]
        [HttpGet]
        [Route("api/[controller]/{UserName}")]
        public async Task<UserDto> GetUser(UserByUserNameQuery request)
        {
            return await Mediator.Send(request);
        }

        [Authorize(Roles = "Leder")]
        [HttpPut]
        [Route("api/[controller]/{UserName}")]
        public async Task<UserDto> Update([FromBody] UpdateUserCommand request)
        {
            return await Mediator.Send(request);
        }

        [Authorize(Roles = "Leder")]
        [HttpPut]
        [Route("api/[controller]/{UserName}/[action]")]
        public async Task<Unit> NewPassword([FromBody] NewPasswordCommand request)
        {
            return await Mediator.Send(request);
        }

        [Authorize(Roles = "Leder")]
        [HttpDelete]
        [Route("api/[controller]/{UserName}")]
        public async Task<Unit> Delete(DeleteUserCommand request)
        {
            return await Mediator.Send(request);
        }

    }
}
