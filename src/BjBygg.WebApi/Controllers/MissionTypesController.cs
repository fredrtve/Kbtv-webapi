using BjBygg.Application.Application.Commands.MissionTypeCommands;
using BjBygg.Application.Application.Commands.MissionTypeCommands.Create;
using BjBygg.Application.Application.Commands.MissionTypeCommands.Update;
using BjBygg.Application.Application.Common.Dto;
using BjBygg.Application.Application.Queries.DbSyncQueries;
using BjBygg.Application.Application.Queries.DbSyncQueries.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BjBygg.WebApi.Controllers
{
    public class MissionTypesController : BaseController
    {
        public MissionTypesController() { }

        [Authorize]
        [HttpGet]
        [Route("api/[controller]/[action]")]
        public async Task<ActionResult<DbSyncResponse<MissionTypeDto>>> Sync(MissionTypeSyncQuery request)
        {
            return await Mediator.Send(request);
        }

        [Authorize(Roles = "Leder")]
        [HttpPost]
        [Route("api/[controller]")]
        public async Task<ActionResult<MissionTypeDto>> Create([FromBody] CreateMissionTypeCommand request)
        {
            return await Mediator.Send(request);
        }

        [Authorize(Roles = "Leder")]
        [HttpPut]
        [Route("api/[controller]/{Id}")]
        public async Task<ActionResult<MissionTypeDto>> Update([FromBody] UpdateMissionTypeCommand request)
        {
            return await Mediator.Send(request);
        }

        [Authorize(Roles = "Leder")]
        [HttpDelete]
        [Route("api/[controller]/{Id}")]
        public async Task<ActionResult> Delete(DeleteMissionTypeCommand request)
        {
            await Mediator.Send(request);
            return NoContent();
        }

        [Authorize(Roles = "Leder")]
        [HttpPost]
        [Route("api/[controller]/DeleteRange")]
        public async Task<ActionResult> DeleteRange([FromBody] DeleteRangeMissionTypeCommand request)
        {
            await Mediator.Send(request);
            return NoContent();
        }
    }
}
