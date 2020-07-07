using BjBygg.Application.Commands.ExportCsvCommands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BjBygg.WebApi.Controllers
{
    public class ExportJsonToCsvController : BaseController
    {
        public ExportJsonToCsvController() { }

        [Authorize(Roles = "Leder")]
        [HttpPost]
        [Route("api/[controller]")]
        public async Task<Uri> Export([FromBody] ExportJsonToCsvCommand request)
        {
            return await Mediator.Send(request);
        }

    }
}
