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

        [Authorize(Roles = RolePermissions.MissionNoteActions.Create)]
        [HttpPost]
        [Route("api/[controller]")]
        public async Task<ActionResult> Create([FromBody] CreateMissionNoteCommand request)
        {
            await Mediator.Send(request);
            return NoContent();
        }

        [Authorize(Roles = RolePermissions.MissionNoteActions.Update)]
        [HttpPut]
        [Route("api/[controller]/{Id}")]
        public async Task<ActionResult> Update([FromBody] UpdateMissionNoteCommand request)
        {
            await Mediator.Send(request);
            return NoContent();
        }

        [Authorize(Roles = RolePermissions.MissionNoteActions.Delete)]
        [HttpDelete]
        [Route("api/[controller]/{Id}")]
        public async Task<ActionResult> Delete(DeleteMissionNoteCommand request)
        {
            await Mediator.Send(request);
            return NoContent();
        }
    }
}
