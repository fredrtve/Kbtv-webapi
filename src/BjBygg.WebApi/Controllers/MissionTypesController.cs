using System.Collections.Generic;
using System.Threading.Tasks;
using BjBygg.Application.Commands.MissionTypeCommands.Create;
using BjBygg.Application.Commands.MissionTypeCommands.Delete;
using BjBygg.Application.Commands.MissionTypeCommands.DeleteRange;
using BjBygg.Application.Commands.MissionTypeCommands.Update;
using BjBygg.Application.Queries.DbSyncQueries;
using BjBygg.Application.Queries.DbSyncQueries.MissionTypeQuery;
using BjBygg.Application.Queries.MissionTypeQueries.List;
using BjBygg.Application.Common;
using CleanArchitecture.Core.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BjBygg.WebApi.Controllers
{
    public class MissionTypesController : BaseController
    {
        public MissionTypesController(IMediator mediator, ILogger<MissionTypesController> logger) :
            base(mediator, logger) {}

        [Authorize]
        [HttpGet]
        [Route("api/[controller]/[action]")]
        public async Task<DbSyncResponse<MissionTypeDto>> Sync(MissionTypeSyncQuery request)
        {
            return await ValidateAndExecute(request);
        }

        [Authorize]
        [HttpGet]
        [Route("api/[controller]")]
        public async Task<IEnumerable<MissionTypeDto>> Index()
        {
            return await _mediator.Send(new MissionTypeListQuery());
        }

        [Authorize(Roles = "Leder")]
        [HttpPost]
        [Route("api/[controller]")]
        public async Task<MissionTypeDto> Create([FromBody] CreateMissionTypeCommand request)
        {
            return await ValidateAndExecute(request);
        }

        [Authorize(Roles = "Leder")]
        [HttpPut]
        [Route("api/[controller]/{Id}")]
        public async Task<MissionTypeDto> Update([FromBody] UpdateMissionTypeCommand request)
        {
            return await ValidateAndExecute(request);
        }

        [Authorize(Roles = "Leder")]
        [HttpDelete]
        [Route("api/[controller]/{Id}")]
        public async Task<bool> Delete(DeleteMissionTypeCommand request)
        {
            return await ValidateAndExecute(request);
        }

        [Authorize(Roles = "Leder")]
        [HttpPost]
        [Route("api/[controller]/DeleteRange")]
        public async Task<bool> DeleteRange([FromBody] DeleteRangeMissionTypeCommand request)
        {
            return await ValidateAndExecute(request);
        }
    }
}
