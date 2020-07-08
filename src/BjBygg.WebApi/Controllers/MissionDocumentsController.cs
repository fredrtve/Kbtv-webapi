using BjBygg.Application.Commands.MissionCommands.Documents;
using BjBygg.Application.Commands.MissionCommands.Documents.Mail;
using BjBygg.Application.Commands.MissionCommands.Documents.Upload;
using BjBygg.Application.Common;
using BjBygg.Application.Queries.DbSyncQueries;
using BjBygg.Application.Queries.DbSyncQueries.Common;
using BjBygg.Application.Common.Exceptions;
using CleanArchitecture.SharedKernel;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BjBygg.WebApi.Controllers
{
    public class MissionDocumentsController : BaseController
    {
        public MissionDocumentsController() { }

        [Authorize]
        [HttpGet]
        [Route("api/[controller]/[action]")]
        public async Task<DbSyncResponse<MissionDocumentDto>> Sync(MissionDocumentSyncQuery request)
        {
            return await Mediator.Send(request);
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

                return await Mediator.Send(request);
            }
        }

        [Authorize(Roles = "Leder")]
        [HttpDelete]
        [Route("api/[controller]/{id}")]
        public async Task<Unit> Delete(DeleteMissionDocumentCommand request)
        {
            return await Mediator.Send(request);
        }

        [Authorize(Roles = "Leder")]
        [HttpPost]
        [Route("api/[controller]/DeleteRange")]
        public async Task<Unit> DeleteRange([FromBody] DeleteRangeMissionDocumentCommand request)
        {
            return await Mediator.Send(request);
        }

        [Authorize(Roles = "Leder")]
        [HttpPost]
        [Route("api/[controller]/SendDocuments")]
        public async Task<Unit> SendDocuments([FromBody] MailMissionDocumentsCommand request)
        {
            return await Mediator.Send(request);
        }

    }
}
