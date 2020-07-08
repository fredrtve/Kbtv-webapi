using BjBygg.Application.Commands.MissionCommands.Images;
using BjBygg.Application.Commands.MissionCommands.Images.Mail;
using BjBygg.Application.Commands.MissionCommands.Images.Upload;
using BjBygg.Application.Common;
using BjBygg.Application.Queries.DbSyncQueries;
using BjBygg.Application.Queries.DbSyncQueries.Common;
using CleanArchitecture.Core.Exceptions;
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
        public async Task<ActionResult<DbSyncResponse<MissionImageDto>>> Sync(MissionImageSyncQuery request)
        {
            return await Mediator.Send(request);
        }

        [Authorize(Roles = "Leder, Mellomleder, Ansatt")]
        [HttpPost]
        [Route("api/[controller]")]
        public async Task<ActionResult<IEnumerable<MissionImageDto>>> Upload(int missionId)
        {
            if (Request.Form.Files.Count() == 0)
                throw new BadRequestException("No files received");

            using (var streamList = new DisposableList<BasicFileStream>())
            {
                streamList.AddRange(Request.Form.Files.ToList()
                    .Select(x => new BasicFileStream(x.OpenReadStream(), Path.GetExtension(x.FileName))));

                var request = new UploadMissionImageCommand()
                {
                    Files = streamList,
                    MissionId = missionId
                };

                return await Mediator.Send(request);
            }
        }

        [Authorize(Roles = "Leder")]
        [HttpDelete]
        [Route("api/[controller]/{id}")]
        public async Task<ActionResult> Delete(DeleteMissionImageCommand request)
        {
            await Mediator.Send(request);
            return NoContent();
        }

        [Authorize(Roles = "Leder")]
        [HttpPost]
        [Route("api/[controller]/DeleteRange")]
        public async Task<ActionResult> DeleteRange([FromBody] DeleteRangeMissionImageCommand request)
        {
            await Mediator.Send(request);
            return NoContent();
        }

        [Authorize(Roles = "Leder")]
        [HttpPost]
        [Route("api/[controller]/SendImages")]
        public async Task<ActionResult> SendImages([FromBody] MailMissionImagesCommand request)
        {
            await Mediator.Send(request);
            return NoContent();
        }
    }
}
