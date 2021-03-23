using BjBygg.Application.Application.Commands.MissionCommands.Images;
using BjBygg.Application.Application.Commands.MissionCommands.Images.Mail;
using BjBygg.Application.Application.Commands.MissionCommands.Images.Upload;
using BjBygg.Application.Application.Common.Dto;
using BjBygg.Application.Application.Queries.DbSyncQueries;
using BjBygg.Application.Application.Queries.DbSyncQueries.Common;
using BjBygg.SharedKernel;
using BjBygg.WebApi.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Linq;
using System.Threading.Tasks;

namespace BjBygg.WebApi.Controllers
{
    public class MissionImagesController : BaseController
    {
        public MissionImagesController() { }

        [Authorize(Roles = RolePermissions.MissionImageActions.Create)]
        [HttpPost]
        [Route("api/[controller]")]
        public async Task<ActionResult> Upload(UploadMissionImageFormDto form)
        {
            var request = new UploadMissionImageCommand() { Id = form.Id, MissionId = form.MissionId };
            using (var stream = form.File.OpenReadStream())
            {
                request.File = new BasicFileStream(stream, form.File.FileName);
                await Mediator.Send(request);
            }

            return NoContent();
        }

        [Authorize(Roles = RolePermissions.MissionImageActions.Delete)]
        [HttpDelete]
        [Route("api/[controller]/{id}")]
        public async Task<ActionResult> Delete(DeleteMissionImageCommand request)
        {
            await Mediator.Send(request);
            return NoContent();
        }

        [Authorize(Roles = RolePermissions.MissionImageActions.Delete)]
        [HttpPost]
        [Route("api/[controller]/DeleteRange")]
        public async Task<ActionResult> DeleteRange([FromBody] DeleteRangeMissionImageCommand request)
        {
            await Mediator.Send(request);
            return NoContent();
        }

        [Authorize(Roles = RolePermissions.MissionImageActions.SendEmail)]
        [HttpPost]
        [Route("api/[controller]/Mail")]
        public async Task<ActionResult> Mail([FromBody] MailMissionImagesCommand request)
        {
            await Mediator.Send(request);
            return NoContent();
        }
    }
}
