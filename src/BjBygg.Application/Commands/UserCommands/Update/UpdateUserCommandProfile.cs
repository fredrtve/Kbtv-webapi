using AutoMapper;
using CleanArchitecture.Infrastructure.Identity;

namespace BjBygg.Application.Commands.UserCommands.Update
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
