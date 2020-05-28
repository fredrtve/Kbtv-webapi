using CleanArchitecture.Core.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BjBygg.Application.Commands.DocumentTypeCommands.Delete
{
    public class DeleteDocumentTypeCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }
}
