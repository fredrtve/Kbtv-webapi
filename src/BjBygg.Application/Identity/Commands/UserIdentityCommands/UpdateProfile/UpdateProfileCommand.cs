using BjBygg.Application.Common.Interfaces;
using BjBygg.Application.Identity.Common;
using CleanArchitecture.SharedKernel;
using MediatR;

namespace BjBygg.Application.Identity.Commands.UserIdentityCommands.UpdateProfile
{
    public class UpdateProfileCommand : IOptimisticCommand, IContactable
    {
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

    }
}
