using BjBygg.Application.Commands.MissionTypeCommands;
using BjBygg.Application.Commands.MissionTypeCommands.Create;
using BjBygg.Application.Commands.MissionTypeCommands.Update;
using BjBygg.Application.Common;
using BjBygg.Application.Queries.DbSyncQueries;
using BjBygg.Application.Queries.DbSyncQueries.Common;
using MediatR;
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
        public async Task<DbSyncResponse<MissionTypeDto>> Sync(MissionTypeSyncQuery request)
        {
            return await Mediator.Send(request);
        }

        [Authorize(Roles = "Leder")]
        [HttpPost]
        [Route("api/[controller]")]
        public async Task<MissionTypeDto> Create([FromBody] CreateMissionTypeCommand request)
        {
            return await Mediator.Send(request);
        }

        [Authorize(Roles = "Leder")]
        [HttpPut]
        [Route("api/[controller]/{Id}")]
        public async Task<MissionTypeDto> Update([FromBody] UpdateMissionTypeCommand request)
        {
            return await Mediator.Send(request);
        }

        [Authorize(Roles = "Leder")]
        [HttpDelete]
        [Route("api/[controller]/{Id}")]
        public async Task<Unit> Delete(DeleteMissionTypeCommand request)
        {
            return await Mediator.Send(request);
        }

        [Authorize(Roles = "Leder")]
        [HttpPost]
        [Route("api/[controller]/DeleteRange")]
        public async Task<Unit> DeleteRange([FromBody] DeleteRangeMissionTypeCommand request)
        {
            return await Mediator.Send(request);
        }
    }
}
