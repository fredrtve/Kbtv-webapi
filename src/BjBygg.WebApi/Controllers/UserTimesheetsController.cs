using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using BjBygg.Application.Commands.TimesheetCommands.Create;
using BjBygg.Application.Commands.TimesheetCommands.Delete;
using BjBygg.Application.Commands.TimesheetCommands.UpdateStatus;
using BjBygg.Application.Commands.TimesheetCommands.UpdateStatusRange;
using BjBygg.Application.Queries.DbSyncQueries;
using BjBygg.Application.Queries.DbSyncQueries.TimesheetQuery;
using BjBygg.Application.Shared;
using CleanArchitecture.Core.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BjBygg.WebApi.Controllers
{
    public class UserTimesheetsController : BaseController
    {
        private readonly IMediator _mediator;

        public UserTimesheetsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize(Roles = "Leder, Mellomleder, Ansatt")]
        [HttpPost]
        [Route("api/[controller]/[action]")]
        public async Task<DbSyncResponse<TimesheetDto>> Sync([FromBody] string FromDate)
        {
            return await _mediator.Send(new UserTimesheetSyncQuery() { 
                FromDate = FromDate,
                UserName = User.FindFirstValue("UserName"),
            });
        }

        [Authorize(Roles = "Leder, Mellomleder, Ansatt")]
        [HttpPost]
        [Route("api/[controller]")]
        public async Task<TimesheetDto> Create([FromBody] CreateTimesheetCommand command)
        {
            if (!ModelState.IsValid)
                throw new BadRequestException(ModelState.Values.ToString());

            command.UserName = User.FindFirstValue("UserName");

            return await _mediator.Send(command);
        }

        [Authorize(Roles = "Leder, Mellomleder, Ansatt")]
        [HttpPut]
        [Route("api/[controller]/{Id}/Status")]
        public async Task<TimesheetDto> UpdateStatus([FromBody] UpdateTimesheetStatusCommand command)
        {
            if (!ModelState.IsValid)
                throw new BadRequestException(ModelState.Values.ToString());

            command.Role = User.FindFirstValue(ClaimTypes.Role);
            command.UserName = User.FindFirstValue("UserName");

            return await _mediator.Send(command);
        }

        [Authorize(Roles = "Leder, Mellomleder, Ansatt")]
        [HttpPut]
        [Route("api/[controller]/Status")]
        public async Task<IEnumerable<TimesheetDto>> UpdateStatuses([FromBody] UpdateTimesheetStatusRangeCommand command)
        {
            if (!ModelState.IsValid)
                throw new BadRequestException(ModelState.Values.ToString());

            command.Role = User.FindFirstValue(ClaimTypes.Role);
            command.UserName = User.FindFirstValue("UserName");

            return await _mediator.Send(command);
        }

        [Authorize(Roles = "Leder, Mellomleder, Ansatt")]
        [HttpDelete]
        [Route("api/[controller]/{Id}")]
        public async Task<bool> Delete(int Id)
        {
            return await _mediator.Send(new DeleteTimesheetCommand() {
                Id = Id,
                UserName = User.FindFirstValue("UserName"),
                Role = User.FindFirstValue(ClaimTypes.Role),
            });
        }

    }
}
