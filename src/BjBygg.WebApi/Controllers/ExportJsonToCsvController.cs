using BjBygg.Application.Application.Commands.ExportCsvCommand;
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
        public async Task<ActionResult<Uri>> Export([FromBody] ExportJsonToCsvCommand request)
        {
            return await Mediator.Send(request);
        }

    }
}
