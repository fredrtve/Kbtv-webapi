using BjBygg.Application.Identity.Common;
using MediatR;

namespace BjBygg.Application.Identity.Commands.UserCommands.Update
{
    public class UpdateUserCommand : IRequest
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
