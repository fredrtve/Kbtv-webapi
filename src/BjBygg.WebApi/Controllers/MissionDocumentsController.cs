using BjBygg.Application.Application.Commands.MissionCommands.Documents;
using BjBygg.Application.Application.Commands.MissionCommands.Documents.Mail;
using BjBygg.Application.Application.Commands.MissionCommands.Documents.Upload;
using BjBygg.Application.Application.Common.Dto;
using BjBygg.Application.Application.Queries.DbSyncQueries;
using BjBygg.Application.Application.Queries.DbSyncQueries.Common;
using CleanArchitecture.SharedKernel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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
        public async Task<ActionResult<DbSyncArrayResponse<MissionDocumentDto>>> Sync(MissionDocumentSyncQuery request)
        {
            return await Mediator.Send(request);
        }

        [Authorize(Roles = RolePermissions.MissionDocumentActions.Create)]
        [HttpPost]
        [Route("api/[controller]")]
        public async Task<ActionResult> Upload()
        {
            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore
            };

            var request = JsonConvert.DeserializeObject<UploadMissionDocumentCommand>(Request.Form["Command"], settings);

            if (Request.Form.Files.Count() == 0)
            {
                await Mediator.Send(request);//Let validator throw exception
                return NoContent();
            }

            var file = Request.Form.Files[0];

            using (var stream = file.OpenReadStream())
            {
                request.File = new BasicFileStream(stream, file.FileName);
                await Mediator.Send(request);
            }

            return NoContent();
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
        [Route("api/[controller]/Mail")]
        public async Task<ActionResult> Mail([FromBody] MailMissionDocumentsCommand request)
        {
            await Mediator.Send(request);
            return NoContent();
        }

    }
}
