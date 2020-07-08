using BjBygg.Application.Commands.EmployerCommands;
using BjBygg.Application.Commands.EmployerCommands.Create;
using BjBygg.Application.Commands.EmployerCommands.Update;
using BjBygg.Application.Common;
using BjBygg.Application.Queries.DbSyncQueries;
using BjBygg.Application.Queries.DbSyncQueries.Common;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BjBygg.WebApi.Controllers
{
    public class EmployersController : BaseController
    {
        public EmployersController() { }

        [Authorize]
        [HttpGet]
        [Route("api/[controller]/[action]")]
        public async Task<ActionResult<DbSyncResponse<EmployerDto>>> Sync(EmployerSyncQuery request)
        {
            return await Mediator.Send(request);
        }

        [Authorize(Roles = "Leder, Mellomleder")]
        [HttpPost]
        [Route("api/[controller]")]
        public async Task<ActionResult<EmployerDto>> Create([FromBody] CreateEmployerCommand request)
        {
            return await Mediator.Send(request);
        }

        [Authorize(Roles = "Leder")]
        [HttpPut]
        [Route("api/[controller]/{Id}")]
        public async Task<ActionResult<EmployerDto>> Update([FromBody] UpdateEmployerCommand request)
        {
            return await Mediator.Send(request);
        }

        [Authorize(Roles = "Leder")]
        [HttpDelete]
        [Route("api/[controller]/{Id}")]
        public async Task<ActionResult> Delete(DeleteEmployerCommand request)
        {
            await Mediator.Send(request);
            return NoContent();
        }

        [Authorize(Roles = "Leder")]
        [HttpPost]
        [Route("api/[controller]/DeleteRange")]
        public async Task<ActionResult> DeleteRange([FromBody] DeleteRangeEmployerCommand request)
        {
            await Mediator.Send(request);
            return NoContent();
        }
    }
}
