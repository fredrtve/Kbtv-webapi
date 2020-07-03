using BjBygg.Application.Commands.InboundEmailPasswordCommands.Create;
using BjBygg.Application.Commands.InboundEmailPasswordCommands.DeleteRange;
using BjBygg.Application.Commands.InboundEmailPasswordCommands.Verify;
using BjBygg.Application.Queries;
using BjBygg.Application.Shared.Dto;
using CleanArchitecture.Core.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BjBygg.WebApi.Controllers
{
    public class InboundEmailPasswordController : BaseController
    {
        public InboundEmailPasswordController(IMediator mediator, ILogger<InboundEmailPasswordController> logger) :
            base(mediator, logger){}

        [Authorize(Roles = "Leder")]
        [HttpGet]
        [Route("api/[controller]")]
        public async Task<IEnumerable<InboundEmailPasswordDto>> GetAll(InboundEmailPasswordListQuery request)
        {
            return await _mediator.Send(request);
        }

        [Authorize(Roles = "Leder")]
        [HttpPost]
        [Route("api/[controller]")]
        public async Task<InboundEmailPasswordDto> Create([FromBody] CreateInboundEmailPasswordCommand request)
        {
            return await ValidateAndExecute(request);
        }

        [Authorize(Roles = "Leder")]
        [HttpPost]
        [Route("api/[controller]/DeleteRange")]
        public async Task<bool> DeleteRange([FromBody] DeleteRangeInboundEmailPasswordCommand request)
        {
            return await ValidateAndExecute(request);
        }


  

    }
}