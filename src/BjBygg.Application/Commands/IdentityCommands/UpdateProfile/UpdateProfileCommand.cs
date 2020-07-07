using BjBygg.Application.Common;
using MediatR;

namespace BjBygg.Application.Commands.IdentityCommands.UpdateProfile
{
    public class UpdateProfileCommand : IRequest<UserDto>
    {
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

    }
}
