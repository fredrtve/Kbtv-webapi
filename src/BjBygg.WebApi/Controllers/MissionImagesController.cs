using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BjBygg.Application.Commands.MissionCommands.Images.Delete;
using BjBygg.Application.Commands.MissionCommands.Images.Upload;
using BjBygg.Application.Queries.DbSyncQueries;
using BjBygg.Application.Queries.DbSyncQueries.MissionImageQuery;
using BjBygg.Application.Shared;
using CleanArchitecture.Core.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BjBygg.WebApi.Controllers
{
    public class MissionImagesController : BaseController
    {
        private readonly IMediator _mediator;

        public MissionImagesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize]
        [HttpPost]
        [Route("api/[controller]/[action]")]
        public async Task<DbSyncResponse<MissionImageDto>> Sync([FromBody] MissionImageSyncQuery query)
        {
            return await _mediator.Send(query);
        }

        [Authorize(Roles = "Leder, Mellomleder, Ansatt")]
        [HttpPost]
        [Route("api/[controller]/{missionId}")]
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
        [Route("api/[controller]/{id}")]
        public async Task<bool> Delete(DeleteMissionImageCommand command)
        {
            return await _mediator.Send(command);
        }
    }
}
