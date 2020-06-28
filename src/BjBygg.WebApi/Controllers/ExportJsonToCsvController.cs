using System;
using System.Threading.Tasks;
using BjBygg.Application.Commands.ExportCsvCommands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BjBygg.WebApi.Controllers
{
    public class ExportJsonToCsvController : BaseController
    {
        private readonly IMediator _mediator;

        public ExportJsonToCsvController(IMediator mediator)
        {
            _mediator = mediator;
        }
 
        [Authorize(Roles = "Leder")]
        [HttpPost]
        [Route("api/[controller]")]
        public async Task<Uri> Export([FromBody] ExportJsonToCsvCommand command)
        {
            return await _mediator.Send(command);
        }

    }
}
