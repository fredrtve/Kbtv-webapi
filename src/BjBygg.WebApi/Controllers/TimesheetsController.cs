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
using BjBygg.Application.Queries.TimesheetQueries;
using BjBygg.Application.Shared;
using CleanArchitecture.Core.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BjBygg.WebApi.Controllers
{
    public class TimesheetsController : BaseController
    {
        private readonly IMediator _mediator;

        public TimesheetsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize(Roles = "Leder")]
        [HttpGet]
        [Route("api/[controller]")]
        public async Task<IEnumerable<TimesheetDto>> Get(TimesheetQuery query)
        {
            //if (!ModelState.IsValid)
            //    throw new BadRequestException(ModelState.Values.ToString());

            return await _mediator.Send(query);
        }

        [Authorize(Roles = "Leder")]
        [HttpPut]
        [Route("api/[controller]/{Id}/Status")]
        public async Task<TimesheetDto> UpdateStatus([FromBody] UpdateTimesheetStatusCommand command)
        {
            if (!ModelState.IsValid)
                throw new BadRequestException(ModelState.Values.ToString());

            return await _mediator.Send(command);
        }

        [Authorize(Roles = "Leder")]
        [HttpPut]
        [Route("api/[controller]/Status")]
        public async Task<IEnumerable<TimesheetDto>> UpdateStatuses([FromBody] UpdateTimesheetStatusRangeCommand command)
        {
            if (!ModelState.IsValid)
                throw new BadRequestException(ModelState.Values.ToString());

            return await _mediator.Send(command);
        }
    }
}
