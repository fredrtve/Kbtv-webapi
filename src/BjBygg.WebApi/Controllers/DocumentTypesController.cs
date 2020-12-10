using BjBygg.Application.Application.Commands.DocumentTypeCommands;
using BjBygg.Application.Application.Commands.DocumentTypeCommands.Create;
using BjBygg.Application.Application.Commands.DocumentTypeCommands.Update;
using BjBygg.Application.Application.Common.Dto;
using BjBygg.Application.Application.Queries.DbSyncQueries;
using BjBygg.Application.Application.Queries.DbSyncQueries.Common;
using BjBygg.Application.Common;
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
        public async Task<ActionResult<DbSyncArrayResponse<DocumentTypeDto>>> Sync(DocumentTypeSyncQuery request)
        {
            return await Mediator.Send(request);
        }

        [Authorize(Roles = RolePermissions.DocumentTypeActions.Create)]
        [HttpPost]
        [Route("api/[controller]")]
        public async Task<ActionResult> Create([FromBody] CreateDocumentTypeCommand request)
        {
            await Mediator.Send(request);
            return NoContent();
        }

        [Authorize(Roles = RolePermissions.DocumentTypeActions.Update)]
        [HttpPut]
        [Route("api/[controller]/{Id}")]
        public async Task<ActionResult> Update([FromBody] UpdateDocumentTypeCommand request)
        {
            await Mediator.Send(request);
            return NoContent();
        }

        [Authorize(Roles = RolePermissions.DocumentTypeActions.Delete)]
        [HttpDelete]
        [Route("api/[controller]/{Id}")]
        public async Task<ActionResult> Delete(DeleteDocumentTypeCommand request)
        {
            await Mediator.Send(request);
            return NoContent();
        }

        [Authorize(Roles = RolePermissions.DocumentTypeActions.Delete)]
        [HttpPost]
        [Route("api/[controller]/DeleteRange")]
        public async Task<ActionResult> DeleteRange([FromBody] DeleteRangeDocumentTypeCommand request)
        {
            await Mediator.Send(request);
            return NoContent();
        }
    }
}
