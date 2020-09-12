using BjBygg.Application.Identity.Commands.InboundEmailPasswordCommands.Create;
using BjBygg.Application.Identity.Commands.InboundEmailPasswordCommands.DeleteRange;
using BjBygg.Application.Identity.Common;
using BjBygg.Application.Identity.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BjBygg.WebApi.Controllers
{
    public class InboundEmailPasswordController : BaseController
    {
        public InboundEmailPasswordController() { }

        [Authorize(Roles = RolePermissions.InboundEmailPasswordActions.Read)]
        [HttpGet]
        [Route("api/[controller]")]
        public async Task<ActionResult<List<InboundEmailPasswordDto>>> GetAll(InboundEmailPasswordListQuery request)
        {
            return await Mediator.Send(request);
        }

        [Authorize(Roles = RolePermissions.InboundEmailPasswordActions.Create)]
        [HttpPost]
        [Route("api/[controller]")]
        public async Task<ActionResult> Create([FromBody] CreateInboundEmailPasswordCommand request)
        {
            await Mediator.Send(request);
            return NoContent();
        }

        [Authorize(Roles = RolePermissions.InboundEmailPasswordActions.Delete)]
        [HttpPost]
        [Route("api/[controller]/DeleteRange")]
        public async Task<ActionResult> DeleteRange([FromBody] DeleteRangeInboundEmailPasswordCommand request)
        {
            await Mediator.Send(request);
            return NoContent();
        }




    }
}