using MediatR;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BjBygg.Application.Commands.MissionCommands.Documents.Mail
{
    public class MailMissionDocumentsCommand : IRequest<bool>
    {
        [Required]
        [EmailAddress]
        public string ToEmail { get; set; }
        [Required]
        public IEnumerable<int> MissionDocumentIds { get; set; }
    }
}
