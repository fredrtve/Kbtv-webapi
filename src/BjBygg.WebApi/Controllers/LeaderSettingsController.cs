using BjBygg.Application.Application.Commands;
using BjBygg.Application.Application.Commands.EmployerCommands;
using BjBygg.Application.Application.Commands.EmployerCommands.Create;
using BjBygg.Application.Application.Commands.EmployerCommands.Update;
using BjBygg.Application.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BjBygg.WebApi.Controllers
{
    public class LeaderSettingsController : BaseController
    {
        public LeaderSettingsController() { }

        [Authorize(Roles = Roles.Leader)]
        [HttpPut]
        [Route("api/[controller]")]
        public async Task<ActionResult> Update([FromBody] UpdateLeaderSettingsCommand request)
        {
            await Mediator.Send(request);
            return NoContent();
        }

    }
}
