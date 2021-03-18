using BjBygg.Application.Common.Interfaces;
using BjBygg.SharedKernel;

namespace BjBygg.Application.Identity.Commands.UserIdentityCommands.UpdateProfile
{
    public class UpdateProfileCommand : IOptimisticCommand, IContactable
    {
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

    }
}
