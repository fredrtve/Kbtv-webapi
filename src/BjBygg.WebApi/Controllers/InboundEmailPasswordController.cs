using BjBygg.Application.Commands.InboundEmailPasswordCommands.Create;
using BjBygg.Application.Commands.InboundEmailPasswordCommands.DeleteRange;
using BjBygg.Application.Common.Dto;
using BjBygg.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BjBygg.WebApi.Controllers
{
    public class InboundEmailPasswordController : BaseController
    {
        public InboundEmailPasswordController() { }

        [Authorize(Roles = "Leder")]
        [HttpGet]
        [Route("api/[controller]")]
        public async Task<ActionResult<List<InboundEmailPasswordDto>>> GetAll(InboundEmailPasswordListQuery request)
        {
            return await Mediator.Send(request);
        }

        [Authorize(Roles = "Leder")]
        [HttpPost]
        [Route("api/[controller]")]
        public async Task<ActionResult<InboundEmailPasswordDto>> Create([FromBody] CreateInboundEmailPasswordCommand request)
        {
            return await Mediator.Send(request);
        }

        [Authorize(Roles = "Leder")]
        [HttpPost]
        [Route("api/[controller]/DeleteRange")]
        public async Task<ActionResult> DeleteRange([FromBody] DeleteRangeInboundEmailPasswordCommand request)
        {
            await Mediator.Send(request);
            return NoContent();
        }




    }
}