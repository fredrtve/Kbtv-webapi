using BjBygg.Application.Commands.DocumentTypeCommands;
using BjBygg.Application.Commands.DocumentTypeCommands.Create;
using BjBygg.Application.Commands.DocumentTypeCommands.Update;
using BjBygg.Application.Common;
using BjBygg.Application.Queries.DbSyncQueries;
using BjBygg.Application.Queries.DbSyncQueries.Common;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BjBygg.WebApi.Controllers
{
    public class DocumentTypesController : BaseController
    {
        public DocumentTypesController() { }

        [Authorize]
        [HttpGet]
        [Route("api/[controller]/[action]")]
        public async Task<ActionResult<DbSyncResponse<DocumentTypeDto>>> Sync(DocumentTypeSyncQuery request)
        {
            return await Mediator.Send(request);
        }

        [Authorize(Roles = "Leder")]
        [HttpPost]
        [Route("api/[controller]")]
        public async Task<ActionResult<DocumentTypeDto>> Create([FromBody] CreateDocumentTypeCommand request)
        {
            return await Mediator.Send(request);
        }

        [Authorize(Roles = "Leder")]
        [HttpPut]
        [Route("api/[controller]/{Id}")]
        public async Task<ActionResult<DocumentTypeDto>> Update([FromBody] UpdateDocumentTypeCommand request)
        {
            return await Mediator.Send(request);
        }

        [Authorize(Roles = "Leder")]
        [HttpDelete]
        [Route("api/[controller]/{Id}")]
        public async Task<ActionResult> Delete(DeleteDocumentTypeCommand request)
        {
            await Mediator.Send(request);
            return NoContent();
        }

        [Authorize(Roles = "Leder")]
        [HttpPost]
        [Route("api/[controller]/DeleteRange")]
        public async Task<ActionResult> DeleteRange([FromBody] DeleteRangeDocumentTypeCommand request)
        {
            await Mediator.Send(request);
            return NoContent();
        }
    }
}
