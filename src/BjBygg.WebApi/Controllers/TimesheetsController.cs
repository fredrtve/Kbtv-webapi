using BjBygg.Application.Application.Commands.TimesheetCommands.UpdateStatus;
using BjBygg.Application.Application.Commands.TimesheetCommands.UpdateStatusRange;
using BjBygg.Application.Application.Common.Dto;
using BjBygg.Application.Application.Queries.TimesheetQueries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BjBygg.WebApi.Controllers
{
    public class TimesheetsController : BaseController
    {
        public TimesheetsController() { }

        [Authorize(Roles = RolePermissions.TimesheetActions.Read)]
        [HttpGet]
        [Route("api/[controller]")]
        public async Task<ActionResult<IEnumerable<TimesheetDto>>> Get(TimesheetQuery request)
        {
            return await Mediator.Send(request);
        }

        [Authorize(Roles = RolePermissions.TimesheetActions.Update)]
        [HttpPut]
        [Route("api/[controller]/{Id}/Status")]
        public async Task<ActionResult> UpdateStatus([FromBody] UpdateTimesheetStatusCommand request)
        {
            await Mediator.Send(request);
            return NoContent();
        }

        [Authorize(Roles = RolePermissions.TimesheetActions.Update)]
        [HttpPut]
        [Route("api/[controller]/Status")]
        public async Task<ActionResult> UpdateStatuses([FromBody] UpdateTimesheetStatusRangeCommand request)
        {
            await Mediator.Send(request);
            return NoContent();
        }
    }
}
