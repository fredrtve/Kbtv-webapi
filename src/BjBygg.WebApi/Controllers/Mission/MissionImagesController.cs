using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BjBygg.Application.Commands.MissionCommands.Images.Delete;
using BjBygg.Application.Commands.MissionCommands.Images.Upload;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BjBygg.WebApi.Controllers.Mission
{
    public class MissionImagesController : Controller
    {
        private readonly IMediator _mediator;

        public MissionImagesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("api/Missions/{missionId}/[controller]")]
        public async Task<IActionResult> Upload(IFormCollection collection, int missionId)
        {
            var command = new UploadMissionImageCommand();
            command.Files = collection.Files;
            command.MissionId = missionId;
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Values);

            return Ok(await _mediator.Send(command));
        }

        [HttpDelete]
        [Route("api/Missions/{missionId}/[controller]/{id}")]
        public async Task<IActionResult> Delete(DeleteMissionImageCommand command)
        {
            return Ok(await _mediator.Send(command));
        }
    }
}
