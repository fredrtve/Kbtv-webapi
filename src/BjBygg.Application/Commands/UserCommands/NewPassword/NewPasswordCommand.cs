using CleanArchitecture.Core.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BjBygg.Application.Commands.UserCommands.NewPassword
{
    public class NewPasswordCommand : IRequest<bool>
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string NewPassword { get; set; }
    }
}
