using BjBygg.Application.Shared;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BjBygg.Application.Commands.MissionCommands.Reports.Upload
{
    public class UploadMissionReportCommand : IRequest<MissionReportDto>
    {
        [Required]
        public IFormFile File { get; set; }

        [Required]
        public int MissionId { get; set; }

        [Required]
        public ReportTypeDto ReportType { get; set; }

    }
}
