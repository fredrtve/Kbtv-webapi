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
        private readonly IMediator _mediator;
        private readonly ILogger<InboundEmailPasswordController> _logger;

        public InboundEmailPasswordController(IMediator mediator, ILogger<InboundEmailPasswordController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [Authorize(Roles = "Leder")]
        [HttpGet]
        [Route("api/[controller]")]
        public async Task<IEnumerable<InboundEmailPasswordDto>> GetAll(InboundEmailPasswordListQuery query)
        {
            return await _mediator.Send(query);
        }

        [Authorize(Roles = "Leder")]
        [HttpPost]
        [Route("api/[controller]")]
        public async Task<InboundEmailPasswordDto> Create([FromBody] CreateInboundEmailPasswordCommand command)
        {
            if (!ModelState.IsValid)
                throw new BadRequestException(ModelState.Values.ToString());

            return await _mediator.Send(command);
        }

        //[Authorize(Roles = "Leder")]
        //[HttpPost]
        //[Route("api/[controller]/[action]")]
        //public async Task<bool> Verify(VerifyInboundEmailPasswordCommand command)
        //{
        //    if (!ModelState.IsValid)
        //        throw new BadRequestException(ModelState.Values.ToString());

        //    return await _mediator.Send(command);
        //}

        [Authorize(Roles = "Leder")]
        [HttpPost]
        [Route("api/[controller]/DeleteRange")]
        public async Task<bool> DeleteRange([FromBody] DeleteRangeInboundEmailPasswordCommand command)
        {
            if (!ModelState.IsValid)
                throw new BadRequestException(ModelState.Values.ToString());
            
            return await _mediator.Send(command);
        }


  

    }
}