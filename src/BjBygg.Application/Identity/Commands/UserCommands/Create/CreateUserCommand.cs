using BjBygg.Application.Common.Interfaces;
using BjBygg.SharedKernel;

namespace BjBygg.Application.Identity.Commands.UserCommands.Create
{
    public class CreateUserCommand : IOptimisticCommand, IContactable
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string Password { get; set; }
        public string? EmployerId { get; set; }
    }
}
