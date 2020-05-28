using System.Collections.Generic;
using System.Threading.Tasks;
using BjBygg.Application.Commands.DocumentTypeCommands.Create;
using BjBygg.Application.Commands.DocumentTypeCommands.Delete;
using BjBygg.Application.Commands.DocumentTypeCommands.DeleteRange;
using BjBygg.Application.Commands.DocumentTypeCommands.Update;
using BjBygg.Application.Queries.DbSyncQueries;
using BjBygg.Application.Queries.DbSyncQueries.DocumentTypeQuery;
using BjBygg.Application.Queries.DocumentTypeQueries.List;
using BjBygg.Application.Shared;
using CleanArchitecture.Core.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BjBygg.WebApi.Controllers
{
    public class DocumentTypesController : BaseController
    {
        private readonly IMediator _mediator;

        public DocumentTypesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize]
        [HttpGet]
        [Route("api/[controller]/[action]")]
        public async Task<DbSyncResponse<DocumentTypeDto>> Sync(DocumentTypeSyncQuery query)
        {
            return await _mediator.Send(query);
        }

        [Authorize]
        [HttpGet]
        [Route("api/[controller]")]
        public async Task<IEnumerable<DocumentTypeDto>> Index()
        {
            return await _mediator.Send(new DocumentTypeListQuery());
        }
 
        [Authorize(Roles = "Leder")]
        [HttpPost]
        [Route("api/[controller]")]
        public async Task<DocumentTypeDto> Create([FromBody] CreateDocumentTypeCommand command)
        {
            if (!ModelState.IsValid)
                throw new BadRequestException(ModelState.Values.ToString());

            return await _mediator.Send(command);
        }

        [Authorize(Roles = "Leder")]
        [HttpPut]
        [Route("api/[controller]/{Id}")]
        public async Task<DocumentTypeDto> Update([FromBody] UpdateDocumentTypeCommand command)
        {
            if (!ModelState.IsValid)
                throw new BadRequestException(ModelState.Values.ToString());

            return await _mediator.Send(command);
        }

        [Authorize(Roles = "Leder")]
        [HttpDelete]
        [Route("api/[controller]/{Id}")]
        public async Task<bool> Delete(DeleteDocumentTypeCommand command)
        {
            return await _mediator.Send(command);
        }

        [Authorize(Roles = "Leder")]
        [HttpPost]
        [Route("api/[controller]/DeleteRange")]
        public async Task<bool> DeleteRange([FromBody] DeleteRangeDocumentTypeCommand command)
        {
            return await _mediator.Send(command);
        }
    }
}
