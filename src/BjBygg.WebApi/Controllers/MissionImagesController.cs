using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BjBygg.Application.Commands.MissionCommands.Images.Delete;
using BjBygg.Application.Commands.MissionCommands.Images.DeleteRange;
using BjBygg.Application.Commands.MissionCommands.Images.Mail;
using BjBygg.Application.Commands.MissionCommands.Images.Upload;
using BjBygg.Application.Queries.DbSyncQueries;
using BjBygg.Application.Queries.DbSyncQueries.MissionImageQuery;
using BjBygg.Application.Shared;
using CleanArchitecture.Core.Exceptions;
using CleanArchitecture.Core.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BjBygg.WebApi.Controllers
{
    public class MissionImagesController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IMailService _mailService;

        public MissionImagesController(IMediator mediator, IMailService mailService)
        {
            _mediator = mediator;
            _mailService = mailService;
        }

        [Authorize]
        [HttpGet]
        [Route("api/[controller]/[action]")]
        public async Task<DbSyncResponse<MissionImageDto>> Sync(MissionImageSyncQuery query)
        {
            return await _mediator.Send(query);
        }

        [Authorize(Roles = "Leder, Mellomleder, Ansatt")]
        [HttpPost]
        [Route("api/[controller]")]
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

        [Authorize(Roles = "Leder")]
        [HttpPost]
        [Route("api/[controller]/DeleteRange")]
        public async Task<bool> DeleteRange([FromBody] DeleteRangeMissionImageCommand command)
        {
            return await _mediator.Send(command);
        }


        [Authorize(Roles = "Leder")]
        [HttpPost]
        [Route("api/[controller]/SendImages")]
        public async Task<bool> SendImages([FromBody] MailMissionImagesCommand command)
        {
            return await _mediator.Send(command);
        }
    }
}
