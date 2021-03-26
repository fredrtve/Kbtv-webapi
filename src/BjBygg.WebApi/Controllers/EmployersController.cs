using BjBygg.Application.Application.Commands.EmployerCommands;
using BjBygg.Application.Application.Commands.EmployerCommands.Create;
using BjBygg.Application.Application.Commands.EmployerCommands.Update;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BjBygg.WebApi.Controllers
{
    public class EmployersController : BaseController
    {
        public EmployersController() { }

        [Authorize(Roles = RolePermissions.EmployerActions.Create)]
        [HttpPost]
        [Route("api/[controller]")]
        public async Task<ActionResult> Create([FromBody] CreateEmployerCommand request)
        {
            await Mediator.Send(request);
            return NoContent();
        }

        [Authorize(Roles = RolePermissions.EmployerActions.Update)]
        [HttpPut]
        [Route("api/[controller]/{Id}")]
        public async Task<ActionResult> Update([FromBody] UpdateEmployerCommand request)
        {
            await Mediator.Send(request);
            return NoContent();
        }

        [Authorize(Roles = RolePermissions.EmployerActions.Delete)]
        [HttpDelete]
        [Route("api/[controller]/{Id}")]
        public async Task<ActionResult> Delete(DeleteEmployerCommand request)
        {
            await Mediator.Send(request);
            return NoContent();
        }

        [Authorize(Roles = RolePermissions.EmployerActions.Delete)]
        [HttpPost]
        [Route("api/[controller]/DeleteRange")]
        public async Task<ActionResult> DeleteRange([FromBody] DeleteRangeEmployerCommand request)
        {
            await Mediator.Send(request);
            return NoContent();
        }
    }
}
