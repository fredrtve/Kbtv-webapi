using BjBygg.Application.Queries.UserQueries;
using BjBygg.Application.Common;
using CleanArchitecture.Core.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BjBygg.Application.Commands.IdentityCommands.UpdatePassword
{
    public class UpdatePasswordCommand : IRequest<Unit>
    {
        [Required]
        public string? UserName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0} må være mellom {2} og {1} tegn.", MinimumLength = 7)]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0} må være mellom {2} og {1} tegn.", MinimumLength = 7)]
        public string NewPassword { get; set; }

    }
}
