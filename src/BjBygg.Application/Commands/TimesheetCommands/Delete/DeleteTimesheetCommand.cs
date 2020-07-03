using CleanArchitecture.Core.Entities;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace BjBygg.Application.Commands.TimesheetCommands.Delete
{
    public class DeleteTimesheetCommand : IRequest<bool>
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Role { get; set; }
        [Required]
        public int Id { get; set; }
    }
}
