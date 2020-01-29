using CleanArchitecture.Core.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BjBygg.Application.Commands.MissionReportTypeCommands.Delete
{
    public class DeleteMissionReportTypeCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }
}
