using AutoMapper;
using CleanArchitecture.Infrastructure.Identity;

namespace BjBygg.Application.Commands.UserCommands.Create
{
    class CreateUserCommandProfile : Profile
    {
        public CreateUserCommandProfile()
        {
            CreateMap<CreateUserCommand, ApplicationUser>()
                .ForSourceMember(x => x.Password, opt => opt.DoNotValidate())
                .ForSourceMember(x => x.Role, opt => opt.DoNotValidate())
                .ForMember(x => x.Role, opt => opt.Ignore());
        }
    }
}
