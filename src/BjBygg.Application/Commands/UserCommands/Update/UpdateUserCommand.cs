using BjBygg.Application.Common;
using MediatR;

namespace BjBygg.Application.Commands.UserCommands.Update
{
    public class UpdateUserCommand : IRequest<UserDto>
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public int? EmployerId { get; set; }

    }
}
