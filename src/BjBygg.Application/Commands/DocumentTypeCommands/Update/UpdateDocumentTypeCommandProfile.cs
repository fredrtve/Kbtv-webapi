using AutoMapper;
using CleanArchitecture.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BjBygg.Application.Commands.DocumentTypeCommands.Update
{
    class UpdateDocumentTypeCommandProfile : Profile
    {
        public UpdateDocumentTypeCommandProfile()
        {
            CreateMap<UpdateDocumentTypeCommand, DocumentType>();
        }
    }
}
