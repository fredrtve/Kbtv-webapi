using BjBygg.Application.Application.Commands.MissionCommands.Images;
using BjBygg.Application.Application.Commands.MissionCommands.Images.Mail;
using BjBygg.Application.Application.Commands.MissionCommands.Images.Upload;
using BjBygg.Application.Application.Common.Dto;
using BjBygg.Application.Application.Queries.DbSyncQueries;
using BjBygg.Application.Application.Queries.DbSyncQueries.Common;
using BjBygg.Application.Common.Exceptions;
using CleanArchitecture.SharedKernel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BjBygg.WebApi.Controllers
{
    public class MissionImagesController : BaseController
    {
        public MissionImagesController() { }

        [Authorize]
        [HttpGet]
        [Route("api/[controller]/[action]")]
        public async Task<ActionResult<DbSyncArrayResponse<MissionImageDto>>> Sync(MissionImageSyncQuery request)
        {
            return await Mediator.Send(request);
        }

        [Authorize(Roles = RolePermissions.MissionImageActions.Create)]
        [HttpPost]
        [Route("api/[controller]")]
        public async Task<ActionResult> Upload(string missionId)
        {
            var request = new UploadMissionImageCommand()
            {
                MissionId = missionId
            };

            if (Request.Form.Files.Count() == 0)
            {
                await Mediator.Send(request);//Let validator throw exception
                return NoContent();
            }

            using (var streamList = new DisposableList<BasicFileStream>())
            {
                streamList.AddRange(Request.Form.Files.ToList()
                    .Select(x => new BasicFileStream(x.OpenReadStream(), x.FileName)));

                request.Files = streamList;
                 
                await Mediator.Send(request);
                return NoContent();
            }          
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
