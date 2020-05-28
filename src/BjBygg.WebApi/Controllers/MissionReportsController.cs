using System;
using System.Linq;
using System.Threading.Tasks;
using BjBygg.Application.Commands.MissionCommands.Images.Delete;
using BjBygg.Application.Commands.MissionCommands.Images.Upload;
using BjBygg.Application.Commands.MissionCommands.Reports.DeleteRange;
using BjBygg.Application.Commands.MissionCommands.Reports.Mail;
using BjBygg.Application.Commands.MissionCommands.Reports.Upload;
using BjBygg.Application.Queries.DbSyncQueries;
using BjBygg.Application.Queries.DbSyncQueries.MissionReportQuery;
using BjBygg.Application.Queries.ReportTypeQueries.List;
using BjBygg.Application.Shared;
using CleanArchitecture.Core.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BjBygg.WebApi.Controllers
{
    public class MissionReportsController : BaseController
    {
        private readonly IMediator _mediator;

        public MissionReportsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize]
        [HttpGet]
        [Route("api/[controller]/[action]")]
        public async Task<DbSyncResponse<MissionReportDto>> Sync(MissionReportSyncQuery query)
        {
            return await _mediator.Send(query);
        }

        [Authorize(Roles = "Leder")]
        [HttpPost]
        [Route("api/[controller]")]
        public async Task<MissionReportDto> Upload(int missionId)
        {
            if (Request.Form.Files.Count() == 0)
                throw new BadRequestException("No files received");

            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore
            };

            var command = new UploadMissionReportCommand()
            {
                File = Request.Form.Files[0],
                ReportType = JsonConvert.DeserializeObject<ReportTypeDto>(Request.Form["ReportType"], settings),
                MissionId = missionId
            };

            return await _mediator.Send(command);
        }

        [Authorize(Roles = "Leder")]
        [HttpDelete]
        [Route("api/[controller]/{id}")]
        public async Task<bool> Delete(DeleteMissionReportCommand command)
        {
            return await _mediator.Send(command);
        }

        [Authorize(Roles = "Leder")]
        [HttpPost]
        [Route("api/[controller]/DeleteRange")]
        public async Task<bool> DeleteRange([FromBody] DeleteRangeMissionReportCommand command)
        {
            return await _mediator.Send(command);
        }


        [Authorize(Roles = "Leder")]
        [HttpPost]
        [Route("api/[controller]/SendReports")]
        public async Task<bool> SendImages([FromBody] MailMissionReportsCommand command)
        {
            return await _mediator.Send(command);
        }

    }
}
