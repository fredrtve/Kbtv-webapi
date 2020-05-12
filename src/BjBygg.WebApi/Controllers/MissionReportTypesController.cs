using System.Collections.Generic;
using System.Threading.Tasks;
using BjBygg.Application.Commands.MissionReportTypeCommands.Create;
using BjBygg.Application.Commands.MissionReportTypeCommands.Delete;
using BjBygg.Application.Commands.MissionReportTypeCommands.DeleteRange;
using BjBygg.Application.Commands.MissionReportTypeCommands.Update;
using BjBygg.Application.Queries.DbSyncQueries;
using BjBygg.Application.Queries.DbSyncQueries.MissionReportTypeQuery;
using BjBygg.Application.Queries.MissionReportTypeQueries.List;
using BjBygg.Application.Shared;
using CleanArchitecture.Core.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BjBygg.WebApi.Controllers
{
    public class MissionReportTypesController : BaseController
    {
        private readonly IMediator _mediator;

        public MissionReportTypesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize]
        [HttpGet]
        [Route("api/[controller]/[action]")]
        public async Task<DbSyncResponse<MissionReportTypeDto>> Sync(MissionReportTypeSyncQuery query)
        {
            return await _mediator.Send(query);
        }

        [Authorize]
        [HttpGet]
        [Route("api/[controller]")]
        public async Task<IEnumerable<MissionReportTypeDto>> Index()
        {
            return await _mediator.Send(new MissionReportTypeListQuery());
        }
 
        [Authorize(Roles = "Leder")]
        [HttpPost]
        [Route("api/[controller]")]
        public async Task<MissionReportTypeDto> Create([FromBody] CreateMissionReportTypeCommand command)
        {
            if (!ModelState.IsValid)
                throw new BadRequestException(ModelState.Values.ToString());

            return await _mediator.Send(command);
        }

        [Authorize(Roles = "Leder")]
        [HttpPut]
        [Route("api/[controller]/{Id}")]
        public async Task<MissionReportTypeDto> Update([FromBody] UpdateMissionReportTypeCommand command)
        {
            if (!ModelState.IsValid)
                throw new BadRequestException(ModelState.Values.ToString());

            return await _mediator.Send(command);
        }

        [Authorize(Roles = "Leder")]
        [HttpDelete]
        [Route("api/[controller]/{Id}")]
        public async Task<bool> Delete(DeleteMissionReportTypeCommand command)
        {
            return await _mediator.Send(command);
        }

        [Authorize(Roles = "Leder")]
        [HttpPost]
        [Route("api/[controller]/DeleteRange")]
        public async Task<bool> DeleteRange([FromBody] DeleteRangeMissionReportTypeCommand command)
        {
            return await _mediator.Send(command);
        }
    }
}
