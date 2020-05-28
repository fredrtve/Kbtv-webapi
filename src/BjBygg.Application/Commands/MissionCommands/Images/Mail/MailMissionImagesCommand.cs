using BjBygg.Application.Shared;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BjBygg.Application.Commands.MissionCommands.Images.Mail
{
    public class MailMissionImagesCommand : IRequest<bool>
    {
        [Required]
        [EmailAddress]
        public string ToEmail { get; set; }
        [Required]
        public IEnumerable<int> MissionImageIds { get; set; }
    }
}
