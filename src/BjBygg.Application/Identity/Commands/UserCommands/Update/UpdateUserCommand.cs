using BjBygg.Application.Common.Interfaces;
using BjBygg.Application.Identity.Common;
using CleanArchitecture.SharedKernel;
using MediatR;

namespace BjBygg.Application.Identity.Commands.UserCommands.Update
{
    public class UpdateUserCommand : IOptimisticCommand, IContactable
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string? EmployerId { get; set; }

    }
}
