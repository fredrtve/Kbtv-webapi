using BjBygg.Application.Common.Interfaces;
using BjBygg.Application.Identity.Common;
using MediatR;

namespace BjBygg.Application.Identity.Commands.UserIdentityCommands.UpdateProfile
{
    public class UpdateProfileCommand : IOptimisticCommand
    {
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

    }
}
