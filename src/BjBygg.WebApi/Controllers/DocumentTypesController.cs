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
        public async Task<DbSyncResponse<DocumentTypeDto>> Sync(DocumentTypeSyncQuery request)
        {
            return await Mediator.Send(request);
        }

        [Authorize(Roles = "Leder")]
        [HttpPost]
        [Route("api/[controller]")]
        public async Task<DocumentTypeDto> Create([FromBody] CreateDocumentTypeCommand request)
        {
            return await Mediator.Send(request);
        }

        [Authorize(Roles = "Leder")]
        [HttpPut]
        [Route("api/[controller]/{Id}")]
        public async Task<DocumentTypeDto> Update([FromBody] UpdateDocumentTypeCommand request)
        {
            return await Mediator.Send(request);
        }

        [Authorize(Roles = "Leder")]
        [HttpDelete]
        [Route("api/[controller]/{Id}")]
        public async Task<Unit> Delete(DeleteDocumentTypeCommand request)
        {
            return await Mediator.Send(request);
        }

        [Authorize(Roles = "Leder")]
        [HttpPost]
        [Route("api/[controller]/DeleteRange")]
        public async Task<Unit> DeleteRange([FromBody] DeleteRangeDocumentTypeCommand request)
        {
            return await Mediator.Send(request);
        }
    }
}
