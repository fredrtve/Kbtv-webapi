using BjBygg.Application.Identity.Commands.UserCommands.Create;
using BjBygg.Application.Identity.Commands.UserCommands.Delete;
using BjBygg.Application.Identity.Commands.UserCommands.NewPassword;
using BjBygg.Application.Identity.Commands.UserCommands.Update;
using BjBygg.Application.Identity.Common;
using BjBygg.Application.Identity.Queries.UserQueries;
using BjBygg.Application.Identity.Queries.UserQueries.UserByUserName;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BjBygg.WebApi.Controllers
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
        public async Task<ActionResult<IEnumerable<UserDto>>> Index()
        {
            return await Mediator.Send(new UserListQuery());
        }

        [Authorize(Roles = "Leder")]
        [HttpPost]
        [Route("api/[controller]")]
        public async Task<ActionResult<UserDto>> Create([FromBody] CreateUserCommand request)
        {
            return await Mediator.Send(request);
        }

        [Authorize(Roles = "Leder")]
        [HttpGet]
        [Route("api/[controller]/{UserName}")]
        public async Task<ActionResult<UserDto>> GetUser(UserByUserNameQuery request)
        {
            return await Mediator.Send(request);
        }

        [Authorize(Roles = "Leder")]
        [HttpPut]
        [Route("api/[controller]/{UserName}")]
        public async Task<ActionResult<UserDto>> Update([FromBody] UpdateUserCommand request)
        {
            return await Mediator.Send(request);
        }

        [Authorize(Roles = "Leder")]
        [HttpPut]
        [Route("api/[controller]/{UserName}/[action]")]
        public async Task<ActionResult> NewPassword([FromBody] NewPasswordCommand request)
        {
            await Mediator.Send(request);
            return NoContent();
        }

        [Authorize(Roles = "Leder")]
        [HttpDelete]
        [Route("api/[controller]/{UserName}")]
        public async Task<ActionResult> Delete(DeleteUserCommand request)
        {
            await Mediator.Send(request);
            return NoContent();
        }

    }
}
