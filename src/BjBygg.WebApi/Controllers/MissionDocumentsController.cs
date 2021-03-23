using BjBygg.Application.Application.Commands.MissionCommands.Documents;
using BjBygg.Application.Application.Commands.MissionCommands.Documents.Mail;
using BjBygg.Application.Application.Commands.MissionCommands.Documents.Upload;
using BjBygg.Application.Application.Common.Dto;
using BjBygg.Application.Application.Queries.DbSyncQueries;
using BjBygg.Application.Application.Queries.DbSyncQueries.Common;
using BjBygg.SharedKernel;
using BjBygg.WebApi.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Linq;
using System.Threading.Tasks;

namespace BjBygg.WebApi.Controllers
{
    public class MissionDocumentsController : BaseController
    {
        public MissionDocumentsController() { }

        [Authorize(Roles = RolePermissions.MissionDocumentActions.Create)]
        [HttpPost]
        [Route("api/[controller]")]
        public async Task<ActionResult> Upload([FromForm] UploadMissionDocumentFormDto form)
        {
            var request = new UploadMissionDocumentCommand() { Id = form.Id, MissionId = form.MissionId, Name = form.Name };

            using (var stream = form.File.OpenReadStream())
            {
                request.File = new BasicFileStream(stream, form.File.FileName);
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
