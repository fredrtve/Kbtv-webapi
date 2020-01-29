using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BjBygg.Application.Commands.MissionCommands.Images.Delete;
using BjBygg.Application.Commands.MissionCommands.Images.Upload;
using BjBygg.Application.Shared;
using CleanArchitecture.Core.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BjBygg.WebApi.Controllers.Mission
{
    public class MissionImagesController : BaseController
    {
        private readonly IMediator _mediator;

        public MissionImagesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize(Roles = "Leder, Mellomleder, Ansatt")]
        [HttpPost]
        [Route("api/Missions/{missionId}/[controller]")]
        public async Task<IEnumerable<MissionImageDto>> Upload(int missionId)
        {
            if (Request.Form.Files.Count() == 0)
                throw new BadRequestException("No files received");

            var command = new UploadMissionImageCommand();
            command.Files = Request.Form.Files;
            command.MissionId = missionId;

            return await _mediator.Send(command);
        }

        [Authorize(Roles = "Leder")]
        [HttpDelete]
        [Route("api/Missions/{missionId}/[controller]/{id}")]
        public async Task<bool> Delete(DeleteMissionImageCommand command)
        {
            return await _mediator.Send(command);
        }
    }
}
