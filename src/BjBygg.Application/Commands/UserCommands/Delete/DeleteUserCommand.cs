using CleanArchitecture.Core.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BjBygg.Application.Commands.UserCommands.Delete
{
    public class DeleteUserCommand : IRequest<bool>
    {
        public string UserName { get; set; }
    }
}
