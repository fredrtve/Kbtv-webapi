using System;
using System.IO;
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
using CleanArchitecture.SharedKernel;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace BjBygg.WebApi.Controllers
{
    public class MissionDocumentsController : BaseController
    {
        public MissionDocumentsController(IMediator mediator, ILogger<MissionDocumentsController> logger) :
            base(mediator, logger){}

        [Authorize]
        [HttpGet]
        [Route("api/[controller]/[action]")]
        public async Task<DbSyncResponse<MissionDocumentDto>> Sync(MissionDocumentSyncQuery request)
        {
            return await ValidateAndExecute(request);
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

            var file = Request.Form.Files[0];

            using (var stream = file.OpenReadStream())
            {
                var request = new UploadMissionDocumentCommand()
                {
                    File = new BasicFileStream(stream, Path.GetExtension(file.FileName)),
                    DocumentType = JsonConvert.DeserializeObject<DocumentTypeDto>(Request.Form["DocumentType"], settings),
                    MissionId = missionId
                };

                return await ValidateAndExecute(request);
            }   
        }

        [Authorize(Roles = "Leder")]
        [HttpDelete]
        [Route("api/[controller]/{id}")]
        public async Task<bool> Delete(DeleteMissionDocumentCommand request)
        {
            return await ValidateAndExecute(request);
        }

        [Authorize(Roles = "Leder")]
        [HttpPost]
        [Route("api/[controller]/DeleteRange")]
        public async Task<bool> DeleteRange([FromBody] DeleteRangeMissionDocumentCommand request)
        {
            return await ValidateAndExecute(request);
        }


        [Authorize(Roles = "Leder")]
        [HttpPost]
        [Route("api/[controller]/SendDocuments")]
        public async Task<bool> SendDocuments([FromBody] MailMissionDocumentsCommand request)
        {
            return await ValidateAndExecute(request);
        }

    }
}
