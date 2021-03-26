using AutoMapper;
using BjBygg.Application.Application.Commands.TimesheetCommands.Create;
using BjBygg.Application.Application.Commands.TimesheetCommands.Delete;
using BjBygg.Application.Application.Commands.TimesheetCommands.Update;
using BjBygg.Application.Identity.Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BjBygg.WebApi.Controllers
{
    public class UserTimesheetsController : BaseController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public UserTimesheetsController(
            IMapper mapper,
            UserManager<ApplicationUser> userManager)
        {
            _mapper = mapper;
            _userManager = userManager;
        }

        [Authorize(Roles = RolePermissions.UserTimesheetActions.Create)]
        [HttpPost]
        [Route("api/[controller]")]
        public async Task<ActionResult> Create([FromBody] CreateTimesheetCommand request)
        {
            await Mediator.Send(request);
            return NoContent();
        }

        [Authorize(Roles = RolePermissions.UserTimesheetActions.Update)]
        [HttpPut]
        [Route("api/[controller]/{Id}")]
        public async Task<ActionResult> Update([FromBody] UpdateTimesheetCommand request)
        {
            await Mediator.Send(request);
            return NoContent();
        }

        [Authorize(Roles = RolePermissions.UserTimesheetActions.Delete)]
        [HttpDelete]
        [Route("api/[controller]/{Id}")]
        public async Task<ActionResult> Delete(DeleteTimesheetCommand request)
        {
            await Mediator.Send(request);
            return NoContent();
        }

    }
}
