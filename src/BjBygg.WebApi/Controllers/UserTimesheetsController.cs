using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using BjBygg.Application.Commands.TimesheetCommands.Create;
using BjBygg.Application.Commands.TimesheetCommands.Delete;
using BjBygg.Application.Commands.TimesheetCommands.Update;
using BjBygg.Application.Commands.TimesheetCommands.UpdateStatus;
using BjBygg.Application.Commands.TimesheetCommands.UpdateStatusRange;
using BjBygg.Application.Queries.DbSyncQueries;
using BjBygg.Application.Queries.DbSyncQueries.TimesheetQuery;
using BjBygg.Application.Shared;
using CleanArchitecture.Core.Exceptions;
using CleanArchitecture.Infrastructure.Identity;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BjBygg.WebApi.Controllers
{
    public class UserTimesheetsController : BaseController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public UserTimesheetsController(
            IMediator mediator, 
            ILogger<UserTimesheetsController> logger, 
            UserManager<ApplicationUser> userManager) : base(mediator, logger)
        {
            _userManager = userManager;
        }

        [Authorize(Roles = "Leder, Mellomleder, Ansatt")]
        [HttpGet]
        [Route("api/[controller]/[action]")]
        public async Task<DbSyncResponse<TimesheetDto>> Sync(UserTimesheetSyncQuery request)
        {
            var user = await _userManager.FindByNameAsync(User.FindFirstValue(ClaimTypes.Name));
            request.User = _mapper.Map<UserDto>(user);
            request.User.Role = User.FindFirstValue(ClaimTypes.Role);
            return await ValidateAndExecute(request);
        }

        [Authorize(Roles = "Leder, Mellomleder, Ansatt")]
        [HttpPost]
        [Route("api/[controller]")]
        public async Task<TimesheetDto> Create([FromBody] CreateTimesheetCommand request)
        {
            request.UserName = User.FindFirstValue(ClaimTypes.Name);
            return await ValidateAndExecute(request);
        }

        [Authorize(Roles = "Leder, Mellomleder, Ansatt")]
        [HttpPut]
        [Route("api/[controller]/{Id}")]
        public async Task<TimesheetDto> Update([FromBody] UpdateTimesheetCommand request)
        {
            request.UserName = User.FindFirstValue(ClaimTypes.Name);
            return await ValidateAndExecute(request);
        }

        [Authorize(Roles = "Leder, Mellomleder, Ansatt")]
        [HttpDelete]
        [Route("api/[controller]/{Id}")]
        public async Task<bool> Delete(int Id)
        {
            var request = new DeleteTimesheetCommand()
            {
                Id = Id,
                UserName = User.FindFirstValue(ClaimTypes.Name),
                Role = User.FindFirstValue(ClaimTypes.Role),
            };

            return await ValidateAndExecute(request);
        }

    }
}
