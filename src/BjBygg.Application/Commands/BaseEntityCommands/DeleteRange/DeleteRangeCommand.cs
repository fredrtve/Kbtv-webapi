using CleanArchitecture.Core.Entities;
using MediatR;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BjBygg.Application.Commands.BaseEntityCommands.DeleteRange
{
    public abstract class DeleteRangeCommand : IRequest<bool>
    {
        [Required]
        public IEnumerable<int> Ids { get; set; }
    }
}
