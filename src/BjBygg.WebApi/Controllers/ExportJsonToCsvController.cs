using System;
using System.Threading.Tasks;
using BjBygg.Application.Commands.ExportCsvCommands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BjBygg.WebApi.Controllers
{
    public class ExportJsonToCsvController : BaseController
    {
        public ExportJsonToCsvController(IMediator mediator, ILogger<ExportJsonToCsvController> logger) :
            base(mediator, logger) {}

        [Authorize(Roles = "Leder")]
        [HttpPost]
        [Route("api/[controller]")]
        public async Task<Uri> Export([FromBody] ExportJsonToCsvCommand request)
        {
            return await ValidateAndExecute(request);
        }

    }
}
