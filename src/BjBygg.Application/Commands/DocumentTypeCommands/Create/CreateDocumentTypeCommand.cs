using BjBygg.Application.Commands.BaseEntityCommands.Create;
using BjBygg.Application.Common;

namespace BjBygg.Application.Commands.DocumentTypeCommands.Create
{
    public class CreateDocumentTypeCommand : CreateCommand<DocumentTypeDto>
    {
        public string Name { get; set; }
    }
}
