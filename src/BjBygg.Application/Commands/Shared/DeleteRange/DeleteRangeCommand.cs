using CleanArchitecture.Core.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BjBygg.Application.Commands.Shared.DeleteRange
{
    public abstract class DeleteRangeCommand : IRequest<bool>
    {
        public IEnumerable<int> Ids { get; set; }
    }
}
