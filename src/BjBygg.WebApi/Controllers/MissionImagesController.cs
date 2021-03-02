using BjBygg.Application.Application.Commands.MissionCommands.Images;
using BjBygg.Application.Application.Commands.MissionCommands.Images.Mail;
using BjBygg.Application.Application.Commands.MissionCommands.Images.Upload;
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
    public class MissionImagesController : BaseController
    {
        public MissionImagesController() { }

        [Authorize(Roles = RolePermissions.MissionImageActions.Create)]
        [HttpPost]
        [Route("api/[controller]")]
        public async Task<ActionResult> Upload()
        {
            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore
            };

            var request = JsonConvert.DeserializeObject<UploadMissionImageCommand>(Request.Form["Command"], settings);

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
