using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BjBygg.Application.Commands.MissionCommands.Images.Delete;
using BjBygg.Application.Commands.MissionCommands.Images.DeleteRange;
using BjBygg.Application.Commands.MissionCommands.Images.Mail;
using BjBygg.Application.Commands.MissionCommands.Images.Upload;
using BjBygg.Application.Queries.DbSyncQueries;
using BjBygg.Application.Queries.DbSyncQueries.MissionImageQuery;
using BjBygg.Application.Shared;
using CleanArchitecture.Core.Exceptions;
using CleanArchitecture.Core.Interfaces;
using CleanArchitecture.Core.Interfaces.Services;
using CleanArchitecture.SharedKernel;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BjBygg.WebApi.Controllers
{
    public class MissionImagesController : BaseController
    {
        public MissionImagesController(IMediator mediator, ILogger<MissionImagesController> logger) :
            base(mediator, logger) {}

        [Authorize]
        [HttpGet]
        [Route("api/[controller]/[action]")]
        public async Task<DbSyncResponse<MissionImageDto>> Sync(MissionImageSyncQuery request)
        {
            return await ValidateAndExecute(request);
        }

        [Authorize(Roles = "Leder, Mellomleder, Ansatt")]
        [HttpPost]
        [Route("api/[controller]")]
        public async Task<IEnumerable<MissionImageDto>> Upload(int missionId)
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

                return await ValidateAndExecute(request);
            }
        }

        [Authorize(Roles = "Leder")]
        [HttpDelete]
        [Route("api/[controller]/{id}")]
        public async Task<bool> Delete(DeleteMissionImageCommand request)
        {
            return await ValidateAndExecute(request);
        }

        [Authorize(Roles = "Leder")]
        [HttpPost]
        [Route("api/[controller]/DeleteRange")]
        public async Task<bool> DeleteRange([FromBody] DeleteRangeMissionImageCommand request)
        {
            return await ValidateAndExecute(request);
        }


        [Authorize(Roles = "Leder")]
        [HttpPost]
        [Route("api/[controller]/SendImages")]
        public async Task<bool> SendImages([FromBody] MailMissionImagesCommand request)
        {
            return await ValidateAndExecute(request);
        }
    }
}
