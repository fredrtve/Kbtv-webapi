using BjBygg.Application.Application.Commands.MissionCommands.Documents;
using BjBygg.Application.Application.Commands.MissionCommands.Documents.Mail;
using BjBygg.Application.Application.Commands.MissionCommands.Documents.Upload;
using BjBygg.Application.Application.Common.Dto;
using BjBygg.Application.Application.Queries.DbSyncQueries;
using BjBygg.Application.Application.Queries.DbSyncQueries.Common;
using BjBygg.Application.Common.Exceptions;
using CleanArchitecture.SharedKernel;
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
        public async Task<ActionResult<DbSyncResponse<MissionDocumentDto>>> Sync(MissionDocumentSyncQuery request)
        {
            return await Mediator.Send(request);
        }

        [Authorize(Roles = RolePermissions.MissionDocumentActions.Create)]
        [HttpPost]
        [Route("api/[controller]")]
        public async Task<ActionResult<MissionDocumentDto>> Upload(int missionId)
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

        [Authorize(Roles = RolePermissions.MissionDocumentActions.Delete)]
        [HttpDelete]
        [Route("api/[controller]/{id}")]
        public async Task<ActionResult> Delete(DeleteMissionDocumentCommand request)
        {
            await Mediator.Send(request);
            return NoContent();
        }

        [Authorize(Roles = RolePermissions.MissionDocumentActions.Delete)]
        [HttpPost]
        [Route("api/[controller]/DeleteRange")]
        public async Task<ActionResult> DeleteRange([FromBody] DeleteRangeMissionDocumentCommand request)
        {
            await Mediator.Send(request);
            return NoContent();
        }

        [Authorize(Roles = RolePermissions.MissionDocumentActions.SendEmail)]
        [HttpPost]
        [Route("api/[controller]/SendDocuments")]
        public async Task<ActionResult> SendDocuments([FromBody] MailMissionDocumentsCommand request)
        {
            await Mediator.Send(request);
            return NoContent();
        }

    }
}
