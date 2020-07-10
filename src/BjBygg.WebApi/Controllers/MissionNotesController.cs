using BjBygg.Application.Application.Commands.MissionCommands.Notes;
using BjBygg.Application.Application.Commands.MissionCommands.Notes.Create;
using BjBygg.Application.Application.Commands.MissionCommands.Notes.Update;
using BjBygg.Application.Application.Common.Dto;
using BjBygg.Application.Application.Queries.DbSyncQueries;
using BjBygg.Application.Application.Queries.DbSyncQueries.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BjBygg.WebApi.Controllers
{
    public class MissionNotesController : BaseController
    {
        public MissionNotesController() { }

        [Authorize]
        [HttpGet]
        [Route("api/[controller]/[action]")]
        public async Task<ActionResult<DbSyncResponse<MissionNoteDto>>> Sync(MissionNoteSyncQuery request)
        {
            return await Mediator.Send(request);
        }

        [Authorize(Roles = "Leder, Mellomleder, Ansatt")]
        [HttpPost]
        [Route("api/[controller]")]
        public async Task<ActionResult<MissionNoteDto>> Create([FromBody] CreateMissionNoteCommand request)
        {
            return await Mediator.Send(request);
        }

        [Authorize(Roles = "Leder")]
        [HttpPut]
        [Route("api/[controller]/{Id}")]
        public async Task<ActionResult<MissionNoteDto>> Update([FromBody] UpdateMissionNoteCommand request)
        {
            return await Mediator.Send(request);
        }

        [Authorize(Roles = "Leder")]
        [HttpDelete]
        [Route("api/[controller]/{Id}")]
        public async Task<ActionResult> Delete(DeleteMissionNoteCommand request)
        {
            await Mediator.Send(request);
            return NoContent();
        }
    }
}
