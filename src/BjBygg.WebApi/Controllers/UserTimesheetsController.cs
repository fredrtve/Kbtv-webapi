using AutoMapper;
using BjBygg.Application.Application.Commands.TimesheetCommands.Create;
using BjBygg.Application.Application.Commands.TimesheetCommands.Delete;
using BjBygg.Application.Application.Commands.TimesheetCommands.Update;
using BjBygg.Application.Application.Common.Dto;
using BjBygg.Application.Application.Queries.DbSyncQueries;
using BjBygg.Application.Application.Queries.DbSyncQueries.Common;
using BjBygg.Application.Identity.Common;
using BjBygg.Application.Identity.Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
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

        [Authorize(Roles = "Leder, Mellomleder, Ansatt")]
        [HttpGet]
        [Route("api/[controller]/[action]")]
        public async Task<ActionResult<DbSyncResponse<TimesheetDto>>> Sync(UserTimesheetSyncQuery request)
        {
            var user = await _userManager.FindByNameAsync(User.FindFirstValue(ClaimTypes.Name));
            request.User = _mapper.Map<UserDto>(user);
            request.User.Role = User.FindFirstValue(ClaimTypes.Role);
            return await Mediator.Send(request);
        }

        [Authorize(Roles = "Leder, Mellomleder, Ansatt")]
        [HttpPost]
        [Route("api/[controller]")]
        public async Task<ActionResult<TimesheetDto>> Create([FromBody] CreateTimesheetCommand request)
        {
            return await Mediator.Send(request);
        }

        [Authorize(Roles = "Leder, Mellomleder, Ansatt")]
        [HttpPut]
        [Route("api/[controller]/{Id}")]
        public async Task<ActionResult<TimesheetDto>> Update([FromBody] UpdateTimesheetCommand request)
        {
            return await Mediator.Send(request);
        }

        [Authorize(Roles = "Leder, Mellomleder, Ansatt")]
        [HttpDelete]
        [Route("api/[controller]/{Id}")]
        public async Task<ActionResult> Delete(DeleteTimesheetCommand request)
        {
            await Mediator.Send(request);
            return NoContent();
        }

    }
}
