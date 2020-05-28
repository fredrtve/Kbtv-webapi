using BjBygg.Application.Shared;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BjBygg.Application.Commands.MissionCommands.Documents.Upload
{
    public class UploadMissionDocumentCommand : IRequest<MissionDocumentDto>
    {
        [Required]
        public IFormFile File { get; set; }

        [Required]
        public int MissionId { get; set; }

        [Required]
        public DocumentTypeDto DocumentType { get; set; }

    }
}
