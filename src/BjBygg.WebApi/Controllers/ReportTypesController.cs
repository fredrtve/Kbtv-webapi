using System.Collections.Generic;
using System.Threading.Tasks;
using BjBygg.Application.Commands.ReportTypeCommands.Create;
using BjBygg.Application.Commands.ReportTypeCommands.Delete;
using BjBygg.Application.Commands.ReportTypeCommands.DeleteRange;
using BjBygg.Application.Commands.ReportTypeCommands.Update;
using BjBygg.Application.Queries.DbSyncQueries;
using BjBygg.Application.Queries.DbSyncQueries.ReportTypeQuery;
using BjBygg.Application.Queries.ReportTypeQueries.List;
using BjBygg.Application.Shared;
using CleanArchitecture.Core.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BjBygg.WebApi.Controllers
{
    public class ReportTypesController : BaseController
    {
        private readonly IMediator _mediator;

        public ReportTypesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize]
        [HttpGet]
        [Route("api/[controller]/[action]")]
        public async Task<DbSyncResponse<ReportTypeDto>> Sync(ReportTypeSyncQuery query)
        {
            return await _mediator.Send(query);
        }

        [Authorize]
        [HttpGet]
        [Route("api/[controller]")]
        public async Task<IEnumerable<ReportTypeDto>> Index()
        {
            return await _mediator.Send(new ReportTypeListQuery());
        }
 
        [Authorize(Roles = "Leder")]
        [HttpPost]
        [Route("api/[controller]")]
        public async Task<ReportTypeDto> Create([FromBody] CreateReportTypeCommand command)
        {
            if (!ModelState.IsValid)
                throw new BadRequestException(ModelState.Values.ToString());

            return await _mediator.Send(command);
        }

        [Authorize(Roles = "Leder")]
        [HttpPut]
        [Route("api/[controller]/{Id}")]
        public async Task<ReportTypeDto> Update([FromBody] UpdateReportTypeCommand command)
        {
            if (!ModelState.IsValid)
                throw new BadRequestException(ModelState.Values.ToString());

            return await _mediator.Send(command);
        }

        [Authorize(Roles = "Leder")]
        [HttpDelete]
        [Route("api/[controller]/{Id}")]
        public async Task<bool> Delete(DeleteReportTypeCommand command)
        {
            return await _mediator.Send(command);
        }

        [Authorize(Roles = "Leder")]
        [HttpPost]
        [Route("api/[controller]/DeleteRange")]
        public async Task<bool> DeleteRange([FromBody] DeleteRangeReportTypeCommand command)
        {
            return await _mediator.Send(command);
        }
    }
}
