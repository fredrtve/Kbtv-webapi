using AutoMapper;
using BjBygg.Application.Commands.BaseEntityCommands.Update;
using BjBygg.Application.Common;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Infrastructure.Data;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BjBygg.Application.Commands.DocumentTypeCommands.Update
{
    public class UpdateDocumentTypeCommand : UpdateCommand<DocumentTypeDto>
    {
        public string Name { get; set; }
    }
    public class UpdateDocumentTypeCommandHandler : UpdateHandler<DocumentType, UpdateDocumentTypeCommand, DocumentTypeDto>
    {
        public UpdateDocumentTypeCommandHandler(AppDbContext dbContext, IMapper mapper) :
            base(dbContext, mapper) {}
    }
}
