using CleanArchitecture.Core.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BjBygg.Application.Commands.TimesheetCommands.Delete
{
    public class DeleteTimesheetCommand : IRequest<bool>
    {
        public string UserName { get; set; }
        public string Role { get; set; }
        public int Id { get; set; }
    }
}
