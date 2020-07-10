using AutoMapper;
using BjBygg.Application.Identity.Common.Models;

namespace BjBygg.Application.Identity.Commands.UserCommands.Update
{
    class UpdateUserCommandProfile : Profile
    {
        public UpdateUserCommandProfile()
        {
            CreateMap<UpdateUserCommand, ApplicationUser>()
                .ForSourceMember(x => x.Role, opt => opt.DoNotValidate());
        }
    }
}
