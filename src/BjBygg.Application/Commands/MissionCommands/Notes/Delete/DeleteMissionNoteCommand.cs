using CleanArchitecture.Core.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BjBygg.Application.Commands.MissionCommands.Notes.Delete
{
    public class DeleteMissionNoteCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }
}
