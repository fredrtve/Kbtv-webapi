using System.Collections.Generic;
using System.Threading.Tasks;
using BjBygg.Application.Commands.EmployerCommands.Create;
using BjBygg.Application.Commands.EmployerCommands.Delete;
using BjBygg.Application.Commands.EmployerCommands.DeleteRange;
using BjBygg.Application.Commands.EmployerCommands.Update;
using BjBygg.Application.Queries.DbSyncQueries;
using BjBygg.Application.Queries.DbSyncQueries.EmployerQuery;
using BjBygg.Application.Queries.EmployerQueries.List;
using BjBygg.Application.Shared;
using CleanArchitecture.Core.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BjBygg.WebApi.Controllers
{
    public class EmployersController : BaseController
    {
        public EmployersController(IMediator mediator, ILogger<EmployersController> logger) :
            base(mediator, logger) {}

        [Authorize]
        [HttpGet]
        [Route("api/[controller]/[action]")]
        public async Task<DbSyncResponse<EmployerDto>> Sync(EmployerSyncQuery request)
        {
            return await ValidateAndExecute(request);
        }

        [Authorize]
        [HttpGet]
        [Route("api/[controller]")]
        public async Task<IEnumerable<EmployerDto>> Index()
        {
            return await _mediator.Send(new EmployerListQuery());
        }

        [Authorize(Roles = "Leder, Mellomleder")]
        [HttpPost]
        [Route("api/[controller]")]
        public async Task<EmployerDto> Create([FromBody] CreateEmployerCommand request)
        {
            return await ValidateAndExecute(request);
        }

        [Authorize(Roles = "Leder")]
        [HttpPut]
        [Route("api/[controller]/{Id}")]
        public async Task<EmployerDto> Update([FromBody] UpdateEmployerCommand request)
        {
            return await ValidateAndExecute(request);
        }

        [Authorize(Roles = "Leder")]
        [HttpDelete]
        [Route("api/[controller]/{Id}")]
        public async Task<bool> Delete(DeleteEmployerCommand request)
        {
            return await ValidateAndExecute(request);
        }

        [Authorize(Roles = "Leder")]
        [HttpPost]
        [Route("api/[controller]/DeleteRange")]
        public async Task<bool> DeleteRange([FromBody] DeleteRangeEmployerCommand request)
        {
            return await ValidateAndExecute(request);
        }
    }
}
