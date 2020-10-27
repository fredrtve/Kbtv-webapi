using AutoMapper;
using BjBygg.Application.Application.Common.Dto;
using BjBygg.Application.Application.Common.Interfaces;
using BjBygg.Application.Common.BaseEntityCommands.Update;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.SharedKernel;

namespace BjBygg.Application.Application.Commands.DocumentTypeCommands.Update
{
    public class UpdateDocumentTypeCommand : UpdateCommand, IName
    {
        public string Name { get; set; }
    }
    public class UpdateDocumentTypeCommandHandler : UpdateCommandHandler<DocumentType, UpdateDocumentTypeCommand>
    {
        public UpdateDocumentTypeCommandHandler(IAppDbContext dbContext) :
            base(dbContext)
        { }
    }
}
