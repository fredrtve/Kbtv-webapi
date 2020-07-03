using CleanArchitecture.Core.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BjBygg.Application.Commands.MissionTypeCommands.Delete
{
    public class DeleteMissionTypeCommand : IRequest<bool>
    {
        [Required]
        public int Id { get; set; }
    }
}
