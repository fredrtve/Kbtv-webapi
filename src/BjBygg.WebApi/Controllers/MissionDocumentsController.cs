using System;
using System.Linq;
using System.Threading.Tasks;
using BjBygg.Application.Commands.MissionCommands.Documents.Delete;
using BjBygg.Application.Commands.MissionCommands.Documents.DeleteRange;
using BjBygg.Application.Commands.MissionCommands.Documents.Mail;
using BjBygg.Application.Commands.MissionCommands.Documents.Upload;
using BjBygg.Application.Queries.DbSyncQueries;
using BjBygg.Application.Queries.DbSyncQueries.MissionDocumentQuery;
using BjBygg.Application.Shared;
using CleanArchitecture.Core.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BjBygg.WebApi.Controllers
{
    public class MissionDocumentsController : BaseController
    {
        private readonly IMediator _mediator;

        public MissionDocumentsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize]
        [HttpGet]
        [Route("api/[controller]/[action]")]
        public async Task<DbSyncResponse<MissionDocumentDto>> Sync(MissionDocumentSyncQuery query)
        {
            return await _mediator.Send(query);
        }

        [Authorize(Roles = "Leder")]
        [HttpPost]
        [Route("api/[controller]")]
        public async Task<MissionDocumentDto> Upload(int missionId)
        {
            if (Request.Form.Files.Count() == 0)
                throw new BadRequestException("No files received");

            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore
            };

            var command = new UploadMissionDocumentCommand()
            {
                File = Request.Form.Files[0],
                DocumentType = JsonConvert.DeserializeObject<DocumentTypeDto>(Request.Form["DocumentType"], settings),
                MissionId = missionId
            };

            return await _mediator.Send(command);
        }

        [Authorize(Roles = "Leder")]
        [HttpDelete]
        [Route("api/[controller]/{id}")]
        public async Task<bool> Delete(DeleteMissionDocumentCommand command)
        {
            return await _mediator.Send(command);
        }

        [Authorize(Roles = "Leder")]
        [HttpPost]
        [Route("api/[controller]/DeleteRange")]
        public async Task<bool> DeleteRange([FromBody] DeleteRangeMissionDocumentCommand command)
        {
            return await _mediator.Send(command);
        }


        [Authorize(Roles = "Leder")]
        [HttpPost]
        [Route("api/[controller]/SendDocuments")]
        public async Task<bool> SendImages([FromBody] MailMissionDocumentsCommand command)
        {
            return await _mediator.Send(command);
        }

    }
}
